using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace PayU.Token
{
	public class TokenResponse
	{
    [JsonProperty("code")]
    public int Code { get; set; }

    [JsonProperty("message")]
    public string Message { get; set; }

    [JsonProperty("TOKEN")]
    public string Token { get; set; }

    [JsonProperty("TOKEN_STATUS")]
    public string TokenStatus { get; set; }

    [JsonProperty("EXPIRATION_DATE")]
    public string ExpirationDate { get; set; }

    [JsonProperty("HISTORY")]
    public IDictionary<int, TokenHistory> History { get; set; }
	}

  public class TokenHistory {
    [JsonProperty("TOKEN_DATE")]
    public string Date { get; set; }
    [JsonProperty("REF_NO")]
    public string ReferenceNumber { get; set; }
    [JsonProperty("AMOUNT")]
    public decimal Amount { get; set; }
    [JsonProperty("CURRENCY")]
    public string Currency { get; set; }
  }
}

