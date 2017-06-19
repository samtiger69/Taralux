using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
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
                , async delegate (SqlDataReader reader)
                {
                    if(await reader.ReadAsync())
                    {
                        content = (byte[])reader["Content"];
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

        public async Task<Response> Delete(Request<int> request)
        {
            try
            {
                var response = new Response
                {
                    ErrorCode = new ErrorCode("", ErrorNumber.Success)
                };

                var result = Convert.ToInt32(await ExecuteScalar(StoredProcedure.IMAGE_DELETE, delegate (SqlCommand cmd)
                {
                    cmd.Parameters.AddWithValue("@SourceId", request.Data);
                }));

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