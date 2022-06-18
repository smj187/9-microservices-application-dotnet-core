using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketService.Contracts.v1.Commands
{
    public record BasketCheckoutCommand(Guid BasketId, Guid UserId, List<Guid> Products, List<Guid> Sets);
}
