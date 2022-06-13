using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TenantService.Contracts.v1.Contracts
{
    public record CreateTenantRequest(string Name, string? Description, string Email, string Phone, string Street, string City, string State, string Country, string Zip, decimal MinimunOrderAmount, bool IsFreeDelivery, decimal? DeliveryCost, string? WebsiteUrl, string? Imprint);


    public record AddTenantWorkingdayRequest([Required] int Workday, string? Message, int? OpeningHour, int? ClosingHour, int? OpeningMinute, int? ClosingMinute);
    public record RemoveTenantWorkingdayRequest(int Workday);
    public record PatchTenantDescription(string Name, string? Description);


    public record PatchTenantAddressRequest(string Street, string City, string State, string Country, string Zip);

    public record TenantResponse(Guid Id);
}
