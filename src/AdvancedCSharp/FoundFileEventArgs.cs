using System;

namespace AdvancedCSharp
{
    public class FoundFileEventArgs : FileSystemVisitorBaseEventArgs
    {
        public bool AbortToggle { get; set; }

        public bool ExcludeToggle { get; set; }

        public bool AbortSearch { get; set; }

        public bool Exclude { get; set; }

        public int FilesFoundCounter { get; set; }

        public int LimitCounter { get; set; }
    }
}
