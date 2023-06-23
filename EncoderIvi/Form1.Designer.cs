namespace EncoderIvi
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            button1 = new Button();
            textBox1 = new TextBox();
            richTextBox1 = new RichTextBox();
            comboBox1 = new ComboBox();
            label1 = new Label();
            label2 = new Label();
            button2 = new Button();
            textBox2 = new TextBox();
            label3 = new Label();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(195, 70);
            button1.Name = "button1";
            button1.Size = new Size(153, 23);
            button1.TabIndex = 0;
            button1.Text = "Make Request";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(69, 70);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(100, 23);
            textBox1.TabIndex = 1;
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(12, 99);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(776, 473);
            richTextBox1.TabIndex = 2;
            richTextBox1.Text = "";
            // 
            // comboBox1
            // 
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "GeoNetworking", "No Header" });
            comboBox1.Location = new Point(456, 70);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(121, 23);
            comboBox1.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(495, 52);
            label1.Name = "label1";
            label1.Size = new Size(45, 15);
            label1.TabIndex = 4;
            label1.Text = "Header";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(90, 52);
            label2.Name = "label2";
            label2.Size = new Size(67, 15);
            label2.TabIndex = 5;
            label2.Text = "Message ID";
            // 
            // button2
            // 
            button2.Enabled = false;
            button2.Location = new Point(608, 69);
            button2.Name = "button2";
            button2.Size = new Size(153, 23);
            button2.TabIndex = 6;
            button2.Text = "Get Binary File";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(62, 12);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(699, 23);
            textBox2.TabIndex = 7;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(28, 15);
            label3.Name = "label3";
            label3.Size = new Size(28, 15);
            label3.TabIndex = 8;
            label3.Text = "URL";
            label3.Click += label3_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 584);
            Controls.Add(label3);
            Controls.Add(textBox2);
            Controls.Add(button2);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(comboBox1);
            Controls.Add(richTextBox1);
            Controls.Add(textBox1);
            Controls.Add(button1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private RichTextBox richTextBox1;
        private TextBox textBox1;
        private ComboBox comboBox1;
        private Label label1;
        private Label label2;
        private Button button2;
        private TextBox textBox2;
        private Label label3;
    }
}