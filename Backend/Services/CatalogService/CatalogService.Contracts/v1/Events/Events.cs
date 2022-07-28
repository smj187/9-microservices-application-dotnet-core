namespace CatalogService.Contracts.v1.Events
{
    public record ItemAllocationInformation(Guid Id, string Name, decimal Price, bool IsVisible);

    public record CatalogProductsAndSetsAllocationSagaSuccessEvent(Guid CorrelationId, List<ItemAllocationInformation> Products, List<ItemAllocationInformation> Sets);
    public record CatalogProductsAndSetsUnavailableSagaErrorEvent(Guid CorrelationId, List<ItemAllocationInformation> Products, List<ItemAllocationInformation> Sets, string? Message = null);
    public record CatalogProductsAndSetsOutOfStockSagaErrorEvent(Guid CorrelationId, List<ItemAllocationInformation> Products, List<ItemAllocationInformation> Sets, string? Message = null);
}
