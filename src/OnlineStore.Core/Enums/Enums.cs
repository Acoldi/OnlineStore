namespace OnlineStore.Core.Enums;

public enum enRole { Admin, User, }

public enum enOrderStatuses
{
  Pending = 1,
  Shipping,
  Delivered,
  Cancelled
}

public enum enPaymentStatus { 
  Pending=1,
  Processing=2,
  Authorized=3,
  Completed=4,
  Failed=5,
  Cancelled=6,
  Voided=7,
  Expired=8,

}

public enum enPaymentMethod
{
  Master = 1,
  ZainCash = 2,
  Other=3
}
