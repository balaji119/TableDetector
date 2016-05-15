using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductFinder
{
    class EvalParser : IParser
    {
        List<DocStruct> mDocStructs = new List<DocStruct>();

        /// <summary>
        /// Constructor - Parses blind file and stores in the form of 'DocStruct'
        /// </summary>
        /// <param name="path">Blind file path</param>
        public EvalParser(string path)
        {
            var lines = File.ReadAllLines(path).ToList();
            lines.RemoveAt(0);
            StringBuilder sb = null;
            DocStruct dc = new DocStruct();
            foreach (var line in lines)
            {
                var tokindex = line.IndexOf(',');
                bool isnumber = false;
                string token = "";
                if (tokindex != -1)
                {
                    int n;
                    token = line.Substring(0, tokindex);
                    isnumber = int.TryParse(token, out n) && n > 1000;
                }
                
                if (isnumber)
                {
                    if (sb != null)
                    {
                        var str = sb.ToString();
                        var index = str.LastIndexOf(',');
                        var tableandurl = str.Substring(0, index);
                        index = tableandurl.LastIndexOf(',');
                        var table = tableandurl.Substring(0, index);
                        var website = tableandurl.Substring(index + 1, tableandurl.Length - (index + 1));
                        dc.Table = table;
                        dc.Website = website;
                        mDocStructs.Add(dc);
                    }
                    sb = new StringBuilder();
                    dc = new DocStruct();
                    dc.HasTable = true;
                    var nline = line.Substring(token.Length + 1, line.Length - (token.Length + 1));
                    sb.Append(nline);
                }
                else
                {
                    sb.Append(line);
                }
            }

            if (sb != null)
            {
                var str = sb.ToString();
                var index = str.LastIndexOf(',');
                var tableandurl = str.Substring(0, index);
                index = tableandurl.LastIndexOf(',');
                var table = tableandurl.Substring(0, index);
                var website = tableandurl.Substring(index + 1, tableandurl.Length - (index + 1));
                dc.Table = table;
                dc.Website = website;
                mDocStructs.Add(dc);
            }
        }

        /// <summary>
        /// Gets the blind file in the form of collection of 'DocStruct' 
        /// </summary>
        public IEnumerable<DocStruct> GetDocs()
        {
            return mDocStructs;
        }
    }
}
