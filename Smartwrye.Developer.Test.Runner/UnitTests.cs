using Xunit.Abstractions;
using Xunit;
using Xunit.Runners;
using Xunit.Sdk;

namespace Smartwrye.Developer.Test.Runner
{
    public static class UnitTests
    {
        public static void Run(string assemblyPath)
        {
            using (var controller = new XunitFrontController(AppDomainSupport.IfAvailable, assemblyPath))
            {
                var discoverySink = new TestDiscoverySink();
                var executionSink = new TestExecutionSink();

                Console.WriteLine("Starting discovery...");
                controller.Find(false, discoverySink, TestFrameworkOptions.ForDiscovery());
                discoverySink.Finished.WaitOne();

                if (discoverySink.TestCases.Count == 0)
                {
                    Console.WriteLine("No tests found or discovery timed out.");
                    return;
                }

                Console.WriteLine("Starting execution...");
                Console.WriteLine("");
                controller.RunTests(discoverySink.TestCases, executionSink, TestFrameworkOptions.ForExecution());
                executionSink.Finished.WaitOne();

                Console.BackgroundColor = ConsoleColor.DarkBlue;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("================================================");
                Console.WriteLine("           📋  Test Results  📋                 ");
                Console.WriteLine("================================================");
                Console.ResetColor();
                Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine("");
                // this used to work pre dot net core
                //foreach (var result in executionSink.TestClasses)
                //{
                //    Console.WriteLine($"Test: {result.DisplayName}, Result: {result.Outcome}");
                //    if (!string.IsNullOrEmpty(result.ErrorMessage))
                //    {
                //        Console.WriteLine($"Error Message: {result.ErrorMessage}");
                //        Console.WriteLine($"Stack Trace: {result.StackTrace}");
                //    }
                //}
                int testsPassed = executionSink.ExecutionSummary.Total -
                    (executionSink.ExecutionSummary.Failed + executionSink.ExecutionSummary.Skipped);
                // Display the total number of tests run
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"📊 Total Tests Run: {executionSink.ExecutionSummary.Total}");
                Console.ResetColor();

                // Display the number of tests passed with a green color and a checkmark emoji
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"✅ Total Tests Passed: {testsPassed}");
                Console.ResetColor();

                // Display the number of tests failed with a red color and a cross emoji
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"❌ Total Tests Failed: {executionSink.ExecutionSummary.Failed}");
                Console.ResetColor();

                // Display the number of tests skipped with a blue color and a warning emoji
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"⚠️  Total Tests Skipped: {executionSink.ExecutionSummary.Skipped}");
                Console.ResetColor();

                Console.WriteLine("");
                Console.WriteLine("Test execution complete.");
                //Console.WriteLine("Press any key to exit...");
                //Console.Read();
            }
        }
    }

    class TestDiscoverySink : Xunit.LongLivedMarshalByRefObject, IMessageSink
    {
        public HashSet<ITestCase> TestCases { get; } = new HashSet<ITestCase>();
        public ManualResetEvent Finished { get; } = new ManualResetEvent(false);

        public bool OnMessage(IMessageSinkMessage message)
        {
            if (message is ITestCaseDiscoveryMessage discoveryMessage)
            {
                TestCases.Add(discoveryMessage.TestCase);
            }
            else if (message is IDiscoveryCompleteMessage)
            {
                Finished.Set();
            }
            return true;
        }
    }
    class TestExecutionSink : Xunit.LongLivedMarshalByRefObject, IMessageSink
    {
        public TestExecutionSummary ExecutionSummary { get; } = new TestExecutionSummary();
        public ManualResetEvent Finished { get; } = new ManualResetEvent(false);

        public bool OnMessage(IMessageSinkMessage message)
        {
            if (message is ITestFailed)
            {
                ExecutionSummary.Failed++;
            }
            else if (message is ITestPassed)
            {
                ExecutionSummary.Total++;
            }
            else if (message is ITestSkipped)
            {
                ExecutionSummary.Skipped++;
            }
            else if (message is ITestAssemblyFinished)
            {
                Finished.Set();
            }

            return true;
        }
    }

    class TestExecutionSummary
    {
        public int Total { get; set; }
        public int Failed { get; set; }
        public int Skipped { get; set; }
    }


}

