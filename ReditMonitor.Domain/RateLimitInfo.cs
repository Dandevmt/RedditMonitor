using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace RedditMonitor.ConsoleApp
{
    public class RateLimitInfo
    {
        public const double MsDelayDefault = 5000;
        public static RateLimitInfo Default => new(0, 0, 0);
        public double RateLimitUsed { get; set; }
        public double RateLimitRemaining { get; set; }
        public double RateLimitReset { get; set; }

        public RateLimitInfo(double rateLimitUsed, double rateLimitRemaining, double rateLimitReset)
        {
            RateLimitUsed = rateLimitUsed;
            RateLimitRemaining = rateLimitRemaining;
            RateLimitReset = rateLimitReset;
        }               

        public double CalculateMsDelay()
        {
            if (RateLimitRemaining == 0)
            {
                return MsDelayDefault;
            }

            return Math.Ceiling((RateLimitReset * 1000) / RateLimitRemaining);
        }
    }
}
