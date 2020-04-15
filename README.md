# GetAzureBlockchainCredentials

To really use Azure Blockchain Workbench programmatically, you need to be able to fetch credentials from Azure in order to get a to get a token, and use that as an authorization header on your API request. Then you can do things such as view contracts and transactions, or upload new smart contracts.

This is an example script in F# that fetches the token, and makes an API request to get contracts.
