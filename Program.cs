namespace baeckjun
{
    internal class Program
    {
        //Main
        private static void Main()
        {
            Silver1003();
        }

        private static void Silver1003()
        {
            int numTests = int.Parse(Console.ReadLine());

            // Create an array to store the results of the Fibonacci1003 function for each value of n.
            // The array is initialized with -1 for all elements, which indicates that the value
            // has not yet been calculated.
            int[][] results = new int[41][];
            for (int i = 0; i < 41; i++)
            {
                results[i] = new int[] { -1, -1 };
            }

            // Set the base cases for n = 0 and n = 1
            results[0] = new int[] { 1, 0 };
            results[1] = new int[] { 0, 1 };

            for (int i = 0; i < numTests; i++)
            {
                int n = int.Parse(Console.ReadLine());
                int[] result = Fibonacci1003(n, results);
                Console.WriteLine(result[0] + " " + result[1]);
            }
        }

        private static int[] Fibonacci1003(int n, int[][] results)
        {
            // If the result has already been calculated, return it
            if (results[n][0] != -1)
            {
                return results[n];
            }

            // Otherwise, calculate the result by summing the results for n-1 and n-2
            int[] result = Fibonacci1003(n - 1, results);
            int[] result2 = Fibonacci1003(n - 2, results);
            int[] newResult = new int[] { result[0] + result2[0], result[1] + result2[1] };

            // Store the result in the array for future use
            results[n] = newResult;

            return newResult;
        }

        public static void Silver1003Slow()
        {
            // 백준 실버3 피보나치 함수
            using StreamReader reader = new StreamReader(Console.OpenStandardInput());
            using StreamWriter writer = new StreamWriter(Console.OpenStandardOutput());
            var output = new System.Text.StringBuilder();
            int numTests = int.Parse(reader.ReadLine());

            for (int i = 0; i < numTests; i++)
            {
                int n = int.Parse(reader.ReadLine());
                int[] result = Fibonacci1003Slow(n);
                output.AppendLine(result[0] + " " + result[1]);
            }
            writer.Write(output);
        }

        static int[] Fibonacci1003Slow(int n)
        {
            // 시간초과인데 남겨두는 이유: 재귀함수를 이용한 피보나치 함수를 이해하기 위해
            if (n == 0)
            {
                return new int[] { 1, 0 }; // 1 0's and 0 1's
            }
            else if (n == 1)
            {
                return new int[] { 0, 1 }; // 0 0's and 1 1's
            }
            else
            {
                int[] result = Fibonacci1003Slow(n - 1);
                int[] result2 = Fibonacci1003Slow(n - 2);
                return new int[] { result[0] + result2[0], result[1] + result2[1] };
            }
        }


        public static void Silver1620()
        {
            //백준 실버4 1620 나는야 포켓몬 마스터 이다솜
            using StreamReader reader = new StreamReader(Console.OpenStandardInput());
            using StreamWriter writer = new StreamWriter(Console.OpenStandardOutput());
            int[] NM = Array.ConvertAll(reader.ReadLine().Split(), int.Parse);
            Dictionary<int, string> dictIdx = new Dictionary<int, string>();
            Dictionary<string, int> dictValue = new Dictionary<string, int>();
            var output = new System.Text.StringBuilder(); //using System.Text 대신

            for (int i = 1; i <= NM[0]; i++) //N. 1부터 넣어주기
            {
                string s = reader.ReadLine();
                dictIdx.Add(i, s);
                dictValue.Add(s, i);
            }
            for (int i = 1; i <= NM[1]; i++) //M
            {
                string s = reader.ReadLine();
                int n;
                if (int.TryParse(s, out n))
                {
                    output.AppendLine(dictIdx[n]);
                }
                else
                {
                    output.AppendLine((dictValue[s]).ToString());
                }
            }
            writer.Write(output);
            //reader.Close(); writer.Close();
        }

        public static void Silver2563()
        {
            // 2563 색종이
            StreamReader reader = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            int n = int.Parse(reader.ReadLine());
            //int[,] canvas = new int[100,100];
            bool[,] canvas = new bool[100, 100];

            int[] xy = new int[2];

            // white canvas set to 0
            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 100; j++)
                {
                    canvas[i, j] = false;
                }
            }

            // canvas set to 1
            for (int i = 0; i < n; i++)
            {
                xy = Array.ConvertAll(reader.ReadLine().Split(' '), int.Parse);

                for (int j = xy[0]; j < xy[0] + 10; j++)
                {
                    for (int k = xy[1]; k < xy[1] + 10; k++)
                    {
                        canvas[j, k] = true;
                    }
                }
            }

            // canvas sum
            int sum = 0;
            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 100; j++)
                {
                    //sum += canvas[i, j];
                    //bool to int
                    sum += canvas[i, j] ? 1 : 0;
                }
            }

            // print sum
            Console.Write(sum);
        }
    }
}


//public static void RunMain()
//{
//    Main(); // for test
//}


