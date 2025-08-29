using System.Windows;
using WPF_Photo_Manager.ViewModels;

namespace WPF_Photo_Manager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = new GalleryViewModel();
        }
    }
}