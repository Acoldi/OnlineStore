using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Core.Enums;

public enum enRole { Admin, User, }

public enum enOrderStatuses { Pending = 1,
                              Shipping,
                              Delivered,
                              Cancelled}
