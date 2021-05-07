﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;
using System.Globalization;

namespace ColdBoxControl
{
    public partial class Form1 : Form
    {
        bool output1 = false;
        bool output2 = false;
        bool chillerOn = false;
        bool autoSend = false;
        int updateRate = 1000;
        int pollIndex = 0;
        string ser1data;
        string ser2data;
        string ser3data;
        string ser4data;
        DateTime ser2time;
        DateTime ser1time;
        DateTime ser3time;
        DateTime ser4time;
        float V1set = 0.0f;
        float I1set = 0.0f;
        float V1measure = 0.0f;
        float I1measure = 0.0f;
        float V2set = 0.0f;
        float I2set = 0.0f;
        float V2measure = 0.0f;
        float I2measure = 0.0f;
        float T1 = 0.0f;
        float T2 = 0.0f;
        float T3 = 0.0f;
        float T4 = 0.0f;
        float coolantTemp = 0.0f;

        string saveFile = "";
        bool[] connectedDevices = new bool[4];
        float[] collectedValues = new float[22];
        string[] valueLables = {
            "Heater1 V",
            "Heater1 I",
            "Heater1 Power",
            "Heater2 V",
            "Heater2 I",
            "Heater2 Power",
            "coolant Temp",
            "Module Temp",
            "AlN Temp",
            "Service Hybrid Temp",
            "Plate Temp"
        };


        public Form1()
        {
            InitializeComponent();
        }

        private void comboBox1_DropDown(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(SerialPort.GetPortNames());
        }

        private void comboBox2_DropDown(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            comboBox2.Items.AddRange(SerialPort.GetPortNames());
        }

        private void comboBox3_DropDown(object sender, EventArgs e)
        {
            comboBox3.Items.Clear();
            comboBox3.Items.AddRange(SerialPort.GetPortNames());
        }

        private void comboBox4_DropDown(object sender, EventArgs e)
        {
            comboBox4.Items.Clear();
            comboBox4.Items.AddRange(SerialPort.GetPortNames());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort1.PortName = comboBox1.Text;
                serialPort1.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort2.PortName = comboBox2.Text;
                serialPort2.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort3.PortName = comboBox3.Text;
                serialPort3.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort4.PortName = comboBox4.Text;
                serialPort4.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            serialPort1.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            serialPort2.Close();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            serialPort3.Close();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            serialPort4.Close();
        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            ser1data = serialPort1.ReadExisting();
            ser1time = DateTime.Now;
            this.Invoke(new EventHandler(displayData1_event));
        }

        private void serialPort2_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //ser2data = serialPort2.ReadExisting();
            ser2data = serialPort2.ReadLine();
            ser2time = DateTime.Now;
            this.Invoke(new EventHandler(displayData2_event));
        }

