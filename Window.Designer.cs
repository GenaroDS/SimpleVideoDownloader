namespace YouTubeDownloader
{
    partial class Window
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            txtUrl = new TextBox();
            lblStatus = new Label();
            btnSelectFolder = new Button();
            lblOutputFolder = new Label();
            txtFolderPath = new TextBox();
            lblDestination = new Label();
            lblURL = new Label();
            lblFormat = new Label();
            radioButton1 = new RadioButton();
            radioButton2 = new RadioButton();
            radioButton3 = new RadioButton();
            radioButton4 = new RadioButton();
            SuspendLayout();
            // 
            // txtUrl
            // 
            txtUrl.Location = new Point(67, 47);
            txtUrl.Name = "txtUrl";
            txtUrl.Size = new Size(248, 23);
            txtUrl.TabIndex = 0;
            txtUrl.TextChanged += textBox1_TextChanged;
            // 
            // lblStatus
            // 
            lblStatus.Anchor = AnchorStyles.None;
            lblStatus.AutoSize = true;
            lblStatus.ForeColor = Color.WhiteSmoke;
            lblStatus.Location = new Point(132, 196);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(85, 15);
            lblStatus.TabIndex = 2;
            lblStatus.Text = "Descargando...";
            lblStatus.TextAlign = ContentAlignment.BottomCenter;
            lblStatus.Visible = false;
            lblStatus.Click += lblStatus_Click;
            // 
            // btnSelectFolder
            // 
            btnSelectFolder.ForeColor = Color.WhiteSmoke;
            btnSelectFolder.Location = new Point(306, 10);
            btnSelectFolder.Name = "btnSelectFolder";
            btnSelectFolder.Size = new Size(28, 23);
            btnSelectFolder.TabIndex = 3;
            btnSelectFolder.Text = "...";
            btnSelectFolder.UseVisualStyleBackColor = true;
            btnSelectFolder.Click += btnSelectFolder_Click;
            // 
            // lblOutputFolder
            // 
            lblOutputFolder.AutoSize = true;
            lblOutputFolder.Location = new Point(67, 180);
            lblOutputFolder.Name = "lblOutputFolder";
            lblOutputFolder.Size = new Size(0, 15);
            lblOutputFolder.TabIndex = 4;
            // 
            // txtFolderPath
            // 
            txtFolderPath.Location = new Point(84, 10);
            txtFolderPath.Name = "txtFolderPath";
            txtFolderPath.ReadOnly = true;
            txtFolderPath.Size = new Size(216, 23);
            txtFolderPath.TabIndex = 5;
            // 
            // lblDestination
            // 
            lblDestination.Anchor = AnchorStyles.None;
            lblDestination.AutoSize = true;
            lblDestination.ForeColor = Color.WhiteSmoke;
            lblDestination.Location = new Point(11, 14);
            lblDestination.Name = "lblDestination";
            lblDestination.Size = new Size(70, 15);
            lblDestination.TabIndex = 6;
            lblDestination.Text = "Destination:";
            lblDestination.TextAlign = ContentAlignment.BottomCenter;
            lblDestination.Click += label1_Click;
            // 
            // lblURL
            // 
            lblURL.Anchor = AnchorStyles.None;
            lblURL.AutoSize = true;
            lblURL.ForeColor = Color.WhiteSmoke;
            lblURL.Location = new Point(30, 51);
            lblURL.Name = "lblURL";
            lblURL.Size = new Size(31, 15);
            lblURL.TabIndex = 7;
            lblURL.Text = "URL:";
            lblURL.TextAlign = ContentAlignment.BottomCenter;
            // 
            // lblFormat
            // 
            lblFormat.Anchor = AnchorStyles.None;
            lblFormat.AutoSize = true;
            lblFormat.ForeColor = Color.WhiteSmoke;
            lblFormat.Location = new Point(13, 85);
            lblFormat.Name = "lblFormat";
            lblFormat.Size = new Size(48, 15);
            lblFormat.TabIndex = 9;
            lblFormat.Text = "Format:";
            lblFormat.TextAlign = ContentAlignment.BottomCenter;
            // 
            // radioButton1
            // 
            radioButton1.AutoSize = true;
            radioButton1.Font = new Font("Segoe UI", 8F);
            radioButton1.ForeColor = Color.WhiteSmoke;
            radioButton1.Location = new Point(68, 84);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new Size(56, 17);
            radioButton1.TabIndex = 10;
            radioButton1.TabStop = true;
            radioButton1.Text = "Audio";
            radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            radioButton2.AutoSize = true;
            radioButton2.Font = new Font("Segoe UI", 8F);
            radioButton2.ForeColor = Color.WhiteSmoke;
            radioButton2.Location = new Point(125, 84);
            radioButton2.Name = "radioButton2";
            radioButton2.Size = new Size(68, 17);
            radioButton2.TabIndex = 11;
            radioButton2.TabStop = true;
            radioButton2.Text = "MP4 480";
            radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton3
            // 
            radioButton3.AutoSize = true;
            radioButton3.Checked = true;
            radioButton3.Font = new Font("Segoe UI", 8F);
            radioButton3.ForeColor = Color.WhiteSmoke;
            radioButton3.Location = new Point(195, 84);
            radioButton3.Name = "radioButton3";
            radioButton3.Size = new Size(68, 17);
            radioButton3.TabIndex = 12;
            radioButton3.TabStop = true;
            radioButton3.Text = "MP4 720";
            radioButton3.UseVisualStyleBackColor = true;
            // 
            // radioButton4
            // 
            radioButton4.AutoSize = true;
            radioButton4.Font = new Font("Segoe UI", 8F);
            radioButton4.ForeColor = Color.WhiteSmoke;
            radioButton4.Location = new Point(264, 84);
            radioButton4.Name = "radioButton4";
            radioButton4.Size = new Size(74, 17);
            radioButton4.TabIndex = 13;
            radioButton4.TabStop = true;
            radioButton4.Text = "MP4 1080";
            radioButton4.UseVisualStyleBackColor = true;
            // 
            // Window
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(346, 140);
            Controls.Add(radioButton4);
            Controls.Add(radioButton3);
            Controls.Add(radioButton2);
            Controls.Add(radioButton1);
            Controls.Add(lblFormat);
            Controls.Add(lblURL);
            Controls.Add(lblDestination);
            Controls.Add(txtFolderPath);
            Controls.Add(lblOutputFolder);
            Controls.Add(btnSelectFolder);
            Controls.Add(lblStatus);
            Controls.Add(txtUrl);
            Name = "Window";
            Text = "Simple Video Downloader";
            Load += Window_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtUrl;
        private Button btnDownload;
        private Label lblStatus;
        private Button btnSelectFolder;
        private Label lblOutputFolder;
        private TextBox txtFolderPath;
        private Label lblDestination;
        private Label lblURL;
        private Label lblFormat;
        private RadioButton radioButton1;
        private RadioButton radioButton2;
        private RadioButton radioButton3;
        private RadioButton radioButton4;
    }
}
