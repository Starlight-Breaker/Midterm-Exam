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
using System.IO;
using Newtonsoft.Json;

namespace Wesley.Midterm
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

        public class Menu
        {

            public categ[] categorys { get; set; }

        }
        public class categ
        {

            public string name { get; set; }

            [JsonProperty(PropertyName = "menu-items")]
            public menuI[] mItem { get; set; }

        }

        public class menuI
        {

            public string name { get; set; }

            [JsonProperty("sub-items")]
            public sItems[] SubI { get; set; }

        }

        public class sItems
        {

            public string name { get; set; }
            public string price { get; set; }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var testing = JsonConvert.DeserializeObject<Menu>(File.ReadAllText(System.AppDomain.CurrentDomain.BaseDirectory + "restaurant_menu.json"));
            int length = testing.categorys.GetLength(0);
            
            for (int x = 0; x < length; x++)
            {
             TreeViewItem ParentItem = new TreeViewItem();
                ParentItem.Header = testing.categorys[x].name;
                TreeView1.Items.Add(ParentItem);

                int length2 = testing.categorys[x].mItem.GetLength(0);

                for (int y = 0; y < length2; y++)
                {                  
                    TreeViewItem Child1Item = new TreeViewItem();
                    Child1Item.Header = testing.categorys[x].name + " - " + testing.categorys[x].mItem[y].name;
                    ParentItem.Items.Add(Child1Item);

                    int length3 = testing.categorys[x].mItem[y].SubI.GetLength(0);

                    for (int z = 0;z < length3; z++)
                    {
                        TreeViewItem SubItem = new TreeViewItem();
                        SubItem.Header = testing.categorys[x].name + " - " + testing.categorys[x].mItem[y].name + " - " + testing.categorys[x].mItem[y].SubI[z].name;
                        Child1Item.Items.Add(SubItem);
                    }
                }
            }
            
        }


        private void TreeView1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var testing = JsonConvert.DeserializeObject<Menu>(File.ReadAllText(System.AppDomain.CurrentDomain.BaseDirectory + "restaurant_menu.json"));
            var selected = TreeView1.SelectedItem as TreeViewItem;
            var whatimlookingfor = selected.Header.ToString();

            
            string find = null;
            int length = testing.categorys.GetLength(0);
            for (int x = 0; x < length; ++x)
            {
                int length2 = testing.categorys[x].mItem.GetLength(0);
                for (int y = 0;y < length2; y++)
                {
                    int length3 = testing.categorys[x].mItem[y].SubI.GetLength(0);
                    for (int z = 0; z < length3;z++)
                    {
                        find = testing.categorys[x].name + " - " + testing.categorys[x].mItem[y].name + " - " + testing.categorys[x].mItem[y].SubI[z].name;
                        if (find == whatimlookingfor)
                        {
                           
                            Window2 newWindow = new Window2(whatimlookingfor);
                            newWindow.Show();
                            
                        }
                    }
                }
            }

        }
    }
}
