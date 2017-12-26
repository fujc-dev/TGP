using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MIS.ClientUI
{
    public partial class MISLogin : Window
    {
        public MISLogin()
        {
            this.InitializeComponent();
            this.DataContext = this;
            this._BindCommand();
        }

        public ICommand ExitCommand { get; private set; }
        public ICommand ForgetPasswordCommand { get; private set; }
        public ICommand HelpCommand { get; private set; }
        public ICommand LoginCommand { get; private set; }


        private void BindCommand(UIElement @ui, ICommand com, Action<object, ExecutedRoutedEventArgs> call)
        {
            var bind = new CommandBinding(com);
            bind.Executed += new ExecutedRoutedEventHandler(call);
            @ui.CommandBindings.Add(bind);
        }

        private void _BindCommand()
        {
            this.ExitCommand = new RoutedUICommand();
            this.ForgetPasswordCommand = new RoutedUICommand();
            this.HelpCommand = new RoutedUICommand();
            this.LoginCommand = new RoutedUICommand();
            this.BindCommand(this, this.LoginCommand, (sender, eventArgs) => { new MainWindow().Show(); this.Close(); });
            this.BindCommand(this, this.ForgetPasswordCommand, (sender, eventArgs) => { });
            this.BindCommand(this, this.HelpCommand, (sender, eventArgs) => { });
            //绑定命令
            this.BindCommand(this, this.ExitCommand, (s, e) => { Application.Current.Shutdown(); });
        }

    }
}