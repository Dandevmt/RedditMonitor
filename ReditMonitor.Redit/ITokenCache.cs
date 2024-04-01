using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditMonitor.RedditApi
{
    internal interface ITokenCache
    {
        string GetToken();
        void SetToken(string token);
    }
}
