using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace cs_wpf_test_11
{
    public class BaseVM : BaseM
    {
        internal ObservableCollection<ChildVM> _ChildVMCollection;
        public ObservableCollection<ChildVM> ChildVMCollection
        {
            get
            {
                return _ChildVMCollection;
            }
            set
            {
                if (_ChildVMCollection != value)
                {
                    _ChildVMCollection = value;
                    OnPropertyChanged("ChildVMCollection");
                }
            }
        }

        public BaseVM() : base()
        {
            ChildVMCollection = new ObservableCollection<ChildVM>();

            ChildVMCollection.Add(new ChildVM()
            {
                Text = "No Group",
                Style = FontStyles.Italic
            });

            ChildMCollection.CollectionChanged += ChildMCollection_CollectionChanged;
            ChildVMCollection.CollectionChanged += ChildVMCollection_CollectionChanged;
        }

        protected bool SynchDisabled = false;

        private void ChildVMCollection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (SynchDisabled)
            {
                return;
            }
            SynchDisabled = true;

            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (object item in e.NewItems)
                    {
                        var mItem = (ChildVM)item;
                        int idx = ChildMCollection.IndexOf(mItem);

                        // the operation could have been either Insert or Add
                        if (0 <= idx && idx <= ChildVMCollection.Count - 1)
                        {
                            ChildMCollection.Insert(idx, mItem);
                        }
                        else
                        {
                            ChildMCollection.Add(mItem);
                        }
                    }
                    break;

                case NotifyCollectionChangedAction.Remove:
                    foreach (object item in e.OldItems)
                    {
                        var vmItem = item as ChildVM;

                        // find VM objects that wrap the relevant model object and remove them
                        IEnumerable<ChildM> query;
                        while ((query = from vm in ChildMCollection
                                        where vm.Text == vmItem.Text
                                        select vm).Count() > 0)
                        {
                            ChildM mItem = query.First();
                            int index = ChildMCollection.IndexOf(vmItem);
                            ChildMCollection.Remove(vmItem); // TODO: what if I implement the == or != operator?
                        }
                    }
                    break;

                case NotifyCollectionChangedAction.Reset:
                    ChildMCollection.Clear();
                    break;

                case NotifyCollectionChangedAction.Move:
                    // TODO: handle multiple items
                    ChildMCollection.Move(e.OldStartingIndex, e.NewStartingIndex);
                    break;

                case NotifyCollectionChangedAction.Replace:
                    // TODO: handle multiple items
                    ChildMCollection[e.OldStartingIndex] = (ChildM)e.NewItems[0];
                    break;

                default:
                    throw new NotImplementedException();
            }

            SynchDisabled = false;
        }

        private void ChildMCollection_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (SynchDisabled)
            {
                return;
            }
            SynchDisabled = true;

            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (object item in e.NewItems)
                    {
                        var vmItem = new ChildVM((ChildM)item);

                        // the operation could have been either Insert or Add
                        if (0 <= e.NewStartingIndex &&
                            e.NewStartingIndex <= ChildMCollection.Count)
                        {
                            ChildVMCollection.Insert(e.NewStartingIndex, vmItem);
                        }
                        else // e.NewStartingIndex < 0, so addition
                        {
                            ChildVMCollection.Insert(ChildMCollection.Count - 1, vmItem);
                        }
                    }
                    break;

                case NotifyCollectionChangedAction.Remove:
                    foreach (object item in e.OldItems)
                    {
                        // find VM objects that wrap the relevant model object and remove them
                        IEnumerable<ChildVM> query;
                        while ((query = from vm in ChildVMCollection
                                        where vm.Text == ((ChildM)item).Text
                                        select vm).Count() > 0)
                        {
                            ChildVM vmItem = query.First();
                            int index = ChildVMCollection.IndexOf(vmItem);
                            ChildVMCollection.Remove(vmItem); // TODO: what if I implement the == or != operator?
                        }
                    }
                    break;

                case NotifyCollectionChangedAction.Reset:
                    for (int i = ChildVMCollection.Count - 2; i >= 0; --i)
                    {
                        ChildVMCollection.RemoveAt(i);
                    }
                    break;

                case NotifyCollectionChangedAction.Move:
                    // TODO: handle multiple items
                    ChildVMCollection.Move(e.OldStartingIndex, e.NewStartingIndex);
                    break;

                case NotifyCollectionChangedAction.Replace:
                    // TODO: handle multiple items
                    ChildVMCollection[e.OldStartingIndex] = new ChildVM((ChildM)e.NewItems[0]);
                    break;

                default:
                    throw new NotImplementedException();
            }

            SynchDisabled = false;
        }
    }
}
