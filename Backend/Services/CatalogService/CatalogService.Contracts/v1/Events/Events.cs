namespace CatalogService.Contracts.v1.Events
{
    public record ItemAllocationInformation(Guid Id, string Name, decimal Price, bool IsVisible);

    public record CatalogAllocationUnavailableErrorSagaEvent(Guid CorrelationId, Guid OrderId, List<ItemAllocationInformation> Products, List<ItemAllocationInformation> Sets, string? Message = null);
    public record CatalogAllocationOutOfStockErrorSagaEvent(Guid CorrelationId, Guid OrderId, List<ItemAllocationInformation> Products, List<ItemAllocationInformation> Sets, string? Message = null);
    public record CatalogAllocationSuccessSagaEvent(Guid CorrelationId, Guid OrderId, List<ItemAllocationInformation> Products, List<ItemAllocationInformation> Sets);
}
