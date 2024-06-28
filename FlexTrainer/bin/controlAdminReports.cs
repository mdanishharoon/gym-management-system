using iText.Kernel.Pdf;
using iText.Layout.Element;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using System.IO;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace DBproj.bin
{
    public partial class controlAdminReports : UserControl
    {
        public controlAdminReports()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            // SQL query to retrieve distinct WorkoutPlans for a specific trainer (ID = 2)
            string queryString = @"
                SELECT DISTINCT wp.*
                FROM WorkoutPlans wp
                JOIN trainers t ON wp.CreatorID = t.id
                WHERE t.id = 2;
            ";

            // Connection string to your SQL Server database
            string connectionString = "Data Source=DANISH\\SQLEXPRESS;Initial Catalog=FlexTrainer;Integrated Security=True;Encrypt=False;";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Create a SqlCommand to execute the query
                    using (SqlCommand command = new SqlCommand(queryString, connection))
                    {
                        // Open the connection
                        connection.Open();

                        // Create a DataTable to hold the query results
                        DataTable dataTable = new DataTable();

                        // Fill the DataTable with data from the query
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            dataTable.Load(reader);
                        }

                        // Bind the DataTable to the DataGridView
                        dataGridView1.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Specify the file path where the PDF will be saved
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            // Specify the file name (e.g., "dataGridViewExport.pdf")
            string fileName = "dataGridViewExport.pdf";

            // Combine the desktop path and file name to create the full file path
            string filePath = Path.Combine(desktopPath, fileName);

            // Call the ExportToPdf method to export the DataGridView to PDF
            ExportToPdf(dataGridView1, filePath);

            //// Create a new PDF document
            //PdfWriter writer = new PdfWriter("DataGridViewExport.pdf");
            //PdfDocument pdf = new PdfDocument(writer);
            //Document document = new Document(pdf);

            //// Create a table with columns matching the DataGridView
            //iText.Layout.Element.Table table = new iText.Layout.Element.Table(dataGridView1.ColumnCount);

            //// Add headers from the DataGridView to the PDF table
            //foreach (DataGridViewColumn column in dataGridView1.Columns)
            //{
            //    Cell headerCell = new Cell().Add(new Paragraph(column.HeaderText));
            //    table.AddHeaderCell(headerCell);
            //}

            //// Add data rows from the DataGridView to the PDF table
            //foreach (DataGridViewRow row in dataGridView1.Rows)
            //{
            //    foreach (DataGridViewCell cell in row.Cells)
            //    {
            //        table.AddCell(cell.Value?.ToString());
            //    }
            //}

            //// Add the table to the PDF document
            //document.Add(table);

            //// Close the document
            //document.Close();

            //MessageBox.Show("DataGridView exported to PDF successfully.", "Export to PDF", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ExportToPdf(DataGridView dataGridView, string filePath)
        {
            try
            {
                // Create a PDF document
                PdfDocument pdfDoc = new PdfDocument(new PdfWriter(filePath));
                Document doc = new Document(pdfDoc);

                // Create a table with columns based on the DataGridView
                iText.Layout.Element.Table pdfTable = new iText.Layout.Element.Table(dataGridView.Columns.Count, true);

                // Add headers from the DataGridView to the PDF table
                foreach (DataGridViewColumn column in dataGridView.Columns)
                {
                    pdfTable.AddHeaderCell(new Cell().Add(new Paragraph(column.HeaderText)));
                }

                // Add rows and cells to the PDF table
                foreach (DataGridViewRow row in dataGridView.Rows)
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        // Handle null values by checking if cell.Value is null
                        string cellValue = (cell.Value == null) ? "" : cell.Value.ToString();
                        pdfTable.AddCell(new Cell().Add(new Paragraph(cellValue)));
                    }
                }

                // Add the PDF table to the document
                doc.Add(pdfTable);

                // Close the document
                doc.Close();

                MessageBox.Show("PDF exported successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error exporting PDF: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
