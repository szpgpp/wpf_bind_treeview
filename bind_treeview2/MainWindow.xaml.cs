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
            {
                XmlElement xeStudents = doc.CreateElement("Student");                
                xeStudents.SetAttribute("Name", "Students");
                xeStudents.SetAttribute("IsSelected", "True");
                XES = xeStudents;
                {
                    XmlElement xeStudent = doc.CreateElement("Student");
                    xeStudent.SetAttribute("Name", "Szp");
                    xeStudents.AppendChild(xeStudent);
                }
                {
                    XmlElement xeStudent = doc.CreateElement("Student");
                    xeStudent.SetAttribute("Name", "Szh");
                    xeStudents.AppendChild(xeStudent);
                }
                xeRoot.AppendChild(xeStudents);
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
            students.SetAttribute("IsExpanded","1");
            this.XDP.Refresh();
        }
    }
}
