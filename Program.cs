namespace baeckjun
{
    internal class Program
    {
        //Main
        private static void Main()
        {
            Silver1541();
        }

        private static void Silver1541()
        {
            using StreamReader reader = new StreamReader(Console.OpenStandardInput());
            using StreamWriter writer = new StreamWriter(Console.OpenStandardOutput());
            //var output = new System.Text.StringBuilder();
            
            // Test the algorithm with a sample formula
            string formula = reader.ReadLine(); // "015-018+7-4-8+9"
            
            int result = MinimizeFormula(formula);
            writer.Write(result);
        }

        private static int MinimizeFormula(string formula)
        {
            // Split the formula into an array of numbers and operators
            // 자른 위치의 연산자로 계산할거면 '연산자만 있는 배열'도 따로 만들어서 반환해야지
            //(string[] elements, string[] operations) = SplitFormula(formula);
            string[] elements = SplitFormula(formula);
            int n = elements.Length;

            // Define a 2D table for dynamic programming
            int[,] dp = new int[n, n];

            // Initialize the table
            for (int i = 0; i < n; i++)
            {
                dp[i, i] = int.Parse(elements[i]);  // elements[i] is the ith element in the formula
            }

            // Use dynamic programming to fill out the rest of the table
            for (int length = 2; length <= n; length++)
            {
                for (int i = 0; i <= n - length; i++)
                {
                    int j = i + length - 1;
                    dp[i, j] = int.MaxValue;
                    for (int k = i; k < j; k++)
                    {
                        // operations 연산자만 있는 배열이라 '숫자 배열'보다 요소가 하나 적다.
                        // 연산자 따로 떼서 계산할거면 elements 에서 각 숫자에 연산자를 붙이지 말아야지
                        int value = Evaluate(dp[i, k], dp[k + 1, j], operations[k]);  // i ~ k ~ j 토막
                        //int value = Evaluate(dp[i, k], dp[k + 1, j], elements[k]);  // i ~ k ~ j 토막
                        dp[i, j] = Math.Min(dp[i, j], value);
                    }
                }
            }

            // The minimum value of the entire formula is dp[0][n-1]
            return dp[0, n - 1];
        }


        // Split the formula into an array of numbers and operators
        /**
         * @return (연산자&숫자 배열, 연산자 배열)
         * 
         */
        //private static string[] SplitFormula(string formula)
        private static (string[], string[]) SplitFormula(string formula)
        {
            List<string> tokens = ParseExpression(formula);
            string[] elements = new string[tokens.Count / 2 + 1]; // 숫자 개수 만큼 반토막+1            
            string[] operations = new string[tokens.Count / 2]; // 숫자 개수 만큼 반토막+1            

            int index = 0;
            
            for (int i = 0; i < tokens.Count; i++)
            {
                if (i % 2 == 1)
                {
                    operations[index] = tokens[i].ToString();
                    elements[index] = tokens[i].ToString(); // 연산자 넣기

                }
                else 
                {
                    elements[index] += tokens[i]; // 숫자 (0,2번째)
                    index++;
                }
            }
            return (elements, operations);
            //return elements;
        }

        // SplitFormula 말고 이것만 하는 게 더 의도에 맞는 듯?
        private static List<string> ParseExpression(string expression)
        {
            // Create a list to store the tokens
            List<string> tokens = new List<string>();

            // Keep track of the current token
            string currentToken = "";

            // Iterate through the characters in the expression
            foreach (char c in expression)
            {
                // If the character is a digit, add it to the current token
                if (char.IsDigit(c))
                {
                    currentToken += c;
                }
                // If the character is an operator, add the current token to the list of tokens and reset the current token
                else if (c == '+' || c == '-')
                {
                    //c# Remove trailing leading zeros. 
                    currentToken= currentToken.TrimStart('0');

                    tokens.Add(currentToken);
                    currentToken = "";
                    tokens.Add(c.ToString());
                }
            }

            // Add the final token to the list of tokens
            tokens.Add(currentToken);

            return tokens;
        }

        private static int Evaluate(int a, int b, string op)
        {
            if (op == "+")
            {
                return a + b;
            }
            else if (op == "-")
            {
                return a - b;
            }
            else
            {
                throw new Exception("Invalid operator: " + op);
            }
        }

        private static void Silver1003()
        {
            // 더 빠른 코드? 재귀를 안 쓰더라. 단일 for문과 무언가의 덧셈으로 끝낸다 > 아마도 상향식 DP?
            // 이해는 못 했는데 피보나치를 계산하는 대신 피보나치0,1count 만 하는 문제로 재해석해서 푼 듯.
            // 피보나치 수열은 0 1 1 2 3 5 8 13 이니
            // 피보나치 01count는 [1,0] [0,1] [1,1] [0,2] [1,3] 으로 여기 규칙을 따로 구한 듯?? (미검증)
            using StreamReader reader = new StreamReader(Console.OpenStandardInput());
            using StreamWriter writer = new StreamWriter(Console.OpenStandardOutput());
            var output = new System.Text.StringBuilder();
            int numTests = int.Parse(reader.ReadLine());

            // Create an array to store the results of the Fibonacci1003 function for each value of n.
            // The array is initialized with -1 for all elements, which indicates that the value
            // has not yet been calculated.
            
            // 피보나치 입력이 될 N은 최대 40. N= 0~40 일 때 {0횟수, 1횟수} 를 저장하는 그릇.
            // 그냥 results[i][2] = { -1,-1} 을 축약해서 쓴 것. 
            int[][] results = new int[41][];
            for (int i = 0; i < 41; i++)
            {
                results[i] = new int[] { -1, -1 };
            }

            // Set the base cases for n = 0 and n = 1
            //2차원에 할당한 공간 크기 몰라서 안 된다. results[0] = { 1, 0 };
            //특정 요소 접근하는 문법. results[0][2] = { 1, 0 };
            results[0] = new int[] { 1, 0 };
            results[1] = new int[] { 0, 1 };

            for (int i = 0; i < numTests; i++)
            {
                int n = int.Parse(reader.ReadLine());
                int[] result = Fibonacci1003(n, results); // 0,1횟수를 다음대에 넘기려면 인자가 하나 더 필요
                output.AppendLine($"{result[0]} {result[1]}");
            }
            writer.Write(output);
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

// C# syntax
// ($"{result[0]} {result[1]}") 및 return (int,int) https://stackoverflow.com/a/36436255
// C# 7.0의 새로운 기능   https://devblogs.microsoft.com/dotnet/new-features-in-c-7-0/

//public static void RunMain()
//{
//    Main(); // for test
//}


