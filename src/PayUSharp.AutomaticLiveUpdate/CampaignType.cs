using System;
using System.Linq;
using System.Collections.Generic;
using PayU.Core;

namespace PayU.AutomaticLiveUpdate
{
  public sealed class CampaignType
  {
    public readonly static CampaignType ExtraInstallments = new CampaignType(new PriorityValue(10, "EXTRA_INSTALLMENTS"));
    public readonly static CampaignType DelayInstallments = new CampaignType(new PriorityValue(20, "DELAY_INSTALLMENTS"));

    private struct PriorityValue
    {
      public string Value;
      public int Priority;

      public PriorityValue(int priority, string value)
      {
        Priority = priority;
        Value = value;
      }
    }

    private CampaignType(params PriorityValue[] values)
    {
      Values = values;
    }

    private PriorityValue[] Values;

    public override string ToString()
    {
      return string.Join(",", Values.OrderBy(pv => pv.Priority).Select(pv => pv.Value).ToArray());
    }

    public static CampaignType operator |(CampaignType left, CampaignType right)
    {
      return new CampaignType(left.Values.Concat(right.Values).ToArray());
    }
  }
}
