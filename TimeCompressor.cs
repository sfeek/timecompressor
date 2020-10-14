using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace Time_Compressor
{
    public partial class TimeCompressor : Form
    {
        class AVGSTD
        {
            public double avg;
            public double std;
        };

        double[,] buffer;

        public TimeCompressor()
        {
            InitializeComponent();
        }

        private void btn_compress_Click(object sender, EventArgs e)
        {
            int interval;
            int i;
            string line;
            int counter;
            int icounter = 0;
            string[] labels;
            string[] fields;
            

            string output = String.Empty;
            string outfilename = String.Empty;

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
                        labels = line.Split(',');
                        buffer = new double[interval, labels.Length];

                        output += "Rec#,";
                        for (int x = 0; x < labels.Length; x++) // Write out our labels
                        {
                            if (x < labels.Length - 1)
                                output += labels[x] + "_avg," + labels[x] + "_std,";
                            else
                                output += labels[x] + "_avg," + labels[x] + "_std";
                        }

                        Task<AVGSTD>[] t = new Task<AVGSTD>[labels.Length];

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

                                    for (int x = 0; x < labels.Length; x++)
                                    {
                                        int p = x;
                                        int ic = icounter;
                                        t[x] = Task.Run(() => OneColumn(p, ic));
                                    }

                                    Task.WaitAll(t);

                                    for (int x = 0; x < labels.Length; x++)
                                    {
                                        if (x < labels.Length - 1)
                                            output += t[x].Result.avg + "," + t[x].Result.std + ",";
                                        else
                                            output += t[x].Result.avg + "," + t[x].Result.std;
                                    }

                                    sw.WriteLine(output);
                                    lblRecordNumber.Text = "Record #" + counter;

                                    icounter = 0;
                                }

                                for (i = 0; i < labels.Length; i++) // Otherwise add to buffer
                                {
                                    fields = line.Split(',');
                                    try
                                    {
                                        buffer[icounter, i] = Convert.ToDouble(fields[i]);
                                    }
                                    catch { buffer[icounter, i] = 0.0; }
                                }

                                counter++;
                                icounter++;
                            }

                            output = String.Empty;
                            output += counter + ",";

                            // Finished the leftover lines
                            for (int x = 0; x < labels.Length; x++)
                            {
                                int p = x;
                                int ic = icounter;
                                Task.Run(() => OneColumn(p, ic));
                            }

                            Task.WaitAll(t);

                            for (int x = 0; x < labels.Length; x++)
                            {
                                if (x < labels.Length - 1)
                                    output += t[x].Result.avg + "," + t[x].Result.std + ",";
                                else
                                    output += t[x].Result.avg + "," + t[x].Result.std;
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

        // Calculate Standard Deviation
        double StandardDeviation(double average,List<double> doubleList)
        {
            double sumOfDerivation = 0;

            foreach (double value in doubleList)
            {
                sumOfDerivation += Math.Pow(value - average, 2);
            }

            return Math.Sqrt(sumOfDerivation / (doubleList.Count - 1));
        }

        AVGSTD OneColumn(int x, int icounter)
        {
            AVGSTD rtn = new AVGSTD();

            List<double> avg = new List<double>();
            List<double> std = new List<double>();

            for (int y = 0; y < icounter; y++)
            {
                avg.Add(buffer[y, x]);
                std.Add(buffer[y, x]);
            }

            rtn.avg = avg.Average();
            rtn.std = StandardDeviation(rtn.avg, std);
                        
            return rtn;
        }
    }
}
