partial class Program
{
    private static void MethodA()
    {
        for (int i = 0; i < 5; i++)
        {
            // Simulate two seconds of work on the current thread.
            Thread.Sleep(Random.Shared.Next(2000));
            // Concatenate the letter "A" to the shared message.
            SharedObjects.Message += "A";
            // Show some activity in the console output.
            Write(".");
        }
    }

    private static void MethodB()
    {
        for (int i = 0; i < 5; i++)
        {
            Thread.Sleep(Random.Shared.Next(2000));
            SharedObjects.Message += "B";
            Write(".");
        }
    }
}