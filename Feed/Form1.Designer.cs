namespace Feed
{
    partial class Form1
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox_Sciezka = new System.Windows.Forms.TextBox();
            this.button_Otworz = new System.Windows.Forms.Button();
            this.buttonRejestracja = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.backgroundWorker_rejestruj = new System.ComponentModel.BackgroundWorker();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_IleSymboliNaPort = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.backgroundWorker_L2_Live = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox_Sciezka
            // 
            this.textBox_Sciezka.Location = new System.Drawing.Point(6, 19);
            this.textBox_Sciezka.Name = "textBox_Sciezka";
            this.textBox_Sciezka.Size = new System.Drawing.Size(262, 20);
            this.textBox_Sciezka.TabIndex = 0;
            // 
            // button_Otworz
            // 
            this.button_Otworz.Location = new System.Drawing.Point(274, 17);
            this.button_Otworz.Name = "button_Otworz";
            this.button_Otworz.Size = new System.Drawing.Size(75, 23);
            this.button_Otworz.TabIndex = 2;
            this.button_Otworz.Text = "Otwórz";
            this.button_Otworz.UseVisualStyleBackColor = true;
            this.button_Otworz.Click += new System.EventHandler(this.button_Otworz_Click);
            // 
            // buttonRejestracja
            // 
            this.buttonRejestracja.Location = new System.Drawing.Point(352, 17);
            this.buttonRejestracja.Name = "buttonRejestracja";
            this.buttonRejestracja.Size = new System.Drawing.Size(69, 23);
            this.buttonRejestracja.TabIndex = 3;
            this.buttonRejestracja.Text = "Rejestracja";
            this.buttonRejestracja.UseVisualStyleBackColor = true;
            this.buttonRejestracja.Click += new System.EventHandler(this.buttonRejestracja_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.RestoreDirectory = true;
            // 
            // backgroundWorker_rejestruj
            // 
            this.backgroundWorker_rejestruj.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_rejestruj_DoWork);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Size = new System.Drawing.Size(433, 360);
            this.splitContainer1.SplitterDistance = 73;
            this.splitContainer1.TabIndex = 4;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBox_IleSymboliNaPort);
            this.groupBox1.Controls.Add(this.textBox_Sciezka);
            this.groupBox1.Controls.Add(this.buttonRejestracja);
            this.groupBox1.Controls.Add(this.button_Otworz);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.MinimumSize = new System.Drawing.Size(433, 64);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(433, 73);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Lista symboli";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(59, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Ile symboli na port/wątek";
            // 
            // textBox_IleSymboliNaPort
            // 
            this.textBox_IleSymboliNaPort.Location = new System.Drawing.Point(6, 42);
            this.textBox_IleSymboliNaPort.Name = "textBox_IleSymboliNaPort";
            this.textBox_IleSymboliNaPort.Size = new System.Drawing.Size(47, 20);
            this.textBox_IleSymboliNaPort.TabIndex = 5;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.richTextBox1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(433, 283);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Log";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Location = new System.Drawing.Point(3, 16);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(427, 264);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(433, 360);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form1";
            this.Text = "Feed";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_Sciezka;
        private System.Windows.Forms.Button button_Otworz;
        private System.Windows.Forms.Button buttonRejestracja;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.ComponentModel.BackgroundWorker backgroundWorker_rejestruj;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_IleSymboliNaPort;
        private System.ComponentModel.BackgroundWorker backgroundWorker_L2_Live;
    }
}

