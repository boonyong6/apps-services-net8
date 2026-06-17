using System.Diagnostics; // To use Stopwatch.

OutputThreadInfo();

Stopwatch timer = Stopwatch.StartNew();
SectionTitle("Running methods synchronously on one thread.");
MethodA();
MethodB();
MethodC();
WriteLine($"{timer.ElapsedMilliseconds:#,##0}ms elapsed.");
