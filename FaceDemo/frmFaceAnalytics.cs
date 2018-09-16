using AForge.Video.DirectShow;
using Microsoft.ProjectOxford.Common.Contract;
using Microsoft.ProjectOxford.Face;
using Microsoft.ProjectOxford.Face.Contract;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;

namespace FaceDemo
{
    public partial class frmFaceAnalytics : Form
    {

        //CONFIG - Edit Api Key
        const string faceAPIKey = "YOUR_API_KEY";
        private readonly IFaceServiceClient faceServiceClient =
                new FaceServiceClient(faceAPIKey, "https://brazilsouth.api.cognitive.microsoft.com/face/v1.0");

        const double reqTimeInSeconds = 4;


        public frmFaceAnalytics()
        {
            InitializeComponent();
        }

        private void frmFaceAnalytics_Load(object sender, EventArgs e)
        {
            FilterInfoCollection devs = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            foreach (FilterInfo x in devs) tsSelCam.DropDownItems.Add(x.Name);

            if (tsSelCam.DropDownItems.Count > 0)
            {
                tsSelCam.Text = tsSelCam.DropDownItems[0].Text;
                SelectedCamera = 0;
            }

            int factor = 100 / ageDistributionMen.Length;
            for (int k = 0; k < ageDistribString.Length; k++)
            {
                ageDistribString[k] = (factor * k).ToString() + " - " + (factor *(k + 1)).ToString();
                ageDistributionX[k] = factor * k;
            }

            zedAge.GraphPane.XAxis.Scale.TextLabels = ageDistribString;
            zedAge.GraphPane.XAxis.Type = AxisType.Text;
            zedAge.GraphPane.XAxis.Scale.FontSpec.Angle = 90;
            zedAge.GraphPane.XAxis.MajorTic.IsBetweenLabels = true;

            zedAge.GraphPane.Title.Text = "Estimated age distribution";
            zedAge.GraphPane.XAxis.Title.Text = "Estimated age";
            zedAge.GraphPane.YAxis.Title.Text = "Quantity";
            zedAge.GraphPane.Legend.Position = LegendPos.BottomCenter;

            zedAge.AxisChange();

            LoadEmotions();
            UpdateGraphics();
        }

        #region Data analytics
        double[] ageDistributionMen = new double[20];
        double[] ageDistributionWomen = new double[20];
        double[] ageDistributionX = new double[20];
        string[] ageDistribString = new string[20];

        /// <summary>Distribution of emotions</summary>
        int[] emotionDistribution = new int[8];

        private void UpdateGraphics()
        {
            zedAge.GraphPane.CurveList.Clear();
            zedAge.GraphPane.AddBar("Men", ageDistributionX, ageDistributionMen, Color.Blue);
            zedAge.GraphPane.AddBar("Women", ageDistributionX, ageDistributionWomen, Color.Pink);

            zedAge.AxisChange();
            zedAge.Refresh();

            int nEmotions = emotionDistribution.Sum();
            int maxW = panEmotion.Width - lstEmotions[0].Width - 20;
            if (nEmotions > 0)
            {
                for (int i = 0; i < Emotions.Length; i++)
                {
                    lstEmotionValues[i].Text = emotionDistribution[i].ToString();
                    lstEmotionValues[i].Width = Math.Max(30, (int)(maxW * (float)emotionDistribution[i] / (float)nEmotions));
                }
            }
            else
            {
                for (int i = 0; i < Emotions.Length; i++)
                {
                    lstEmotionValues[i].Text = "0";
                    lstEmotionValues[i].Width = maxW;
                }
            }
        }

