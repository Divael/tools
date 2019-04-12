using System;
using System.Windows;
using System.Windows.Media.Imaging;
using AForge.Video.DirectShow;
using Tools.WPF;

namespace FaceCapture
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        BitmapSource ImagePlay;
        BitmapSource ImageStop;

        public MainWindow()
        {
            InitializeComponent();

            // 设置窗体图标
            this.Icon = System.Windows.Interop.Imaging.CreateBitmapSourceFromHIcon(
                Properties.Resources.FingerPictureBox.Handle,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());

            // 图像源初始化
            ImagePlay = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                Properties.Resources.Button_Play_icon2.GetHbitmap(),
                IntPtr.Zero,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());

            ImageStop = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                Properties.Resources.Button_Stop_icon.GetHbitmap(),
                IntPtr.Zero,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());

            // 设置按钮图像
            image_Play.Source = ImagePlay;
            image_Capture.Source = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                Properties.Resources.capture.GetHbitmap(),
                IntPtr.Zero,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());            

            // 设置窗体装载后事件处理器
            this.Loaded += new RoutedEventHandler(MainWindow_Loaded);
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // 设定初始视频设备
            FilterInfoCollection videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            if (videoDevices.Count > 0)
            {   // 默认设备
                sourcePlayer.VideoSource = new VideoCaptureDevice(videoDevices[0].MonikerString);                
            }
            else
            {
                button_Play.IsEnabled = false;
                button_Capture.IsEnabled = false;
            }

            // 设置图片框初始图像
            BitmapSource bs = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                Properties.Resources.noimage.GetHbitmap(),
                IntPtr.Zero,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());

            fingerPictureBox1.InitialImage = bs;
            fingerPictureBox2.InitialImage = bs;
            fingerPictureBox3.InitialImage = bs;
            fingerPictureBox4.InitialImage = bs;
        }

        private void button_Play_Click(object sender, RoutedEventArgs e)
        {            
            if (image_Play.Source == ImagePlay)
            {   // 开启视频
                sourcePlayer.Start();
                if (sourcePlayer.IsRunning)
                {
                    // 改变按钮为“停止”状态
                    image_Play.Source = ImageStop;
                    label_Play.Content = "停止";

                    // 允许拍照
                    button_Capture.IsEnabled = true;
                }
            }  
            else
            {
                if (sourcePlayer.IsRunning)
                {   // 停止视频
                    sourcePlayer.SignalToStop();
                    sourcePlayer.WaitForStop();

                    // 改变按钮为“开始”状态
                    image_Play.Source = ImagePlay;
                    label_Play.Content = "开启摄像头"; ;

                    // 关闭拍照
                    button_Capture.IsEnabled = false;
                }                
            }
        }

        private void button_Capture_Click(object sender, RoutedEventArgs e)
        {
            // 判断视频设备是否开启
            if (sourcePlayer.IsRunning)
            {   // 进行拍照
                for (Int32 i = 1; i <= 4; i++)
                {
                    object box = this.FindName("fingerPictureBox" + i);
                    if(box is FingerPictureBox)
                    {
                        if ((box as FingerPictureBox).ActiveImage == (box as FingerPictureBox).InitialImage)
                        {   // 更新图像
                            (box as FingerPictureBox).ActiveImage = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                                sourcePlayer.GetCurrentVideoFrame().GetHbitmap(),//获取bitmap图像
                                IntPtr.Zero,
                                Int32Rect.Empty,
                                BitmapSizeOptions.FromEmptyOptions());
                            break;
                        }                    
                    }
                }
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (sourcePlayer.IsRunning)
            {   // 停止视频
                sourcePlayer.SignalToStop();
                sourcePlayer.WaitForStop();
            }
        }
    }
}
