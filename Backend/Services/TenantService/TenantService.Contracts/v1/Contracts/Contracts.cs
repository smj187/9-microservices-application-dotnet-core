using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TenantService.Contracts.v1.Contracts
{
    // requests
    public record CreateTenantRequest([Required] string Name, [Required] string Email, [Required] string Phone, [Required] string Street, [Required] string City, [Required] string State, [Required] string Country, [Required] string Zip);

    public record AddTenantWorkingdayRequest([Required] int Workingday, string? Message, int? OpeningHour, int? ClosingHour, int? OpeningMinute, int? ClosingMinute);
    public record RemoveTenantWorkingdayRequest([Required] int Workingday);
    public record PatchTenantInformationRequest([Required] string Name, string? Description, [Required] decimal MinimunOrderAmount, [Required] bool IsFreeDelivery, decimal? DeliveryCost, string? WebsiteUrl, string? Imprint, [Required] string Email, [Required] string Phone, string? Payments);
    public record PatchTenantAddressRequest([Required] string Street, [Required] string City, [Required] string State, [Required] string Country, [Required] string Zip);



    // responses
    public record TenantResponse(Guid Id, string Name, string? Description, string Email, string Phone, TenantAddressResponse Address, List<TenantWorkingDayResponse> Workingdays, decimal MinimunOrderAmount, bool IsFreeDelivery, decimal? DeliveryCost, string? WebsiteUrl, string? Imprint, string? Payments, Guid? BrandImageAssetId, Guid? LogoAssetId, Guid? VideoAssetId, Guid? BannerAssetId, DateTimeOffset CreatedAt, DateTimeOffset? ModifiedAt);
    public record TenantAddressResponse(string Street, string City, string State, string Country, string Zip);
    public record TenantWorkingDayResponse(string? Opening, string? Closing, string WeekdayValue, string WeekdayDescription, string? Message, bool IsClosedToday);
}
