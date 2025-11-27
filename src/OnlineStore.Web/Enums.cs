namespace OnlineStore.Web;

public enum enTransactionTypesPaytab
{
  Sale,
  Auth,
  Capture,
  Void,
  Register,
  Refund,
}

public enum enTransactionClass
{
  ecom,
  recurring,
  moto
}
public enum CartCurrency
{
  SAR, AED, BHD, EGP, EUR, GBP, HKD, IDR, INR, IQD, JOD, JPY, KWD, MAD, OMR, PKR, QAR, USD
}

public enum ResponseStatus
{
  A, H, P, V, E, D, X
}

public enum ZainCashPaymentStatus
{
  pending, success, completed, failed
}

