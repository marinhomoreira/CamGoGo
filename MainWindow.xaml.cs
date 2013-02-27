using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.IO;
using System.Media;

using System.Windows.Interop;
using System.Drawing.Imaging;

using TouchlessLib;

namespace CamGoGo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        TouchlessMgr tm;

        public MainWindow()
        {
            InitializeComponent();

            this.tm = new TouchlessMgr();

            this.tm.CurrentCamera = this.tm.Cameras[0];
            this.tm.CurrentCamera.OnImageCaptured += new EventHandler<CameraEventArgs>(CurrentCamera_OnImageCaptured);
        }

        void CurrentCamera_OnImageCaptured(object sender, CameraEventArgs e)
        {
            this.Dispatcher.Invoke(new Action(delegate()
            {
                this.webcam.Source = Imaging.CreateBitmapSourceFromHBitmap(e.Image.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());
            }));
        }

        private void snapshotBtn_Click(object sender, RoutedEventArgs e)
        {
            System.Drawing.Bitmap bp = this.tm.CurrentCamera.GetCurrentImage();
            BitmapSource bs = Imaging.CreateBitmapSourceFromHBitmap(bp.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());

            encode(bs);

            this.Dispatcher.Invoke(new Action(delegate()
            {
                this.snapshot.Source = bs;

            }));
        }

        void encode(BitmapSource image)
        {
            String fName = this.textBox1.Text;
            FileStream stream = new FileStream(fName+".jpg", FileMode.Create);
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.FlipHorizontal = true;
            encoder.FlipVertical = false;
            encoder.QualityLevel = 100;
            //encoder.Rotation = Rotation.Rotate90;
            encoder.Frames.Add(BitmapFrame.Create(image));
            encoder.Save(stream);
        }



    }
}
