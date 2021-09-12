using System;

// This example code shows how you could implement the required main function for a 
// Console UWP Application. You can replace all the code inside Main with your own custom code.

// You should also change the Alias value in the AppExecutionAlias Extension in the 
// Package.appxmanifest to a value that you define. To edit this file manually, right-click
// it in Solution Explorer and select View Code, or open it with the XML Editor.

namespace Calculatre
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Rolling();
            }
            else
            {
                if (args[0].Equals("help"))
                {
                    GetHelp();
                }
                else
                {
                    Calculator(args[0]);
                }
            }
        }

        static void Rolling()
        {
            string input;
            Console.Write("Calculatre@ ");
            input = Console.ReadLine();

            while (!input.Equals("quit"))
            {
                if (input.Equals("help"))
                {
                    GetHelp();
                }
                else if (input.Equals(""))
                {
                    // do nothing
                }
                else
                {
                    Calculator(input);
                }

                Console.Write("Calculatre@ ");
                input = Console.ReadLine();
            }
        }

        static void Calculator(string formula)
        {
            // formula += '\0';
            Calculate cal = new Calculate(formula);
            cal.Start();

            switch (cal.error)
            {
                case 1:
                    Console.WriteLine("div cannot be 0");
                    break;

                case 2:
                    Console.WriteLine("cannot recognize the input");
                    break;

                default:
                    Console.WriteLine($"{cal.formula} = {cal.result}");
                    break;
            }
        }

        static void GetHelp()
        {
            Console.WriteLine("supporting operations: ^, !, e, pi, +, -, *, /");
            Console.WriteLine("sin(), cos(), tan(), asin(), acos(), atan(), sinh(), cosh(), tanh(), exp(), ln(), lg()");
            Console.WriteLine("Please enter \"quit\" to exit this application.");
        }
    }
}
