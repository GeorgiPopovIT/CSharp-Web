using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Framework
{
    public class MvcContext
    {
        private static MvcContext Instance;

        private MvcContext() { }
        public static MvcContext Get => Instance == null ? (Instance = new MvcContext()) : Instance;
        public string AssemblyName { get; set; }
        public string ControllersFolder { get; set; }
        public string ControllersSuffix { get; set; }
        public string ViewsFolder { get; set; }
        public string ModelsFolder { get; set; }


    }
}
