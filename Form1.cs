using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;


namespace PDFMerge
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "PDF Files|*.pdf";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.Multiselect = true;
            openFileDialog1.FileName = String.Empty;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                string outputPath = Path.GetDirectoryName(openFileDialog1.FileNames[0]);
                string outputName = "Merged_" + Path.GetFileName(openFileDialog1.FileNames[0]);

                //foreach (String filePath in openFileDialog1.FileNames)
                {
                    MergePDFs(outputPath + "\\" + outputName, openFileDialog1.FileNames);
                }
            }
        }


        public void MergePDFs(string targetPath, params string[] pdfs)
        {
            using (PdfDocument targetDoc = new PdfDocument())
            {
                foreach (string pdf in pdfs)
                {
                    using (PdfDocument pdfDoc = PdfReader.Open(pdf, PdfDocumentOpenMode.Import))
                    {
                        for (int i = 0; i < pdfDoc.PageCount; i++)
                        {
                            targetDoc.AddPage(pdfDoc.Pages[i]);
                        }
                    }
                }
                targetDoc.Save(targetPath);
                Process.Start(Path.GetDirectoryName(targetPath));

                if (checkBox1.Checked)
                    Process.Start(targetPath);

                Application.Exit();
            }
        }

    }
}
