using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ProductFinder
{
    class TrainParser : IParser
    {
        const string YES = "yes";
        const string NO = "no";
        List<DocStruct> mDocStructs = new List<DocStruct>();

        /// <summary>
        /// Constructor - Parses tain file and stores in the form of 'DocStruct'
        /// </summary>
        /// <param name="path">Train file path</param>
        public TrainParser(string path)
        {
            var lines = File.ReadAllLines(path).ToList();
            lines.RemoveAt(0);
            StringBuilder sb = null;
            DocStruct dc = new DocStruct();
            foreach (var line in lines)
            {
                if (line.StartsWith(YES) || line.StartsWith(NO))
                {
                    if (sb != null)
                    {
                        var str = sb.ToString();
                        var index = str.LastIndexOf(',');
                        var table = str.Substring(0, index);
                        var website = str.Substring(index + 1, str.Length - (index + 1));
                        dc.Table = table;
                        dc.Website = website;
                        mDocStructs.Add(dc);
                    }
                    if (line.StartsWith(YES))
                    {
                        sb = new StringBuilder();
                        dc = new DocStruct();
                        dc.HasTable = true;
                        var nline = line.Substring(4, line.Length - 4);
                        sb.Append(nline);
                    }
                    else if (line.StartsWith(NO))
                    {
                        sb = new StringBuilder();
                        dc = new DocStruct();
                        dc.HasTable = false;
                        var nline = line.Substring(3, line.Length - 3);
                        sb.Append(nline);
                    }
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
                var table = str.Substring(0, index);
                var website = str.Substring(index + 1, str.Length - (index + 1));
                dc.Table = table;
                dc.Website = website;
                mDocStructs.Add(dc);
            }
        }

        /// <summary>
        /// Gets the training file in the form of collection of 'DocStruct' 
        /// </summary>
        public IEnumerable<DocStruct> GetDocs()
        {
            return mDocStructs;
        }
    }
}
