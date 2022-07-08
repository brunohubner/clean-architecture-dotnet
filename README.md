- Run project
```
dotnet run --project CleanArchMvc.WebUI/CleanArchMvc.WebUI.csproj
```

- Create migrations
```
dotnet ef --startup-project ../CleanArchMvc.WebUI/ migrations add <MigrationName>
```

- Apply migrations
````
dotnet ef --startup-project ../CleanArchMvc.WebUI/ database update
````

- Install packages
```
dotnet add package <Package.Name>
```

user@localhost

@User12345

admin@localhost

@Admin12345
