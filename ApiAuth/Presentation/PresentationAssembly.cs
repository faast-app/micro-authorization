using System.Reflection;
using System.Reflection.Metadata;

namespace ApiAuth.Presentation;

public static class PresentationAssembly
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}