        string[] Emotions = new string[] { "happiness", "neutral", "sadness", "disgust", "anger", "contempt", "fear", "surprise" };
        List<PictureBox> lstEmotions = new List<PictureBox>();
        List<System.Windows.Forms.Label> lstEmotionValues = new List<System.Windows.Forms.Label>();
        private void LoadEmotions()
        {
            int h = 0;
            int heights = panEmotion.Height / 8;
            foreach (string s in Emotions)
            {
                Bitmap bmp = new Bitmap(Application.StartupPath + "\\" + s + ".png");
                Graphics g = Graphics.FromImage(bmp);
                g.DrawString(s, new Font("Arial", 15, FontStyle.Regular), Brushes.Black, 0, 0);

                PictureBox pb = new PictureBox();
                pb.SizeMode = PictureBoxSizeMode.StretchImage;
                pb.Width = heights; pb.Height = heights;
                pb.Image = bmp;

                pb.Top = h;
                h += pb.Height;

                panEmotion.Controls.Add(pb);
                lstEmotions.Add(pb);

                //scales
                System.Windows.Forms.Label pbLevel = new System.Windows.Forms.Label();
                pbLevel.AutoSize = false;
                pbLevel.Height = heights >> 2;
                pbLevel.Width = panEmotion.Width - pb.Width - 30;
                pbLevel.Top = pb.Top + pb.Height / 4;
                pbLevel.Left = pb.Left + pb.Width;

                pbLevel.BorderStyle = BorderStyle.FixedSingle;
                pbLevel.BackColor = Color.LightGreen;

                pbLevel.Font = new Font("Arial", 12, FontStyle.Bold);
                pbLevel.TextAlign = ContentAlignment.MiddleCenter;

                pbLevel.Text = "0";

                panEmotion.Controls.Add(pbLevel);
                lstEmotionValues.Add(pbLevel);
            }
        }

        float threshEmotion = 0.3f;
        private void UpdateStatistics(Face[] facesData)
        {
            foreach(Face f in facesData)
            {
                double ageEstim = f.FaceAttributes.Age;

                int factor = 100 / ageDistributionMen.Length;

                int bucket = (int)Math.Floor(ageEstim / (double)factor);
                if (bucket < 0) bucket = 0; if (bucket >= ageDistributionX.Length) bucket = ageDistributionX.Length - 1;

                if (f.FaceAttributes.Gender == "male") ageDistributionMen[bucket]++;
                else ageDistributionWomen[bucket]++;

                if (f.FaceAttributes.Emotion.Happiness > threshEmotion) emotionDistribution[0]++;
                if (f.FaceAttributes.Emotion.Neutral > threshEmotion) emotionDistribution[1]++;
                if (f.FaceAttributes.Emotion.Sadness > threshEmotion) emotionDistribution[2]++;
                if (f.FaceAttributes.Emotion.Disgust > threshEmotion) emotionDistribution[3]++;
                if (f.FaceAttributes.Emotion.Anger > threshEmotion) emotionDistribution[4]++;
                if (f.FaceAttributes.Emotion.Contempt > threshEmotion) emotionDistribution[5]++;
                if (f.FaceAttributes.Emotion.Fear > threshEmotion) emotionDistribution[6]++;
                if (f.FaceAttributes.Emotion.Surprise > threshEmotion) emotionDistribution[7]++;
            }

            UpdateGraphics();
        }

        private void btnDelStatist_Click(object sender, EventArgs e)
        {
            for (int k = 0; k < emotionDistribution.Length; k++) emotionDistribution[k] = 0;

            for (int k = 0; k < ageDistributionMen.Length; k++)
            {
                ageDistributionMen[k] = 0;
                ageDistributionWomen[k] = 0;
            }

            UpdateGraphics();
        }
        #endregion 

        #region Face detection


        Face[] faces;

