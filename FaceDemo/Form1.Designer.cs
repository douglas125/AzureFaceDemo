namespace FaceDemo
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fromimageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fromCameraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeVideoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.picFace = new System.Windows.Forms.PictureBox();
            this.flowPan = new System.Windows.Forms.FlowLayoutPanel();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picFace)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 437);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(641, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip";
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(39, 17);
            this.lblStatus.Text = "Status";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(641, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fromimageToolStripMenuItem,
            this.fromCameraToolStripMenuItem,
            this.closeVideoToolStripMenuItem});
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
            this.loadToolStripMenuItem.Text = "&Load";
            // 
            // fromimageToolStripMenuItem
            // 
            this.fromimageToolStripMenuItem.Name = "fromimageToolStripMenuItem";
            this.fromimageToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.fromimageToolStripMenuItem.Text = "From &image...";
            this.fromimageToolStripMenuItem.Click += new System.EventHandler(this.fromimageToolStripMenuItem_Click);
            // 
            // fromCameraToolStripMenuItem
            // 
            this.fromCameraToolStripMenuItem.Name = "fromCameraToolStripMenuItem";
            this.fromCameraToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.fromCameraToolStripMenuItem.Text = "From camera...";
            this.fromCameraToolStripMenuItem.Click += new System.EventHandler(this.fromCameraToolStripMenuItem_Click);
            // 
            // closeVideoToolStripMenuItem
            // 
            this.closeVideoToolStripMenuItem.Name = "closeVideoToolStripMenuItem";
            this.closeVideoToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.closeVideoToolStripMenuItem.Text = "Close video";
            this.closeVideoToolStripMenuItem.Click += new System.EventHandler(this.closeVideoToolStripMenuItem_Click);
            // 
            // picFace
            // 
            this.picFace.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.picFace.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picFace.Location = new System.Drawing.Point(232, 36);
            this.picFace.Name = "picFace";
            this.picFace.Size = new System.Drawing.Size(397, 387);
            this.picFace.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picFace.TabIndex = 2;
            this.picFace.TabStop = false;
            // 
            // flowPan
            // 
            this.flowPan.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.flowPan.AutoScroll = true;
            this.flowPan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowPan.Location = new System.Drawing.Point(12, 36);
            this.flowPan.Name = "flowPan";
            this.flowPan.Size = new System.Drawing.Size(214, 387);
            this.flowPan.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(641, 459);
            this.Controls.Add(this.flowPan);
            this.Controls.Add(this.picFace);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Face Demo";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picFace)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fromimageToolStripMenuItem;
        private System.Windows.Forms.PictureBox picFace;
        private System.Windows.Forms.FlowLayoutPanel flowPan;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.ToolStripMenuItem fromCameraToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeVideoToolStripMenuItem;
    }
}

