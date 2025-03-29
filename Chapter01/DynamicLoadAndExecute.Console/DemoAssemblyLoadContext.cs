using System.Runtime.Loader; // AssemblyDependencyResolver

// Define a custom assembly load context to enable unloading of assemblies.
internal class DemoAssemblyLoadContext : AssemblyLoadContext
{
    private AssemblyDependencyResolver _resolver;

    public DemoAssemblyLoadContext(string mainAssemblyToLoadPath)
        : base(isCollectible: true)
    {
        _resolver = new AssemblyDependencyResolver(mainAssemblyToLoadPath);
    }
}