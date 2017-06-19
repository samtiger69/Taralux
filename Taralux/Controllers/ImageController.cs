using System.Net;
using System.Net.Http;
using System.Web.Http;
using Taralux.Services;
using System.Threading.Tasks;
using System.Net.Http.Headers;

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

    }
}
