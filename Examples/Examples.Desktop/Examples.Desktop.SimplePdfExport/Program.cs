using JToolbox.Desktop.Dialogs;
using MigraDoc.DocumentObjectModel;
using System;

namespace Examples.Desktop.SimplePdfExport
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Press any key to create simple PDF document");
                Console.ReadKey();

                var filter = new FilterPair
                {
                    Extensions = "pdf",
                    Title = "PDF file"
                };

                var dialogs = new DialogsService();
                var filePath = dialogs.SaveFile("Save PDF file", null, "sample.pdf", filter);
                if (!string.IsNullOrEmpty(filePath))
                {
                    var documentInfo = new DocumentInfo
                    {
                        Author = "JTJT",
                        Title = "SimpleDocument",
                    };
                    var simpleDocument = new SimpleDocument(documentInfo, 1, 2);
                    simpleDocument.Fill();
                    simpleDocument.Save(filePath);

                    Console.WriteLine("Document created successfully");
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
            Console.ReadKey();
        }
    }
}