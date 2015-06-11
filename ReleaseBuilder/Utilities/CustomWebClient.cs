using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ReleaseBuilder.Utilities
{
    internal class CustomWebClient : WebClient
    {
        public Uri ResponseURI { get; private set; }

        protected override WebResponse GetWebResponse(WebRequest request)
        {
            WebResponse response = base.GetWebResponse(request);
            ResponseURI = response.ResponseUri;
            return response;
        }
    }
}
