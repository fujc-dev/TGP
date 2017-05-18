using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Interop;

namespace TGP.WindowDemo
{
    /// <summary>
    /// 自定义一个窗体基类 by dane
    /// </summary>
    [TemplatePart(Name = GWindowBase.TGP_BG_PART, Type = typeof(Border))]
    [TemplatePart(Name = GWindowBase.TGP_INNER_PART, Type = typeof(Border))]
    public partial class GWindowBase : Window
    {



        public GWindowBase()
        {
            //设置窗体基础样式
            this.WindowStyle = WindowStyle.None;
            this.AllowsTransparency = true;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            //this.Style = this.FindResource("DefaultWindowStyle") as Style;
            //初始化并绑定命令
            this.DragCommand = new RoutedUICommand();
            this.MinCommand = new RoutedUICommand();
            this.MaxCommand = new RoutedUICommand();
            this.CloseCommand = new RoutedUICommand();
            this.BindCommand(DragCommand, OnDragCommand);
            this.BindCommand(MinCommand, OnMinCommand);
            this.BindCommand(MaxCommand, OnMaxCommand);
            this.BindCommand(CloseCommand, OnCloseCommand);
            this.SourceInitialized += (sender, e) =>
            {
                System.IntPtr handle = (new WindowInteropHelper(this)).Handle;
                HwndSource.FromHwnd(handle).AddHook(new HwndSourceHook(WindowProc));
            };
        }



        //依赖属性

        #region 是否允许拖动，默认值true
        public Boolean CanDrag
        {
            get { return (Boolean)GetValue(CanDragProperty); }
            set { SetValue(CanDragProperty, value); }
        }

        public static readonly DependencyProperty CanDragProperty = DependencyProperty.Register("CanDrag", typeof(Boolean), typeof(GWindowBase), new PropertyMetadata(true));

        #endregion

        #region 图标
        public ImageSource GIcon
        {
            get { return (ImageSource)GetValue(GIconProperty); }
            set { SetValue(GIconProperty, value); }
        }
        public static readonly DependencyProperty GIconProperty = DependencyProperty.Register("GIcon", typeof(ImageSource), typeof(GWindowBase), new PropertyMetadata(new BitmapImage(new Uri("pack://application:,,,/TGP.WindowDemo;component/icon/102.ico", UriKind.RelativeOrAbsolute))));
        #endregion

        #region 标题
        public String GTitle
        {
            get { return (String)GetValue(GTitleProperty); }
            set { SetValue(GTitleProperty, value); }
        }

        public static readonly DependencyProperty GTitleProperty = DependencyProperty.Register("GTitle", typeof(String), typeof(GWindowBase), new PropertyMetadata("TGP.UI控件库"));

        #endregion

        #region 背景图片

        public ImageSource BackgroundImg
        {
            get { return (ImageSource)GetValue(BackgroundImgProperty); }
            set { SetValue(BackgroundImgProperty, value); }
        }

        public static readonly DependencyProperty BackgroundImgProperty = DependencyProperty.Register("BackgroundImg", typeof(ImageSource), typeof(GWindowBase), new PropertyMetadata(new BitmapImage(new Uri("pack://application:,,,/TGP.WindowDemo;component/bg/葡萄红棱形.jpg", UriKind.RelativeOrAbsolute)), (DependencyObject d, DependencyPropertyChangedEventArgs e) =>
        {
            //属性变化后操作

        }));

        [DllImport("Kernel32.dll", EntryPoint = "RtlMoveMemory", SetLastError = true)]
        private static extern void CopyMemory(IntPtr Dest, IntPtr src, int Length);                 // Marshal.Copy 居然没有从一个内存地址直接复制到另外一个内存的重载函数        
        private static Object lockObj = new Object();
        private Bitmap Bmp;
        private IntPtr ImageCopyPointer, ImagePointer;
        private int DataLength;

        private const string TGP_BG_PART = "PART_Bg";  //背景
        private const string TGP_INNER_PART = "PART_Inner";  //内容部分
        private const string TGP_MAX_PART = "PART_Max"; //最大化按钮
        private Border PART_Bg;
        private GButton PART_Max;
        private ImageSource CurrentIcon = null;
        //应用模板回调
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            //高斯模糊部分
            this.PART_Bg = GetTemplateChild(TGP_BG_PART) as Border;
            //最大化按钮处理
            this.PART_Max = GetTemplateChild(TGP_MAX_PART) as GButton;
            this.CurrentIcon = this.PART_Max.GIcon;
            //获取背景图片源图片信息
            OnGaussianBlur();

        }

