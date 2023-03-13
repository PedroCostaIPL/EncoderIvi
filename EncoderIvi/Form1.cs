using Newtonsoft.Json;
using System.Net.Http.Headers;
using PerEncDec.IVI;
using System.Collections;

namespace EncoderIvi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Json2PerBitAdapter.MakeRequest(textBox1.Text);
            
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private static void richTextBox1Update(String json)
        {
        }
    }
}