using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using TreeViewDemo.ViewModels;

namespace TreeViewDemo.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        protected override async void OnDataContextEndUpdate()
        {
            base.OnDataContextEndUpdate();
            if (DataContext is MainWindowViewModel viewModel)
                await viewModel.InitAsync();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
