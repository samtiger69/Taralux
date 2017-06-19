using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Taralux.Models;

namespace Taralux.Services
{
    public class CategoryService : BaseService
    {
        public async Task<Response<Category>> Get(Request request)
        {
            try
            {
                var response = new Response<Category>
                {
                    Data = new Category
                    {
                        Id = 0,
                        NameEn = "Root",
                        ParentId = null
                    }
                };

                var categoryList = new List<Category>();

                var imageService = new ImageService();
                await ExecuteReader(StoredProcedure.CATEGORY_GET, delegate (SqlCommand cmd)
                {
                    cmd.Parameters.AddWithValue("@PageSize", request.Settings.PageSize);
                    cmd.Parameters.AddWithValue("@PageNumber", request.Settings.PageNumber);

                }, async delegate (SqlDataReader reader)
                {
                    while (await reader.ReadAsync())
                    {
                        var categoryId = Convert.ToInt32(reader["Id"]);
                        categoryList.Add(new Category
                        {
                            Id = categoryId,
                            NameAr = reader["NameAr"].ToString(),
                            NameEn = reader["NameEn"].ToString(),
                            ParentId = (reader["ParentId"] == DBNull.Value ? -1 : Convert.ToInt32(reader["ParentId"])),
                            IconId = Convert.ToInt32(reader["ImageId"])
                        });
                    }
                });

                // Get category items

                if (categoryList != null && categoryList.Count > 0)
                {
                    response.Data.Children = categoryList.Where(m => m.ParentId == -1).ToList();
                    foreach(var child in response.Data.Children)
                    {
                        FormTree(categoryList, child);
                    }
                }
                
                return response;
            }
            catch (TaraluxException ex)
            {
                throw ex;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<Response<Category>> Create(Request<Category> request)
        {
            try
            {
                var response = new Response<Category>
                {
                    Data = request.Data
                };

                await ExecuteReader(StoredProcedure.CATEGORY_CREATE, delegate (SqlCommand cmd)
                {
                    cmd.Parameters.AddWithValue("@NameAr", request.Data.NameAr);
                    cmd.Parameters.AddWithValue("@NameEn", request.Data.NameEn);
                    cmd.Parameters.AddWithValue("@ParentId", request.Data.ParentId);
                    cmd.Parameters.AddWithValue("@ImageContent", request.Data.Icon.Content);
                    cmd.Parameters.AddWithValue("@ImageIsDefault", request.Data.Icon.IsDefault);
                    cmd.Parameters.AddWithValue("@ImageType", ImageType.CategoryImage);
                },
                async delegate(SqlDataReader reader)
                {
                    if(await reader.ReadAsync())
                    {
                        response.Data.Id = Convert.ToInt32(reader["Id"]);
                        request.Data.IconId = response.Data.Icon.Id = Convert.ToInt32(reader["ImageId"]);
                    }
                });

                return response;
            }
            catch (TaraluxException ex)
            {
                throw ex;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<Response<Category>> Update(Request<Category> request)
        {
            try
            {
                var response = new Response<Category>
                {
                    Data = request.Data
                };

                await ExecuteNonQuery(StoredProcedure.CATEGORY_UPDATE, delegate (SqlCommand cmd)
                {
                    cmd.Parameters.AddWithValue("@Id", request.Data.Id);
                    cmd.Parameters.AddWithValue("@NameAr", request.Data.NameAr);
                    cmd.Parameters.AddWithValue("@NameEn", request.Data.NameEn);
                    cmd.Parameters.AddWithValue("@ParentId", request.Data.ParentId);
                });

                return response;
            }
            catch (TaraluxException ex)
            {
                throw ex;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //public async Task<Response> Delete(Request<int?> request)
        //{
        //    try
        //    {
        //        var response = new Response
        //        {
        //            ErrorCode = new ErrorCode("",ErrorNumber.Success)
        //        };

        //        await ExecuteNonQuery(StoredProcedure.CATEGORY_DELETE, delegate (SqlCommand cmd)
        //        {
        //            cmd.Parameters.AddWithValue("@Id", request.Data.Value);
        //        });

        //        return response;
        //    }
        //    catch (TaraluxException ex)
        //    {
        //        throw ex;
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //}

        private void FormTree(List<Category> categoryList, Category node)
        {
            if (node != null)
            {
                node.Children = categoryList.Where(m => m.ParentId == node.Id).ToList();

                if(node.Children != null && node.Children.Count > 0)
                {
                    foreach(var child in node.Children)
                    {
                        FormTree(categoryList, child);
                    }
                }
            }
        }
    }
}