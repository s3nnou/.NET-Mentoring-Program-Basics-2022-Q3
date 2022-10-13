using OopFundamentalsAndDesignPrinciples.Models;
using Spectre.Console;

namespace OopFundamentalsAndDesignPrinciples.Extensions
{
    public static class TableExtensions
    {
        public static Table BuildDocumentTable(this Table table, Document document)
        {
            switch (document.Item.DocumentType)
            {
                case DocumentItemType.Patent:
                    {
                        table.AddColumn(new TableColumn("Id").Centered())
                           .AddColumn(new TableColumn("Title").Centered())
                           .AddColumn(new TableColumn("Authors").Centered())
                           .AddColumn(new TableColumn("Expiration Date").Centered())
                           .AddColumn(new TableColumn("Publish Date").Centered());
                        var patent = document.GetItem<Patent>();
                        table.AddRow(patent.Id, patent.Title, patent.Authors, patent.ExpirationDate.ToShortDateString(), patent.PublishDate.ToShortDateString());

                        break;
                    }
                case DocumentItemType.Book:
                    {
                        table.AddColumn(new TableColumn("ISBN").Centered())
                           .AddColumn(new TableColumn("Title").Centered())
                           .AddColumn(new TableColumn("Authors").Centered())
                           .AddColumn(new TableColumn("Publisher").Centered())
                           .AddColumn(new TableColumn("NumberOfPages").Centered())
                           .AddColumn(new TableColumn("Publish Date").Centered());
                        var book = document.GetItem<Book>();
                        table.AddRow(book.ISBN, book.Title, book.Authors, book.Publisher, book.NumberOfPages.ToString(), book.PublishDate.ToShortDateString());

                        break;
                    }
                case DocumentItemType.LocalaziedBook:
                    {
                        table.AddColumn(new TableColumn("ISBN").Centered())
                           .AddColumn(new TableColumn("Title").Centered())
                           .AddColumn(new TableColumn("Authors").Centered())
                           .AddColumn(new TableColumn("Original Publisher").Centered())
                           .AddColumn(new TableColumn("Local Publisher").Centered())
                           .AddColumn(new TableColumn("County of Localization").Centered())
                           .AddColumn(new TableColumn("Number Of Pages").Centered())
                           .AddColumn(new TableColumn("Publish Date").Centered());
                        var book = document.GetItem<LocalaziedBook>();
                        table.AddRow(book.ISBN, book.Title, book.Authors, book.OriginalPublisher, book.LocalPublisher,
                            book.CountryOfLocalization, book.NumberOfPages.ToString(), book.PublishDate.ToShortDateString());

                        break;
                    }
                case DocumentItemType.Magazine:
                    {
                        table.AddColumn(new TableColumn("Title").Centered())
                           .AddColumn(new TableColumn("Publisher").Centered())
                           .AddColumn(new TableColumn("Release Number").Centered())
                           .AddColumn(new TableColumn("Pulish Date").Centered());

                        var magazine = document.GetItem<Magazine>();
                        table.AddRow(magazine.Title, magazine.Publisher, magazine.ReleaseNumber.ToString(), magazine.PublishDate.ToShortDateString());
                        break;
                    }
                default:
                    {
                        table.AddColumn(new TableColumn("Error").Centered());
                        table.AddRow("Unknown document type.");
                        break;
                    }
            }

            return table;
        }

        public static Table BuildFilesTable(this Table table, List<string> fileNames, ref List<int> choises)
        {     
            table.AddColumn(new TableColumn("Number").Centered())
            .AddColumn(new TableColumn("Type").Centered())
            .AddColumn(new TableColumn("Document Name").Centered());

            foreach (var file in fileNames)
            {
                var fileTypeAndNumber = file.Split('_');
                choises.Add(int.Parse(fileTypeAndNumber.Last()));
                table.AddRow(fileTypeAndNumber.Last(), fileTypeAndNumber.First(), file);
            }

            return table;
        }
    }
}
