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
using System.Xml;

namespace bind_treeview2
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private XmlElement XES = null;
        private XmlDataProvider XDP = null;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            XmlDocument doc = new XmlDocument();

            XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", "utf-8", "yes");
            doc.AppendChild(dec);

            XmlElement xeRoot = doc.CreateElement("Root");
            xeRoot.SetAttribute("IsSelected", "True");
            {
                XmlElement xeStudents = doc.CreateElement("Student");                
                xeStudents.SetAttribute("Name", "Students");
                xeStudents.SetAttribute("IsSelected", "True");

                XES = xeStudents;
                {
                    XmlElement xeStudent = doc.CreateElement("Student");
                    xeStudent.SetAttribute("Name", "Szp");
                    xeStudent.SetAttribute("IsSelected", "True");
                    xeStudents.AppendChild(xeStudent);
                }
                {
                    XmlElement xeStudent = doc.CreateElement("Student");
                    xeStudent.SetAttribute("Name", "Szh");
                    xeStudent.SetAttribute("IsSelected", "True");
                    xeStudents.AppendChild(xeStudent);
                }
                xeRoot.AppendChild(xeStudents);;
            }
            doc.AppendChild(xeRoot);

            XmlDataProvider provider = new XmlDataProvider
            {
                Document = doc,
                XPath = "Root/Student"
            };
            this.XDP = provider;
            this.XDP.Document.NodeChanged += (sender1, e1) => { this.XDP.Refresh(); };

            //this.TV1.Items.Clear();

            this.TV1.DataContext = provider;

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var provider = this.TV1.DataContext as XmlDataProvider;
            var xmldoc = provider.Document;
            var students = xmldoc.SelectSingleNode("Root/Student") as XmlElement;
            students.SetAttribute("IsExpanded","True");
            students.SetAttribute("Name","Students_x");

            //this.TV1.UpdateLayout();
            //this.XDP.Refresh();
            //this.XDP.DeferRefresh();
            //binded treeview has no items.

            /*
            this.TV1.UpdateLayout();
            
            foreach (object item in this.TV1.Items)
            {
                TreeViewItem treeItem = this.TV1.ItemContainerGenerator.ContainerFromItem(item) as TreeViewItem;
                if (treeItem != null)
                    ExpandAll(treeItem, true);
                treeItem.IsExpanded = true;
            }*/
        }
        private void ExpandAll(ItemsControl items, bool expand)
        {
            foreach (object obj in items.Items)
            {
                ItemsControl childControl = items.ItemContainerGenerator.ContainerFromItem(obj) as ItemsControl;
                if (childControl != null)
                {
                    ExpandAll(childControl, expand);
                }
                TreeViewItem item = childControl as TreeViewItem;
                if (item != null)
                    item.IsExpanded = true;
            }
        }
    }
}
