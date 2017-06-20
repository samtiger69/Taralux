using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Taralux.Models;

namespace Taralux.Services
{
    public class ElectricianService : BaseService
    {
        public async Task<Response<List<Electrician>>> Get(Request<Electrician> request)
        {
            try
            {
                var response = new Response<List<Electrician>>
                {
                    Data = new List<Electrician>()
                };

                await ExecuteReader(StoredProcedure.ELECTRICIAN_GET, delegate (SqlCommand cmd)
                {
                    if (request.Data.Id != 0)
                    {
                        cmd.Parameters.AddWithValue("@Id", request.Data.Id);
                    }
                }, async delegate (SqlDataReader reader)
                {
                    while (await reader.ReadAsync())
                    {
                        response.Data.Add(new Electrician
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            AverageRate = Convert.ToDouble(reader["AverageRate"]),
                            NameAr = reader["NameAr"].ToString(),
                            NameEn = reader["NameEn"].ToString(),
                            PhoneNumber = reader["PhoneNumber"].ToString(),
                            Location = reader["Location"].ToString(),
                            Description = reader["Description"].ToString(),
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

        public async Task<Response<Electrician>> Create(Request<Electrician> request)
        {
            try
            {
                var response = new Response<Electrician>
                {
                    Data = request.Data
                };

                await ExecuteReader(StoredProcedure.ELECTRICIAN_CREATE, delegate (SqlCommand cmd)
                {
                    cmd.Parameters.AddWithValue("@NameAr", request.Data.NameAr);
                    cmd.Parameters.AddWithValue("@NameEn", request.Data.NameEn);
                    cmd.Parameters.AddWithValue("@PhoneNumber", request.Data.PhoneNumber);
                    cmd.Parameters.AddWithValue("@Location", request.Data.Location);
                    cmd.Parameters.AddWithValue("@Description", request.Data.Description);
                    cmd.Parameters.AddWithValue("@ImageContent", request.Data.Icon.Content);
                    cmd.Parameters.AddWithValue("@ImageIsDefault", request.Data.Icon.IsDefault);
                    cmd.Parameters.AddWithValue("@ImageType", ImageType.ElectricianImage);
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

        public async Task<Response<Electrician>> Update(Request<Electrician> request)
        {
            try
            {
                var response = new Response<Electrician>
                {
                    Data = request.Data
                };

                await ExecuteNonQuery(StoredProcedure.ELECTRICIAN_UPDATE, delegate (SqlCommand cmd)
                {
                    cmd.Parameters.AddWithValue("@Id", request.Data.Id);
                    cmd.Parameters.AddWithValue("@NameAr", request.Data.NameAr);
                    cmd.Parameters.AddWithValue("@NameEn", request.Data.NameEn);
                    cmd.Parameters.AddWithValue("@PhoneNumber", request.Data.PhoneNumber);
                    cmd.Parameters.AddWithValue("@Location", request.Data.Location);
                    cmd.Parameters.AddWithValue("@Description", request.Data.Description);
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
                    ErrorCode = new ErrorCode("", ErrorNumber.Success)
                };

                await ExecuteNonQuery(StoredProcedure.ELECTRICIAN_DELETE, delegate (SqlCommand cmd)
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