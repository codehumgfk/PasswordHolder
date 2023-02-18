using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordHolder
{
    public static class ActionArgNums
    {
        public static int Add = 3;
        public static int DelAcc = 2;
        public static int DelWeb = 1;
        public static int Search = 1;
        public static int Help = 0;
        public static int Show = 0;
    }
    internal static class ArgumentParser
    {
        public static List<string[]> Parse(string[] args)
        { 
            var res = new List<string[]>();
            var actions = SearchActionHeader(args);
            for (var i = 0; i < actions.Count; i++)
            {
                var start = actions[i];
                var end = i != actions.Count - 1 ? actions[i + 1] : args.Length - 1;
                CheckActionArguments(args, start, end);
                var temp = new string[end - start + 1];
                Array.Copy(args, start, temp, 0, temp.Length);
                res.Add(temp);
            }

            return res;
        }

        private static List<int> SearchActionHeader(string[] args)
        {
            var res = new List<int>();
            for (var i = 0; i < args.Length; i++)
            { 
                if (args[i].Contains("--")) res.Add(i);
            }

            return res;
        }

        private static void CheckActionArguments(string[] args, int start, int end)
        {
            var argnum = end - start;
            switch (args[start].Trim('-').ToLower())
            {
                case "add":
                    if (argnum != ActionArgNums.Add) throw new ArgumentException("Add requires SiteName, Id, and, Pass.");
                    break;
                case "delacc":
                    if (argnum != ActionArgNums.DelAcc) throw new ArgumentException("DelAcc requires SiteName, and, Id.");
                    break;
                case "delweb":
                    if (argnum != ActionArgNums.DelWeb) throw new ArgumentException("DelWeb requires SiteName.");
                    break;
                case "search":
                    if (argnum != ActionArgNums.Search) throw new ArgumentException("Search requires SiteName.");
                    break;
                case "help":
                    if (argnum != ActionArgNums.Help) throw new ArgumentException("Show requires nothing.");
                    break;
                case "show":
                    if (argnum != ActionArgNums.Show) throw new ArgumentException("Show requires nothing.");
                    break;
                default:
                    throw new NotSupportedException();
            }
        }
    }
}
