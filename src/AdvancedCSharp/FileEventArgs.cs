namespace AdvancedCSharp
{
    public class FileEventArgs : FileSystemVisitorBaseEventArgs
    {
        public Item Leaf { get; set; }
    }
}