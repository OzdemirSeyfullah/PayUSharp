using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace PayU.Token
{
	public class TokenResponse
	{
    [JsonProperty("code")]
    public int Code;

    [JsonProperty("message")]
    public string Message;

    [JsonProperty("TOKEN")]
    public string Token;

    [JsonProperty("TOKEN_STATUS")]
    public string TokenStatus;

    [JsonProperty("EXPIRATION_DATE")]
    public string ExpirationDate;

    [JsonProperty("HISTORY")]
    public IDictionary<int, TokenHistory> History;
	}

  public class TokenHistory {
    [JsonProperty("TOKEN_DATE")]
    public string Date;
    [JsonProperty("REF_NO")]
    public string ReferenceNumber;
    [JsonProperty("AMOUNT")]
    public decimal Amount;
    [JsonProperty("CURRENCY")]
    public string Currency;
  }
}

