using System.Reflection;

namespace CurrencyObserver.Common.Managers;

public interface IEmbeddedResourcesManager
{
    IEnumerable<string> GetEmbeddedFiles(
        Assembly embeddedFilesAssembly,
        string? embeddedFilesPath);

    string ReadEmbeddedFile(
        Assembly assembly,
        string embeddedFile);
}