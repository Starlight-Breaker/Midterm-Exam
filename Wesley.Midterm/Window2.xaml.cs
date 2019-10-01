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
using System.Windows.Shapes;
using Newtonsoft.Json;
using System.IO;

namespace Wesley.Midterm
{
    /// <summary>
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        string item;
        public Window2(string whatimlookingfor)
        {
            InitializeComponent();
            this.item = whatimlookingfor;
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

            public string description { get; set; }

            [JsonProperty("sub-items")]
            public sItems[] SubI { get; set; }

        }

        public class sItems
        {

            public string name { get; set; }
            public string price { get; set; }
            public disc discount { get; set; } 
        }

        public class disc
        {
            public string amount { get; set; }
        }

        private void Windows_Loaded(object sender, EventArgs e)
        {
            var theitem = item;
            var testing = JsonConvert.DeserializeObject<Menu>(File.ReadAllText(System.AppDomain.CurrentDomain.BaseDirectory + "restaurant_menu.json"));
            string find = null;
            int length = testing.categorys.GetLength(0);
            for (int x = 0; x < length; ++x)
            {
                int length2 = testing.categorys[x].mItem.GetLength(0);
                for (int y = 0; y < length2; y++)
                {
                    int length3 = testing.categorys[x].mItem[y].SubI.GetLength(0);
                    for (int z = 0; z < length3; z++)
                    {
                        find = testing.categorys[x].name + " - " + testing.categorys[x].mItem[y].name + " - " + testing.categorys[x].mItem[y].SubI[z].name;
                        if (find == item)
                        {
                            lblProduct.Content = testing.categorys[x].mItem[y].name;
                            lblPrice.Content = testing.categorys[x].mItem[y].SubI[z].price;
                            lblDesc.Content = testing.categorys[x].mItem[y].description;                           
                            lblDiscount.Content = testing.categorys[x].mItem[y].SubI[z].discount.amount;
                            
                        }
                    }
                }
            }

        }

    }
}
