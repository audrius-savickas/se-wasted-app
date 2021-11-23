# Backend

## Stack of technologies
- C# 
- ASP. NET

## Set up
```bash
git clone git@github.com:audrius-savickas/se-wasted-app.git
```

## Configuration

### SMTP
Our application uses a SMTP client to send welcome email. In order to use this client, you should define your connection this way:
```bash
cd backend/WebApi
dotnet user-secrets init
dotnet user-secrets set "EmailOptions:Host" "<your-mail-host>"   
dotnet user-secrets set "EmailOptions:Port" "<your-mail-ssl-port>"   
dotnet user-secrets set "EmailOptions:UserName" "<your-mail-username>"   
dotnet user-secrets set "EmailOptions:Password" "<your-mail-password>"   
```
For gmail you can use:
```bash
dotnet user-secrets set "EmailOptions:Host" "smtp.gmail.com"  
dotnet user-secrets set "EmailOptions:Port" "465"
```

Be sure you can see all this data by running this command:
```bash
dotnet user-secrets list
```

### Authorization
Our application use authorization with JWT. Therefore, you should write a secret key to create the tokens.
```bash
cd backend/WebApi
dotnet user-secrets set "TokenOptions:SecurityKey" "<your-security-key>" 
```
Be sure you can see all this data by running this command:
```bash
dotnet user-secrets list
```
Also, we use google cloud api to validate tokens created by Google, to enable social login.
```bash
dotnet user-secrets set "GoogleOptions:ClientId" "<your-client-id>"
```

## Build and run
- Open [the solution file](https://github.com/audrius-savickas/se-wasted-app/blob/main/backend/wasted-app.sln) your favourite IDE.
- Build and run the project.

## Documentation
* Check it [here](./Documentation)