        Font fnt = new Font("Arial", 10, FontStyle.Bold);
        private async Task<Bitmap> DetectImgsInFace(string filePath)
        {
            Bitmap bitmapSource = new Bitmap(filePath);

            string dateTimeString = DateTime.Now.ToString();
            faces = await UploadAndDetectFaces(filePath);
            string result = String.Format("Detection Finished. {0} face(s) detected", faces.Length);

            Graphics g = Graphics.FromImage(bitmapSource);
            g.DrawString("Envio: " + dateTimeString + "\r\nRetorno: " + DateTime.Now.ToString(), fnt, Brushes.Red, 0, 0);
            //foreach (Face f in faces)
            //{
            //    PictureBox pb = new PictureBox();
            //    pb.Image = ExtractFace(bitmapSource, f);
            //    toolTip.SetToolTip(pb, FaceDescription(f));
            //    pb.BorderStyle = BorderStyle.FixedSingle;
            //    pb.SizeMode = PictureBoxSizeMode.AutoSize;
            //    pb.MouseEnter += Pb_MouseEnter;
            //    flowPan.Controls.Add(pb);
            //}
            string allFacesData = "";
            foreach (Face f in faces)
            {
                Pen p = new Pen(Brushes.Pink);
                p.Width = 3;
                if (f.FaceAttributes.Gender == "male") p.Color = Color.Blue;

                g.DrawRectangle(p, f.FaceRectangle.Left, f.FaceRectangle.Top, f.FaceRectangle.Width, f.FaceRectangle.Height);

                string allInfo = Newtonsoft.Json.JsonConvert.SerializeObject(f);
                allFacesData += allInfo + "\r\n";

                //emotion string
                string emoteString = f.FaceAttributes.Age.ToString() + ", " + f.FaceAttributes.Emotion.ToRankedList().FirstOrDefault().Key;
                foreach (Accessory ac in f.FaceAttributes.Accessories)
                {
                    emoteString += "\r\n" + ac.Type.ToString() + ": " + ac.Confidence.ToString();
                }

                SizeF s = g.MeasureString(emoteString, fnt);
                g.FillRectangle(Brushes.White, f.FaceRectangle.Left, f.FaceRectangle.Top + f.FaceRectangle.Height, s.Width, s.Height);
                g.DrawString(emoteString, fnt, Brushes.Black, f.FaceRectangle.Left, f.FaceRectangle.Top + f.FaceRectangle.Height);


                FaceLandmarks lm = f.FaceLandmarks;

                g.DrawEllipse(Pens.Red, (float)lm.NoseTip.X - 1, (float)lm.NoseTip.Y - 1, 3, 3);

                g.DrawEllipse(Pens.Red, (float)lm.PupilLeft.X - 1, (float)lm.PupilLeft.Y - 1, 3, 3);
                g.DrawEllipse(Pens.Red, (float)lm.EyeLeftTop.X - 1, (float)lm.EyeLeftTop.Y - 1, 3, 3);
                g.DrawEllipse(Pens.Red, (float)lm.EyeLeftBottom.X - 1, (float)lm.EyeLeftBottom.Y - 1, 3, 3);

                g.DrawEllipse(Pens.Red, (float)lm.PupilRight.X - 1, (float)lm.PupilRight.Y - 1, 3, 3);
                g.DrawEllipse(Pens.Red, (float)lm.EyeRightTop.X - 1, (float)lm.EyeRightTop.Y - 1, 3, 3);
                g.DrawEllipse(Pens.Red, (float)lm.EyeRightBottom.X - 1, (float)lm.EyeRightBottom.Y - 1, 3, 3);

                g.DrawEllipse(Pens.Red, (float)lm.MouthLeft.X - 1, (float)lm.MouthLeft.Y - 1, 3, 3);
                g.DrawEllipse(Pens.Red, (float)lm.MouthRight.X - 1, (float)lm.MouthRight.Y - 1, 3, 3);
            }
            try { File.WriteAllText(filePath + ".txt", allFacesData); }
            catch { }
            

            //picFace.Image = bitmapSource;
            return bitmapSource;
        }


        private Bitmap ExtractFace(Bitmap myBitmap, Face f, int desiredWidth)
        {
            // Clone a portion of the Bitmap object.
            Rectangle cloneRect = new Rectangle(f.FaceRectangle.Left, f.FaceRectangle.Top, f.FaceRectangle.Width, f.FaceRectangle.Height);
            System.Drawing.Imaging.PixelFormat format =
                myBitmap.PixelFormat;
            Bitmap cloneBitmap = myBitmap.Clone(cloneRect, format);

            int w = desiredWidth;
            return new Bitmap(cloneBitmap, w, cloneBitmap.Height * w / cloneBitmap.Width);
        }

        private string FaceDescription(Face face)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("Face: ");

            // Add the gender, age, and smile.
            sb.Append(face.FaceAttributes.Gender);
            sb.Append(", ");
            sb.Append(face.FaceAttributes.Age);
            sb.Append(", ");
            sb.Append(String.Format("smile {0:F1}%, ", face.FaceAttributes.Smile * 100));

            // Add the emotions. Display all emotions over 10%.
            sb.Append("Emotion: ");
            EmotionScores emotionScores = face.FaceAttributes.Emotion;
            if (emotionScores.Anger >= 0.1f) sb.Append(String.Format("anger {0:F1}%, ", emotionScores.Anger * 100));
            if (emotionScores.Contempt >= 0.1f) sb.Append(String.Format("contempt {0:F1}%, ", emotionScores.Contempt * 100));
            if (emotionScores.Disgust >= 0.1f) sb.Append(String.Format("disgust {0:F1}%, ", emotionScores.Disgust * 100));
            if (emotionScores.Fear >= 0.1f) sb.Append(String.Format("fear {0:F1}%, ", emotionScores.Fear * 100));
            if (emotionScores.Happiness >= 0.1f) sb.Append(String.Format("happiness {0:F1}%, ", emotionScores.Happiness * 100));
            if (emotionScores.Neutral >= 0.1f) sb.Append(String.Format("neutral {0:F1}%, ", emotionScores.Neutral * 100));
            if (emotionScores.Sadness >= 0.1f) sb.Append(String.Format("sadness {0:F1}%, ", emotionScores.Sadness * 100));
            if (emotionScores.Surprise >= 0.1f) sb.Append(String.Format("surprise {0:F1}%, ", emotionScores.Surprise * 100));

