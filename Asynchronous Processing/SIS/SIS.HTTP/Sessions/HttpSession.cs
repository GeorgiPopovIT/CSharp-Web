using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.HTTP.Sessions
{
    public class HttpSession : IHttpSession
    {
        private readonly Dictionary<string, object> parameters;
        public HttpSession(string id)
        {
            this.Id = id;
            this.parameters = new();
        }
        public string Id { get; }

        public void AddParameter(string name, object parameter)
        {
            this.parameters.Add(name, parameter);
        }

        public void ClearParameters() =>
            this.parameters.Clear();

        public bool ContainsParameter(string name) => this.parameters.ContainsKey(name);

        public object GetParameter(string name) => this.parameters.First(p => p.Key == name);
    }
}
