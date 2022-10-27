using System.Collections.ObjectModel;
using System.Windows;

namespace AdvancedCSharp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string FolderPath = @"..\..\..\folder\";

        public FileSystemVisitor Visitor { get; set; }

        public ObservableCollection<Node> Folders { get; set; } = new ObservableCollection<Node>();

        public ObservableCollection<Node> SearchResult { get; set; } = new ObservableCollection<Node>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((bool)this.SearchByFileNameCheckbox.IsChecked && this.FileExtentionSearch.Text.Length > 0)
                {
                    Visitor = FileSystemVisitorFactory.GetFileSystemVisitor(FolderPath, this.FileExtentionSearch.Text);
                    Visitor.Started += Visitor_Started;
                    Visitor.Finished += Visitor_Finished;
                    Visitor.DirectoryFound += Visitor_DirectoryFound;
                    Visitor.FilteredFileFound += Visitor_FilteredFileFound;
                    Visitor.FileFound += Visitor_FileFound;
                    Visitor.FilteredDirectoryFound += Visitor_FilteredDirectoryFound;

                    var node = Visitor.GetNode();
                    Folders = new ObservableCollection<Node>() { new Node(node) };
                    Files.ItemsSource = new ObservableCollection<Node>(Folders);

                    if ((bool)this.TookOnly.IsChecked)
                    {
                        var value = (int)this.CounterSlider.Value;
                        var exlude = (bool)this.ExcludeCheckBox.IsChecked;
                        var nodes = Visitor.FilterNode(node, false, exlude, value);
                        SearchResult = new ObservableCollection<Node>(nodes);

                        FileSearchResult.ItemsSource = SearchResult;

                        return;
                    }

                    SearchResult = new ObservableCollection<Node>(Visitor.FilterNode(node, (bool)this.AbortCheckBox.IsChecked, (bool)this.ExcludeCheckBox.IsChecked));
                    FileSearchResult.ItemsSource = SearchResult;
                }
                else
                {
                    Visitor = FileSystemVisitorFactory.GetFileSystemVisitor(FolderPath);
                    Visitor.Started += Visitor_Started;
                    Visitor.Finished += Visitor_Finished;
                    Visitor.DirectoryFound += Visitor_DirectoryFound;
                    Visitor.FilteredFileFound += Visitor_FilteredFileFound;
                    Visitor.FileFound += Visitor_FileFound;
                    Visitor.FilteredDirectoryFound += Visitor_FilteredDirectoryFound;

                    var node = Visitor.GetNode();
                    Folders = new ObservableCollection<Node>() { node };
                    Files.ItemsSource = new ObservableCollection<Node>(Folders);
                }

            }
            catch (FileSystemVisitorException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Visitor_FilteredDirectoryFound(object? sender, FoundFileEventArgs e)
        {
            this.LogSearch.Text += e.Message;
        }

        private void Visitor_FileFound(object? sender, FileEventArgs e)
        {
            this.LogSearch.Text += e.Message;
        }

        private void Visitor_FilteredFileFound(object? sender, FoundFileEventArgs e)
        {
            this.LogSearch.Text += e.Message;
        }

        private void Visitor_FolderFound1(object? sender, FileEventArgs e)
        {
            this.LogSearch.Text += e.Message;
        }

        private void Visitor_DirectoryFound(object? sender, FileEventArgs e)
        {
            this.LogSearch.Text += e.Message;
        }

        private void Visitor_Finished(object? sender, FileSystemVisitorBaseEventArgs e)
        {
            this.LogSearch.Text += e.Message;
        }

        private void Visitor_Started(object? sender, FoundFileEventArgs e)
        {
            this.LogSearch.Text += e.Message;
        }

        private void SearchByFileNameCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            this.FileExtentionSearch.Visibility = Visibility.Visible;
            this.AbortCheckBox.Visibility = Visibility.Visible;
            this.ExcludeCheckBox.Visibility = Visibility.Visible;
            this.TookOnly.Visibility = Visibility.Visible;
            this.CounterSlider.Visibility = Visibility.Visible;
        }

        private void SearchByFileNameCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            this.FileExtentionSearch.Visibility = Visibility.Hidden;
            this.AbortCheckBox.Visibility = Visibility.Hidden;
            this.ExcludeCheckBox.Visibility = Visibility.Hidden;
            this.CounterSlider.Visibility = Visibility.Hidden;
            this.TookOnly.Visibility = Visibility.Hidden;
        }

        private void TookOnly_Checked(object sender, RoutedEventArgs e)
        {
            this.AbortCheckBox.Visibility = Visibility.Hidden;
            this.ExcludeCheckBox.Visibility = Visibility.Visible;
            this.CounterSlider.Visibility = Visibility.Visible;
        }

        private void TookOnly_Unchecked(object sender, RoutedEventArgs e)
        {
            this.AbortCheckBox.Visibility = Visibility.Visible;
            this.ExcludeCheckBox.Visibility = Visibility.Visible;
            this.CounterSlider.Visibility = Visibility.Visible;
        }
    }
}
