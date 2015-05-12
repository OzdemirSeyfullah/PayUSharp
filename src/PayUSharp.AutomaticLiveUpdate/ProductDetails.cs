using System;
using PayU.Core;

namespace PayU.AutomaticLiveUpdate
{
  public class ProductDetails: PayU.Core.Base.ProductDetails
  {
    [Parameter(Name = "ORDER_VER")]
    public string Version { get; set; }
  }
}
