using static System.Console;

WriteLine("Top-level functions example");
WhatsMyNamespace(); // Call the function.

void WhatsMyNamespace() // Define a local function.
{
    WriteLine("Namespace of Program class: {0}", 
        arg0: typeof(Program).Namespace ?? "null");
}
