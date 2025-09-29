namespace OnlineStore.Core.Enums;

public enum enRole { Admin, User, }

public enum enOrderStatuses
{
  Pending = 1,
  Shipping,
  Delivered,
  Cancelled
}

public enum enPaymentStatus
{
  Pending = 1,
  Processing = 2,
  Authorized = 3,
  Completed = 4,
  Failed = 5,
  Cancelled = 6,
  Voided = 7,
  Expired = 8,
}

public enum enPaymentStatusZaincash
{
  Pending = 1,
  success,
  completed,
  failed
}

public enum enPaymentMethod
{
  MasterVisa = 1,
  ZainCash = 2,
  Other = 3
}

public enum enCurrencies
{
  IQD,
  USD
}

public enum enTransactionTypes
{
  Sale,
  Auth,
  Capture,
  Void,
  Register,
  Refund,
}
