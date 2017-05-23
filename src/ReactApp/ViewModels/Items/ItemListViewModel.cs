using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactApp.ViewModels.Items
{
    public class ItemListViewModel
    {
        public List<ItemViewModel> Items { get; set; }

        public ItemListViewModel(List<ItemViewModel> items)
        {
            Items = items;
        }
        public ItemListViewModel()
        {

        }
    }
}
