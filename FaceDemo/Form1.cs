using Microsoft.ProjectOxford.Common.Contract;
using Microsoft.ProjectOxford.Face;
using Microsoft.ProjectOxford.Face.Contract;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using AForge.Video.DirectShow;
using System.Drawing.Drawing2D;

namespace FaceDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private readonly IFaceServiceClient faceServiceClient =
                new FaceServiceClient("a02910e6808d4edaa25f3ccdd4bd5cde", "https://brazilsouth.api.cognitive.microsoft.com/face/v1.0");

        Face[] faces;                   // The list of detected faces.
        //String[] faceDescriptions;      // The list of descriptions for the detected faces.
        //double resizeFactor;            // The resize factor for the displayed image.

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private async void fromimageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Get the image file to scan from the user.
            var openDlg = new OpenFileDialog();

            openDlg.Filter = "Image|*.jpg;*.png;*.bmp";
            if (openDlg.ShowDialog() == DialogResult.OK)
            {
                // Display the image file.
                string filePath = openDlg.FileName;

                Bitmap bitmapSource = new Bitmap(filePath);

                if (bitmapSource.Width > 1300)
                {
                    Bitmap bmpResize = bmpResizer(bitmapSource, 1300, bitmapSource.Height * 1300 / bitmapSource.Width);

                    filePath = Application.StartupPath + "\\temp" + kFile.ToString() + ".png";
                    kFile++;
                    bmpResize.Save(filePath, System.Drawing.Imaging.ImageFormat.Png);

                    bitmapSource = bmpResize;
                }


                picFace.Image = bitmapSource;

                await DetectImgsInFace(filePath);

                

            }


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

        private async Task<Bitmap> DetectImgsInFace(string filePath)
        {
            Bitmap bitmapSource = new Bitmap(filePath);
            faces = await UploadAndDetectFaces(filePath);
            string result = String.Format("Detection Finished. {0} face(s) detected", faces.Length);

            Graphics g = Graphics.FromImage(bitmapSource);
            foreach (Face f in faces)
            {
                PictureBox pb = new PictureBox();
                pb.Image = ExtractFace(bitmapSource, f);
                toolTip.SetToolTip(pb, FaceDescription(f));
                pb.BorderStyle = BorderStyle.FixedSingle;
                pb.SizeMode = PictureBoxSizeMode.AutoSize;
                pb.MouseEnter += Pb_MouseEnter;
                flowPan.Controls.Add(pb);
            }
            foreach (Face f in faces)
            {
                Pen p = new Pen(Brushes.Pink);
                p.Width = 3;
                if (f.FaceAttributes.Gender == "male") p.Color = Color.Blue;

                g.DrawRectangle(p, f.FaceRectangle.Left, f.FaceRectangle.Top, f.FaceRectangle.Width, f.FaceRectangle.Height);

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

            picFace.Image = bitmapSource;
            return bitmapSource;
        }

        private void Pb_MouseEnter(object sender, EventArgs e)
        {
            string s = toolTip.GetToolTip((PictureBox)sender);
            lblStatus.Text = s;
        }

        private Bitmap ExtractFace(Bitmap myBitmap, Face f)
        {
            // Clone a portion of the Bitmap object.
            Rectangle cloneRect = new Rectangle(f.FaceRectangle.Left, f.FaceRectangle.Top, f.FaceRectangle.Width, f.FaceRectangle.Height);
            System.Drawing.Imaging.PixelFormat format =
                myBitmap.PixelFormat;
            Bitmap cloneBitmap = myBitmap.Clone(cloneRect, format);

            int w = flowPan.Width - 40;
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
                new FaceAttributeType[] { FaceAttributeType.Gender, FaceAttributeType.Age, FaceAttributeType.Smile, FaceAttributeType.Emotion, FaceAttributeType.Glasses, FaceAttributeType.Hair };

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
                MessageBox.Show(e.Message, "Error");
                return new Face[0];
            }
        }



        #region Display and process webcam image

        bool fechando = false;
        VideoCaptureDevice vcd;
        private void StartCam()
        {
            delegUpdtPic = UpdtPic;
            FilterInfoCollection devs = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            if (devs.Count > 0)
            {
                vcd = new VideoCaptureDevice(devs[0].MonikerString);
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
        private async void UpdtPic()
        {
            picFace.Image = bmp;
            picFace.Refresh();

            this.Text = ((double)nframes / (double)sw.Elapsed.TotalSeconds).ToString() + " fps";

            if (!sw2.IsRunning) sw2.Start();
            if (sw.Elapsed.TotalSeconds - lastUpdt > 4)
            {
                lastUpdt = sw.Elapsed.TotalSeconds;
                string filePath = Application.StartupPath + "\\temp" +kFile.ToString() + ".png";
                if (File.Exists(filePath)) File.Delete(filePath);
                kFile++;
                bmp.Save(filePath, System.Drawing.Imaging.ImageFormat.Png);

                Bitmap bmp2 = await DetectImgsInFace(filePath);
                bmp2.Save(Application.StartupPath + "\\tempProc" + kFile.ToString() + ".png", System.Drawing.Imaging.ImageFormat.Png);
            }
        }

        private void closeVideoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseVIdeo();
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

        private void fromCameraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StartCam();
        }

        #endregion


    }
}
