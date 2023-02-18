using PasswordHolder.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordHolder
{
    internal class ActionExecutor
    {
        public IEnumerable<WebAccountPOCO> Original;
        public IEnumerable<WebAccount> Result;
        public static string HelpText = "--add SiteName Id Pass\n--delacc SiteName Id\n--delweb SiteName\n--search SiteName\n--help";

        public ActionExecutor(IEnumerable<WebAccountPOCO> original)
        { 
            Original = original;
            Result = WebAccountUtil.ConvertPOCO(Original);
        }
        public void Execute(List<string[]> actions)
        {
            for (var i = 0; i < actions.Count; i++)
            { 
                var action = actions[i];
                var args = new string[action.Length-1];
                Array.Copy(action, 1, args, 0, args.Length);
                ExecuteAction(action[0], args);
            }
        }

        private void ExecuteAction(string action, string[] args)
        {
            switch (action.Trim('-').ToLower())
            {
                case "add":
                    Result = WebAccountUtil.AddAccount(Result, args[0], args[1], args[2]);
                    break;
                case "delacc":
                    Result = WebAccountUtil.DeleteAccount(Result, args[0], args[1]);
                    break;
                case "delweb":
                    Result = WebAccountUtil.DeleteWebAccout(Result, args[0]);
                    break;
                case "search":
                    Console.WriteLine(string.Join("\n", WebAccountUtil.SearchAccounts(Result, args[0]).Select(wa => wa.ToString()).ToArray()));
                    break;
                case "help":
                    Console.WriteLine(HelpText);
                    break;
                default:
                    throw new NotSupportedException();
            }
        }
    }
}
