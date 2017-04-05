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

namespace TGP.UI.Core.Controls
{
	/// <summary>
	/// ModPrivilege.xaml 的交互逻辑
	/// </summary>
	public partial class ModPrivilege : Control
	{
		public ModPrivilege()
		{

		}



		public String ModTitle
		{
			get { return (String)GetValue(ModTitleProperty); }
			set { SetValue(ModTitleProperty, value); }
		}
		public static readonly DependencyProperty ModTitleProperty = DependencyProperty.Register("ModTitle", typeof(String), typeof(ModPrivilege), new PropertyMetadata(""));



		public String ModContent
		{
			get { return (String)GetValue(ModContentProperty); }
			set { SetValue(ModContentProperty, value); }
		}

		public static readonly DependencyProperty ModContentProperty = DependencyProperty.Register("ModContent", typeof(String), typeof(ModPrivilege), new PropertyMetadata(""));




		public ImageSource OwnerIcon
		{
			get { return (ImageSource)GetValue(OwnerIconProperty); }
			set { SetValue(OwnerIconProperty, value); }
		}

		public static readonly DependencyProperty OwnerIconProperty = DependencyProperty.Register("OwnerIcon", typeof(ImageSource), typeof(ModPrivilege), new PropertyMetadata(null));




		public ImageSource ChildIcon
		{
			get { return (ImageSource)GetValue(ChildIconProperty); }
			set { SetValue(ChildIconProperty, value); }
		}

		public static readonly DependencyProperty ChildIconProperty = DependencyProperty.Register("ChildIcon", typeof(ImageSource), typeof(ModPrivilege), new PropertyMetadata(null));





	}
}
