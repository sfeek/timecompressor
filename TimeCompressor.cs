using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Time_Compressor
{
    public partial class TimeCompressor : Form
    {
        public TimeCompressor()
        {
            InitializeComponent();
        }

        private void btn_compress_Click(object sender, EventArgs e)
        {
            int interval;
            int x, y, i;
            string line;
            int counter = 0;
            int icounter = 0;
            string[] labels;
            string[] fields;
            double[,] buffer;
            double a, s;
            string output = String.Empty;
            string outfilename = String.Empty;

            List<double> avg = new List<double>();
            List<double> std = new List<double>();

            btn_compress.Enabled = false;
            
            try
            {
                interval = Convert.ToInt32(txtCount.Text);
            }
            catch { return; }

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "csv files (*.csv)|*.csv";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    outfilename = openFileDialog.FileName + "-cmp.csv";
                    if (File.Exists(outfilename)) File.Delete(outfilename); // Make sure the old file is gone

                    var fileStream = openFileDialog.OpenFile();

                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        line = reader.ReadLine();
                        labels = ParseLine(line);
                        buffer = new double[interval, labels.Length];

                        output += "Rec#,";
                        for (x = 0; x < labels.Length; x++) // Write out our labels
                        {
                            if (x < labels.Length - 1)
                                output += labels[x] + "_avg," + labels[x] + "_std,";
                            else
                                output += labels[x] + "_avg," + labels[x] + "_std";
                        }

                        using (StreamWriter sw = File.AppendText(outfilename))
                        {
                            sw.WriteLine(output);

                            counter = 1;
                            while ((line = reader.ReadLine()) != null)
                            {
                                if (counter % interval == 0) // Calculate values and output them
                                {
                                    Application.DoEvents();
                                    output = String.Empty;
                                    output += counter + ",";

                                    for (x = 0; x < labels.Length; x++)
                                    {
                                        avg.Clear();
                                        std.Clear();

                                        for (y = 0; y < icounter; y++)
                                        {
                                            avg.Add(buffer[y, x]);
                                            std.Add(buffer[y, x]);
                                        }

                                        a = avg.Average();
                                        s = StandardDeviation(a,std);

                                        if (x < labels.Length - 1)
                                            output += a + "," + s + ",";
                                        else
                                            output += a + "," + s;
                                    }


                                    sw.WriteLine(output);
                                    lblRecordNumber.Text = "Record #" + counter;

                                    icounter = 0;
                                }

                                for (i = 0; i < labels.Length; i++) // Otherwise add to buffer
                                {
                                    fields = ParseLine(line);
                                    try
                                    {
                                        buffer[icounter, i] = Convert.ToDouble(fields[i]);
                                    }
                                    catch { buffer[icounter, i] = 0.0; }
                                }

                                counter++;
                                icounter++;
                            }

                            // Process the remaining values
                            output = String.Empty;
                            output += counter + ",";

                            for (x = 0; x < labels.Length; x++)
                            {
                                avg.Clear();
                                std.Clear();

                                for (y = 0; y < icounter; y++)
                                {
                                    avg.Add(buffer[y, x]);
                                    std.Add(buffer[y, x]);
                                }

                                a = avg.Average();
                                s = StandardDeviation(a, std);

                                if (x < labels.Length - 1)
                                    output += a + "," + s + ",";
                                else
                                    output += a + "," + s;
                            }

                            sw.WriteLine(output);
                            lblRecordNumber.Text = "Record #" + counter;
                            icounter = 0;
                        }
                    }
                }
            }
            btn_compress.Enabled = true;
        }

        // Parse an individual line of CSV
        static string[] ParseLine(string lines)
        {
            string[] fields;
            string[] lineArray;

            try
            {
                fields = Regex.Split(lines, ",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");

                lineArray = new string[fields.Length];

                int x = 0;
                foreach (string value in fields)
                {
                    string check = Regex.Replace(value, "^\"|\"$", "");
                    lineArray[x] = check;
                    x++;
                }
            }
            catch 
            {
                return null;
            }

            return lineArray;
        }

        // Calculate Standard Deviation
        static double StandardDeviation(double average,List<double> doubleList)
        {
            double sumOfDerivation = 0;

            foreach (double value in doubleList)
            {
                sumOfDerivation += Math.Pow(value - average, 2);
            }

            return Math.Sqrt(sumOfDerivation / (doubleList.Count - 1));
        }
    }
}
