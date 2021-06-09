using SIS.Framework.ActionResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Framework.Views
{
    public class View : IRenderable
    {
        private readonly string fullyQualifiedTemplateName;
        public View(string fullyQualifiedTemplateName)
        {
            this.fullyQualifiedTemplateName = fullyQualifiedTemplateName;
        }
        public string ReadFile(string fullyQualifiedTemplateName)
        {
            return null;
        }
        public string Render()
        {
            var fullHtml = this.ReadFile(this.fullyQualifiedTemplateName);

            return fullHtml;
        }
    }
}
