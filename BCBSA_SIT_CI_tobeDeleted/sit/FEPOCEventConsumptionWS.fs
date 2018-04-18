module FEPOCEventConsumptionWS
open HttpFs.Client
open Newtonsoft.Json
open Hopac
open System
open canopy.csharp


type TokenRequest = {
  [<JsonProperty("userType")>]
  UserType : string
  [<JsonProperty("userName")>]
  UserName : string
}

type ClientConfig = {
  ClientId : string
  ClientSecret: string
}

type TokenResponse = {
  [<JsonProperty("token_type")>]
  TokenType : string
  [<JsonProperty("access_token")>]
  AccessToken : string
  [<JsonProperty("expires_in")>]
  ExpiresIn : int
}

let jsonPost headers body url =
  let reqBody = JsonConvert.SerializeObject(body)
  let request = Request.createUrl Post url
  let requestWithHeaders =
    List.fold (fun req header -> Request.setHeader header req) request headers
  printfn "Posting %s to URL %s" reqBody url
  let response = 
    requestWithHeaders
    |> Request.bodyString reqBody
    |> getResponse
    |> run
  let resBody = Response.readBodyAsString response |> run
  let statusCode = response.statusCode
  printfn "URL %s returns %d with body %s" url statusCode resBody
  statusCode

let getOuthToken config (tokenRequest : TokenRequest) = 
  printfn "getting OAuth Token\n"  
  let reqBody = JsonConvert.SerializeObject(tokenRequest)
  let contentType = ContentType.parse "application/json; charset=UTF-8"
  let resBody =
    Request.createUrl Post "https://apist.fepblue.org/fepoc/st/oauth2/v1/token"
    |> Request.setHeader (Custom ("x-ibm-client-id", config.ClientId))
    |> Request.setHeader (Custom ("grant_type", "client_credentials"))
    |> Request.setHeader (Accept "application/json")
    |> Request.setHeader (ContentType contentType.Value)
    |> Request.setHeader (Custom ("x-ibm-client-secret", config.ClientSecret))
    |> Request.bodyString reqBody
    |> Request.responseAsString
    |> run
  printfn "OAuth Token Response : %s\n" resBody
  JsonConvert.DeserializeObject<TokenResponse> resBody

type EventsRequestDto = {
  [<JsonProperty("eventType")>]
  EventType : string
  [<JsonProperty("subType")>]
  SubType : string
  [<JsonProperty("eventDateTime")>]
  EventDateTime : string
  [<JsonProperty("sourceSystem")>]
  SourceSystem : string
  [<JsonProperty("contractID")>]
  ContractId : string
  [<JsonProperty("memberID")>]
  MemberId : string
  [<JsonProperty("externalID")>]
  ExternalId : string
}

type EventsRequest = {
  EventType : string
  SubType : string
  SourceSystem : string
  ContractId : string
  MemberId : string
}

let eventsRequestStatusCode config tokenRequest eventsReq =
  printfn "inside events request"
  let tokenResponse = getOuthToken config tokenRequest
  printfn "Bearer Token : %s" tokenResponse.AccessToken
  let contentType = ContentType.parse "application/json; charset=UTF-8"
  let eventsRequestDto : EventsRequestDto = {
    EventType = eventsReq.EventType
    SubType = eventsReq.SubType
    SourceSystem = eventsReq.SourceSystem
    ContractId = eventsReq.ContractId
    MemberId = eventsReq.MemberId
    ExternalId = Guid.NewGuid().ToString()
    EventDateTime = DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss.fffK")

  }
  let headers = [
    Custom ("x-ibm-client-id", config.ClientId)
    Accept "application/json"
    Authorization ("Bearer " + tokenResponse.AccessToken)
    ContentType contentType.Value
  ]
  jsonPost headers eventsRequestDto "https://apist.fepblue.org/fepoc/st/events/v1/events"


  
// --------------------- could come from a config file ------------------------------

let config = {
    ClientId = "bc2aa626-47ad-47be-91db-76c15917a2cb"
    ClientSecret = "hD3nI6fF4qN4iQ4vM7uO5rG2vQ5xH2xG3gS2bU4gY8qW4lA4lY"
}

let tokenRequest = {
    UserName = "bluestester01"
    UserType = "Vendor"
}

let eventsRequest = {
    EventType = "NOTIFICATION_EVENT"
    SubType = "A1C_2ND_HALF_TEST_NOT_COMPLTD"
    SourceSystem = "CHIPREWARDS"
    ContractId = "R60378908"
    MemberId = "14046128"
}
       
//let statusCode = eventsRequestStatusCode config tokenRequest eventsRequest
