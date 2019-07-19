# aspnetcore-mvc-jwt-webapi
Insurance Company API with JWT authentication

## Install

### Prerequisites
- [.Net Core 2.2](https://github.com/dotnet/core)

### Steps
```
git clone
cd aspnetcore-mvc-jwt-webapi
```
### Run
```
dotnet run -p AspNetCore.Mvc.Jwt.WebApi
```
## Usage
Open the app [http://localhost:5000/swagger](http://localhost:5000/swagger) and get fun!

### Authentication

1.- We have to execute the endpoint **/api/Account/authenticate**.
**In this particulary case password is not required, its only necessary to fill in the username because we dont have a pasword to check**
![I'm a relative reference to a repository file](https://github.com/gabrieldalborgo/aspnetcore-mvc-jwt-webapi/blob/master/extra/Captura-1.JPG)

2.- The response body is the JWT token generated, so copy it!

![I'm a relative reference to a repository file](https://github.com/gabrieldalborgo/aspnetcore-mvc-jwt-webapi/blob/master/extra/Captura-2.JPG)

3.- Click the **Authorize** button or any of the **Padlock icon** button

![I'm a relative reference to a repository file](https://github.com/gabrieldalborgo/aspnetcore-mvc-jwt-webapi/blob/master/extra/Captura-3.JPG)

4.- We have to fill in the input with the format "Bearer 'JWT-Token'" (replacing 'JWT-Token with the token generated at the step #2)

![I'm a relative reference to a repository file](https://github.com/gabrieldalborgo/aspnetcore-mvc-jwt-webapi/blob/master/extra/Captura-4.JPG)

5.- Now, we are able to consume the other services!

## 3rd party libraries
- [File logger - Serilog.Extensions.Logging.File](https://github.com/serilog/serilog-extensions-logging-file)
- [Swagger tool - Swashbuckle.AspNetCore](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)

## Authors
- **Gabriel Dal Borgo** - [gabrieldalborgo](https://github.com/gabrieldalborgo)
