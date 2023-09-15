# HNGxBackendStageOneTask

# CRUD API DOCUMENTATION

Sample Request and response for the endpoint

# create Endpoint  
  HTTP Verb = POST 
  Local url = http://localhost:44326
  Live url = https://hngxbackendstageonetask-u5q5-dev.fl0.io/api
**RequestBody**
    {
      "name": "Ahmad"
    }
**Response**
  {
    "status": true,
    "status_code": 201,
    "message": "Successfull",
    "data": {
        "id": "1",
        "name": "Ahmad"
    }
}

# Read Endpoint  
  HTTP Verb = GET 
  Local url = http://localhost:44326/api/1
  Live url = https://hngxbackendstageonetask-u5q5-dev.fl0.io/api/1
**RequestPath**
  {user_Id}
**Response**
  {
    "status": true,
    "status_code": 200,
    "message": "Successfull",
    "data": {
       "id": "1",
        "name": "Ahmad"
    }
}

# Update Endpoint  
   HTTP Verb = PUT 
   Local url = http://localhost:44326/api/1
   Live url = https://hngxbackendstageonetask-u5q5-dev.fl0.io/api/1
  **RequestPath**
    {user_Id}
**RequestBody**
    {
      "name": "Ahmad"
    }    
**Response**
  {
    "status": true,
    "status_code": 200,
    "message": "Successfull",
    "data": {
       "id": "1",
        "name": "Ahmad"
    }
}

# Delete Endpoint  
  HTTP Verb = DELETE
   Local url = http://localhost:44326/api/1
   Live url = https://hngxbackendstageonetask-u5q5-dev.fl0.io/api/1
**RequestPath**
  {user_Id}
**Response**
  {
    "status": true,
    "status_code": 200,
    "message": "Successfull",
    "data": null
}

#Setting up locally
  Requirement
  - .NET SDK - includes the .NET runtime and command line tools.\
  - Integrated development environment (IDE)  - Visual Studio or Visual Studio Code
  - Visual Studio Code - code editor that runs on Windows, Mac and Linux. If you have a different preferred code editor that's fine too.
  - C# extension for Visual Studio Code - adds support to VS Code for developing .NET applications.
  - PostgreSQL - you'll need access to a running PostgreSQL server instance for the API to connect to, it can be remote  or on your local machine. For this project a create an postgreSQL on render
 
  Setting Up
    - Download or clone the tutorial project code 
    - Start the api by running dotnet run from the command line in the project root folder (where the HNGBACKENDTrack.csproj file is located), this will build and download all Nuget 
      you should see the message Now listening on: http://localhost:44326.
    - You can test the API directly with a tool such as Postman 
    