            // Add glasses.
            sb.Append(face.FaceAttributes.Glasses);
            sb.Append(", ");

            // Add hair.
            sb.Append("Hair: ");

            // Display baldness confidence if over 1%.
            if (face.FaceAttributes.Hair.Bald >= 0.01f)
                sb.Append(String.Format("bald {0:F1}% ", face.FaceAttributes.Hair.Bald * 100));

            // Display all hair color attributes over 10%.
            HairColor[] hairColors = face.FaceAttributes.Hair.HairColor;
            foreach (HairColor hairColor in hairColors)
            {
                if (hairColor.Confidence >= 0.1f)
                {
                    sb.Append(hairColor.Color.ToString());
                    sb.Append(String.Format(" {0:F1}% ", hairColor.Confidence * 100));
                }
            }

            // Return the built string.
            return sb.ToString();
        }

        private async Task<Face[]> UploadAndDetectFaces(string imageFilePath)
        {
            // The list of Face attributes to return.
            IEnumerable<FaceAttributeType> faceAttributes =
                new FaceAttributeType[] { FaceAttributeType.Gender, FaceAttributeType.Age, FaceAttributeType.Smile, FaceAttributeType.Emotion, FaceAttributeType.Glasses,
                    FaceAttributeType.Hair, FaceAttributeType.Accessories , FaceAttributeType.FacialHair, FaceAttributeType.Occlusion };

            // Call the Face API.
            try
            {
                using (Stream imageFileStream = File.OpenRead(imageFilePath))
                {
                    Face[] faces = await faceServiceClient.DetectAsync(imageFileStream, returnFaceId: true, returnFaceLandmarks: true, returnFaceAttributes: faceAttributes);
                    return faces;
                }
            }
            // Catch and display Face API errors.
            catch (FaceAPIException f)
            {
                MessageBox.Show(f.ErrorMessage, f.ErrorCode);
                return new Face[0];
            }
            // Catch and display all other errors.
            catch (Exception e)
            {
                //MessageBox.Show(e.Message, "Error");
                return new Face[0];
            }
        }
        #endregion


        #region Display and process webcam image
        private async void ProcessBmp()
        {
            string filePath = Application.StartupPath + "\\temp" + kFile.ToString() + ".png";
            if (File.Exists(filePath)) File.Delete(filePath);
            kFile++;
            bmp.Save(filePath, System.Drawing.Imaging.ImageFormat.Png);
            Bitmap bmp2 = await DetectImgsInFace(filePath);
            try
            {
                kFile++;
                bmp2.Save(Application.StartupPath + "\\tempProc" + kFile.ToString() + ".png", System.Drawing.Imaging.ImageFormat.Png);
            }
            catch
            {
                this.Text = "Erro ao salvar " + Application.StartupPath + "\\tempProc" + kFile.ToString() + ".png";
            }

            int desiredW = splitContainer1.Panel2.Width >> 2;
            pbImg.Image = bmpResizer(bmp2, desiredW, bmp2.Height * desiredW / bmp2.Width); ;

            UpdateStatistics(faces);
        }

