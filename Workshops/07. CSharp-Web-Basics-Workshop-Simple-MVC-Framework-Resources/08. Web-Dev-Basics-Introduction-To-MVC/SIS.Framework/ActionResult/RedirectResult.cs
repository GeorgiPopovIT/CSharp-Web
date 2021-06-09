using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Framework.ActionResult
{
    public class RedirectResult : IRedirectable
    {
        public RedirectResult(string redirectUrl)
        {
            this.RedirectUrl = redirectUrl;
        }
        public string RedirectUrl { get; }

        public string Invoke() => this.RedirectUrl;
    }
}
