using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using System.IO;
using System.Reflection;

namespace ProductFinder
{
    /// <summary>
    /// Wrapper class for Python script 'predictor.py'  
    /// </summary>
    public class Predictor
    {
        const string PYTHON_LIBRARY = @"C:\Python27\Lib\site-packages";
        const string IRON_PYTHON_PATH = @"C:\Program Files (x86)\IronPython 2.7\Lib";
        const string PYTHON_SCRIPT_PATH = @"..\..\Resources\predictor.py";
        dynamic mPython;

        /// <summary>
        /// Constructor - Loads python script.
        /// </summary>
        public Predictor()
        {
            var engine = Python.CreateEngine();
            string localFile = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var path = engine.GetSearchPaths();

            List<string> searchPaths = new List<string>();
            searchPaths.Add(AppDomain.CurrentDomain.BaseDirectory + @"\Lib");
            searchPaths.Add(PYTHON_LIBRARY);
            searchPaths.Add(IRON_PYTHON_PATH);

            engine.SetSearchPaths(searchPaths);
            var modeule = engine.GetModuleFilenames();
            mPython = engine.ExecuteFile(PYTHON_SCRIPT_PATH);
        }

        /// <summary>
        /// Creates modedl based on the given train file.
        /// </summary>
        /// <param name="path">Train file path.</param>
        public void Train(string path)
        {
            mPython.learn(path);
        }

        /// <summary>
        /// Evaluates the given blind file.
        /// </summary>
        /// <param name="path">File to evaluate</param>
        /// <param name="predFile">File to which predicted output to be written.</param>
        public void Evaluate(string path, string predFile)
        {
            mPython.evaluate(path, predFile);
        }
    }
}
