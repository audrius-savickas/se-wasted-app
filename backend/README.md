# Backend

## Stack of technologies
- C# 
- ASP. NET

## Set up
```bash
git clone git@github.com:audrius-savickas/se-wasted-app.git
```

## Configuration
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

## Build and run
- Open [the solution file](https://github.com/audrius-savickas/se-wasted-app/blob/main/backend/wasted-app.sln) your favourite IDE.
- Build and run the project.

## Documentation
* Check it [here](./Documentation)
