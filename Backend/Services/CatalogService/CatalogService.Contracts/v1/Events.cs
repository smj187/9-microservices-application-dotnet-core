using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Contracts.v1
{
    public record ItemAllocationInformation(Guid Id, string Name, decimal Price, bool IsVisible);

    public record CatalogProductsAndSetsAllocationSagaSuccessEvent(Guid CorrelationId, List<ItemAllocationInformation> Products, List<ItemAllocationInformation> Sets);
    public record CatalogProductsAndSetsUnavailableSagaErrorEvent(Guid CorrelationId, List<ItemAllocationInformation> Products, List<ItemAllocationInformation> Sets, string? Message = null);
    public record CatalogProductsAndSetsOutOfStockSagaErrorEvent(Guid CorrelationId, List<ItemAllocationInformation> Products, List<ItemAllocationInformation> Sets, string? Message = null);
}
