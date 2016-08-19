﻿using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using dnlib.DotNet;
using dnlib.DotNet.Writer;

namespace Noisette
{
    public partial class MainForm : Form
    {
        #region Declarations

        //todo : make a proper core.property class
        public string DirectoryName = "";
        public ModuleDefMD module;
        public static ModuleWriterOptions opts;

        #endregion

        #region Form_and_Design

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
            (
            int nLeftRect, // x-coordinate of upper-left corner
            int nTopRect, // y-coordinate of upper-left corner
            int nRightRect, // x-coordinate of lower-right corner
            int nBottomRect, // y-coordinate of lower-right corner
            int nWidthEllipse, // height of ellipse
            int nHeightEllipse // width of ellipse
            );

        public MainForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width - 2, Height - 2, 10, 10));
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void textBox1_DragDrop(object sender, DragEventArgs e)
        {
            try
            {

                //



                textBox0.Visible = false;
                label5.Visible = false;
                label4.Visible = false;
                panel2.Visible = false;
                shapedPanel1.Visible = false;
                pictureBox5.Visible = false;


                pictureBox4.Visible = true;

                label1.Visible = true;
                label1.BringToFront();
                label2.Visible = true;
                label2.BringToFront();
                pictureBox6.Visible = true;
                label6.Visible = true;
                label6.BringToFront();

                logbox.Visible = true;
                logbox.BringToFront();
                //
                Array array = (Array) e.Data.GetData(DataFormats.FileDrop);
                if (array == null) return;
                string text = array.GetValue(0).ToString();
                int num = text.LastIndexOf(".", StringComparison.Ordinal);
                if (num == -1) return;
                string text2 = text.Substring(num);
                text2 = text2.ToLower();
                if (text2 != ".exe" && text2 != ".dll") return;
                //Activate();

                textBox1.Text = text;
                ModuleDefMD module = ModuleDefMD.Load(textBox1.Text);
                logbox.AppendText("---------------------------" + Environment.NewLine);
                logbox.AppendText("Obfuscation process started on " + module.Name + Environment.NewLine);
                logbox.AppendText("---------------------------" + Environment.NewLine);


                DoObfusction(module);

                logbox.AppendText("Done ! :)" + Environment.NewLine);

                int num2 = text.LastIndexOf("\\", StringComparison.Ordinal);
                if (num2 != -1)
                {
                    DirectoryName = text.Remove(num2, text.Length - num2);
                }
                if (DirectoryName.Length == 2)
                {
                    DirectoryName += "\\";
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

                //
                textBox0.Visible = true;
                label5.Visible = true;
                label4.Visible = true;
                panel2.Visible = true;
                shapedPanel1.Visible = true;
                pictureBox5.Visible = true;

                label1.Visible = false;
                label2.Visible = false;
                pictureBox4.Visible = false;

                //
            }
        }

        private void textBox1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.Copy : DragDropEffects.None;

        }

        private void textBox0_TextChanged(object sender, EventArgs e)
        {

        }



        private void panel2_Paint_1(object sender, PaintEventArgs e)
        {

            Point[] polygonPoints2 = new Point[5];

            polygonPoints2[0] = new Point(0, 0);
            polygonPoints2[1] = new Point(0, 0);
            polygonPoints2[2] = new Point(0, 0);
            polygonPoints2[3] = new Point(200, 35);
            polygonPoints2[4] = new Point(0, 200);
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            e.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

            e.Graphics.DrawPolygon(new Pen(new SolidBrush(Color.FromArgb(214, 32, 54))), polygonPoints2);
            e.Graphics.FillPolygon(new SolidBrush(Color.FromArgb(214, 32, 54)), polygonPoints2);

            Point[] polygonPoints = new Point[5];

            polygonPoints[0] = new Point(0, 290);
            polygonPoints[1] = new Point(0, 50);
            polygonPoints[2] = new Point(290, 0);
            polygonPoints[3] = new Point(290, 156);
            polygonPoints[4] = new Point(250, 290);
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            e.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

            e.Graphics.DrawPolygon(new Pen(new SolidBrush(Color.FromArgb(234, 228, 228))), polygonPoints);
            e.Graphics.FillPolygon(new SolidBrush(Color.FromArgb(234, 228, 228)), polygonPoints);

        }




        private void pictureBox6_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void label3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Noisette - The nuts-breaker obfuscator"
                + Environment.NewLine + 
                "Made by XenocodeRCE - 2016"
                + Environment.NewLine + 
                "dnlib by 0xd4d");
        }

        #endregion


        public static void DoObfusction(ModuleDefMD module)
        {
            //Prepare our mdulewriter for urther usage
            //todo : make a pre and post processing class. i mean come on it's sloppy as fuck dude
            opts = new ModuleWriterOptions(module);


            //outline constant
            Protection.ConstantOutlinning.ConstantOutlinningProtection.OutlineConstant(module);
            //Inject Antitamper class
            Protection.AntiTampering.AntiTamperingProtection.AddCall(module);
            //rename all
            Protection.Renaming.RenamingProtection.RenameModule(module);
            //invalid metadata
            //Protection.InvalidMetadata.InvalidMD.InsertInvalidMetadata(module);

            //Save assembly
            opts.Logger = DummyLogger.NoThrowInstance;
            //todo : make a propre saving function because now its ridiculous
            module.Write(module.Location + "_protected.exe", opts);
            //post-stage antitamper
            Protection.AntiTampering.AntiTamperingProtection.Md5(module.Location + "_protected.exe");
        }

        public static string tst()
        {
            return "test";
        }
    }
}