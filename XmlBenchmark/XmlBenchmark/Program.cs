using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Validators;

namespace XmlBenchmark
{
    class Program
    {
        static void Main(string[] args)
        {
#if DEBUG
            Console.WriteLine("Benchmarks need to be run in Release mode. Commencing sanitization test");
            TestSanitizationResults();
            Console.ReadLine();
#endif
#if !DEBUG
            Console.WriteLine("Tests need to be run in Debug mode. Commencing benchmark");
            Benchmark();
#endif
        }

        private static void Benchmark()
        {
            var benchmarkRunMode = RunMode.Short;
            var defaultBenchmarkJob = Job.ShortRun;

            BenchmarkRunner
                .Run<XmlStringSanitizer>(
                    ManualConfig
                        .Create(DefaultConfig.Instance)
                        .With(defaultBenchmarkJob)
                        .With(
                                new Job("x86LegacyJob_ConcurrentServerGC", benchmarkRunMode, EnvMode.LegacyJitX86)
                                {
                                    Env = { Runtime = Runtime.Clr, Gc = { Server = true, Concurrent = true }}
                                })
                        .With(
                                new Job("x64LegacyJob_ConcurrentServerGC", benchmarkRunMode, EnvMode.LegacyJitX64)
                                {
                                    Env = { Runtime = Runtime.Clr, Gc = { Server = true, Concurrent = true } }
                                })
                        .With(
                                new Job("RyuJitJob_ConcurrentServerGC", benchmarkRunMode, EnvMode.RyuJitX64)
                                {
                                    Env = { Runtime = Runtime.Clr, Gc = { Server = true, Concurrent = true } }
                                })
                        .With(
                                new Job("x86LegacyJob_ConcurrentWorkstationGC", benchmarkRunMode, EnvMode.LegacyJitX86)
                                {
                                    Env = { Runtime = Runtime.Clr, Gc = { Server = false, Concurrent = true } }
                                })
                        .With(
                                new Job("x64LegacyJob_ConcurrentWorkstationGC", benchmarkRunMode, EnvMode.LegacyJitX64)
                                {
                                    Env = { Runtime = Runtime.Clr, Gc = { Server = false, Concurrent = true } }
                                })
                        .With(
                                new Job("RyuJitJob_ConcurrentWorkstationGC", benchmarkRunMode, EnvMode.RyuJitX64)
                                {
                                    Env = { Runtime = Runtime.Clr, Gc = { Server = false, Concurrent = true } }
                                })
                        .With(
                                new Job("x86LegacyJob_SingleWorkstationGC", benchmarkRunMode, EnvMode.LegacyJitX86)
                                {
                                    Env = { Runtime = Runtime.Clr, Gc = { Server = false, Concurrent = false } }
                                })
                        .With(
                                new Job("x64LegacyJob_SingleWorkstationGC", benchmarkRunMode, EnvMode.LegacyJitX64)
                                {
                                    Env = { Runtime = Runtime.Clr, Gc = { Server = false, Concurrent = false } }
                                })
                        .With(
                                new Job("RyuJitJob_SingleWorkstationGC", benchmarkRunMode, EnvMode.RyuJitX64)
                                {
                                    Env = { Runtime = Runtime.Clr, Gc = { Server = false, Concurrent = false } }
                                })
                        .With(ExecutionValidator.FailOnError));
        }

        private static void TestSanitizationResults()
        {
            var results = new List<KeyValuePair<string, string>>();
            var sanitizer = new XmlStringSanitizer();

            var testMethods = new XmlStringSanitizer().GetType()
                .GetMethods()
                .Where(m => m.Name.StartsWith("Sanitize"));

            // Compute the results of all sanitization methods
            foreach (MethodInfo methodInfo in testMethods)
            {
                results.Add(new KeyValuePair<string, string>(methodInfo.Name, methodInfo.Invoke(sanitizer, new object[] { }).ToString()));
            }

            // Check that all results are consistent, using the char range sanitization as a baseline
            KeyValuePair<string, string> baselineResult = results.First(r => r.Key.Equals("SanitizeWithCharRange"));

            foreach (KeyValuePair<string, string> result in results)
            {
                Debug.Assert(baselineResult.Value.Equals(result.Value), $"Consistency mismatch between {baselineResult.Key} and {result.Key}");
            }

            Console.WriteLine("Test finished");
        }
    }
}
