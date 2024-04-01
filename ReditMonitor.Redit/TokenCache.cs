using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditMonitor.RedditApi
{
    internal class TokenCache : ITokenCache
    {
        private string token = string.Empty;

        public string GetToken()
        {
            return token;
        }

        public void SetToken(string token)
        {
            this.token = token;
        }
    }
}
