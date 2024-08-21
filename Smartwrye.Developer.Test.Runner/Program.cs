using System.Text;

namespace Smartwrye.Developer.Test.Runner
{
    class Program
    {
        static void Main(string[] args)
        {
            
            string assemblyPath = typeof(Tests.Tests.PaymentServiceTestsFact).Assembly.Location;

            Console.OutputEncoding = Encoding.UTF8;

            while (true)
            {
                Console.Clear();

                // Set background and foreground colors for the title section
                Console.BackgroundColor = ConsoleColor.DarkBlue;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("================================================");
                Console.WriteLine("      🚀 Welcome to Smartwrye Test Runner       ");
                Console.WriteLine("================================================");
                Console.ResetColor();
                Console.WriteLine("");
                // Menu options with emojis and background colors
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(" [1] 🛠   0Run Simple Rebate Calculator Factory");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(" [2] ⚙️  Run Strategy Rebate Calculator Factory");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(" [3] 🧪  Run All Tests");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine(" [4] 📊  Run Tests Summary");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(" [5] ❌  Exit");
                Console.ResetColor();

                Console.WriteLine("");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("================================================");
                Console.ResetColor();

                Console.Write("\nPlease select an option: ");

                // Capture the key press
                ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true); // Intercept prevents the key from being displayed

                switch (keyInfo.Key)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        Console.Clear();
                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("\n🛠 Running Simple Rebate Calculator Factory...");
                        Console.ResetColor();
                        CodeRunner.Run(true);  // Runs SimpleRebateCalculatorFactory
                        PauseConsole();
                        break;

                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        Console.Clear();
                        Console.BackgroundColor = ConsoleColor.DarkYellow;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("\n⚙️ Running Strategy Rebate Calculator Factory...");
                        Console.ResetColor();
                        CodeRunner.Run(false);  // Runs StrategyRebateCalculatorFactory
                        PauseConsole();
                        break;

                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        Console.Clear();
                        Console.BackgroundColor = ConsoleColor.DarkMagenta;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("\n🧪 Running Test Runner...");
                        Console.ResetColor();
                        TestRunner.Run(assemblyPath);  // Runs the TestRunner
                        PauseConsole();
                        break;

                    case ConsoleKey.D4:
                    case ConsoleKey.NumPad4:
                        Console.Clear();
                        Console.BackgroundColor = ConsoleColor.Yellow;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n\n🧪 Running Test summary...");
                        Console.ResetColor();
                        UnitTests.Run(assemblyPath);
                        PauseConsole();
                        break;

                    case ConsoleKey.D5:
                    case ConsoleKey.NumPad5:
                        Console.Clear();
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("\n❌ Exiting the application...");
                        Console.ResetColor();
                        return;


                    default:
                        Console.Clear();
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("\n⚠️ Invalid selection. Please choose a valid option.");
                        Console.ResetColor();
                        PauseConsole();
                        break;
                }
            }
        }

        static void PauseConsole()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\nPress any key to return to the menu...");
            Console.ResetColor();
            Console.ReadKey(intercept: true); // Wait for any key press without displaying it
        }


    }
}
