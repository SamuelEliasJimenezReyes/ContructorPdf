using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
QuestPDF.Settings.License = LicenseType.Community;



Console.WriteLine("Generando el PDF...");

var issuanceOrder = new IssuanceOrder()
{
    PolicyNumber = "w6rqivcfja2qw02",
    ClientName = "Eliott Reyes",
    ClientEmail = "Gordon.Luettgen@gmail.com",
    Insurer = "Humano",
    Department = "Salud Internacional",
    SubDepartment = "Raynor, Haag and Deckow",
    LineOfBusiness = "Incendio y Lineas Aliadas"
};

string cleanName = CleanName(issuanceOrder.ClientName);
var directoryPath = Path.Combine(@"C:\Users\samue\Desktop\Code\pdf\Pdf", cleanName);

Directory.CreateDirectory(directoryPath);

var pdfFilePath = Path.Combine(directoryPath, "documento.pdf");

GeneratePdf(pdfFilePath, issuanceOrder);

Console.WriteLine($"PDF generado exitosamente en: {pdfFilePath}");



static void GeneratePdf(string filePath, IssuanceOrder obj)
{
    Document.Create(container =>
    {
        container.Page(page =>
        {
            page.Size(PageSizes.A4);
            page.Margin(20);
            page.DefaultTextStyle(x => x.FontSize(12));

            page.Content().Column(column =>
            {
                column.Item().AlignCenter().Text("Resumen de registro de Orden de emision").FontSize(16).Bold().Underline();
                column.Item().Height(10);
                column.Item().Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.RelativeColumn(1);
                        columns.RelativeColumn(2);
                    });

                    table.Cell().Text("Numero de orden").Bold();
                    table.Cell().Text("A1B2C3D4");
                    
                    
                    table.Cell().Text("Fecha de respuesta").Bold();
                    table.Cell().Text("22/01/2025");
                });
                
                column.Item().AlignCenter().Text("Datos Generales").FontSize(16).Bold().Underline();

                column.Item().Height(10);

                column.Item().Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        //key
                        columns.RelativeColumn(1);
                        //value
                        columns.RelativeColumn(2);
                    });

                    table.Cell().Text("Policy Number").Bold();
                    table.Cell().Text(obj.PolicyNumber);

                    table.Cell().Text("Client Name").Bold();
                    table.Cell().Text(obj.ClientName);

                    table.Cell().Text("Client Email").Bold();
                    table.Cell().Text(obj.ClientEmail);

                    table.Cell().Text("Insurer").Bold();
                    table.Cell().Text(obj.Insurer);

                    table.Cell().Text("Department").Bold();
                    table.Cell().Text(obj.Department);

                    table.Cell().Text("SubDepartment").Bold();
                    table.Cell().Text(obj.SubDepartment);

                    table.Cell().Text("Line of Business").Bold();
                    table.Cell().Text(obj.LineOfBusiness);
                });
                column.Item().AlignCenter().Text("Vigencias de poliza y Movimientos").FontSize(16).Bold().Underline();
                column.Item().Height(10);
                column.Item().Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.RelativeColumn(1);
                        columns.RelativeColumn(2);
                    });

                    table.Cell().Text("Inicia Poliza").Bold();
                    table.Cell().Text("01/01/2025");
                    
                    
                    table.Cell().Text("Final Poliza").Bold();
                    table.Cell().Text("03/02/2025");
                    
                    table.Cell().Text("Inicial Mov").Bold();
                    table.Cell().Text("21/01/2025");
                    
                    
                    table.Cell().Text("Final Mov").Bold();
                    table.Cell().Text("03/2/2025");
                });
                
                
                column.Item().AlignCenter().Text("Observaciones").FontSize(16).Bold().Underline();
                
            });
        });
    }).GeneratePdf(filePath);
}

static string CleanName(string fileName)
 {
     foreach (char notValidChar in Path.GetInvalidFileNameChars())
     {
         fileName = fileName.Replace(notValidChar, '-');
     }
     return fileName;
 }


internal class IssuanceOrder
 {
     public string PolicyNumber { get; set; }
     public string ClientName { get; set; }
     public string ClientEmail { get; set; }
     public string Insurer { get; set; }
     public string Department { get; set; }
     public string SubDepartment { get; set; }
     public string LineOfBusiness { get; set; }
 }