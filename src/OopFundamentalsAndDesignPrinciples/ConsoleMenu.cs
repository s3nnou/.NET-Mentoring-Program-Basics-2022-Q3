using OopFundamentalsAndDesignPrinciples.Extensions;
using OopFundamentalsAndDesignPrinciples.Models;
using OopFundamentalsAndDesignPrinciples.Services;
using Spectre.Console;

namespace OopFundamentalsAndDesignPrinciples
{
    public class ConsoleMenu : IMenu
    {
        private readonly IDocumentService _documentService;

        public ConsoleMenu(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        public void Run()
        {
            while (true)
            {
                var rule = new Rule("[red]Library Application Menu[/]");
                AnsiConsole.Write(rule);

                var choise = AnsiConsole.Prompt( new SelectionPrompt<string>()
                    .AddChoices("Show documents","Quit"));

                switch (choise)
                {
                    case "Show documents":
                        {
                            ShowDocuments();
                            break;
                        }
                    case "Quit":
                        {
                            Environment.Exit(0);
                            break;
                        }
                    default:
                        break;
                }
            }
        }

        private void ShowDocuments()
        {
            var files = _documentService.GetAllDocumentFileNames();
            var choises = new List<int>();
            var table = new Table().BuildFilesTable(files, ref choises);
            AnsiConsole.WriteLine("Available documets:");

            AnsiConsole.Write(table);

            var selectedDocument = AnsiConsole.Prompt(
                new TextPrompt<int>("Type document number to display contents: ")
                    .Validate(choise =>
                    {
                        return choises.Contains(choise) ? ValidationResult.Success() : ValidationResult.Error("[red]Wrong document number.[/]");
                    }));

            var document = _documentService.GetDocumentById(selectedDocument);
            if (document != null)
            {
                ShowDocumentContent(document);
            }
            else
            {
                AnsiConsole.Write("Please try again.");
            }

            AnsiConsole.WriteLine("Press any button to return...");
            Console.ReadKey();
            AnsiConsole.Clear();
        }

        private void ShowDocumentContent(Document document)
        {
            var table = new Table().BuildDocumentTable(document);
            AnsiConsole.Write(table);
        }
    }
}
