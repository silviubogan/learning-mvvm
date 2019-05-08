using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace cs_wpf_test_11
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static readonly DependencyProperty MyVMProperty =
            DependencyProperty.Register("MyVM", typeof(BaseVM), typeof(MainWindow),
                new FrameworkPropertyMetadata(null));
        public BaseVM MyVM
        {
            get
            {
                return (BaseVM)GetValue(MyVMProperty);
            }
            set
            {
                SetValue(MyVMProperty, value);
            }
        }

        public static readonly DependencyProperty MyItemsProperty =
            DependencyProperty.Register("MyItems",
                typeof(ObservableCollection<ItemType>),
                typeof(MainWindow),
                new FrameworkPropertyMetadata(null));
        public ObservableCollection<ItemType> MyItems
        {
            get
            {
                return (ObservableCollection<ItemType>)GetValue(MyItemsProperty);
            }
            set
            {
                SetValue(MyItemsProperty, value);
            }
        }

        public MainWindow()
        {
            InitializeComponent();

            MyVM = new BaseVM();
            MyItems = new ObservableCollection<ItemType>();

            foreach (string existingItem in new string[]
                {
                    "existing item 1",
                    "existing item 2",
                    "existing item 3"
                })
            {
                MyVM.ChildVMCollection.Insert(0, new ChildVM()
                {
                    Text = existingItem
                });
                MyItems.Add(new ItemType()
                {
                    Value = existingItem
                });
            }
        }

        private void ComboBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            var cb = sender as ComboBox;

            if ((e.Key == Key.Return ||
                e.Key == Key.Enter) &&
                cb.Text != "")
            {
                bool duplicate = false;
                foreach (ChildVM vm in MyVM.ChildVMCollection)
                {
                    if (vm.Text == cb.Text)
                    {
                        cb.SelectedItem = vm;
                        duplicate = true;
                        break;
                    }
                }

                if (duplicate)
                {
                    return;
                }

                // create a ChildM and corresponding ChildVM
                // (ChildVM inherits from ChildM)
                var cvm = new ChildVM()
                {
                    Text = cb.Text
                };
                MyVM.ChildMCollection.Insert(0, cvm);
                cb.SelectedItem = cvm;
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var cb = sender as ComboBox;

            //if (cb.SelectedItem is the VM with Style != Normal)

            ChildVM foundVM = null;
            foreach (ChildVM vm in MyVM.ChildVMCollection)
            {
                if (vm.Style != FontStyles.Normal &&
                    cb.SelectedItem == vm)
                {
                    foundVM = vm;
                    break;
                }
            }

            if (foundVM != null)
            {
                cb.Text = "";
                e.Handled = true;
            }
        }
    }
}
