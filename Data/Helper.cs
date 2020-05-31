using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Advantage.API.Data
{
    public class Helper
    {
        private static Random _rand = new Random();

        internal static string MakeUniqueCustomerName(List<string> names)
        {
            var maxNames = bizPrefix.Count * bizPrefix.Count;
            if (names.Count >= maxNames)
            {
                throw new System.InvalidOperationException("Maxed names");
            }
            var prefix = GetRandom(bizPrefix);
            var suffix = GetRandom(BizSuffix);
            var bizName = prefix + suffix;

            if (names.Contains(bizName))
            {
                MakeUniqueCustomerName(names);
            }
            return bizName;
        }

        internal static string GetRandom(IList<string> items)
        {
            return items[_rand.Next(items.Count)];
        }

        private static readonly List<string> bizPrefix = new List<string>() 
        { 
        "ABC","Mainst","Sales","Enterprise","Quick","Magic","Peak","Family","Comfort","John","Kelly","Homo","Budget"
        };
        private static readonly List<string> BizSuffix = new List<string>()
        {
        "Foods","BCD","CDE","DEF","EFG","FGH","GHI","HIJ","IJK","JKL","KLM","LMN","MNO"
        };

        internal static object MakeEmail(string customerName)
        {
            return $"contact@{customerName.ToLower()}.com";
        }

        internal static string GetRandomState()
        {
            List<string> usCodes = new List<string>()
            {
                "AL","MO","NE"
            };
            return GetRandom(usCodes);
        }

        internal static decimal GetRandomOrderTotal()
        {
            return _rand.Next(100, 1000);    
         }

        internal static DateTime GetRandomOrderPlaced()
        {
            var end = DateTime.Now;
            var start = end.AddDays(-90);
            TimeSpan possibleSpan = end - start;
            TimeSpan newSpan = new TimeSpan(0, _rand.Next(0,(int)possibleSpan.TotalMinutes),0);
            return start + newSpan;
        }

        internal static DateTime? GetRandomOrderCompleted(DateTime orderPlaced)
        {
            var now = DateTime.Now;
            var minleadTime = TimeSpan.FromDays(7);
            var timespan = now - orderPlaced;
            if (timespan < minleadTime)
            {
                return null;
            }
            return orderPlaced.AddDays(_rand.Next(7, 14));
        }
    }
}
