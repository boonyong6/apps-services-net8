using System.Diagnostics; // To use Stopwatch.

WriteLine("Please wait for the tasks to complete.");
Stopwatch watch = Stopwatch.StartNew();

Task a = Task.Factory.StartNew(MethodA);
Task b = Task.Factory.StartNew(MethodB);
Task.WaitAll([a, b]); // Both threads modify the message concurrently.

WriteLine();
WriteLine($"Results: {SharedObjects.Message}.");

WriteLine($"{watch.ElapsedMilliseconds:N0} elapsed milliseconds.");
