using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace MIS.ClientUI.Behaviors
{
    public class MouseDragFrameworkElementBehavior : Behavior<FrameworkElement>
    {
        public MouseDragFrameworkElementBehavior()
        {
            /* 在创建WPF自定义窗体时，往往会取消默认的窗体样式，
             * 那么当WindowStyle设置为None时，
             * 需要移动窗体时就必需对某一个UIElement控件进去指定的DragMove操作;
             * 
             * 下面就为指定的控件绑定一个点击左键事件移动窗体的行为
             */
        }
        private Window m_CurrentWindow;

        protected override void OnAttached()
        {
            base.OnAttached();
            //为行为对象添加一个模拟移动的点击事件
            this.AssociatedObject.PreviewMouseLeftButtonDown += OnDragMove;
        }

        private void OnDragMove(object sender, MouseButtonEventArgs e)
        {
            //获取鼠标坐标
            Point position = e.GetPosition(AssociatedObject);
            //鼠标坐标在控件范围内运行移动窗体
            if (position.X < AssociatedObject.ActualWidth && position.Y < AssociatedObject.ActualHeight)
            {
                //获取当前附加行为的控件所在窗体(Window对象)
                if (m_CurrentWindow == null)
                {
                    m_CurrentWindow = Window.GetWindow(AssociatedObject);
                }
                m_CurrentWindow.DragMove();
            }
        }
    }
}
