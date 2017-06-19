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
    public class ItemController : ApiController
    {
        private ItemService _itemService;

        public ItemController()
        {
            _itemService = new ItemService();
        }

        [HttpPost]
        public async Task<Response<List<Item>>> Get(Request<Item> request)
        {
            try
            {
                if (request == null)
                {
                    request = new Request<Item>
                    {
                        Settings = new Settings
                        {
                            PageNumber = 1,
                            PageSize = int.MaxValue
                        },
                        Data = new Item()
                    };
                }

                if (request.Data == null)
                {
                    request.Data = new Item();
                }

                if (request.Settings == null)
                {
                    request.Settings = new Settings
                    {
                        PageNumber = 1,
                        PageSize = int.MaxValue
                    };
                }

                return await _itemService.Get(request);
            }
            catch (TaraluxException ex)
            {
                return new Response<List<Item>>
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
                return new Response<List<Item>>
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
        public async Task<Response<Item>> Create(Request<Item> request)
        {
            try
            {
                ValidateCreateItem(request);

                return await _itemService.Create(request);
            }
            catch (TaraluxException ex)
            {
                return new Response<Item>
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
                return new Response<Item>
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
        public async Task<Response<Item>> Update(Request<Item> request)
        {
            try
            {
                ValidateUpdateItem(request);

                return await _itemService.Update(request);
            }
            catch (TaraluxException ex)
            {
                return new Response<Item>
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
                return new Response<Item>
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
                if(request == null || !request.Data.HasValue)
                {
                    throw new TaraluxException
                    {
                        ErrorCode = new ErrorCode("Empty required field",ErrorNumber.EmptyRequiredField)
                    };
                }

                return await _itemService.Delete(request);
            }
            catch (TaraluxException ex)
            {
                return new Response<Item>
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
                return new Response<Item>
                {
                    ErrorCode = new ErrorCode
                    {
                        ErrorMessage = e.Message,
                        ErrorNumber = ErrorNumber.GeneralError
                    }
                };
            }
        }

        private void ValidateCreateItem(Request<Item> request)
        {
            try
            {
                if(request == null || request.Data == null || string.IsNullOrEmpty(request.Data.NameAr) || string.IsNullOrEmpty(request.Data.NameEn) || request.Data.CategoryId == 0 || request.Data.Price == 0 || request.Data.Icon == null || request.Data.Icon.Content.Length <= 0)
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

        private void ValidateUpdateItem(Request<Item> request)
        {
            try
            {
                if (request == null || request.Data == null || string.IsNullOrEmpty(request.Data.NameAr) || string.IsNullOrEmpty(request.Data.NameEn) || request.Data.CategoryId == 0 || request.Data.Price == 0)
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
