using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace cs_wpf_test_11
{
    /// <summary>
    /// Interaction logic for TestWindow.xaml
    /// </summary>
    public partial class TestWindow : Window
    {
        public ObservableCollection<ItemType> MyItems { get; set; }

        public ObservableCollection<string> MyGroups { get; set; }

        public TestWindow()
        {
            InitializeComponent();

            MyGroups = new ObservableCollection<string>();
            MyGroups.Add("test");
            MyGroups.Add("test 2");

            MyItems = new ObservableCollection<ItemType>();
            MyItems.Add(new ItemType()
            {
                Value = "test"
            });
            MyItems.Add(new ItemType()
            {
                Value = "test 2"
            });
            MyDataGrid.ItemsSource = MyItems;
        }
    }
}
