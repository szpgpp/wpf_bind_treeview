using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace bind_treeview3
{
    public class BaseNotifyPropertyChanged : System.ComponentModel.INotifyPropertyChanged
    {
        /// <summary>
        /// 属性值变化时发生
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// 属性值变化时发生
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void OnPropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            var propertyName = (propertyExpression.Body as MemberExpression).Member.Name;
            this.OnPropertyChanged(propertyName);
        }

        public virtual event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
    }
    class DataItem : BaseNotifyPropertyChanged, ICloneable
    {
        public DataItem(string header, int deepth = 1)
        {
            Header = header;
            Deepth = deepth;
        }

        public object Clone()
        {
            DataItem dataItem = new DataItem(Header, Deepth);
            dataItem.IsExpanded = this.IsExpanded;
            dataItem.IsSelected = this.IsSelected;
            dataItem.IsChecked = this.IsChecked;
            dataItem.Deepth = Deepth;
            foreach (DataItem item in Items)
                dataItem.Items.Add((DataItem)item.Clone());
            return dataItem;
        }

        private string header;
        public string Header
        {
            get { return header; }
            set
            {
                 this.header = value; this.OnPropertyChanged("Header"); 
            }
        }

        private bool isSelected;
        public bool IsSelected
        {
            get { return isSelected; }
            set { this.isSelected = value;this.OnPropertyChanged("IsSelected"); }
        }

        private bool isExpanded;
        public bool IsExpanded
        {
            get { return isExpanded; }
            set { this.isExpanded = value; this.OnPropertyChanged("IsExpanded"); }
        }

        private bool isChecked;
        public bool IsChecked
        {
            get { return isChecked; }
            set { this.isChecked = value; this.OnPropertyChanged("IsChecked"); }
        }

        private int deepth;
        public int Deepth
        {
            get { return deepth; }
            set { this.deepth = value; this.OnPropertyChanged("Deepth"); }
        }

        private ObservableCollection<DataItem> mItems = null;
        public ObservableCollection<DataItem> Items
        {
            get
            {
                if (mItems == null)
                    mItems = new ObservableCollection<DataItem>();
                return mItems;
            }
        }

    }

    class Data
    {
        private static Data mInstance = new Data();

        public static Data Instance
        {
            get { return mInstance; }
        }

        private ObservableCollection<DataItem> GenerateTreeViewItems()
        {
            ObservableCollection<DataItem> items = new ObservableCollection<DataItem>();

            DataItem item1 = new DataItem("TreeViewItem1");
            item1.Items.Add(new DataItem("SubItem1", item1.Deepth));
            item1.Items.Add(new DataItem("SubItem2", item1.Deepth));
            item1.Items.Add(new DataItem("SubItem3", item1.Deepth));
            item1.Items.Add(new DataItem("SubItem4", item1.Deepth));
            items.Add(item1);

            DataItem item2 = new DataItem("TreeViewItem2");
            item2.Items.Add(new DataItem("SubItem1", item2.Deepth));
            item2.Items.Add(new DataItem("SubItem2", item2.Deepth));
            items.Add(item2);

            DataItem item3 = new DataItem("TreeViewItem3");
            //item3.IsExpanded = true;
            item3.Items.Add(new DataItem("SubItem1", item3.Deepth));
            item3.Items.Add(new DataItem("SubItem2", item3.Deepth));
            item3.Items.Add(new DataItem("SubItem3", item3.Deepth));
            item3.Items.Add(new DataItem("SubItem4", item3.Deepth));
            item3.Items.Add(new DataItem("SubItem5", item3.Deepth));
            item3.Items.Add(new DataItem("SubItem6", item3.Deepth));
            item3.Items.Add(new DataItem("SubItem7", item3.Deepth));
            item3.Items.Add(new DataItem("SubItem8", item3.Deepth));
            items.Add(item3);

            return items;
        }

        private ObservableCollection<DataItem> GenerateListItems()
        {
            ObservableCollection<DataItem> items = new ObservableCollection<DataItem>();
            items.Add(new DataItem("ListBoxItem1"));
            items.Add(new DataItem("ListBoxItem2"));
            items.Add(new DataItem("ListBoxItem3"));
            items.Add(new DataItem("ListBoxItem4"));
            items.Add(new DataItem("ListBoxItem5"));
            return items;
        }

        public ObservableCollection<DataItem> TreeViewItems
        {
            get
            {
                if (mTreeViewItems == null)
                    mTreeViewItems = GenerateTreeViewItems();
                return mTreeViewItems;
            }
        }

        public ObservableCollection<DataItem> ListBoxItems
        {
            get
            {
                if (mListBoxItems == null)
                    mListBoxItems = GenerateListItems();
                return mListBoxItems;
            }
        }

        private ObservableCollection<DataItem> mTreeViewItems = null;
        private ObservableCollection<DataItem> mListBoxItems = null;
    }
}
