| `TokenResponse` Alanı | Alan Tipi | PayU Token Cevap Alanı |
| ----             | ---       | ---                 |
| `Code` | `int` | `code` |
| `Message` | `string` | `message` |
| `Token` | `string` | `TOKEN` |
| `TokenStatus` | `string` | `TOKEN_STATUS` |
| `ExpirationDate` | `string` | `EXPIRATION_DATE` |
| `History` | `IDictionary<int,TokenHistory>` | `HISTORY` |

| `TokenHistory` Alanı | Alan Tipi | PayU Token Cevap Alanı |
| ----             | ---       | ---                 |
| `Date` | `string` | `TOKEN_DATE` |
| `ReferenceNumber` | `string` | `REF_NO` |
| `Amount` | `decimal` | `AMOUNT` |
| `Currency` | `string` | `CURRENCY` |
