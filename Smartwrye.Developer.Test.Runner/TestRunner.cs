using System.Diagnostics;
using System.Reflection;
using System.Text;
using Xunit;
using Xunit.Runners;
using System.Threading;

namespace Smartwrye.Developer.Test.Runner
{
    public static class TestRunner
    {
        private static int _totalTests = 0;
        private static int _passedTests = 0;
        private static int _failedTests = 0;
        private static int _skippedTests = 0;

        private static readonly Dictionary<string, TestResultInfo> _testResultsDict = new();

        public static void Run(string assemblyPath)
        {
            AnalyzeTestMethods(assemblyPath);

            var executionCompleteEvent = new ManualResetEventSlim(false);

            try
            {
                using (var runner = AssemblyRunner.WithoutAppDomain(assemblyPath))
                {
                    runner.OnDiscoveryComplete = info =>
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($"\n========== Test Discovery Complete ==========");
                        Console.WriteLine($"Discovered {info.TestCasesToRun} tests in {assemblyPath}");
                        Console.ResetColor();
                    };

                    runner.OnTestStarting = info =>
                    {
                        _totalTests++;
                        string testName = GetTestName(info.TestDisplayName);
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"\n[Running Test] {testName}");
                        Console.ResetColor();
                    };

                    runner.OnTestFailed = info =>
                    {
                        _failedTests++;
                        string testName = GetTestName(info.TestDisplayName);
                        string failureMessage = info.ExceptionMessage ?? "Unknown failure";
                        string stackTrace = info.ExceptionStackTrace ?? "No stack trace available";

                        UpdateTestResult(testName, "Failed", failureMessage: failureMessage, stackTrace: stackTrace);
                    };

                    runner.OnTestPassed = info =>
                    {
                        _passedTests++;
                        string testName = GetTestName(info.TestDisplayName);
                        UpdateTestResult(testName, "Passed");
                    };

                    runner.OnTestSkipped = info =>
                    {
                        _skippedTests++;
                        string testName = GetTestName(info.TestDisplayName);
                        UpdateTestResult(testName, "Skipped", info.SkipReason);
                    };

                    runner.OnExecutionComplete = info =>
                    {
                        DisplaySummary();
                        Console.WriteLine("Test execution complete.");
                        executionCompleteEvent.Set(); // Signal that execution is complete
                    };

                    runner.Start();

                    Console.WriteLine("Running tests...");
                    executionCompleteEvent.Wait(); // Wait for tests to complete

                    // Ensure console stays open if running from IDE
                    //Console.WriteLine("Press any key to exit...");
                    //Console.ReadKey();
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"An error occurred: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                Console.ResetColor();
            }
        }

        // Other methods remain unchanged

        private static void AnalyzeTestMethods(string assemblyPath)
        {
            const string Theory = "Theory";
            try
            {
                var assembly = Assembly.LoadFrom(assemblyPath);
                foreach (var type in assembly.GetTypes())
                {
                    foreach (var method in type.GetMethods().Where(a => a.GetCustomAttribute(typeof(FactAttribute)) != null
                                                                     || a.GetCustomAttribute(typeof(TheoryAttribute)) != null))
                    {
                        string testType = method.GetCustomAttribute(typeof(TheoryAttribute)) != null
                            ? Theory
                            : method.GetCustomAttribute(typeof(FactAttribute)) != null ? "Fact" : string.Empty;

                        if (!string.IsNullOrEmpty(testType))
                        {
                            var methodName = method.Name;

                            // Count the number of InlineData attributes for Theory tests
                            int inlineDataCount = testType == Theory
                                ? method.GetCustomAttributes(typeof(InlineDataAttribute), false).Length
                                : 0;

                            if (!_testResultsDict.ContainsKey(methodName))
                            {
                                _testResultsDict[methodName] = new TestResultInfo
                                {
                                    Name = methodName,
                                    Type = testType,
                                    InlineDataCount = inlineDataCount
                                };
                            }
                        }
                    }
                }

#if _viewDiscovered
                Console.WriteLine("\nDiscovered Tests:");
                foreach (var test in _testResultsDict.Values)
                {
                    if (test.Type == "Theory")
                    {
                        Console.WriteLine($"{test.Name} ({test.Type}) - InlineData Count: {test.InlineDataCount}");
                    }
                    else
                    {
                        Console.WriteLine($"{test.Name} ({test.Type})");
                    }

                }
#endif
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"An error occurred while analyzing test methods: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                Console.ResetColor();
            }
        }

