using MyWebServer.Server.Common;
using System.Collections.Generic;

namespace MyWebServer.Server.Http
{
    public class HttpSession
    {
        public const string SessionCookieName = "MyWebServerSID";

        private Dictionary<string, string> data;

        public HttpSession(string id)
        {
            Guard.AgainstNull(id, nameof(id));

            this.data = new Dictionary<string, string>();
            this.SessionId = id;
        }

        public string SessionId { get; init; }

        public bool IsNew { get; set; }

        public string this[string key]
        {
            get => this.data[key];
            set => this.data[key] = value;
        }

        public bool Contains(string key)
        {
            return this.data.ContainsKey(key);
        }

        public int Count()
        {
            return this.data.Count;
        }

        public void Remove(string key)
        {
            if (this.data.ContainsKey(key))
            {
                this.data.Remove(key);
            }
        }
    }
}
