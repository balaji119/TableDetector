using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ProductFinder
{
    public class Program
    {
        static void Main(string[] args)
        {
            if (args.Count() < 3)
            {
                PrintUsage();
            }

            IParser parser;

            #region Prepare train file and build model.
            if (args[0] == "train")
            {
                parser = new TrainParser(args[1]);
                var docs = parser.GetDocs().ToList();

                var train = new List<string>();
                foreach (var v in docs)
                {
                    var features = v.Table.GetXpaths();
                    train.Add(string.Format("{0}\t{1}", string.Join("\t", features), v.HasTable ? "yes" : "no"));
                }
                File.WriteAllLines(args[2], train);
                docs.Clear(); // Clearing due to memory constrain.
                
                Predictor predictor = new Predictor();
                predictor.Train(args[2]);

            }
            #endregion

            #region Predict ouput.
            else if (args[0] == "predict")
            {
                var evalparser = new EvalParser(args[1]);
                var evaldocs = evalparser.GetDocs().ToList();
                var eval = new List<string>();
                foreach (var v in evaldocs)
                {
                    var features = v.Table.GetXpaths();
                    eval.Add(string.Format("{0}", string.Join("\t", features)));
                }
                File.WriteAllLines(args[2], eval);

                Predictor predictor = new Predictor();
                predictor.Evaluate(args[2], args[3]);
            }
            #endregion

            else
            {
                PrintUsage();
            }
        }

        static void PrintUsage()
        {
            Console.WriteLine("Usage:");
            Console.WriteLine("\tTablePredictor.exe train data.csv train.txt");
            Console.WriteLine("\tTablePredictor.exe predict blindset_table_out.csv eval.txt predicted_output.txt");
        }
    }
}
