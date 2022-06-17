namespace CatalogService.Contracts.v1.Events
{
    public record ItemAllocationInformation(Guid Id, string Name, decimal Price, bool IsVisible, int? Quantity = null);

    public record CatalogAllocationNotVisibleEvent(Guid CorrelationId, Guid OrderId, List<ItemAllocationInformation> Items, string? Message = null);
    public record CatalogAllocationOutOfStockEvent(Guid CorrelationId, Guid OrderId, List<ItemAllocationInformation> Items, string? Message = null);
    public record CatalogAllocationSuccessEvent(Guid CorrelationId, Guid OrderId, List<ItemAllocationInformation> Items, string? Message = null);
}
