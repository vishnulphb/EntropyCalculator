using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace entropyCalculator_ClusterBased
{
    class Program
    {
        static void Main(string[] args)
        {
            string ipFilePath = @"C:\Users\vpillai\Documents\ADE\KB_entropy_CRTS.csv";
            string opPath = @"c:\opfiles\entropy.csv";

            Console.WriteLine("The Process has started");

            preProcess(ipFilePath);
            mainProcess("Temp/temp.csv", opPath);
            Console.WriteLine("Press Enter to Exit");
            Console.ReadLine();
        }

        private static void mainProcess(string ipFilePath, string opPath)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();



            StreamWriter sw = new StreamWriter(opPath);

            int FNPosition = 1;
            int LNPosition = 2;
            int SSNPosition = 3;
            int comboPosition = 4;

            Dictionary<string, double> FNentropyDict = entropyFunctions.entropyDictionary(ipFilePath, FNPosition);
            Dictionary<string, double> LNentropyDict = entropyFunctions.entropyDictionary(ipFilePath, LNPosition);
            Dictionary<string, double> SSNentropyDict = entropyFunctions.entropyDictionary(ipFilePath, SSNPosition);
            Dictionary<string, double> COMBOentropyDict = entropyFunctions.entropyDictionary(ipFilePath, comboPosition);

            StreamReader sr = new StreamReader(ipFilePath);
            string line = sr.ReadLine();
            sw.WriteLine("UID|FN|Entropy|LN|Entropy|SSN|Entropy|FN+LN+SSN|Entropy"); 
            while ((line = sr.ReadLine()) != null)
            {
                string[] words = line.Split('|');
                words = words.Select(s => s.ToUpperInvariant()).ToArray();
                words = words.Select(s => s.Trim()).ToArray();

                string FNEntropy = words[0] + "|" + words[FNPosition] + "|" + FNentropyDict[words[FNPosition] + "," + words[0]];
                string LNEntropy = words[LNPosition] + "|" + LNentropyDict[words[LNPosition] + "," + words[0]];
                string SSNEntropy =words[SSNPosition] + "|" + SSNentropyDict[words[SSNPosition] + "," + words[0]];
                string COMBOEntropy = words[comboPosition] + "|" + COMBOentropyDict[words[comboPosition] + "," + words[0]];

                sw.WriteLine(FNEntropy+"|"+LNEntropy+"|"+SSNEntropy+"|"+COMBOEntropy);
                //sw.WriteLine(FNEntropy+"|"+LNEntropy);
            }

            //Dictionary<string, double> FNentropyDict = entropyFunctions.Entropy(ipFilePath, FNPosition);
            //Dictionary<string, double> LNentropyDict = entropyFunctions.Entropy(ipFilePath, LNPosition);
            //Dictionary<string, double> SSNentropyDict = entropyFunctions.Entropy(ipFilePath, SSNPosition);
            //Dictionary<string, double> COMBOentropyDict = entropyFunctions.Entropy(ipFilePath, comboPosition);

            //StreamReader sr = new StreamReader(ipFilePath);
            //string line = sr.ReadLine();

            //sw.WriteLine("FN|Entropy|LN|Entropy|SSN|Entropy|FN+LN+SSN|Entropy");
            //while ((line = sr.ReadLine()) != null)
            //{
            //    string[] words = line.Split('|');
            //    words = words.Select(s => s.ToUpperInvariant()).ToArray();
            //    words = words.Select(s => s.Trim()).ToArray();
            //    sw.WriteLine(words[FNPosition] + "|" + FNentropyDict[words[FNPosition]] + "|" + words[LNPosition] + "|" + LNentropyDict[words[LNPosition]] + "|" + words[SSNPosition] + "|" + SSNentropyDict[words[SSNPosition]] + "|" + words[comboPosition] + "|" + COMBOentropyDict[words[comboPosition]]);
            //}

            //sr.Close();
            timer.Stop();
            sw.Close();
            Console.WriteLine("time elapsed : " + timer.ElapsedMilliseconds + " milliseconds");

            //File.Delete("Temp/temp.csv");
            //Directory.Delete("Temp");

        }

        static void preProcess(string inPath)
        {
            Directory.CreateDirectory("Temp");
            //File.Copy(inPath, "Temp/temp.csv");
            StreamWriter sw = new StreamWriter("Temp/temp.csv");

            StreamReader sr = new StreamReader(inPath);
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                string[] words = line.Split('|');
                string combo = words[3] + words[5] + words[6];
                string newLine = words[0] + "|" + words[3] + "|" + words[5] + "|" + words[6] +"|" + combo;
                sw.WriteLine(newLine);
            }
            sw.Close();
            sr.Close();
            //Directory.Delete("Temp");
        }

    }
}
