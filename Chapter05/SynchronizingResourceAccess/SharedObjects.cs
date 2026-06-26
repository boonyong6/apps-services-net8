public static class SharedObjects
{
    public static string? Message; // A shared resource
    public static object Conch = new(); // A shared object to lock.
    public static int Counter; // Another shared resource.
}