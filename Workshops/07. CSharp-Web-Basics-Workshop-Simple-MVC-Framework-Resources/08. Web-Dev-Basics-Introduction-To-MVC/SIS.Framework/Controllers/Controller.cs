using SIS.Framework.ActionResult;
using SIS.Framework.Utilities;
using SIS.Framework.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WebServer.Http.Contracts;

namespace SIS.Framework.Controllers
{
    public abstract class Controller
    {
        protected Controller() { }

        public IHttpRequest Request { get; set; }
        protected IViewable View([CallerMemberName] string caller = "")
        {
            var controllerName = ControllerUtilities.GetControllerName(this);

            var fullyQualifiedName = ControllerUtilities
                .GetViewFullQualifiedName(controllerName, caller);

            var view = new View(fullyQualifiedName);

            return new ViewResult(view);
        }
        protected IRedirectable RedirectToAction(string redirectUrl) => new RedirectResult(redirectUrl);
    }
}
