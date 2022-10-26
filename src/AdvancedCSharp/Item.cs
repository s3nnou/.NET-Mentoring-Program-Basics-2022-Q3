namespace AdvancedCSharp
{
    public enum FileType
    {
        Folder = 0,
        File
    }

    public class Item
    {
        public string Name { get; set; }

        public FileType FileType { get; set; }
    }
}
