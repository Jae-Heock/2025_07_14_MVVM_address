using _20250714_4.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _20250714_4
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            // InitializeComponent();
            
            this.DataContext = new MainViewModel();
            InitializeComponent();
            box();
        }

        public void box()
        {
            SearchComboBox.SelectedIndex = 0;
            SearchComboBox.ItemsSource = searchBox;
        }

        public List<string> searchBox = new List<string>()
        {
           "전체",
           "이름",
           "소속",
           "직급"
        };

        private void Search_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SearchComboBox.SelectedIndex == 0)
            {
                SearchTextBox.IsReadOnly = true;
                SearchTextBox.Clear();
                SearchTextBox.Background = Brushes.Gray;
            }
            else
            {
                SearchTextBox.IsReadOnly= false;
                SearchTextBox.Background = Brushes.White;
            }
        }
    }
}
