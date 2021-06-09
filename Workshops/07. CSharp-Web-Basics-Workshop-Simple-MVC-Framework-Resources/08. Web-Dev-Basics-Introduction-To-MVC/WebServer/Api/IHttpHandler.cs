using System;
using System.Collections.Generic;
using System.Text;
using WebServer.Http.Contracts;

namespace WebServer.Api
{
    public interface IHttpHandler
    {
        IHttpResponse Handle(IHttpRequest request);
    }
}
