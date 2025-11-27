using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineStore.Core.InterfacesAndServices;

namespace OnlineStoreTests;
public class UtilityTests
{
  [Fact]
  public void GenereateSlug_TextWithDashesAndSpaces_RetunsStringWithoutSpacesAndDashes()
  {
    // act
    string result = UtilityService.GenerateSlug("Ali - This text is suitable as URL");

    // assert
    Assert.Equal("ali---this-text-is-suitable-as-url", result);
  }
}
