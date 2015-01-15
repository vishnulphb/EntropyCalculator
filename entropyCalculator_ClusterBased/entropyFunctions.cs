using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace entropyCalculator_ClusterBased
{
    class entropyFunctions
    {
        public static Dictionary<string, double> clusterOccurances(string ipFilePath)
        {
            Dictionary<string, double> so = new Dictionary<string, double>();
            StreamReader sr = new StreamReader(ipFilePath);
            string line = sr.ReadLine();
            while ((line = sr.ReadLine()) != null)
            {
                string[] words = line.Split('|');
                words[0] = words[0].ToUpper();
                if (so.ContainsKey(words[0]))
                {
                    so[words[0]]++;
                }
                else
                {
                    so[words[0]] = 1.0;
                }
            }
            sr.Close();
            return so;

        }

        public static Dictionary<string, double> stringOccuranceinCluster(string ipFilePath, int attributePosition)
        {
            Dictionary<string, double> so = new Dictionary<string, double>();
            StreamReader sr = new StreamReader(ipFilePath);
            string line = sr.ReadLine();
            while ((line = sr.ReadLine()) != null)
            {
                string[] words = line.Split('|');
                words[attributePosition] = words[attributePosition].ToUpper();
                if (so.ContainsKey(words[attributePosition] + "," + words[0]))
                {
                    so[words[attributePosition] + "," + words[0]]++;
                }
                else
                {
                    so[words[attributePosition] + "," + words[0]] = 1.0;
                }
            }
            sr.Close();
            return so;

        }

        public static Dictionary<string, double> entropyDictionary(string ipFilePath, int attributePosition)
        {
            Dictionary<string, double> so = stringOccuranceinCluster(ipFilePath, attributePosition);
            Dictionary<string, double> co = clusterOccurances(ipFilePath);
            Dictionary<string, double> eo = new Dictionary<string, double>();

            //Console.WriteLine("Cluster | frequency");
            //foreach (KeyValuePair<string, double> pair in co)
            //{
            //    Console.WriteLine(pair.Key + " : " + pair.Value);
            //}
            //Console.WriteLine("Attribute,Cluster | frequency");
            //foreach (KeyValuePair<string, double> pair in so)
            //{
            //    Console.WriteLine(pair.Key + " : " + pair.Value);
            //}

            foreach (KeyValuePair<string, double> pair in so)
            {
                double p = pair.Value / co[pair.Key.Split(',')[1]];
                double entropy = -p * Math.Log(p);
                eo.Add(pair.Key, entropy);
                //Console.WriteLine(pair.Key +" : "+ p);
            }
            //Console.WriteLine("Attribute | entropy");
            //foreach (KeyValuePair<string, double> pair in eo)
            //{
            //    Console.WriteLine(pair.Key + " : " + pair.Value);
            //}

           

            return eo;
        }


    }
}

