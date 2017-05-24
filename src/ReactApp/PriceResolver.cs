using AutoMapper;
using Models;
using ReactApp.ViewModels.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactApp
{
    public class PriceResolver : IValueResolver<Item, ItemViewModel, double>
    {
        public double Resolve(Item source, ItemViewModel destination, double member, ResolutionContext context)
        {
            return source.Price / 100.0;
        }
    }
}
