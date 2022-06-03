using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TrainTickets.View.HomePage
{
    public class ItemMenu
    {
        public ItemMenu(string header, List<SubItem> subItems, string icon)
        {
            Header = header;
            SubItems = subItems;
            Icon = icon;
        }

        public string Header { get; private set; }
        public string Icon { get; private set; }
        public List<SubItem> SubItems { get; private set; }
        public UserControl Screen { get; private set; }
    }
}
