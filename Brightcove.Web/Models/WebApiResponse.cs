using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Brightcove.Web.Models
{
    public class EmptyApiResponse : WebApiResponse<object>
    {
        public EmptyApiResponse() : base(null)
        {
        }

        public EmptyApiResponse(string errorMessage) : base(null)
        {
            this.Message = errorMessage;
            Success = false;
        }
    }

    public class WebApiResponse<T> where T: class
    {
        public T Data { get; set; }

        public string Message { get; set; }

        public bool Success { get; set; }

        public WebApiResponse(T Data)
        {
            this.Data = Data;
            Success = true;
            Message = "Success";
        }
    }
}