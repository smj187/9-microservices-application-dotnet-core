﻿using CatalogService.Core.Domain.Products;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.Commands.Products
{
    public class PatchProductQuantityCommand : IRequest<Product>
    {
        public Guid ProductId { get; set; }
        public int? Quantity { get; set; }
    }
}
