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

                await ExecuteReader(StoredProcedure.CATEGORY_GET, delegate (SqlCommand cmd)
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
    }
}