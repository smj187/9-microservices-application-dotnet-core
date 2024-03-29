﻿using CatalogService.Core.Domain.Categories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.Commands.Categories
{
    public class AddSetToCategoryCommand : IRequest<Category>
    {
        public Guid CategoryId { get; set; }
        public Guid SetId { get; set; }
    }
}
