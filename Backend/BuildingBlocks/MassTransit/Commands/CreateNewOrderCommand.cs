using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.MassTransit.Commands
{
    public class CreateNewOrderCommand
    {
        private readonly Guid _basketId;
        private readonly Guid _userId;
        private readonly List<Guid> _products;
        private readonly List<Guid> _sets;

        public CreateNewOrderCommand(Guid basketId, Guid userId, List<Guid> products, List<Guid> sets)
        {
            _basketId = basketId;
            _userId = userId;
            _products = products;
            _sets = sets;
        }

        public Guid BasketId => _basketId;
        public Guid UserId => _userId;
        public List<Guid> Products => _products;
        public List<Guid> Sets => _sets;
    }
}
