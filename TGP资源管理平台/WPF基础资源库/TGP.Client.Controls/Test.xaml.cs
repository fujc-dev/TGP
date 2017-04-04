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
using System.Windows.Shapes;

namespace TGP.UI.Core
{
	/// <summary>
	/// Test.xaml 的交互逻辑
	/// </summary>
	public partial class Test : Window
	{
		public Test()
		{
			InitializeComponent();

			List<BitmapImage> ls_adv_img = new List<BitmapImage>();

			String pathAdv = "/TGP.UI.Core;component/images";
			List<Adv> listAdv = new List<Adv>();

			listAdv.Add(new Adv() { pic = "ninja149109685357094.jpg" });//	
			listAdv.Add(new Adv() { pic = "ninja149117866132190.jpg" });
			listAdv.Add(new Adv() { pic = "ninja149117897937143.jpg" });
			listAdv.Add(new Adv() { pic = "ninja149117926310714.jpg" });
			listAdv.Add(new Adv() { pic = "ninja149118860857529.jpg" });
			listAdv.Add(new Adv() { pic = "ninja149118869656826.jpg" });
			listAdv.Add(new Adv() { pic = "ninja149121042484369.jpg" });
			// 根据自己的业务逻辑进行赋值操作  
			foreach (Adv a in listAdv)
			{
				BitmapImage img;
				try
				{
					img= new BitmapImage();
					img.BeginInit();
					///TGP.UI.Core;component/images/ninja149109685357094.jpg
					img.UriSource = new Uri(string.Format(@"{0}/{1}", pathAdv, a.pic), UriKind.RelativeOrAbsolute);
					img.EndInit();
					
				}
				catch (Exception ex)
				{
					img = new BitmapImage();
				}
				ls_adv_img.Add(img);
			}

			//this.rollImg.ls_images = ls_adv_img;

			//this.rollImg.Begin();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			e.Handled = false;
		}
	}

	public class Adv
	{
		public String pic { get; set; }
	}
}