        private void OnGaussianBlur()
        {
            //
            if (this.Bmp != null)
            {
                Bmp.Dispose();
                Marshal.FreeHGlobal(ImageCopyPointer);
            }
            try
            {
                Bmp = this.BackgroundImg.ToBitmap();
                BitmapData BmpData = new BitmapData();
                Bmp.LockBits(new System.Drawing.Rectangle(0, 0, Bmp.Width, Bmp.Height), ImageLockMode.ReadWrite, Bmp.PixelFormat, BmpData);    // 用原始格式LockBits,得到图像在内存中真正地址，这个地址在图像的大小，色深等未发生变化时，每次Lock返回的Scan0值都是相同的。
                ImagePointer = BmpData.Scan0;                            //  记录图像在内存中的真正地址
                DataLength = BmpData.Stride * BmpData.Height;           //  记录整幅图像占用的内存大小
                ImageCopyPointer = Marshal.AllocHGlobal(DataLength);    //  直接用内存数据来做备份，AllocHGlobal在内部调用的是LocalAlloc函数
                CopyMemory(ImageCopyPointer, ImagePointer, DataLength); //  这里当然也可以用Bitmap的Clone方式来处理，但是我总认为直接处理内存数据比用对象的方式速度快。
                Bmp.UnlockBits(BmpData);
                UpdateImage();  //更新图片地址
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void UpdateImage()
        {
            if (Bmp != null)
            {
                lock (lockObj)
                {
                    CopyMemory(ImagePointer, ImageCopyPointer, DataLength);             // 需要恢复原始的图像数据，不然模糊就会叠加了。
                    System.Drawing.Rectangle Rect = new System.Drawing.Rectangle(0, 0, Bmp.Width, Bmp.Height);
                    Bmp.GaussianBlur(ref Rect, 10, true);  //设置高斯模糊半径以及扩展边界 
                    ImageBrush ib = new ImageBrush(Bmp.ToImageSource());
                    PART_Bg.Background = ib;
                }
            }
        }

        #endregion

        #region 标题栏头部扩展

        public ControlTemplate HeaderEx
        {
            get { return (ControlTemplate)GetValue(HeaderExProperty); }
            set { SetValue(HeaderExProperty, value); }
        }

        public static readonly DependencyProperty HeaderExProperty = DependencyProperty.Register("HeaderEx", typeof(ControlTemplate), typeof(GWindowBase), new PropertyMetadata(null));






        #endregion

        #region 最小化按钮显示状态
        public Visibility MinButtonVisibility
        {
            get { return (Visibility)GetValue(MinButtonVisibilityProperty); }
            set { SetValue(MinButtonVisibilityProperty, value); }
        }

        public static readonly DependencyProperty MinButtonVisibilityProperty = DependencyProperty.Register("MinButtonVisibility", typeof(Visibility), typeof(GWindowBase), new PropertyMetadata(Visibility.Visible));

        #endregion

        #region 最大化按钮显示状态
        public Visibility MaxButtonVisibility
        {
            get { return (Visibility)GetValue(MaxButtonVisibilityProperty); }
            set { SetValue(MaxButtonVisibilityProperty, value); }
        }

        public static readonly DependencyProperty MaxButtonVisibilityProperty = DependencyProperty.Register("MaxButtonVisibility", typeof(Visibility), typeof(GWindowBase), new PropertyMetadata(Visibility.Visible));

        #endregion

        #region 关闭按钮显示状态
        public Visibility CloseButtonVisibility
        {
            get { return (Visibility)GetValue(CloseButtonVisibilityProperty); }
            set { SetValue(CloseButtonVisibilityProperty, value); }
        }

        public static readonly DependencyProperty CloseButtonVisibilityProperty = DependencyProperty.Register("CloseButtonVisibility", typeof(Visibility), typeof(GWindowBase), new PropertyMetadata(Visibility.Visible));

        #endregion

        #region 默认命令
        public ICommand DragCommand { get; protected set; }  //窗体拖动
        public ICommand MinCommand { get; protected set; } //最小化
        public ICommand MaxCommand { get; protected set; } //最大化
        public ICommand CloseCommand { get; protected set; } //关闭

        private void OnDragCommand(object sender, ExecutedRoutedEventArgs e)
        {
            if (CanDrag)
            {
                base.DragMove();
                e.Handled = true;
            }
        }
        private void OnMinCommand(object sender, ExecutedRoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
            e.Handled = true;
        }

        private void OnMaxCommand(object sender, ExecutedRoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal; this.PART_Max.GIcon = this.CurrentIcon;
            }
            else
            {
                this.WindowState = WindowState.Maximized;
                this.CurrentIcon = this.PART_Max.GIcon;  //替换图片处理
                this.PART_Max.GIcon = this.PART_Max.GToggleIcon;
            }
            e.Handled = true;
        }
        private void OnCloseCommand(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }

        #endregion

        #region 自定义窗体不显示任务栏Win32解决方案

        private static System.IntPtr WindowProc(System.IntPtr hwnd, int msg, System.IntPtr wParam, System.IntPtr lParam, ref bool handled)
        {
            switch (msg)
            {
                case 0x0024:
                    WmGetMinMaxInfo(hwnd, lParam);
                    handled = true;
                    break;
            }

            return (System.IntPtr)0;
        }

        private static void WmGetMinMaxInfo(System.IntPtr hwnd, System.IntPtr lParam)
        {

            MINMAXINFO mmi = (MINMAXINFO)Marshal.PtrToStructure(lParam, typeof(MINMAXINFO));

            // Adjust the maximized size and position to fit the work area of the correct monitor
            int MONITOR_DEFAULTTONEAREST = 0x00000002;
            System.IntPtr monitor = MonitorFromWindow(hwnd, MONITOR_DEFAULTTONEAREST);

            if (monitor != System.IntPtr.Zero)
            {

                MONITORINFO monitorInfo = new MONITORINFO();
                GetMonitorInfo(monitor, monitorInfo);
                RECT rcWorkArea = monitorInfo.rcWork;
                RECT rcMonitorArea = monitorInfo.rcMonitor;
                mmi.ptMaxPosition.x = Math.Abs(rcWorkArea.left - rcMonitorArea.left);
                mmi.ptMaxPosition.y = Math.Abs(rcWorkArea.top - rcMonitorArea.top);
                mmi.ptMaxSize.x = Math.Abs(rcWorkArea.right - rcWorkArea.left);
                mmi.ptMaxSize.y = Math.Abs(rcWorkArea.bottom - rcWorkArea.top);
            }

            Marshal.StructureToPtr(mmi, lParam, true);
        }


        /// <summary>
        /// POINT aka POINTAPI
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            /// <summary>
            /// x coordinate of point.
            /// </summary>
            public int x;
            /// <summary>
            /// y coordinate of point.
            /// </summary>
            public int y;

            /// <summary>
            /// Construct a point of coordinates (x,y).
            /// </summary>
            public POINT(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MINMAXINFO
        {
            public POINT ptReserved;
            public POINT ptMaxSize;
            public POINT ptMaxPosition;
            public POINT ptMinTrackSize;
            public POINT ptMaxTrackSize;
        };



        /// <summary>
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public class MONITORINFO
        {
            /// <summary>
            /// </summary>            
            public int cbSize = Marshal.SizeOf(typeof(MONITORINFO));

            /// <summary>
            /// </summary>            
            public RECT rcMonitor = new RECT();

            /// <summary>
            /// </summary>            
            public RECT rcWork = new RECT();

            /// <summary>
            /// </summary>            
            public int dwFlags = 0;
        }


        /// <summary> Win32 </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 0)]
        public struct RECT
        {
            /// <summary> Win32 </summary>
            public int left;
            /// <summary> Win32 </summary>
            public int top;
            /// <summary> Win32 </summary>
            public int right;
            /// <summary> Win32 </summary>
            public int bottom;

            /// <summary> Win32 </summary>
            public static readonly RECT Empty = new RECT();

            /// <summary> Win32 </summary>
            public int Width
            {
                get { return Math.Abs(right - left); }  // Abs needed for BIDI OS
            }
            /// <summary> Win32 </summary>
            public int Height
            {
                get { return bottom - top; }
            }

            /// <summary> Win32 </summary>
            public RECT(int left, int top, int right, int bottom)
            {
                this.left = left;
                this.top = top;
                this.right = right;
                this.bottom = bottom;
            }


            /// <summary> Win32 </summary>
            public RECT(RECT rcSrc)
            {
                this.left = rcSrc.left;
                this.top = rcSrc.top;
                this.right = rcSrc.right;
                this.bottom = rcSrc.bottom;
            }

            /// <summary> Win32 </summary>
            public bool IsEmpty
            {
                get
                {
                    // BUGBUG : On Bidi OS (hebrew arabic) left > right
                    return left >= right || top >= bottom;
                }
            }
            /// <summary> Return a user friendly representation of this struct </summary>
            public override string ToString()
            {
                if (this == RECT.Empty) { return "RECT {Empty}"; }
                return "RECT { left : " + left + " / top : " + top + " / right : " + right + " / bottom : " + bottom + " }";
            }

            /// <summary> Determine if 2 RECT are equal (deep compare) </summary>
            public override bool Equals(object obj)
            {
                if (!(obj is Rect)) { return false; }
                return (this == (RECT)obj);
            }

            /// <summary>Return the HashCode for this struct (not garanteed to be unique)</summary>
            public override int GetHashCode()
            {
                return left.GetHashCode() + top.GetHashCode() + right.GetHashCode() + bottom.GetHashCode();
            }


            /// <summary> Determine if 2 RECT are equal (deep compare)</summary>
            public static bool operator ==(RECT rect1, RECT rect2)
            {
                return (rect1.left == rect2.left && rect1.top == rect2.top && rect1.right == rect2.right && rect1.bottom == rect2.bottom);
            }

            /// <summary> Determine if 2 RECT are different(deep compare)</summary>
            public static bool operator !=(RECT rect1, RECT rect2)
            {
                return !(rect1 == rect2);
            }


        }

        [DllImport("user32")]
        internal static extern bool GetMonitorInfo(IntPtr hMonitor, MONITORINFO lpmi);

        /// <summary>
        /// 
        /// </summary>
        [DllImport("User32")]
        internal static extern IntPtr MonitorFromWindow(IntPtr handle, int flags);

        #endregion

    }
}