        private void serialPort3_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            ser3data = serialPort3.ReadExisting();
            ser3time = DateTime.Now;
            this.Invoke(new EventHandler(displayData3_event));
        }

        private void serialPort4_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            ser4data = serialPort4.ReadExisting();
            ser4time = DateTime.Now;
            this.Invoke(new EventHandler(displayData4_event));
        }

        private void displayData1_event(object sender, EventArgs e)
        {
            string[] measuredVals = ser1data.Split(';');
            V1measure = float.Parse(measuredVals[0]);
            I1measure = float.Parse(measuredVals[1]);
           // textBox2.AppendText("Heater Voltage: " + V1measure.ToString() + '\t' +  "Heater Current: " + I1measure.ToString() + (Environment.NewLine));
            this.chart1.Series["Heater1V"].Points.AddXY(ser1time, V1measure);
            this.chart1.Series["Heater1I"].Points.AddXY(ser1time, I1measure);
            this.chart1.Series["Heater1 Power"].Points.AddXY(ser1time, (V1measure * I1measure));
            collectedValues[0] = V1measure;
            collectedValues[1] = I1measure;
            collectedValues[2] = (V1measure * I1measure);
            advanceCollection();
            if (this.chart1.Series["Heater1V"].Points.Count() > 20)
            {
                this.chart1.Series["Heater1V"].Points.RemoveAt(0);
                this.chart1.Series["Heater1I"].Points.RemoveAt(0);
                this.chart1.Series["Heater1 Power"].Points.RemoveAt(0);
                this.chart1.ChartAreas[0].RecalculateAxesScale();
            }
        }

        private void displayData2_event(object sender, EventArgs e)
        {
            string[] measuredVals = ser2data.Split(',');
            int i = 0;
            foreach(string temp in measuredVals)
            {
                collectedValues[i + 7] = float.Parse(temp);
                i++;
            }

            T1 = float.Parse(measuredVals[0]);
            T2 = float.Parse(measuredVals[1]);
            T3 = float.Parse(measuredVals[2]);
            T4 = float.Parse(measuredVals[3]);
            textBox2.AppendText("T1: " + T1.ToString() + '\t' + "T2: " + T2.ToString() + '\t' + "T3: " + T3.ToString() + '\t' + "T4: " + T4.ToString() + (Environment.NewLine));
            //collectedValues[7] = T1;
            //collectedValues[8] = T2;
            //collectedValues[9] = T3;
            //collectedValues[10] =T4;
            this.chart3.Series["T1"].Points.AddXY(ser2time, T1);    
            this.chart3.Series["T2"].Points.AddXY(ser2time, T2);
            this.chart3.Series["T3"].Points.AddXY(ser2time, T3);
            this.chart3.Series["T4"].Points.AddXY(ser2time, T4);

            if (this.chart3.Series["T1"].Points.Count() > 20)
            {
                this.chart3.Series["T1"].Points.RemoveAt(0);
                this.chart3.Series["T2"].Points.RemoveAt(0);
                this.chart3.Series["T3"].Points.RemoveAt(0);
                this.chart3.Series["T4"].Points.RemoveAt(0);
                this.chart3.ChartAreas[0].RecalculateAxesScale();
            }
        }

        private void displayData3_event(object sender, EventArgs e)
        {
            try // ocasionaly chiller gives iproperly formatted string, this lets it read past that
            {
                coolantTemp = float.Parse(ser3data);
            }
            catch
            {
                coolantTemp = 0;
            }
            //coolantTemp = float.Parse(ser3data);
            //textBox2.AppendText("Coolent temp: " + coolantTemp.ToString() + Environment.NewLine);
            collectedValues[6] = coolantTemp;
            this.chart2.Series["Coolent Temp"].Points.AddXY(ser3time, coolantTemp);
            advanceCollection();
            if (this.chart2.Series["Coolent Temp"].Points.Count() > 20)
            {
                this.chart2.Series["Coolent Temp"].Points.RemoveAt(0);
                this.chart2.ChartAreas[0].RecalculateAxesScale();
            }
        }

        private void displayData4_event(object sender, EventArgs e)
        {
            string[] measuredVals = ser4data.Split(';');
            V2measure = float.Parse(measuredVals[0]);
            I2measure = float.Parse(measuredVals[1]);
            //textBox2.AppendText("Heater Voltage: " + V2measure.ToString() + '\t' + "Heater Current: " + I2measure.ToString() + (Environment.NewLine));
            this.chart1.Series["Heater2V"].Points.AddXY(ser2time, V2measure);
            this.chart1.Series["Heater2I"].Points.AddXY(ser2time, I2measure);
            this.chart1.Series["Heater2 Power"].Points.AddXY(ser2time, (V2measure * I2measure));
            collectedValues[3] = V2measure;
            collectedValues[4] = I2measure;
            collectedValues[5] = (V2measure * I2measure);
            advanceCollection();
            if (this.chart1.Series["Heater2V"].Points.Count() > 20)
            {
                this.chart1.Series["Heater2V"].Points.RemoveAt(0);
                this.chart1.Series["Heater2I"].Points.RemoveAt(0);
                this.chart1.Series["Heater2 Power"].Points.RemoveAt(0);
                this.chart1.ChartAreas[0].RecalculateAxesScale();
            }
        }

        private void writeCSVline()
        {
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(saveFile, true))
            {
                sw.WriteAsync(DateTime.Now.ToString());
                foreach (float value in collectedValues)
                {
                    //sw.WriteAsync(DateTime.Now.ToString());
                    sw.WriteAsync("," + value);
                }
                sw.WriteAsync(Environment.NewLine);
                //collectedValues.Clear();
            }
        }

        private void writeCSVHeader()
        {
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(saveFile, true))
            {
                sw.WriteAsync("Time");
                for (int i = 0; i < valueLables.Count(); i++)
                {
                    sw.WriteAsync("," + valueLables[i]);
                }
                sw.WriteAsync(Environment.NewLine);
            }
        }

        private void advanceCollection()
        {
            switch (pollIndex)
            {
                case 0:
                    pollIndex++;
                    if (serialPort1.IsOpen)
                    {
                        serialPort1.Write(":meas:volt:dc?;:meas:curr:dc?" + '\n');
                    }
                    else
                    {
                        advanceCollection();
                        collectedValues[0] = 0;
                        collectedValues[1] = 0;
                        collectedValues[2] = 0;
                    }
                    break;
                case 1:
                    pollIndex++;
                    if (serialPort4.IsOpen)
                    {
                        serialPort4.Write(":meas:volt:dc?;:meas:curr:dc?" + '\n');
                    }
                    else
                    {
                        advanceCollection();
                        collectedValues[3] = 0;
                        collectedValues[4] = 0;
                        collectedValues[5] = 0;
                    }
                    break;
                case 2:
                    pollIndex++;
                    if (serialPort3.IsOpen)
                    {
                        serialPort3.Write("in_pv_00" + Environment.NewLine);
                    }
                    else
                    {
                        advanceCollection();
                        collectedValues[6] = 0;
                    }
                    break;
                case 3:
                    pollIndex = 0;
                    if (serialPort2.IsOpen)
                    {
                        serialPort2.Write("T");
                    }
                    else
                    {
                        collectedValues[7] = 0;
                        collectedValues[8] = 0;
                        collectedValues[9] = 0;
                        collectedValues[10] = 0;
                        collectedValues[11] = 0;
                        collectedValues[12] = 0;
                        collectedValues[13] = 0;
                        collectedValues[14] = 0;
                        collectedValues[15] = 0;
                        collectedValues[16] = 0;
                        collectedValues[17] = 0;
                        collectedValues[18] = 0;
                        collectedValues[19] = 0;
                        collectedValues[20] = 0;
                        collectedValues[21] = 0;
                    }
                    if (saveFile != "")
                    {
                        writeCSVline();
                    }
                    for (int i=0; i<valueLables.Count(); i++)
                    {
                        textBox2.AppendText(valueLables[i]);
                        textBox2.AppendText(": ");
                        textBox2.AppendText(collectedValues[i].ToString());
                        textBox2.AppendText("   ");
                    }
                    textBox2.AppendText(Environment.NewLine);
                    break;

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            V1set = float.Parse(textBox1.Text);
            I1set = float.Parse(textBox3.Text);
            serialPort1.Write("APPL " + V1set.ToString() + ',' + I1set.ToString() + '\n');
        }

        private void button17_Click(object sender, EventArgs e)
        {
            V2set = float.Parse(textBox5.Text);
            I2set = float.Parse(textBox4.Text);
            serialPort4.Write("APPL " + V2set.ToString() + ',' + I2set.ToString() + '\n');
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (output1)
            {
                button7.Text = "Enabled Output";
                output1 = false;
                // timer1.Enabled = false;
                //timer1.Stop();
                serialPort1.Write("OUTP OFF" + '\n');
            }
            else
            {
                button7.Text = "Disable Output";
                output1 = true;
                //timer1.Enabled = true;
                //timer1.Start();
                serialPort1.Write("OUTP ON" + '\n');
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            if (output2)
            {
                button16.Text = "Enabled Output";
                output2 = false;
                // timer1.Enabled = false;
                //timer1.Stop();
                serialPort4.Write("OUTP OFF" + '\n');
            }
            else
            {
                button16.Text = "Disable Output";
                output2 = true;
                //timer1.Enabled = true;
                //timer1.Start();
                serialPort4.Write("OUTP ON" + '\n');
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            pollIndex = 0;
            advanceCollection();
            ////MessageBox.Show("tick");
            ////serialPort2.Write(testInc.ToString() + '\n');
            //serialPort1.Write(":meas:volt:dc?;:meas:curr:dc?" + '\n');
            ////testInc = (testInc + 1) % 10;
        }

        //private void button6_Click(object sender, EventArgs e)
        //{
        //    //serialPort1.Write("MEASure[:SCALar]:VOLTage[:DC]?");
        //    //serialPort1.Write(":APPLy? \n");
        //    serialPort1.Write(":meas:volt:dc?;:meas:curr:dc?;:meas:pow:dc?" + '\n');
        //}

        //private void button8_Click(object sender, EventArgs e)
        //{
        //    if (autoSend)
        //    {
        //        button8.Text = "Enabled timer";
        //        autoSend = false;
        //        // timer1.Enabled = false;
        //        timer1.Stop();
        //    }
        //    else
        //    {
        //        button8.Text = "Disable timmer";
        //        autoSend = true;
        //        //timer1.Enabled = true;
        //        timer1.Start();
        //    }
        //}

        //private void button11_Click(object sender, EventArgs e)
        //{
        //    //serialPort3.Write("out_sp_00" + ' ' + "15.5" + Environment.NewLine);  //sets working temp to 15.5c
        //    serialPort3.Write("out_mode_05" + ' ' + "1" + Environment.NewLine);      //turns on
        //    serialPort3.DiscardInBuffer();
        //    serialPort3.Write("status" + Environment.NewLine);
        //    serialPort3.DiscardInBuffer();
        //    //serialPort3.Write("in_pv_00" + Environment.NewLine);    //gets current temp
        //    //serialPort3.Write("out_mode_05" + ' ' + "2" + Environment.NewLine); //forces error
        //}

        //private void button12_Click(object sender, EventArgs e)
        //{
        //    serialPort3.Write("in_sp_00" + Environment.NewLine);      //gets set temp
        //    serialPort3.DiscardInBuffer();
        //    //serialPort3.Write("status" + Environment.NewLine);
        //    //serialPort3.Write("out_mode_05" + ' ' + "0" + Environment.NewLine); //turns off
        //}

        //private void button13_Click(object sender, EventArgs e)
        //{
        //    serialPort3.Write("out_mode_05" + ' ' + "0" + Environment.NewLine);
        //    serialPort3.DiscardInBuffer();
        //    serialPort3.Write("status" + Environment.NewLine);
        //    serialPort3.DiscardInBuffer();
        //}

        private void button21_Click(object sender, EventArgs e)
        {
            try
            {
                updateRate = Convert.ToInt16(float.Parse(textBox7.Text) * 1000);
                //MessageBox.Show(updateRate.ToString());
                timer1.Interval = updateRate;
            }
            catch
            {
                MessageBox.Show("inproperly formated input");
            }
            if (autoSend)
            {
                button21.Text = "Enabled Auto Collect";
                autoSend = false;
                // timer1.Enabled = false;
                timer1.Stop();
            }
            else
            {
                button21.Text = "Disable Auto Collect";
                autoSend = true;
                //timer1.Enabled = true;
                timer1.Start();
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            openFileDialog1.DefaultExt = "txt";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt";
            openFileDialog1.Title = "Browse Text Files";
            openFileDialog1.FileName = "";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = openFileDialog1.FileName;
                saveFile = openFileDialog1.FileName;
                writeCSVHeader();
            }
        }

        private void button22_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "txt files (*.txt)|*.txt";
            saveFileDialog1.ShowDialog();

            // If the file name is not an empty string open it for saving.
            if (saveFileDialog1.FileName != "")
            {
                textBox6.Text = saveFileDialog1.FileName;
                saveFile = saveFileDialog1.FileName;
                writeCSVHeader();
                // StreamWriter File = new StreamWriter(saveFileDialog1.FileName);
            }
               
        }

        private void button20_Click(object sender, EventArgs e)
        {
            serialPort3.Write("out_sp_00" + ' ' + textBox8.Text + Environment.NewLine);
        }

        private void button19_Click(object sender, EventArgs e)
        {
            if (chillerOn)
            {
                button19.Text = "Enabled Chiller";
                chillerOn = false;
                serialPort3.Write("out_mode_05" + ' ' + "0" + Environment.NewLine);
            }
            else
            {
                button19.Text = "Disable Chiller";
                chillerOn = true;
                serialPort3.Write("out_mode_05" + ' ' + "1" + Environment.NewLine);
            }
        }


        private void groupBox8_SizeChanged(object sender, EventArgs e)
        {
            chart1.Width = (groupBox8.Width / 3);
            chart2.Width = (groupBox8.Width / 3);
            chart3.Width = (groupBox8.Width / 3);
        }
        

    }
}