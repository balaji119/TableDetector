using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace ProductFinder
{
    static class Utils
    {
        /// <summary>
        /// Gets a collection of valid words given a text.
        /// </summary>
        public static IEnumerable<string> GetTokens(this string str)
        {
            var tokens = new List<string>();
            if (str.Length <= 3) return tokens;
            var words = str.Split(' ');
            foreach (var word in words)
            {
                Regex pattern = new Regex(@"\W|_");
                var matches = pattern.Matches(word);
                var alnums = pattern.Split(word);
                foreach (var alnum in alnums)
                    if (!String.IsNullOrWhiteSpace(alnum))
                        tokens.Add(alnum);
                foreach (var match in matches)
                {
                    var strMatch = match.ToString();
                    if (!string.IsNullOrWhiteSpace(strMatch))
                        tokens.Add(strMatch);
                }
            }
            return tokens;
        }

        /// <summary>
        /// Given a html string, returs a collection of xpaths and valid words.
        /// </summary>
        /// <param name="s">Text in HTML format</param>
        public static IEnumerable<string> GetXpaths(this string s)
        {
            HashSet<string> xpaths = new HashSet<string>();
            using (MemoryStream stream = new MemoryStream())
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(s);
                    writer.Flush();
                    stream.Position = 0;

                    var html = new HtmlDocument();
                    html.Load(stream);
                    var root = html.DocumentNode;
                    var nodes = root.Descendants();
                    foreach (var node in nodes)
                    {
                        if (node.Name == "#comment")
                        {
                            xpaths.Add(node.InnerHtml);
                        }
                        if (!string.IsNullOrEmpty(node.Name) && node.Name[0] != '#')
                        {
                            xpaths.Add(node.XPath);
                        }
                        if (!string.IsNullOrEmpty(node.Name) && node.Name[0] == '#')
                        {
                            var toks = node.InnerHtml.GetTokens().ToList();
                            toks.ForEach(x => xpaths.Add(x));
                        }
                    }
                }
            }
            return xpaths;
        }
    }
}
