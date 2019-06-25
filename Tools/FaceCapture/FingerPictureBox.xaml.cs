/* ----------------------------------------------------------
文件名称：FingerPictureBox.xaml.cs

作者：秦建辉

MSN：splashcn@msn.com
QQ：36748897

博客：http://blog.csdn.net/jhqin

开发环境：
    Visual Studio V2010
    .NET Framework 4 Client Profile

产品类型：
    基于WPF的图片框控件
 
版本历史：
    V1.0	2011年07月19日
			基于WPF，实现一个具有拖入和删除功能的图片框控件
------------------------------------------------------------ */
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FaceCapture
{
    /// <summary>
    /// FingerPictureBox.xaml 的交互逻辑
    /// </summary>
    public partial class FingerPictureBox : UserControl
    {
        /// <summary>
        /// MouseDown位置信息
        /// </summary>
        private Point previousMousePoint;

        /// <summary>
        /// 初始图像
        /// </summary>
        private ImageSource _InitialImage = null;

        public FingerPictureBox()
        {
            InitializeComponent();         

            // 设置装载事件处理器
            this.Loaded += new RoutedEventHandler(FingerPictureBox_Loaded);
        }

        private void FingerPictureBox_Loaded(object sender, RoutedEventArgs e)
        {
            // 设置拖动属性
            this.AllowDrop = true;

            // 设置裁剪属性
            this.ClipToBounds = true;

            // 设置拖动事件处理器
            this.DragEnter += new DragEventHandler(FingerPictureBox_DragEnter);
            this.Drop += new DragEventHandler(FingerPictureBox_Drop);

            // 设置各元素大小和位置
            this.image1.Width = this.canvas1.Width = this.Width - this.BorderThickness.Left - this.BorderThickness.Right;
            this.image1.Height = this.canvas1.Height = this.Height - this.BorderThickness.Top - this.BorderThickness.Bottom;
            Canvas.SetLeft(this.image1, 0);
            Canvas.SetTop(this.image1, 0);

            // 设置图像框伸展属性
            this.image1.Stretch = System.Windows.Media.Stretch.Fill;
            this.image1.StretchDirection = StretchDirection.Both;    
        }

        private void FingerPictureBox_DragEnter(object sender, DragEventArgs e)
        {
            // 拖放时显示的效果
            e.Effects = DragDropEffects.Link;            
        }

        private void FingerPictureBox_Drop(object sender, DragEventArgs e)
        {
            //  获取拖入的文件
            String[] DropFiles = (String[])(e.Data.GetData(DataFormats.FileDrop));
            if (DropFiles != null)
            {   // 设置控件图像
                BitmapImage DropImage = new BitmapImage();
                DropImage.BeginInit();
                DropImage.UriSource = new Uri(DropFiles[0]);                
                DropImage.EndInit();

                // 设置活动图像
                this.ActiveImage = DropImage; 
            }
        }

        private void FingerPictureBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // 记录鼠标单击时的初始位置
            previousMousePoint = e.GetPosition(this);

            // 设置鼠标捕获
            this.image1.CaptureMouse();            
        }

        private void FingerPictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Point currentMousePoint = e.GetPosition(this);
                Canvas.SetLeft(sender as Image, currentMousePoint.X - previousMousePoint.X);
                Canvas.SetTop(sender as Image, currentMousePoint.Y - previousMousePoint.Y);
            }
        }

        private void FingerPictureBox_MouseUp(object sender, MouseButtonEventArgs e)
        {
            // 释放鼠标捕获
            this.image1.ReleaseMouseCapture();

            // 判断图像是否移出边界
            Double Left = Canvas.GetLeft(sender as Image);
            Double Top = Canvas.GetTop(sender as Image);
            if (Left > this.Width || Left < -this.Width ||
                Top > this.Height || Top < -this.Height)
            {
                // 图像已经移出边界，恢复初始图像
                this.ActiveImage = _InitialImage;
            }

            // 图片框复位
            Canvas.SetLeft(sender as Image, 0);
            Canvas.SetTop(sender as Image, 0);
        }

        /// <summary>
        /// 图像伸展方式
        /// </summary>
        public Stretch ImageLayout
        {
            get
            {
                return this.image1.Stretch;
            }
            set
            {
                this.image1.Stretch = value;
            }
        }

        /// <summary>
        /// 初始图像
        /// </summary>
        public ImageSource InitialImage
        {
            get
            {
                return _InitialImage;
            }
            set
            {
                this.image1.Source = _InitialImage = value;
            }
        }

        /// <summary>
        /// 活动图像
        /// </summary>
        public ImageSource ActiveImage
        {
            get
            {
                return this.image1.Source;
            }
            set
            { 
                this.image1.Source = value;
                if (value == _InitialImage)
                {    // 禁止图像拖动删除功能        
                    this.image1.MouseDown -= new MouseButtonEventHandler(FingerPictureBox_MouseDown);
                    this.image1.MouseMove -= new MouseEventHandler(FingerPictureBox_MouseMove);
                    this.image1.MouseUp -= new MouseButtonEventHandler(FingerPictureBox_MouseUp);
                }
                else
                {   // 恢复图像拖动删除功能
                    this.image1.MouseDown += new MouseButtonEventHandler(FingerPictureBox_MouseDown);
                    this.image1.MouseMove += new MouseEventHandler(FingerPictureBox_MouseMove);
                    this.image1.MouseUp += new MouseButtonEventHandler(FingerPictureBox_MouseUp);
                }     
            }            
        }
    }
}