        int SelectedCamera = -1;
        private void tsSelCam_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            SelectedCamera = tsSelCam.DropDownItems.IndexOf(e.ClickedItem);
        }

        bool fechando = false;
        VideoCaptureDevice vcd;
        private void StartCam()
        {
            delegUpdtPic = UpdtPic;
            FilterInfoCollection devs = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            if (devs.Count > 0)
            {
                vcd = new VideoCaptureDevice(devs[SelectedCamera].MonikerString);
                //vcd = new VideoCaptureDevice(devs[devs.Count - 1].MonikerString);
                vcd.NewFrame += str_NewFrame;
                vcd.Start();
                //vcd.DesiredFrameRate = 10;
                //vcd.DesiredFrameSize = new Size(800, 600);
            }
            else MessageBox.Show("Unable to find webcam", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        Bitmap bmp;
        int bmpWidth, bmpHeight;

        bool terminou = true;

        System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
        int nframes = 0;
        void str_NewFrame(object sender, AForge.Video.NewFrameEventArgs eventArgs)
        {
            if (!terminou) return;
            terminou = false;

            if (!sw.IsRunning) sw.Start();
            nframes++;

            bmp = (Bitmap)eventArgs.Frame;//.Clone();
            //bmp = CLHoughTransg.CLHoughTransform.TestFuncs.MedianFilter(bmp);

            //bmp.RotateFlip(RotateFlipType.RotateNoneFlipX);

            if (bmpWidth == 0)
            {
                bmpWidth = bmp.Width;
                bmpHeight = bmp.Height;
            }


            //bmp = CLHoughTransg.CLHoughTransform.TestFuncs.MedianFilter(bmp);
            //List<float[]> interestPts;
            //bmp = BlockDetector.GetInterestPtsVisualization(bmp, out interestPts);

            //Tries to form interest points
            //ProcessBMP(ref bmp);
            try
            {
                if (!fechando) this.Invoke(delegUpdtPic);
            }
            catch { }
            //pic.Image = bmp; // imgdt.WriteData(bmp);
            //pic.Refresh();
            bmp.Dispose();

            terminou = true;
        }

        private void CloseVIdeo()
        {
            if (vcd != null)
            {
                if (vcd.IsRunning)
                {
                    vcd.SignalToStop();
                    vcd = null;
                }
            }
        }

        delegate void voidFunc();
        voidFunc delegUpdtPic;


        System.Diagnostics.Stopwatch sw2 = new System.Diagnostics.Stopwatch();
        double lastUpdt = 0;
        int kFile = 0;
        private void UpdtPic()
        {
            if (pbCam.Visible)
            {
                pbCam.Image = (Bitmap)bmp.Clone();
                pbCam.Refresh();
            }

            this.Text = ((double)nframes / (double)sw.Elapsed.TotalSeconds).ToString() + " fps";

            if (!sw2.IsRunning) sw2.Start();
            if (sw.Elapsed.TotalSeconds - lastUpdt > reqTimeInSeconds)
            {
                lastUpdt = sw.Elapsed.TotalSeconds;

                ProcessBmp();

            }
        }



        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DirectoryInfo di = new DirectoryInfo(Application.StartupPath);
            FileInfo[] fis = di.GetFiles("temp*.png");
            foreach (FileInfo f in fis)
            {
                try
                {
                    File.Delete(f.FullName);
                }
                catch
                {

                }
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            btnFromFile.Enabled = true;
            CloseVIdeo();
            btnStart.Enabled = true;
        }


        private void btnStart_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = false;
            btnFromFile.Enabled = false;
            StartCam();
        }


        #endregion

        #region Load from file
        private void btnFromFile_Click(object sender, EventArgs e)
        {
            // Get the image file to scan from the user.
            var openDlg = new OpenFileDialog();

            openDlg.Filter = "Image|*.jpg;*.png;*.bmp";

            openDlg.Multiselect = true;

            if (openDlg.ShowDialog() == DialogResult.OK)
            {
                foreach (string s in openDlg.FileNames)
                {
                    // Display the image file.
                    string filePath = s;

                    Bitmap bitmapSource = new Bitmap(filePath);

                    if (bitmapSource.Width > 1300)
                    {
                        Bitmap bmpResize = bmpResizer(bitmapSource, 1300, bitmapSource.Height * 1300 / bitmapSource.Width);

                        bitmapSource = bmpResize;
                    }

                    bmp = bitmapSource;
                    ProcessBmp();

                    System.Threading.Thread.Sleep(1000 * (int)reqTimeInSeconds);
                }
            }
        }

        private void btnShowHide_Click(object sender, EventArgs e)
        {
            pbCam.Visible = !pbCam.Visible;
        }

        private Bitmap bmpResizer(Bitmap img, int newWidth, int newHeight)
        {
            Bitmap newImage = new Bitmap(newWidth, newHeight);
            using (Graphics gr = Graphics.FromImage(newImage))
            {
                gr.SmoothingMode = SmoothingMode.HighQuality;
                gr.InterpolationMode = InterpolationMode.HighQualityBicubic;
                gr.PixelOffsetMode = PixelOffsetMode.HighQuality;
                gr.DrawImage(img, new Rectangle(0, 0, newWidth, newHeight));
            }
            return newImage;
        }
        #endregion 

    }
}
