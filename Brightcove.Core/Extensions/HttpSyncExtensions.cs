using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Brightcove.Core.Extensions
{
    public static class HttpSyncExtensions
    {
        public static HttpResponseMessage Send(this HttpClient client, HttpRequestMessage request)
        {
            var task = client.SendAsync(request);
            task.Wait();
            return task.Result;
        }

        public static string ReadAsString(this HttpContent content)
        {
            var task = content.ReadAsStringAsync();
            task.Wait();
            return task.Result;
        }
    }
}
