using System.Net;
using System.Net.Http;
using System.Web.Http;
using Taralux.Services;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Taralux.Models;
using System;

namespace Taralux.Controllers
{
    public class ImageController : ApiController
    {
        private ImageService _imageService;

        public ImageController()
        {
            _imageService = new ImageService();
        }

        public async Task<HttpResponseMessage> GetImage(int id)
        {
            var image = await _imageService.GetById(id);

            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(image)
            };
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/png");
            return response;
        }

        public async Task<Response<ImageBase>> Create(Request<ImageBase> request)
        {
            try
            {
                ValidateCreateImage(request);
                if (!string.IsNullOrEmpty(request.Data.Base64))
                {
                    request.Data.Content = Convert.FromBase64String(request.Data.Base64);
                }
                return await _imageService.Create(request);
            }
            catch (TaraluxException ex)
            {
                return new Response<ImageBase>
                {
                    ErrorCode = new ErrorCode(ex.ErrorCode.ErrorMessage, ex.ErrorCode.ErrorNumber)
                };
            }
            catch (Exception e)
            {
                return new Response<ImageBase>
                {
                    ErrorCode = new ErrorCode(e.Message, ErrorNumber.GeneralError)
                };
            }
        }

        public async Task<Response> Delete(Request<int?> request)
        {
            try
            {
                if(request == null || !request.Data.HasValue)
                {
                    throw new TaraluxException
                    {
                        ErrorCode = new ErrorCode("Empty required field", ErrorNumber.EmptyRequiredField)
                    };
                }
                return await _imageService.Delete(request);
            }
            catch (TaraluxException ex)
            {
                return new Response
                {
                    ErrorCode = new ErrorCode(ex.ErrorCode.ErrorMessage, ex.ErrorCode.ErrorNumber)
                };
            }
            catch (Exception e)
            {
                return new Response
                {
                    ErrorCode = new ErrorCode(e.Message, ErrorNumber.GeneralError)
                };
            }
        }

        private void ValidateCreateImage(Request<ImageBase> request)
        {
            try
            {
                if (request == null || request.Data == null || request.Data.SourceId == 0 || request.Data.Type == ImageType.Unspecified || ((request.Data.Content == null || request.Data.Content.Length <= 0) && string.IsNullOrEmpty(request.Data.Base64)))
                {
                    throw new TaraluxException
                    {
                        ErrorCode = new ErrorCode("Empty required field", ErrorNumber.EmptyRequiredField)
                    };
                }
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
