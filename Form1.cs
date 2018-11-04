using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public String sourceDir;
        public String outputFile;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void SelectFolder_Click(object sender, EventArgs e)
        {
            using(var folderBrowserDialog = new FolderBrowserDialog())
            {
                if(folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    sourceDir = folderBrowserDialog.SelectedPath;
                }
            }
        }

        private void SelectFile_Click(object sender, EventArgs e)
        {
            using (var saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "JSON File|*.json";
                saveFileDialog.Title = "Select file";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    outputFile = saveFileDialog.FileName;
                }
            }
        }

        private void Generate_Click(object sender, EventArgs e)
        {
            MessageBox.Show(sourceDir);
            GenerateJson(outputFile, sourceDir);
        }
        private void GenerateJson(String output, String input)
        {
            dynamic GeneratedArr = new JObject();
            GeneratedArr.images = new JArray();
            try
            {
                string[] files = System.IO.Directory.GetFiles(input, "*.png");
                foreach (string element in files)
                {
                    Bitmap image = new Bitmap(element);
                    bool isOpaque = false;
                    for (int x = 1; x <= image.Width; x++)
                    {
                        for (int y = 1; y <= image.Height; y++)
                        {
                            if (image.GetPixel(x - 1, y - 1).A <= 254)
                            {
                                isOpaque = true;
                            }
                        }
                    }
                    if (isOpaque == true)
                    {
                        GeneratedArr.images.Add(element.ToString().Substring(sourceDir.Length + 1));
                    }

                }
                try
                {
                    System.IO.File.WriteAllText(outputFile, GeneratedArr.ToString());
                    MessageBox.Show("Done!");
                }
                catch (System.ArgumentNullException)
                {
                    MessageBox.Show("Output file not selected!");
                }
            } catch(System.ArgumentNullException)
            {
                MessageBox.Show("Input folder not selected!");
            }
        }

        private void openFileDialog2_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void saveFileDialog2_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void folderBrowserDialog2_HelpRequest(object sender, EventArgs e)
        {

        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }
    }
}
