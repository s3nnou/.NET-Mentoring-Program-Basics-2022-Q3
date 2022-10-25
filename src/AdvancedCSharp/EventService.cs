using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace AdvancedCSharp
{
    public class EventService
    {
        private TextBlock _textBlock;

        public EventService()
        {
            var mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            if (mainWindow != null)
            {
                _textBlock = mainWindow.LogSearch;
            }
            else
            {
                _textBlock = new TextBlock();
            }
        }

        public void OnStart(object source, FoundFileEventArgs eventArgs)
        {
            _textBlock.Text += $"AppService: Search started \n";

            Console.WriteLine($"AppService: Search started");

            if (eventArgs.AbortToggle)
            {
                eventArgs.AbortSearch = true;

                _textBlock.Text += $"AppService: Search will be aborted on 1st occurance \n";
                Console.WriteLine($"AppService: Search will be aborted on 1st occurance");
            }

            if (eventArgs.ExcludeToggle)
            {
                eventArgs.Exclude = true;

                _textBlock.Text += $"AppService: Filtered occurances will be removed from root\n";
                Console.WriteLine($"AppService: Filtered occurances will be removed from root");
            }
        }

        public void OnFinish(object source, EventArgs eventArgs)
        {
            _textBlock.Text += $"AppService: Search finished \n";
            Console.WriteLine($"AppService: Search finished");
        }

        public void OnFileFound(object source, FileEventArgs eventArgs)
        {
            _textBlock.Text += $"AppService: found {eventArgs.Leaf.Name} file \n";

            Console.WriteLine($"AppService: found {eventArgs.Leaf.Name} file");
        }

        public void OnFolderFound(object source, FileEventArgs eventArgs)
        {
            _textBlock.Text += $"AppService: found {eventArgs.Leaf.Name} folder \n";

            Console.WriteLine($"AppService: found {eventArgs.Leaf.Name} folder");
        }

        public void OnFilteredFileFound(object source, FoundFileEventArgs eventArgs)
        {
            _textBlock.Text += $"AppService: found file by condtions. Found {eventArgs.FilesFoundCounter} out of {eventArgs.LimitCounter} \n";

            Console.WriteLine($"AppService: found file by condtions. Found {eventArgs.FilesFoundCounter} out of {eventArgs.LimitCounter}");

            if (eventArgs.FilesFoundCounter < eventArgs.LimitCounter - 1)
            {
                eventArgs.FilesFoundCounter++;
            }
            else if (eventArgs.FilesFoundCounter >= eventArgs.LimitCounter - 1)
            {
                _textBlock.Text += $"AppService: Search will be aborted\n";
                Console.WriteLine($"AppService: Search will be aborted");

                eventArgs.AbortSearch = true;
            }
        }

        public void OnFilteredFolderFound(object source, FoundFileEventArgs eventArgs)
        {
            _textBlock.Text += $"AppService: found file by condtions. Found {eventArgs.FilesFoundCounter} out of {eventArgs.LimitCounter} \n";
            Console.WriteLine($"AppService: found file by condtions. Found {eventArgs.FilesFoundCounter} out of {eventArgs.LimitCounter}");

            if (eventArgs.FilesFoundCounter < eventArgs.LimitCounter - 1)
            {
                eventArgs.FilesFoundCounter++;
            }
            else if (eventArgs.FilesFoundCounter >= eventArgs.LimitCounter - 1)
            {
                _textBlock.Text += $"AppService: Search will be aborted\n";
                Console.WriteLine($"AppService: Search will be aborted");

                eventArgs.AbortSearch = true;
            }
        }
    }
}
