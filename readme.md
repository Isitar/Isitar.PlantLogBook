# Plant Log Book

[![Build Status](https://travis-ci.com/Isitar/Isitar.PlantLogBook.svg?branch=master)](https://travis-ci.com/Isitar/Isitar.PlantLogBook)

Simple plant log book application

## Generate Migrations
    
    dotnet ef migrations add NAME -o Data/Migrations --startup-project ../Isitar.PlantLogBook.Api/Isitar.PlantLogBook.Api.csproj --context PlantLogBookContext