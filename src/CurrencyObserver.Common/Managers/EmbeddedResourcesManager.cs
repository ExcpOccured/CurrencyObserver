using System.Reflection;

namespace CurrencyObserver.Common.Managers;

public class EmbeddedResourcesManager : IEmbeddedResourcesManager
{
    public IEnumerable<string> GetEmbeddedFiles(
        Assembly embeddedFilesAssembly,
        string? embeddedFilesPath) =>
        embeddedFilesAssembly
            .GetManifestResourceNames()
            .Where(str => str.StartsWith($"{embeddedFilesAssembly.GetName().Name}.{embeddedFilesPath}"))
            .OrderBy(str => str);


    public string ReadEmbeddedFile(
        Assembly assembly,
        string embeddedFile)
    {
        using var stream = assembly.GetManifestResourceStream(embeddedFile);

        if (stream is null)
        {
            throw new InvalidOperationException($"Embedded file {embeddedFile} not found");
        }

        using var reader = new StreamReader(stream);

        return reader.ReadToEnd();
    }
}