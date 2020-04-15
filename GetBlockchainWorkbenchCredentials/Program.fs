open System.Text
open Newtonsoft.Json.Linq
open Hopac
open HttpFs.Client

let TENANT = "johndoehotmail.onmicrosoft.com"
let WORKBENCH_API_ROUTE = "your-app-api.azurewebsites.net/api/v2/contracts?workflowId=1&sortBy=Timestamp&top=50&skip=0"
let RESOURCE = "UUID"
let CLIENT_ID = "UUID"
let CLIENT_SECRET = "HASH"

let encoded = Encoding.ASCII.GetBytes(sprintf "resource=%s\n\
                                      &client_id=%s\n\
                                      &client_secret=%s=\n\
                                      &grant_type=client_credentials\n\
                                      " RESOURCE CLIENT_ID CLIENT_SECRET)

let getToken =
    Request.createUrl Post (sprintf "https://login.microsoftonline.com/%s/oauth2/token" TENANT)
    |> Request.setHeader (Accept "application/x-www-form-urlencoded")
    |> Request.body (BodyRaw encoded)
    |> Request.responseAsString
    |> run
    |> JObject.Parse
    
let tokenPath = getToken.SelectToken "access_token" |> string

let getAuth token =
    Request.createUrl Get WORKBENCH_API_ROUTE
    |> Request.setHeader (Authorization (sprintf "Bearer %s" token))
    |> Request.responseAsString
    |> run

[<EntryPoint>]
let main argv =
    tokenPath
    |> getAuth
    |> printfn "%s"
    0 