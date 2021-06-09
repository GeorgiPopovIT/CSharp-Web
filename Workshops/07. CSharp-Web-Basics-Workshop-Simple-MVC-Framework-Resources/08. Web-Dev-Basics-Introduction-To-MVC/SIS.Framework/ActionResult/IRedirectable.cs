using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Framework.ActionResult
{
    public interface IRedirectable : IActionResult
    {
        string RedirectUrl { get; }
    }
}
