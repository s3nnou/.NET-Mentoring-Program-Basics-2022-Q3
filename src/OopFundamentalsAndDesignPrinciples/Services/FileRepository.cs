using Newtonsoft.Json;
using OopFundamentalsAndDesignPrinciples.Models;

namespace OopFundamentalsAndDesignPrinciples.Services
{
    public class FileRepository : IFileRepository
    {
        private const string FolderPath = @"..\..\..\Files\";

        public Document FindById(int id)
        {
            var partialSearchName = $"_{id}";
            var hdDirectoryInWhichToSearch = new DirectoryInfo(FolderPath);
            var filesInDir = hdDirectoryInWhichToSearch.GetFiles("*" + partialSearchName + ".json");
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
            var files = di.GetFiles("*.json");
            foreach (var file in files)
            {
                names.Add(Path.GetFileNameWithoutExtension(file.Name));
            }

            return names;
        }
    }
}
