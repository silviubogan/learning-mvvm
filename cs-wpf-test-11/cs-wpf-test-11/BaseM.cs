using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs_wpf_test_11
{
    public class BaseM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public ObservableCollection<ChildM> _ChildMCollection = null;
        public ObservableCollection<ChildM> ChildMCollection
        {
            get
            {
                return _ChildMCollection;
            }
            set
            {
                if (_ChildMCollection != value)
                {
                    _ChildMCollection = value;
                    OnPropertyChanged("ChildMCollection");
                }
            }
        }

        public BaseM()
        {
            ChildMCollection = new ObservableCollection<ChildM>();
        }
    }
}
