using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Brightcove.Core.Extensions;

namespace Brightcove.Core.Exceptions
{
    public class HttpStatusException : Exception
    {
        public HttpResponseMessage Response { get; }
        public HttpRequestMessage Request { get;  }

        public HttpStatusException(HttpRequestMessage request, HttpResponseMessage response)
        {
            this.Request = request;
            this.Response = response;
        }

        public override string Message 
        { 
            get
            {
                string content = "";
                try { content = Response?.Content?.ReadAsString(); } catch { }

                return $"\nCODE: {((int)Response?.StatusCode)}\nREQUEST: '{Request?.RequestUri}'\nRESPONSE: '{content}'\n";
            } 
        }
    }
}
