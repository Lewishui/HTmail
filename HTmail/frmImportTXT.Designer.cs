namespace HTmail
{
    partial class frmImportTXT
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.cancelButton = new System.Windows.Forms.Button();
            this.importButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.pathTextBox = new System.Windows.Forms.TextBox();
            this.openFileBtton = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.closeButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.progressMsgLabel = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "HACCYU";
            this.openFileDialog1.Filter = "Text Files (.txt)|*.txt|All Files (*.*)|*.*";
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.cancelButton.Location = new System.Drawing.Point(290, 218);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(106, 32);
            this.cancelButton.TabIndex = 11;
            this.cancelButton.Text = "开始导入";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // importButton
            // 
            this.importButton.Location = new System.Drawing.Point(178, 218);
            this.importButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.importButton.Name = "importButton";
            this.importButton.Size = new System.Drawing.Size(106, 32);
            this.importButton.TabIndex = 10;
            this.importButton.Text = "下载模板";
            this.importButton.UseVisualStyleBackColor = true;
            this.importButton.Click += new System.EventHandler(this.importButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 102);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 13;
            this.label1.Text = "文件路径";
            // 
            // pathTextBox
            // 
            this.pathTextBox.Location = new System.Drawing.Point(153, 98);
            this.pathTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pathTextBox.Name = "pathTextBox";
            this.pathTextBox.Size = new System.Drawing.Size(322, 21);
            this.pathTextBox.TabIndex = 8;
            // 
            // openFileBtton
            // 
            this.openFileBtton.Location = new System.Drawing.Point(483, 97);
            this.openFileBtton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.openFileBtton.Name = "openFileBtton";
            this.openFileBtton.Size = new System.Drawing.Size(36, 27);
            this.openFileBtton.TabIndex = 9;
            this.openFileBtton.Text = "...";
            this.openFileBtton.UseVisualStyleBackColor = true;
            this.openFileBtton.Click += new System.EventHandler(this.openFileBtton_Click);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            // 
            // closeButton
            // 
            this.closeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.closeButton.Location = new System.Drawing.Point(402, 218);
            this.closeButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(106, 32);
            this.closeButton.TabIndex = 12;
            this.closeButton.Text = "关闭";
            this.closeButton.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("MS PGothic", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(15, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(534, 27);
            this.label2.TabIndex = 16;
            this.label2.Text = "批量导入";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // progressMsgLabel
            // 
            this.progressMsgLabel.AutoSize = true;
            this.progressMsgLabel.Location = new System.Drawing.Point(42, 139);
            this.progressMsgLabel.Name = "progressMsgLabel";
            this.progressMsgLabel.Size = new System.Drawing.Size(23, 12);
            this.progressMsgLabel.TabIndex = 15;
            this.progressMsgLabel.Text = "0/0";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(42, 159);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(477, 27);
            this.progressBar1.TabIndex = 14;
            this.progressBar1.Visible = false;
            // 
            // frmImportTXT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(565, 283);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.importButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pathTextBox);
            this.Controls.Add(this.openFileBtton);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.progressMsgLabel);
            this.Controls.Add(this.progressBar1);
            this.Name = "frmImportTXT";
            this.Text = "frmImportTXT";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button importButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox pathTextBox;
        private System.Windows.Forms.Button openFileBtton;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label progressMsgLabel;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}