        private static string GetTestName(string fullDisplayName)
        {
            try
            {
                var methodName = fullDisplayName.Split(new char[] { '(', ')' })[0].Trim();
                methodName = methodName.Trim().Split('.').Last();

                return !string.IsNullOrWhiteSpace(methodName) && methodName.Length >= 3 ? methodName : "Unknown";
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"An error occurred while extracting the test name: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                Console.ResetColor();
                return "Unknown";
            }
        }

        static void UpdateTestResult(string testName, string result, string skipReason = null, string failureMessage = null, string stackTrace = null)
        {
            if (_testResultsDict.TryGetValue(testName, out TestResultInfo? value))
            {
                value.Result = result;
                value.SkipReason = skipReason;
                value.FailureMessage = failureMessage;
                value.StackTrace = stackTrace;
            }
            else
            {
                _testResultsDict[testName] = new TestResultInfo
                {
                    Name = testName,
                    Result = result,
                    SkipReason = skipReason,
                    FailureMessage = failureMessage,
                    StackTrace = stackTrace
                };
            }
        }

        private static void DisplaySummary()
        {
            Console.WriteLine("");
            // Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("================================================");
            Console.WriteLine("           📋  Test Summary  📋                 ");
            Console.WriteLine("================================================");
            Console.ResetColor();
            Console.WriteLine("");

            Console.WriteLine($"Total Tests: {_totalTests}");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Passed: {_passedTests}");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Failed: {_failedTests}");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"Skipped: {_skippedTests}");
            Console.ResetColor();

            Console.WriteLine("");
            //Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("================================================");
            Console.WriteLine("           🔍  Test Details  🔍                ");
            Console.WriteLine("================================================");
            Console.ResetColor();
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine("");

            foreach (var testResult in _testResultsDict.Values)
            {
                string resultIcon = testResult.Result switch
                {
                    "Passed" => "✅",
                    "Failed" => "❌",
                    "Skipped" => "⚠️",
                    _ => ""
                };

                // Assign the result of the switch statement to a variable
                ConsoleColor color = testResult.Result switch
                {
                    "Passed" => ConsoleColor.Green,
                    "Failed" => ConsoleColor.Red,
                    "Skipped" => ConsoleColor.Blue,
                    _ => ConsoleColor.White
                };

                Console.ForegroundColor = color;
                if (testResult.Type == "Theory")
                {
                    Console.WriteLine($"{testResult.Result} - {resultIcon} {testResult.Name} ({testResult.Type}) -  InlineData Count: {testResult.InlineDataCount}");
                }
                else
                {
                    Console.WriteLine($"{testResult.Result} - {resultIcon} {testResult.Name} ({testResult.Type}) ");
                }

                if (testResult.Result == "Failed" && !string.IsNullOrEmpty(testResult.FailureMessage))
                {
                    Console.WriteLine($"   Failure Message: {testResult.FailureMessage}");
                    Console.WriteLine($"   Stack Trace: {testResult.StackTrace}");
                }

                if (testResult.Result == "Skipped" && !string.IsNullOrEmpty(testResult.SkipReason))
                {
                    Console.WriteLine($"   Reason: {testResult.SkipReason}");
                }
                Console.ResetColor();
            }
        }

        private class TestResultInfo
        {
            public string Name { get; set; } = string.Empty;
            public string Type { get; set; } = string.Empty;
            public string Result { get; set; } = string.Empty;
            public string SkipReason { get; set; } = string.Empty;
            public int InlineDataCount { get; set; }
            public string FailureMessage { get; set; } = string.Empty;
            public string StackTrace { get; set; } = string.Empty;
        }
    }
}
