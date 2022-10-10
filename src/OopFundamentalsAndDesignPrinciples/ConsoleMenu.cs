using OopFundamentalsAndDesignPrinciples.Extensions;
using OopFundamentalsAndDesignPrinciples.Models;
using OopFundamentalsAndDesignPrinciples.Services;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            try
            {
                var document = _documentService.GetDocumentById(selectedDocument);
                ShowDocumentContent(document);
            }
            catch (Exception ex)
            {
                AnsiConsole.Write(ex.Message);
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
