# PrismHelperDotNetCoreWPF

## Libs.Prism.Navigation
This project uses the .netcore 3.1 DI features to make page navigation simple on WPF projects.

### Setup
#### 1 - Install Libs.Prism.Navigation
##### If you are using Visual Studio do it with the following command on your Package Manager Console or search for it in the Nuget Package Manager 
Install-Package Libs.Prism.Navigation -Version 0.1.4
##### Else if you are using visual studio code use the following command
dotnet add package Libs.Prism.Navigation --version 0.1.4
#### 2 - With the package installed, go to the App.xaml.cs and make the App class extends from PrismApplication instead of Application
```csharp
public partial class App : PrismApplication
```
#### 3 - After you do it you will need to override, ConfigureServices, CreateConfiguration and CreateWindow Methods
```csharp
public partial class App : PrismApplication
{
    protected override void ConfigureServices(IServiceCollection collection, IConfiguration configuration)
    {
        throw new NotImplementedException();
    }

    protected override void CreateConfiguration(IConfigurationBuilder builder)
    {
        throw new NotImplementedException();
    }

    protected override Window CreateWindow(IServiceProvider provider)
    {
        throw new NotImplementedException();
    }
}
 ```
* **CreateConfiguration(IConfigurationBuilder builder)**:

This method has a IConfigurationBuilder as his parameter, you can use it to configure your app, read more about the configuration builder from the microsoft documentation clicking [here](https://docs.microsoft.com/pt-br/dotnet/api/microsoft.extensions.configuration.iconfigurationbuilder?view=dotnet-plat-ext-3.1)

* **ConfigureServices(IServiceCollection collection, IConfiguration configuration)**:

This is one of the mos important method's in this libs, here is where the things happen's, here you can register your services, repositories, DAO's and all you need on your dependency injection.

This method has two parameteres, **IServiceCollection services** and a **IConfiguration configuration**.

The parameter collection of type **IServiceCollection** is the "Collection" of services where you will register you classes to **.netcore DI** with somthing like the code below, read more about IServiceCollection [here](https://docs.microsoft.com/pt-br/dotnet/api/microsoft.extensions.dependencyinjection.iservicecollection?view=dotnet-plat-ext-3.1).

```csharp
collection.AddScoped<IMyService, MyService>();
```

The configuration parameter of type **IConfiguration** is the result obtained from the CreateConfiguration method, this parameter is commonly used to retrieve data such as the database connection string and other settings that the application may have, read more about it [here](https://docs.microsoft.com/pt-br/dotnet/api/microsoft.extensions.configuration.iconfiguration?view=dotnet-plat-ext-3.1).

* **CreateWindow(IServiceProvider provider)**:

This method is has a single parameter that is nothing less than the IServiceProvider. This parameter will be able to resolve all the classes que foram registradas no método ConfigureServices.
