using System;
using Microsoft.AspNetCore.Http;

namespace UnitTestsProject.Tests.Components
{
    public class HttpContextAccessorMock : IHttpContextAccessor
    {
        public HttpContext HttpContext { get => ObtenerHttpContext(); set => throw new NotImplementedException(); }

        private HttpContext ObtenerHttpContext()
        {
            return new DefaultHttpContext();
        }
    }
}
