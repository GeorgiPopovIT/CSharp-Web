using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.HTTP.Cookies
{
    public class HttpCookieCollection : IHttpCookieCollection
    {
        private readonly Dictionary<string, HttpCookie> cookies;
        public HttpCookieCollection()
        {
            this.cookies = new();
        }

        public void AddCookie(HttpCookie cookie) => this.cookies.Add(cookie.Key, cookie);

        public bool ContainsCookie(string key) => this.cookies.Select(c => c.Key).Contains(key);

        public HttpCookie GetCookie(string key) =>
             this.cookies.First(c => c.Key == key).Value;

        public bool HasCookies() => this.cookies.Any();


        public IEnumerator<HttpCookie> GetEnumerator()
        {
            return this.cookies.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        public override string ToString()
        {
            //HttpCookieStringSeparator
            return string.Join(" ",this.cookies.Values);
        }
    }
}
