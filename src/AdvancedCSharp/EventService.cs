using System.Text;

namespace AdvancedCSharp
{
    public class EventService
    {
        public void OnStart(object source, FoundFileEventArgs eventArgs)
        {
            var stringBuilder = new StringBuilder($"EventService: Search started \n");

            if (eventArgs.AbortToggle)
            {
                eventArgs.LimitCounter = 1;
                stringBuilder.AppendLine($"EventService: Search will be aborted on 1st occurance");
            }

            if (eventArgs.ExcludeToggle)
            {
                eventArgs.Exclude = true;
                stringBuilder.AppendLine($"EventService: Filtered occurances will be removed from root");
            }

            eventArgs.Message = stringBuilder.ToString();
        }

        public void OnFinish(object source, FileSystemVisitorBaseEventArgs eventArgs)
        {
            eventArgs.Message = $"EventService: Search finished \n";
        }

        public void OnFileFound(object source, FileEventArgs eventArgs)
        {
            eventArgs.Message = $"EventService: found {eventArgs.Leaf.Name} file \n";
        }

        public void OnDirectoryFound(object source, FileEventArgs eventArgs)
        {
            eventArgs.Message = $"EventService: found {eventArgs.Leaf.Name} directory \n";
        }

        public void OnFilteredFileFound(object source, FoundFileEventArgs eventArgs)
        {
            if (eventArgs.LimitCounter > 0)
            {
                var stringBuilder = new StringBuilder($"EventService: found file by condtions. Found {eventArgs.FilesFoundCounter + 1} out of {eventArgs.LimitCounter}");
                if (eventArgs.FilesFoundCounter == eventArgs.LimitCounter)
                {
                    stringBuilder.AppendLine($"EventService: Search will be aborted\n");
                    eventArgs.AbortSearch = true;
                }

                if (eventArgs.FilesFoundCounter < eventArgs.LimitCounter)
                {
                    eventArgs.FilesFoundCounter++;
                }

                eventArgs.Message = stringBuilder.ToString();
            }
            else
            {
                eventArgs.Message = $"EventService: found file by condtions";
            }
        }

        public void OnFilteredDirectoryFound(object source, FoundFileEventArgs eventArgs)
        {
            if(eventArgs.LimitCounter > 0)
            {
                var stringBuilder = new StringBuilder($"EventService: found folder by condtions. Found {eventArgs.FilesFoundCounter + 1} out of {eventArgs.LimitCounter}");
                
                if (eventArgs.FilesFoundCounter == eventArgs.LimitCounter)
                {
                    stringBuilder.AppendLine($"EventService: Search will be aborted\n");
                    eventArgs.AbortSearch = true;
                }

                if (eventArgs.FilesFoundCounter < eventArgs.LimitCounter)
                {
                    eventArgs.FilesFoundCounter++;
                }

                eventArgs.Message = stringBuilder.ToString();
            }
            else
            {
                eventArgs.Message = $"EventService: found directory by condtions";
            }
        }
    }
}
