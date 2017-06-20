using System;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using Taralux.Models;

namespace Taralux.Services
{
    public class ImageService : BaseService
    {
        public async Task<byte[]> GetById(int imageId)
        {
            try
            {
                byte[] content = null;
                 await ExecuteReader(StoredProcedure.IMAGE_GET_BY_ID, delegate (SqlCommand cmd)
                 {
                     cmd.Parameters.AddWithValue("@Id", imageId);
                 }
                , delegate (SqlDataReader reader)
                {
                    try
                    {
                        if (reader.Read())
                        {
                            content = (byte[])reader["Content"];
                        }
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                });
                
                return content;
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

        public async Task<Response<ImageBase>> Create(Request<ImageBase> request)
        {
            try
            {
                var response = new Response<ImageBase>
                {
                    Data = request.Data
                };

                var result = Convert.ToInt32(await ExecuteScalar(StoredProcedure.IMAGE_CREATE, delegate (SqlCommand cmd)
                 {
                     cmd.Parameters.AddWithValue("@SourceId", request.Data.SourceId);
                     cmd.Parameters.AddWithValue("@Content", request.Data.Content);
                     cmd.Parameters.AddWithValue("@IsDefault", request.Data.IsDefault);
                     cmd.Parameters.AddWithValue("@Type", request.Data.Type);
                 }));

                response.Data.Id = result;

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

               await ExecuteNonQuery(StoredProcedure.IMAGE_DELETE, delegate (SqlCommand cmd)
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