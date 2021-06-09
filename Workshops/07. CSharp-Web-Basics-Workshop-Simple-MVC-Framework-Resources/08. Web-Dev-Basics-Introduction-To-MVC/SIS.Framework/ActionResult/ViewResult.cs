using SIS.Framework.ActionResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Framework.ActionResult
{
    public class ViewResult : IViewable
    {
        public ViewResult(IRenderable view)
        {
            this.View = view;
        }
        public IRenderable View { get; set; }

        public string Invoke() => this.View.Render();
    }
}
