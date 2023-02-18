namespace PasswordHolder.Core
{
    public class WebAccount : WebAccountPOCO
    {

        #region Constructor
        public WebAccount(WebAccountPOCO poco)
        { 
            SiteName= poco.SiteName;
            IdAndPass= poco.IdAndPass;
        }
        public WebAccount(string siteName, string id, string pass)
        {
            SiteName = siteName;
            IdAndPass = new Dictionary<string, string>();
            IdAndPass.Add(id, pass);
        }
        public WebAccount(string siteName, IEnumerable<KeyValuePair<string, string>> idAndPass)
        {
            SiteName = siteName;
            IdAndPass = new Dictionary<string, string>();
            foreach (var pair in idAndPass)
            {
                IdAndPass.Add(pair.Key, pair.Value);
            }
        }
        #endregion

        public void AddAccount(string id, string pass) { IdAndPass.Add(id, pass); }
        public void DelAccount(string id) { IdAndPass.Remove(id); }
        public void UpdatePass(string id, string pass) { IdAndPass[id] = pass; }
        public bool HasAccount(string id) { return IdAndPass.ContainsKey(id); }
        public override string ToString()
        {
            var txt = "Web: " + SiteName + "\n";
            var form = "Account: {0}, Password: {1}\n";
            foreach (var key in IdAndPass.Keys)
            {
                txt += string.Format(form, key, IdAndPass[key]);
            }

            return txt;
        }

    }
}