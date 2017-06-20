using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Taralux.Models;
using Taralux.Services;

namespace Taralux.Controllers
{
    public class ElectricianController : ApiController
    {
        private ElectricianService _electricianService;

        public ElectricianController()
        {
            _electricianService = new ElectricianService();
        }

        [HttpPost]
        public async Task<Response<List<Electrician>>> Get(Request<Electrician> request)
        {
            try
            {
                if (request == null)
                {
                    request = new Request<Electrician>
                    {
                        Settings = new Settings
                        {
                            PageNumber = 1,
                            PageSize = int.MaxValue
                        },
                        Data = new Electrician()
                    };
                }

                if (request.Data == null)
                {
                    request.Data = new Electrician();
                }

                if (request.Settings == null)
                {
                    request.Settings = new Settings
                    {
                        PageNumber = 1,
                        PageSize = int.MaxValue
                    };
                }

                return await _electricianService.Get(request);
            }
            catch (TaraluxException ex)
            {
                return new Response<List<Electrician>>
                {
                    ErrorCode = new ErrorCode
                    {
                        ErrorMessage = ex.ErrorCode.ErrorMessage,
                        ErrorNumber = ex.ErrorCode.ErrorNumber
                    }
                };
            }
            catch (Exception e)
            {
                return new Response<List<Electrician>>
                {
                    ErrorCode = new ErrorCode
                    {
                        ErrorMessage = e.Message,
                        ErrorNumber = ErrorNumber.GeneralError
                    }
                };
            }
        }

        [HttpPost]
        public async Task<Response<Electrician>> Create(Request<Electrician> request)
        {
            try
            {
                ValidateCreate(request);

                if (!string.IsNullOrEmpty(request.Data.Icon.Base64))
                {
                    request.Data.Icon.Content = Convert.FromBase64String(request.Data.Icon.Base64);
                }

                return await _electricianService.Create(request);
            }
            catch (TaraluxException ex)
            {
                return new Response<Electrician>
                {
                    ErrorCode = new ErrorCode
                    {
                        ErrorMessage = ex.ErrorCode.ErrorMessage,
                        ErrorNumber = ex.ErrorCode.ErrorNumber
                    }
                };
            }
            catch (Exception e)
            {
                return new Response<Electrician>
                {
                    ErrorCode = new ErrorCode
                    {
                        ErrorMessage = e.Message,
                        ErrorNumber = ErrorNumber.GeneralError
                    }
                };
            }
        }

        [HttpPost]
        public async Task<Response<Electrician>> Update(Request<Electrician> request)
        {
            try
            {
                ValidateUpdate(request);

                return await _electricianService.Update(request);
            }
            catch (TaraluxException ex)
            {
                return new Response<Electrician>
                {
                    ErrorCode = new ErrorCode
                    {
                        ErrorMessage = ex.ErrorCode.ErrorMessage,
                        ErrorNumber = ex.ErrorCode.ErrorNumber
                    }
                };
            }
            catch (Exception e)
            {
                return new Response<Electrician>
                {
                    ErrorCode = new ErrorCode
                    {
                        ErrorMessage = e.Message,
                        ErrorNumber = ErrorNumber.GeneralError
                    }
                };
            }
        }

        [HttpPost]
        public async Task<Response> Delete(Request<int?> request)
        {
            try
            {
                if (request == null || !request.Data.HasValue)
                {
                    throw new TaraluxException
                    {
                        ErrorCode = new ErrorCode("Empty required field", ErrorNumber.EmptyRequiredField)
                    };
                }

                return await _electricianService.Delete(request);
            }
            catch (TaraluxException ex)
            {
                return new Response
                {
                    ErrorCode = new ErrorCode
                    {
                        ErrorMessage = ex.ErrorCode.ErrorMessage,
                        ErrorNumber = ex.ErrorCode.ErrorNumber
                    }
                };
            }
            catch (Exception e)
            {
                return new Response
                {
                    ErrorCode = new ErrorCode
                    {
                        ErrorMessage = e.Message,
                        ErrorNumber = ErrorNumber.GeneralError
                    }
                };
            }
        }

        private void ValidateCreate(Request<Electrician> request)
        {
            try
            {
                if (request == null || request.Data == null || string.IsNullOrEmpty(request.Data.NameAr) || string.IsNullOrEmpty(request.Data.PhoneNumber) || string.IsNullOrEmpty(request.Data.Location) || string.IsNullOrEmpty(request.Data.Description) || string.IsNullOrEmpty(request.Data.NameEn) || request.Data.Icon == null || ((request.Data.Icon.Content == null || request.Data.Icon.Content.Length <= 0) && string.IsNullOrEmpty(request.Data.Icon.Base64)))
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

        private void ValidateUpdate(Request<Electrician> request)
        {
            try
            {
                if (request == null || request.Data == null || string.IsNullOrEmpty(request.Data.NameAr) || string.IsNullOrEmpty(request.Data.NameEn) || string.IsNullOrEmpty(request.Data.PhoneNumber) || string.IsNullOrEmpty(request.Data.Location) || string.IsNullOrEmpty(request.Data.Description))
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
