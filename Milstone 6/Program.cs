using System;
using System.IO;
using System.Xml.Linq;
using iTextSharp.text;
using iTextSharp.text.pdf;

class Program
{
    static void Main()
    {
        string pdfPath = "SalesReport.pdf";
        string title = "Monthly Sales Report";
        string text = "Total Sales: $10,000";
        string imagePath = @"C:\images\sales_chart.png";

        try
        {
            Document document = new Document();
            PdfWriter.GetInstance(document, new FileStream(pdfPath, FileMode.Create));
            document.Open();

            // Add title
            document.Add(new Paragraph(title, new Font(Font.FontFamily.HELVETICA, 18, Font.BOLD)));

            // Add text
            document.Add(new Paragraph(text));

            // Add image
            if (File.Exists(imagePath))
            {
                iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(imagePath);
                document.Add(image);
            }
            else
            {
                Console.WriteLine("Image not found.");
            }

            document.Close();
            Console.WriteLine("PDF created successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}

