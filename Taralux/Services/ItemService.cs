using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Taralux.Models;

namespace Taralux.Services
{
    public class ItemService : BaseService
    {
        public async Task<Response<List<Item>>> Get(Request<Item> request)
        {
            try
            {
                var response = new Response<List<Item>>
                {
                    Data = new List<Item>()
                };

                await ExecuteReader(StoredProcedure.ITEM_GET, delegate (SqlCommand cmd)
                {
                    if (request.Data.Id != 0)
                    {
                        cmd.Parameters.AddWithValue("@Id", request.Data.Id);
                    }
                    if (request.Data.Id != 0)
                    {
                        cmd.Parameters.AddWithValue("@CategoryId", request.Data.CategoryId);
                    }
                }, async delegate (SqlDataReader reader)
                {
                    while (await reader.ReadAsync())
                    {
                        response.Data.Add(new Item
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Price = Convert.ToDecimal(reader["Price"]),
                            NameAr = reader["NameAr"].ToString(),
                            NameEn = reader["NameEn"].ToString(),
                            CategoryId = Convert.ToInt32(reader["CategoryId"]),
                            IconId = Convert.ToInt32(reader["ImageId"])
                        });
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

        public async Task<Response<Item>> Create(Request<Item> request)
        {
            try
            {
                var response = new Response<Item>
                {
                    Data = request.Data
                };

                await ExecuteReader(StoredProcedure.ITEM_CREATE, delegate (SqlCommand cmd)
                {
                    cmd.Parameters.AddWithValue("@NameAr", request.Data.NameAr);
                    cmd.Parameters.AddWithValue("@Price", request.Data.Price);
                    cmd.Parameters.AddWithValue("@NameEn", request.Data.NameEn);
                    cmd.Parameters.AddWithValue("@CategoryId", request.Data.CategoryId);
                    cmd.Parameters.AddWithValue("@ImageContent", request.Data.Icon.Content);
                    cmd.Parameters.AddWithValue("@ImageIsDefault", request.Data.Icon.IsDefault);
                    cmd.Parameters.AddWithValue("@ImageType", ImageType.ItemImage);
                },
                async delegate (SqlDataReader reader)
                {
                    if (await reader.ReadAsync())
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

        public async Task<Response<Item>> Update(Request<Item> request)
        {
            try
            {
                var response = new Response<Item>
                {
                    Data = request.Data
                };

                await ExecuteNonQuery(StoredProcedure.ITEM_UPDATE, delegate (SqlCommand cmd)
                {
                    cmd.Parameters.AddWithValue("@NameAr", request.Data.NameAr);
                    cmd.Parameters.AddWithValue("@Price", request.Data.Price);
                    cmd.Parameters.AddWithValue("@NameEn", request.Data.NameEn);
                    cmd.Parameters.AddWithValue("@CategoryId", request.Data.CategoryId);
                    cmd.Parameters.AddWithValue("@Id", request.Data.Id);
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

        public async Task<Response> Delete(Request<int?> request)
        {
            try
            {
                var response = new Response
                {
                    ErrorCode = new ErrorCode("",ErrorNumber.Success)
                };

                await ExecuteNonQuery(StoredProcedure.ITEM_DELETE, delegate (SqlCommand cmd)
                {
                    cmd.Parameters.AddWithValue("@Id", request.Data.Value);
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
    }
}