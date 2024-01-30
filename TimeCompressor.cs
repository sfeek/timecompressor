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
            public double med;
            public double std;
            public double var;
            public double min;
            public double max;
            public int z_plus;
            public int z_minus;
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
            catch { btn_compress.Enabled = true; return; }

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "csv files (*.csv)|*.csv";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    outfilename = openFileDialog.FileName + "-cmp.csv";
                    if (File.Exists(outfilename))
                    {
                        try
                        {
                            File.Delete(outfilename); // Make sure the old file is gone
                        }
                        catch { btn_compress.Enabled = true; return; }
                    }

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
                                if (chkFieldSeparators.Checked == false) 
                                    output += labels[x] + "_avg," + labels[x] + "_med," + labels[x] + "_std," + labels[x] + "_var," + labels[x] + "_min," + labels[x] + "_max," + labels[x] + "_zminus," + labels[x] + "_zplus,";
                                else
                                    output += labels[x] + "_avg," + labels[x] + "_med," + labels[x] + "_std," + labels[x] + "_var," + labels[x] + "_min," + labels[x] + "_max," + labels[x] + "_zminus," + labels[x] + "_zplus,,";
                            else
                                output += labels[x] + "_avg," + labels[x] + "_med," + labels[x] + "_std," + labels[x] + "_var," + labels[x] + "_min," + labels[x] + "_max" + labels[x] + "_zminus," + labels[x] + "_zplus"; 
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
                                            if (chkFieldSeparators.Checked == false)
                                                output += t[x].Result.avg + "," + t[x].Result.med + "," + t[x].Result.std + "," + t[x].Result.var + "," + t[x].Result.min + "," + t[x].Result.max + "," + t[x].Result.z_minus + "," + t[x].Result.z_plus + ",";
                                            else
                                                output += t[x].Result.avg + "," + t[x].Result.med + "," + t[x].Result.std + "," + t[x].Result.var + "," + t[x].Result.min + "," + t[x].Result.max + "," + t[x].Result.z_minus + "," + t[x].Result.z_plus + ",,";
                                        else
                                            output += t[x].Result.avg + "," + t[x].Result.med + "," + t[x].Result.std + "," + t[x].Result.var + "," + t[x].Result.min + "," + t[x].Result.max + "," + t[x].Result.z_minus + "," + t[x].Result.z_plus;
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
                                    if (chkFieldSeparators.Checked == false)
                                        output += t[x].Result.avg + "," + t[x].Result.med + "," + t[x].Result.std + "," + t[x].Result.var + "," + t[x].Result.min + "," + t[x].Result.max + "," + t[x].Result.z_minus + "," + t[x].Result.z_plus + ",";
                                    else
                                        output += t[x].Result.avg + "," + t[x].Result.med + "," + t[x].Result.std + "," + t[x].Result.var + "," + t[x].Result.min + "," + t[x].Result.max + "," + t[x].Result.z_minus + "," + t[x].Result.z_plus + ",,";
                                else
                                    output += t[x].Result.avg + "," + t[x].Result.med + "," + t[x].Result.std + "," + t[x].Result.var + "," + t[x].Result.min + "," + t[x].Result.max + "," + t[x].Result.z_minus + "," + t[x].Result.z_plus;
                            }

                            sw.WriteLine(output);
                            lblRecordNumber.Text = "Record #" + counter;

                            icounter = 0;
                        }
                    }
                }
            }
            btn_compress.Enabled = true;
            lblRecordNumber.Text = "Record # Done!";
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

        double Median(List<double> doubleList)
        {
            int s = doubleList.Count;
            doubleList.Sort();

            if (s % 2 == 0)
            {
                return (doubleList[s / 2] + doubleList[s / 2 + 1]) / 2.0;
            } 
            else
            {
                return doubleList[s / 2];
            }
        }

        AVGSTD OneColumn(int x, int icounter)
        {
            AVGSTD rtn = new AVGSTD();

            List<double> avg = new List<double>();
            List<double> med = new List<double>();
            List<double> std = new List<double>();
            List<double> var = new List<double>();
            List<double> min = new List<double>();
            List<double> max = new List<double>();
            List<int> z_minus = new List<int>();
            List<int> z_plus = new List<int>();

            rtn.z_plus = 0;
            rtn.z_minus = 0;
            double z_thresh = 0.0;
            double z = 0.0;

            try
            {
                z_thresh = Math.Abs(Convert.ToDouble(txtZThresh.Text));
            }
            catch { }

            for (int y = 0; y < icounter; y++)
            {
                avg.Add(buffer[y, x]);
                med.Add(buffer[y, x]);
                std.Add(buffer[y, x]);
                var.Add(buffer[y, x]);
            }

            rtn.avg = avg.Average();
            rtn.med = Median(med);
            rtn.std = StandardDeviation(rtn.avg, std);
            rtn.var = rtn.std * rtn.std;
            rtn.min = avg.Min();
            rtn.max = avg.Max();
            
            if (z_thresh > 0.0)
            {
                for (int y = 0; y < icounter; y++)
                {
                    z = (buffer[y, x] - rtn.avg) / rtn.std;
                    if (z < -z_thresh) rtn.z_minus++;
                    if (z > z_thresh) rtn.z_plus++;
                }
            }

            return rtn;
        }
    }
}
