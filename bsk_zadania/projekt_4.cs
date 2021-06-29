using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace bsk_zadania
{
    public class projekt_4
    {
        public string time;
        public int Iterations;
        public double Average;
        private Random random;
        private List<double> ListOfResults;
        public projekt_4()
        {
            random = new Random();
            ListOfResults = new List<double>();
        }
        static string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.Default.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }

        public void process(int iterations)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            for (int i = 0; i < iterations; i++)
            {
                int lenght = random.Next(1, 500);
                var rawData = RandomString(lenght);
                // Create a SHA256   
                using (SHA256 sha256Hash = SHA256.Create())
                {
                    // ComputeHash - returns byte array  
                    string hashFromRawData = ComputeSha256Hash(rawData);
                    Random random = new Random();
                    var bytes = Encoding.Default.GetBytes(rawData);
                    var randomByte = random.Next(0, bytes.Length);
                    var bitArray = new BitArray(new byte[] { bytes[randomByte] });
                    var randomBit = random.Next(0, bitArray.Length);
                    bitArray[randomBit] = bitArray[randomBit] == false ? bitArray[randomBit] = true : bitArray[randomBit] = false;
                    var changedByte = bitArray.ConvertToByte();
                    bytes[randomByte] = changedByte;
                    var hashFromChangedBytes = ComputeSha256Hash(Encoding.Default.GetString(bytes));
                    ListOfResults.Add(CalculateSimilarity(hashFromRawData, hashFromChangedBytes));
                };
            }
            timer.Stop();
            time = timer.Elapsed.Milliseconds.ToString()+"ms";
            Average = ListOfResults.Average();
            Iterations = iterations;
        }
        /// <summary>
        /// Returns the number of steps required to transform the source string
        /// into the target string.
        /// </summary>
        static int ComputeLevenshteinDistance(string source, string target)
        {
            if ((source == null) || (target == null)) return 0;
            if ((source.Length == 0) || (target.Length == 0)) return 0;
            if (source == target) return source.Length;

            int sourceWordCount = source.Length;
            int targetWordCount = target.Length;

            // Step 1
            if (sourceWordCount == 0)
                return targetWordCount;

            if (targetWordCount == 0)
                return sourceWordCount;

            int[,] distance = new int[sourceWordCount + 1, targetWordCount + 1];

            // Step 2
            for (int i = 0; i <= sourceWordCount; distance[i, 0] = i++) ;
            for (int j = 0; j <= targetWordCount; distance[0, j] = j++) ;

            for (int i = 1; i <= sourceWordCount; i++)
            {
                for (int j = 1; j <= targetWordCount; j++)
                {
                    // Step 3
                    int cost = (target[j - 1] == source[i - 1]) ? 0 : 1;

                    // Step 4
                    distance[i, j] = Math.Min(Math.Min(distance[i - 1, j] + 1, distance[i, j - 1] + 1), distance[i - 1, j - 1] + cost);
                }
            }

            return distance[sourceWordCount, targetWordCount];
        }
        double CalculateSimilarity(string source, string target)
        {
            if ((source == null) || (target == null)) return 0.0;
            if ((source.Length == 0) || (target.Length == 0)) return 0.0;
            if (source == target) return 1.0;

            int stepsToSame = ComputeLevenshteinDistance(source, target);
            return (1.0 - ((double)stepsToSame / (double)Math.Max(source.Length, target.Length)));
        }
        public string RandomString(int length)
        { 
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
    public static class ByteExtensions
    {
        public static void CompareHashes(this byte[] val, byte[] val2)
        {
            int i = 0;
            List<bool> comparer = new List<bool>();
            foreach (var item in val)
            {
                if (item.Equals(val2[i]))
                {
                    comparer.Add(true);
                }
                else
                {
                    comparer.Add(false);
                }
                i++;
            }

            var resemblance = comparer.GroupBy(p => p.Equals(true)).Count();
            var result = (resemblance / comparer.Count) * 100;
        }
        public static byte ConvertToByte(this BitArray bits)
        {
            if (bits.Count != 8)
            {
                throw new ArgumentException("bits");
            }
            byte[] bytes = new byte[1];
            bits.CopyTo(bytes, 0);
            return bytes[0];
        }
    }
}
