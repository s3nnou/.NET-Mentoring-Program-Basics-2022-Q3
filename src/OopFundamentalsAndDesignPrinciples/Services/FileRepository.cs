using Newtonsoft.Json;
using OopFundamentalsAndDesignPrinciples.Models;

namespace OopFundamentalsAndDesignPrinciples.Services
{
    public class FileRepository : IFileRepository
    {
        private const string FolderPath = @"..\..\..\Files\";
        private const string FileFormat = "*.json";

        public Document FindById(int id)
        {
            var hdDirectoryInWhichToSearch = new DirectoryInfo(FolderPath);
            var filesInDir = hdDirectoryInWhichToSearch.GetFiles($"*_{id}.json");
            var fullName = filesInDir.First().FullName;
            var contents = File.ReadAllText(fullName);

            return JsonConvert.DeserializeObject<Document>(contents, new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Auto
            });
        }

        public List<string> GetAllDocumentsByName()
        {
            var names = new List<string>();
            var di = new DirectoryInfo(FolderPath);
            var files = di.GetFiles(FileFormat);
            foreach (var file in files)
            {
                names.Add(Path.GetFileNameWithoutExtension(file.Name));
            }

            return names;
        }
    }
}
