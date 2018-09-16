namespace FaceDemo
{
    partial class frmFaceAnalytics
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFaceAnalytics));
            this.tsMain = new System.Windows.Forms.ToolStrip();
            this.btnFromFile = new System.Windows.Forms.ToolStripButton();
            this.tsSelCam = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnStart = new System.Windows.Forms.ToolStripButton();
            this.btnStop = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnDelStatist = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panEmotion = new System.Windows.Forms.Panel();
            this.pbImg = new System.Windows.Forms.PictureBox();
            this.zedAge = new ZedGraph.ZedGraphControl();
            this.btnShowHide = new System.Windows.Forms.ToolStripButton();
            this.pbCam = new System.Windows.Forms.PictureBox();
            this.tsMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbImg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCam)).BeginInit();
            this.SuspendLayout();
            // 
            // tsMain
            // 
            this.tsMain.ImageScalingSize = new System.Drawing.Size(64, 64);
            this.tsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnFromFile,
            this.tsSelCam,
            this.toolStripSeparator1,
            this.btnStart,
            this.btnStop,
            this.btnShowHide,
            this.toolStripSeparator2,
            this.btnDelStatist});
            this.tsMain.Location = new System.Drawing.Point(0, 0);
            this.tsMain.Name = "tsMain";
            this.tsMain.Size = new System.Drawing.Size(696, 71);
            this.tsMain.TabIndex = 0;
            this.tsMain.Text = "toolStrip1";
            // 
            // btnFromFile
            // 
            this.btnFromFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnFromFile.Image = ((System.Drawing.Image)(resources.GetObject("btnFromFile.Image")));
            this.btnFromFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFromFile.Name = "btnFromFile";
            this.btnFromFile.Size = new System.Drawing.Size(68, 68);
            this.btnFromFile.Text = "Open picture";
            this.btnFromFile.Click += new System.EventHandler(this.btnFromFile_Click);
            // 
            // tsSelCam
            // 
            this.tsSelCam.Image = ((System.Drawing.Image)(resources.GetObject("tsSelCam.Image")));
            this.tsSelCam.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsSelCam.Name = "tsSelCam";
            this.tsSelCam.Size = new System.Drawing.Size(125, 68);
            this.tsSelCam.Text = "Camera";
            this.tsSelCam.ToolTipText = "Select camera";
            this.tsSelCam.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.tsSelCam_DropDownItemClicked);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 71);
            // 
            // btnStart
            // 
            this.btnStart.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnStart.Image = ((System.Drawing.Image)(resources.GetObject("btnStart.Image")));
            this.btnStart.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(68, 68);
            this.btnStart.Text = "Start recording";
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnStop.Image = ((System.Drawing.Image)(resources.GetObject("btnStop.Image")));
            this.btnStop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(68, 68);
            this.btnStop.Text = "Stop recording";
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 71);
            // 
            // btnDelStatist
            // 
            this.btnDelStatist.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDelStatist.Image = ((System.Drawing.Image)(resources.GetObject("btnDelStatist.Image")));
            this.btnDelStatist.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDelStatist.Name = "btnDelStatist";
            this.btnDelStatist.Size = new System.Drawing.Size(68, 68);
            this.btnDelStatist.Text = "Clear statistics";
            this.btnDelStatist.Click += new System.EventHandler(this.btnDelStatist_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Location = new System.Drawing.Point(0, 74);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panEmotion);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.pbCam);
            this.splitContainer1.Panel2.Controls.Add(this.pbImg);
            this.splitContainer1.Panel2.Controls.Add(this.zedAge);
            this.splitContainer1.Size = new System.Drawing.Size(696, 443);
            this.splitContainer1.SplitterDistance = 122;
            this.splitContainer1.TabIndex = 1;
            // 
            // panEmotion
            // 
            this.panEmotion.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panEmotion.AutoScroll = true;
            this.panEmotion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panEmotion.Location = new System.Drawing.Point(4, 4);
            this.panEmotion.Name = "panEmotion";
            this.panEmotion.Size = new System.Drawing.Size(113, 434);
            this.panEmotion.TabIndex = 1;
            // 
            // pbImg
            // 
            this.pbImg.Location = new System.Drawing.Point(4, 4);
            this.pbImg.Name = "pbImg";
            this.pbImg.Size = new System.Drawing.Size(153, 111);
            this.pbImg.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbImg.TabIndex = 1;
            this.pbImg.TabStop = false;
            // 
            // zedAge
            // 
            this.zedAge.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.zedAge.Location = new System.Drawing.Point(4, 4);
            this.zedAge.Name = "zedAge";
            this.zedAge.ScrollGrace = 0D;
            this.zedAge.ScrollMaxX = 0D;
            this.zedAge.ScrollMaxY = 0D;
            this.zedAge.ScrollMaxY2 = 0D;
            this.zedAge.ScrollMinX = 0D;
            this.zedAge.ScrollMinY = 0D;
            this.zedAge.ScrollMinY2 = 0D;
            this.zedAge.Size = new System.Drawing.Size(561, 434);
            this.zedAge.TabIndex = 0;
            this.zedAge.UseExtendedPrintDialog = true;
            // 
            // btnShowHide
            // 
            this.btnShowHide.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnShowHide.Image = ((System.Drawing.Image)(resources.GetObject("btnShowHide.Image")));
            this.btnShowHide.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnShowHide.Name = "btnShowHide";
            this.btnShowHide.Size = new System.Drawing.Size(68, 68);
            this.btnShowHide.Text = "Show/hide video";
            this.btnShowHide.Click += new System.EventHandler(this.btnShowHide_Click);
            // 
            // pbCam
            // 
            this.pbCam.Location = new System.Drawing.Point(4, 4);
            this.pbCam.Name = "pbCam";
            this.pbCam.Size = new System.Drawing.Size(313, 83);
            this.pbCam.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbCam.TabIndex = 2;
            this.pbCam.TabStop = false;
            this.pbCam.Visible = false;
            // 
            // frmFaceAnalytics
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(696, 517);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.tsMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmFaceAnalytics";
            this.Text = "Face Analytics";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.frmFaceAnalytics_Load);
            this.tsMain.ResumeLayout(false);
            this.tsMain.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbImg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCam)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip tsMain;
        private System.Windows.Forms.ToolStripDropDownButton tsSelCam;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnStart;
        private System.Windows.Forms.ToolStripButton btnStop;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnDelStatist;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private ZedGraph.ZedGraphControl zedAge;
        private System.Windows.Forms.ToolStripButton btnFromFile;
        private System.Windows.Forms.Panel panEmotion;
        private System.Windows.Forms.PictureBox pbImg;
        private System.Windows.Forms.ToolStripButton btnShowHide;
        private System.Windows.Forms.PictureBox pbCam;
    }
}