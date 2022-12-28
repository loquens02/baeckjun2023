using NUnit.Framework;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace baeckjun
{
    internal class ProgramTests
    {

        [TestCase("4 3\napple\nbanana\norange\ngrapes\n2\napple\n3\n", "banana\ngrapes\n")]
        [TestCase("5 4\napple\nbanana\norange\ngrapes\nkiwi\n2\napple\nkiwi\norange\n4\n", "banana\nkiwi\norange\ngrapes\n")]
        public void TestProgram(string input, string expectedOutput)
        {
            // Convert input and expected output strings to streams
            var inputStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(input));
            var expectedOutputStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(expectedOutput));

            // Redirect standard input and output streams
            Console.SetIn(new StreamReader(inputStream));
            Console.SetOut(new StreamWriter(expectedOutputStream));

            // Call the program
            //Program.RunMain(); // TODO

            // Reset standard input and output streams
            Console.SetIn(new StreamReader(Console.OpenStandardInput()));
            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));

            // Compare the output of the program with the expected output
            inputStream.Position = 0;
            expectedOutputStream.Position = 0;
            var output = new StreamReader(expectedOutputStream).ReadToEnd();
            Assert.AreEqual(expectedOutput, output);
            
            Assert.That(output, Is.EqualTo(expectedOutput));
        }
    }
}



//namespace Tests
//{
//    public class ProgramTests
//    }
//}
