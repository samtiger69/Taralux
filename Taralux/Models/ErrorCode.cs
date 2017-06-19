using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Taralux.Models
{
    public class ErrorCode
    {

        public ErrorCode()
        {

        }

        public ErrorCode(string errorMessage, ErrorNumber errorNumber)
        {
            ErrorNumber = errorNumber;
            ErrorMessage = errorMessage;
        }

        public ErrorNumber ErrorNumber { get; set; }
        public string ErrorMessage { get; set; }
    }

    public enum ErrorNumber
    {
        Success = 0,

        GeneralError = 100,

        EmptyRequiredField = 1,

    }
}