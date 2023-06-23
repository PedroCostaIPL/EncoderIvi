using Newtonsoft.Json;
using System.Net.Http.Headers;
using PerEncDec.IVI;
using System.Collections;
using EncoderIvi.Message;
using System.Windows.Forms;

namespace EncoderIvi
{
    public partial class Form1 : Form
    {
        PerEncDec.IVI.IVIMPDUDescriptions.IVIM rootIVI;
        public Form1()
        {
            InitializeComponent();
            textBox2.Text = "http://projeto-informatico2.test/api/getivimnotc/json/";
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            rootIVI = await Request.MakeRequest(textBox1.Text, this);
            //Enable binary button
            enableButton2();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedItem = "GeoNetworking";
        }

        public string getComboBoxValue()
        {
            return Convert.ToString(comboBox1.SelectedItem);
        }
        public string getIvimId()
        {
            return textBox1.Text;
        }

        public string getURL()
        {
            return textBox2.Text;
        }

        public string setRichTextBox(String newText)
        {
            return richTextBox1.Text = newText;
        }
        public void enableButton2()
        {
            button2.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Object2Byte.ObjectTransform(rootIVI, getComboBoxValue(), getIvimId());

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}