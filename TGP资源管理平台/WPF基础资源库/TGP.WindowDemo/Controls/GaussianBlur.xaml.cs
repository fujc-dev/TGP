using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TGP.WindowDemo
{
    /// <summary>
    /// 全局背景图片高斯模糊  by dane
    /// </summary>
    public partial class GaussianBlur : Window, INotifyPropertyChanged
    {

        [DllImport("Kernel32.dll", EntryPoint = "RtlMoveMemory", SetLastError = true)]
        private static extern void CopyMemory(IntPtr Dest, IntPtr src, int Length);                 // Marshal.Copy 居然没有从一个内存地址直接复制到另外一个内存的重载函数        
        private static Object lockObj = new Object();

        private Bitmap Bmp;
        private IntPtr ImageCopyPointer, ImagePointer;
        private int DataLength;

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private int _GaussianValue = 30;

        public int GaussianValue
        {
            get
            {
                return _GaussianValue;
            }
            set
            {
                _GaussianValue = value;
                OnPropertyChanged("GaussianValue");
                UpdateImage();
                //Task.Factory.StartNew(new Action(() => { }));
            }
        }






        public GaussianBlur()
        {
            InitializeComponent();

            this.DataContext = this;
            //
            if (this.Bmp != null)
            {
                Bmp.Dispose();
                Marshal.FreeHGlobal(ImageCopyPointer);
            }
            try
            {
                Bmp = PART_Gsmh.Source.ToBitmap();
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
                    Bmp.GaussianBlur(ref Rect, GaussianValue, true);  //设置高斯模糊半径以及扩展边界 
                    this.PART_Gsmh.Source = Bmp.ToImageSource();
                }
            }
        }
    }
}
