using Avalonia.Controls;
using Avalonia.Controls.Mixins;
using Avalonia.NETCoreMVVMApp1.ViewModels;
using Avalonia.ReactiveUI;
using ReactiveUI;

namespace Avalonia.NETCoreMVVMApp1.Views
{
    public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
    {
        public MainWindow()
        {
            
            InitializeComponent();
            this.WhenActivated(disposable =>
            {
               
            });
        }
    }
}