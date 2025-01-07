using System;
using System.Security.Cryptography;
using MathNet.Numerics.Random;

namespace Randomize {
    public interface IRandomIntegerContext {
        int GetNextRandomNumber();
       IEnumerable<int> NextN_Random_Numbers(int N, int min, int max);
        List<T> SampleWithReplacement<T>(IList<T> source, int sampleSize);
    }

    public class CRandomIntegerContext : IRandomIntegerContext {
        
        protected Random random;
        
        public CRandomIntegerContext() {
            random = new Random();
        }
        public CRandomIntegerContext(int seed) {
            random = new Random(seed);
        }

        // This method is used to generate a random number
        // between 0 and 10. It also stores the generated
        // number in the distribution dictionary with the
        // key recall_name.
        public int GetNextRandomNumber() {
            int result = random.Next(10);
            return result;
        }

        // This method is used to generate N random numbers
        // between min and max. It uses the Random.Next method
        // to generate the random numbers. 
        public IEnumerable<int> NextN_Random_Numbers(int N, int min, int max) {
            for (int i = 0; i < N; i++) {
                yield return random.Next(min,max)%11;
            }
        }

        // This method is used to generate N random numbers
        // between min and max. It uses the RandomNumberGenerator
        // class to generate the random numbers.
        public static IEnumerable<int> NextN_RandomGenerator_Method(int N, int min, int max) {
            for (int i = 0; i < N; i++) {
                yield return RandomNumberGenerator.GetInt32(min,max+1);
            }
        }

        // This method is used to generate N random numbers
        // between min and max. It uses the MersenneTwister
        // class to generate the random numbers.
        public static IEnumerable<int> NextN_MersenneTwister_Method(int N, int min, int max) {
            MersenneTwister random = MersenneTwister.Default;
            for (int i = 0; i < N; i++) {
                yield return random.Next(min*10,max*10+1)%11;
            }
        }

        public List<T> SampleWithReplacement<T>(IList<T> source, int sampleSize) {
            random ??= new Random();
            return Enumerable.Range(0, sampleSize)
                .Select(_ => source[random.Next(source.Count)])
                .ToList();
        }
    }

    public class CRandomIntegerContextMemory : CRandomIntegerContext {
        Dictionary<string, List<int>> distribution = new Dictionary<string, List<int>>();


        // This method is used to recall the value of a
        // previously generated random number. The recall_name
        // is used to look up the value in the distribution
        // dictionary.
        public int RecallValue(string recall_name, int index = 0) {
            return distribution[recall_name][index];
        }

        // This method is used to generate a random number
        // between 0 and 10. It also stores the generated
        // number in the distribution dictionary with the
        // key recall_name.
        public int GetNextRandomNumber(string recall_name) {
            int result = random.Next(10);
            if (!distribution.ContainsKey(recall_name)) {
                distribution[recall_name] = new List<int>();
            }
            distribution[recall_name].Add(result);
            return result;
        }
    }
}
