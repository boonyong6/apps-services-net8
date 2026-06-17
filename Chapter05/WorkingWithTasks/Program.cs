using System.Diagnostics; // To use Stopwatch.

OutputThreadInfo();

Stopwatch timer = Stopwatch.StartNew();

#region Running multiple actions synchronously
//SectionTitle("Running methods synchronously on one thread.");
//MethodA();
//MethodB();
//MethodC();
#endregion

#region Starting tasks
//SectionTitle("Running methods asynchronously on multiple threads.");
//Task taskA = new(MethodA);
//taskA.Start();
//Task taskB = Task.Factory.StartNew(MethodB);
//Task taskC = Task.Run(MethodC);
#endregion

#region Using wait methods with tasks
//Task[] tasks = [taskA, taskB, taskC];
//Task.WaitAll(tasks);
#endregion

#region Continuing with another task
//SectionTitle("Passing the result of one task as an input into another.");
//// You might see two different threads running, or the same thread might be reused.
//Task<string> taskServiceThenSProc = Task.Factory
//    .StartNew(CallWebService) // returns Task<decimal>
//    .ContinueWith(previousTask => // returns Task<string>
//        CallStoredProcedure(previousTask.Result));
//WriteLine($"Result: {taskServiceThenSProc.Result}");
#endregion

#region Nested and child tasks
SectionTitle("Nested and child tasks");
Task outerTask = Task.Factory.StartNew(OuterMethod);
outerTask.Wait();
WriteLine("Console app is stopping.");
#endregion

WriteLine($"{timer.ElapsedMilliseconds:#,##0}ms elapsed.");
