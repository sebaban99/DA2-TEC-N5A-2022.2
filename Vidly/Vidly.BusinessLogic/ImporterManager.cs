using System.Reflection;
using Vidly.Domain.Entities;
using Vidly.Exceptions;
using Vidly.IBusinessLogic;
using Vidly.ImporterInterface;

namespace Vidly.BusinessLogic;

public class ImporterManager : IImporterManager
{
    // Aca devuelve strings solamente, pero los importadores pueden tambien tener que recibir
    // algun tipo de parametros, por lo que quizas estaría bueno armar una estructura que represente el importador
    // que especifique los parametros necesarios y sus tipos
    public List<string> GetAllImporters()
    {
        return GetImporterImplementations().Select(importer => importer.GetName()).ToList();
    }

    public List<Movie> ImportMovies(string importerName)
    {
        List<IImporter> importers = GetImporterImplementations();
        IImporter? desiredImplementation = null;

        foreach (IImporter importer in importers)
        {
            if (importer.GetName() == importerName)
            {
                desiredImplementation = importer;
                break;
            }
        }

        if (desiredImplementation == null)
            throw new ResourceNotFoundException("No se pudo encontrar el importador solicitado");

        List<Movie> importedMovies = desiredImplementation.ImportMovies();
        return importedMovies;
    }

    private List<IImporter> GetImporterImplementations()
    {
        List<IImporter> availableImporters = new List<IImporter>();
        // Va a estar adentro de WebApi, ya que mira relativo de donde se ejecuta el programa
        string importersPath = "./Importers";
        string[] filePaths = Directory.GetFiles(importersPath);

        foreach (string filePath in filePaths)
        {
            if (filePath.EndsWith(".dll"))
            {
                FileInfo fileInfo = new FileInfo(filePath);
                Assembly assembly = Assembly.LoadFile(fileInfo.FullName);

                foreach (Type type in assembly.GetTypes())
                {
                    if (typeof(IImporter).IsAssignableFrom(type) && !type.IsInterface)
                    {
                        IImporter importer = (IImporter)Activator.CreateInstance(type);
                        if (importer != null)
                            availableImporters.Add(importer);
                    }
                }
            }
        }

        return availableImporters;
    }
}