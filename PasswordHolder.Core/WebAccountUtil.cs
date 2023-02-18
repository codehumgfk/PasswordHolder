using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordHolder.Core
{
    public static class WebAccountUtil
    {
        public static IEnumerable<WebAccount> ConvertPOCO(IEnumerable<WebAccountPOCO> pocos)
        {
            foreach (var poco in pocos)
            { 
                yield return new WebAccount(poco);
            }
        }
        public static IEnumerable<WebAccount> AddAccount(IEnumerable<WebAccount> was, string siteName, string id, string pass)
        {
            WebAccount wa;
            try
            {
                wa = GetWebAccount(was, siteName);
            }
            catch
            { 
                return AddWebAccount(was, siteName, id, pass);
            }
            if (wa.HasAccount(id)) throw new ArgumentException("The account already exists!");
            wa.AddAccount(id, pass);

            return was;
        }
        private static IEnumerable<WebAccount> AddWebAccount(IEnumerable<WebAccount> was, string siteName, string id, string pass)
        {
            var newWA = new WebAccount(siteName, id, pass);

            foreach (var wa in was)
            {
                yield return wa;
            }
            yield return newWA;

        }
        public static IEnumerable<WebAccount> DeleteAccount(IEnumerable<WebAccount> was, string siteName, string id)
        {
            var wa = GetWebAccount(was, siteName);
            if (!wa.HasAccount(id)) throw new ArgumentException("The account does not exist!");
            wa.DelAccount(id);

            return was;
        }
        public static IEnumerable<WebAccount> DeleteWebAccout(IEnumerable<WebAccount> was, string siteName)
        {
            foreach (var wa in was)
            {
                if (wa.SiteName.ToLower() == siteName.ToLower()) continue;
                yield return wa;
            }
        }
        private static WebAccount GetWebAccount(IEnumerable<WebAccount> was, string siteName) 
        {
            foreach (var wa in was)
            { 
                if (wa.SiteName.ToLower() == siteName.ToLower()) return wa;
            }

            throw new ArgumentException("Could not find accounts for " + siteName);
        }
        public static IEnumerable<WebAccount> SearchAccounts(IEnumerable<WebAccount> was, string siteNameStr) 
        {
            foreach (var wa in was)
            {
                if (wa.SiteName.ToLower().Contains(siteNameStr.ToLower())) yield return wa; 
            }
        }
    }
}
