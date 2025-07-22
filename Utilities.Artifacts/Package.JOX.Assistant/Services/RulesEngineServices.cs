using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using JOX.Assistant.Interface;
using JOX.Assistant.Models;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text;

namespace JOX.Assistant.Services
{
    public class RulesEngineServices : IRulesEngineServices, IDisposable
    {     
        private IConfiguration _configuration;
        protected HttpClient httpClient = new();
        private string url;
        private string cookie;
        private bool isDisposed;
        private IntPtr nativeResource = Marshal.AllocHGlobal(100);
        public RulesEngineServices(IConfiguration configuration)
        {
            _configuration = configuration;            
            url = _configuration["RulesEngine:EndPoint"];
            cookie = _configuration["RulesEngine:Cookie"];
            httpClient.Timeout = TimeSpan.FromSeconds(300);
        }

        public string DispatchWorkFlow(RequestRulesEngine request)
        {
            try
            {
                var data = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
                httpClient.DefaultRequestHeaders
                    .Accept
                    .Add(new MediaTypeWithQualityHeaderValue("application/json"));

                httpClient.DefaultRequestHeaders.Add("Cookie", cookie);
                using (var httpResponse = httpClient.PostAsync(url, data).GetAwaiter().GetResult())
                {
                    return httpResponse.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (isDisposed)
            {
                return;
            }

            if (disposing)
            {
                // free managed resources
                httpClient.Dispose();
            }

            // free native resources if there are any.
            if (nativeResource != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(nativeResource);
                nativeResource = IntPtr.Zero;
            }

            isDisposed = true;
        }

        ~RulesEngineServices()
        {
            Dispose(false);
        }
    }
}
