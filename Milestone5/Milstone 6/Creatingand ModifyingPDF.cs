using System;
using System.Data;
using System.IO;
using System.Xml.Linq;
using iTextSharp.text;
using iTextSharp.text.pdf;

class Program
{
    static void Main()
    {
        string csvPath = "data.csv";
        string pdfPath = "SummaryReport.pdf";

        DataTable dt = new DataTable();
        dt.Columns.Add("Name");
        dt.Columns.Add("Amount");

        try
        {
            // Read CSV file
            foreach (var line in File.ReadLines(csvPath))
            {
                var values = line.Split(',');
                dt.Rows.Add(values[0], values[1]);
            }

            // Create PDF
            Document document = new Document();
            PdfWriter.GetInstance(document, new FileStream(pdfPath, FileMode.Create));
            document.Open();

            // Create table
            PdfPTable table = new PdfPTable(dt.Columns.Count);
            foreach (DataColumn column in dt.Columns)
            {
                table.AddCell(new Phrase(column.ColumnName));
            }

            foreach (DataRow row in dt.Rows)
            {
                foreach (var cell in row.ItemArray)
                {
                    table.AddCell(new Phrase(cell.ToString()));
                }
            }

            document.Add(table);
            document.Close();
            Console.WriteLine("PDF created successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
