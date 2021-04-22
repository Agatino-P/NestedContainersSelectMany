using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NestedContainersSelectMany
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private class L3
        {
            public string Text{ get; set; }
            public L3(string text) => Text = text;

            public List<string> L3Strings { get; set; } = new List<string>();
            public List<string> L3StringsNamed => L3Strings.Select(s=>$"[{Text}]:{s}").ToList();
        }
        private class L2
        {
            public string Text { get; set; }
            public L2(string text) => Text = text;

            public List<L3> L3s { get; set; } = new List<L3>();
        }
        private class L1
        {
            public string Text { get; set; }
            public L1(string text) => Text = text;

            public List<L2> L2s { get; set; } = new List<L2>();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            L3 l3a = new L3("l3a");
            l3a.L3Strings.Add("l3a_1");
            l3a.L3Strings.Add("l3a_2");
            L3 l3b = new L3("l3b");
            l3b.L3Strings.Add("l3b_1");
            l3b.L3Strings.Add("l3b_2");
            L2 l2a = new L2("l2a");
            l2a.L3s.Add(l3a);
            l2a.L3s.Add(l3b);
            
            L3 l3c = new L3("l3c");
            l3c.L3Strings.Add("l3c_1");
            l3c.L3Strings.Add("l3c_2");
            L3 l3d = new L3("l3d");
            l3d.L3Strings.Add("l3d_1");
            l3d.L3Strings.Add("l3d_2");
            L2 l2b = new L2("l2b");
            l2b.L3s.Add(l3c);
            l2b.L3s.Add(l3d);

            L1 l1 = new L1("l1");
            l1.L2s.Add(l2a);
            l1.L2s.Add(l2b);

            IEnumerable<string> strings = l1.L2s.SelectMany(l2 => l2.L3s.SelectMany(l3 => l3.L3Strings));
            IEnumerable<string> namedStrings = l2a.L3s.SelectMany(l3 => l3.L3StringsNamed).ToList();
            string firstEndingWith2 = strings.FirstOrDefault(s => s.Substring(4) == "2");
        }
    }
}
