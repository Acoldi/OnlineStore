using Microsoft.Identity.Client;
using OnlineStore.Core.DTOs;
using OnlineStore.Core.Entities;
using OnlineStore.Core.Enums;
using OnlineStore.Core.ValueObjects;
using OnlineStore.Web.DTOs;

namespace OnlineStore.Core.InterfacesAndServices.Payment;
public interface IPaytabPaymentService
{

  public Task<string> GenereateTransactionURL(Order order,
    CustomerDetails payTabsCustomerDetails, string? description = null);

  public bool ValidateCallBackPayloadSignature(PayTabCallBackReturnResponse payTabCallBackReturnResponse, string RequestSignature);
}
