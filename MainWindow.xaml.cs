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
    }
}
