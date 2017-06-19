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
    public class CategoryController : ApiController
    {
        private CategoryService _categoryService;

        public CategoryController()
        {
            _categoryService = new CategoryService();
        }

        [HttpPost]
        public async Task<Response<Category>> Get(Request request)
        {
            try
            {
                if(request == null)
                {
                    request = new Request
                    {
                        Settings = new Settings
                        {
                            PageNumber = 1,
                            PageSize = int.MaxValue
                        }
                    };
                }

                if(request.Settings == null)
                {
                    request.Settings = new Settings
                    {
                        PageNumber = 1,
                        PageSize = int.MaxValue
                    };
                }

                var response = (await _categoryService.Get(request));

                return response;
            }
            catch (Exception e)
            {
                return new Response<Category>
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
        public async Task<Response<Category>> Create(Request<Category> request)
        {
            try
            {
                ValidateCreateCategaory(request);
                if (!string.IsNullOrEmpty(request.Data.Icon.Base64))
                {
                    request.Data.Icon.Content = Convert.FromBase64String(request.Data.Icon.Base64);
                }
                return await _categoryService.Create(request);
            }
            catch (TaraluxException ex)
            {
                return new Response<Category>
                {
                    ErrorCode = new ErrorCode(ex.ErrorCode.ErrorMessage, ex.ErrorCode.ErrorNumber)
                };
            }
            catch (Exception e)
            {
                return new Response<Category>
                {
                    ErrorCode = new ErrorCode(e.Message, ErrorNumber.GeneralError)
                };
            }
        }

        [HttpPost]
        public async Task<Response<Category>> Update(Request<Category> request)
        {
            try
            {
                ValidateUpdateCategory(request);

                return await _categoryService.Update(request);
            }
            catch (TaraluxException ex)
            {
                return new Response<Category>
                {
                    ErrorCode = new ErrorCode(ex.ErrorCode.ErrorMessage, ex.ErrorCode.ErrorNumber)
                };
            }
            catch (Exception e)
            {
                return new Response<Category>
                {
                    ErrorCode = new ErrorCode(e.Message, ErrorNumber.GeneralError)
                };
            }
        }

        //[HttpPost]
        //public async Task<Response> Delete(Request<int?> request)
        //{
        //    try
        //    {
        //        if(request == null || !request.Data.HasValue)
        //        {
        //            throw new TaraluxException
        //            {
        //                ErrorCode = new ErrorCode("Empty Required Field", ErrorNumber.EmptyRequiredField)
        //            };
        //        }

        //        return await _categoryService.Delete(request);
        //    }
        //    catch (TaraluxException ex)
        //    {
        //        return new Response<Category>
        //        {
        //            ErrorCode = new ErrorCode(ex.ErrorCode.ErrorMessage, ex.ErrorCode.ErrorNumber)
        //        };
        //    }
        //    catch (Exception e)
        //    {
        //        return new Response<Category>
        //        {
        //            ErrorCode = new ErrorCode(e.Message, ErrorNumber.GeneralError)
        //        };
        //    }
        //}

        private void ValidateCreateCategaory(Request<Category> request)
        {
            try
            {
                if (request == null || request.Data == null || string.IsNullOrEmpty(request.Data.NameAr) || string.IsNullOrEmpty(request.Data.NameEn) || request.Data.Icon == null || request.Data.Icon == null || ((request.Data.Icon.Content == null || request.Data.Icon.Content.Length <= 0) && string.IsNullOrEmpty(request.Data.Icon.Base64)))
                    throw new TaraluxException
                    {
                        ErrorCode = new ErrorCode("Empty Required Field", ErrorNumber.EmptyRequiredField)
                    };
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

        private void ValidateUpdateCategory(Request<Category> request)
        {
            try
            {
                if (request == null || request.Data == null || string.IsNullOrEmpty(request.Data.NameAr) || string.IsNullOrEmpty(request.Data.NameEn))
                    throw new TaraluxException
                    {
                        ErrorCode = new ErrorCode("Empty Required Field", ErrorNumber.EmptyRequiredField)
                    };
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
