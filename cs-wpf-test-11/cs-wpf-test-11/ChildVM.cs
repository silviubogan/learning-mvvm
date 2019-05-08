using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace cs_wpf_test_11
{
    public class ChildVM : ChildM
    {
        internal FontStyle _Style = FontStyles.Normal;
        public FontStyle Style
        {
            get
            {
                return _Style;
            }
            set
            {
                if (_Style != value)
                {
                    _Style = value;
                    OnPropertyChanged("Style");
                }
            }
        }

        public ChildVM(ChildM m)
        {
            Text = m.Text;
        }

        public ChildVM()
        {
        }

        public override string ToString()
        {
            return Text;
        }
    }
}
