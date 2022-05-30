using CatalogService.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Core.Models
{
    public class CategoryDetailsModel
    {
        public Category Category { get; set; }
        public List<Product> Products { get; set; }
    }
}
