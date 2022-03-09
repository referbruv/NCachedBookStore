# NCachedBookStore

NCachedBookStore is a simple project to demonstrate query caching with Entity Framework Core via NCache. The solution is a web application built on ASP.NET Core 3.1 which connects to a local SQL Server instance and a local NCache cluster for caching.

# What is NCache?

NCache is a popular Cache provider in the .NET space. The cache is built using .NET and has a very good support for .NET applications. It has a rich set of library which can help in implementing query caching over Entity Framework Core. NCache offers a great set of features and comes in three flavors - Open Source, Professional and Enterprise; which customers can choose from based on their needs.

# Features

* A simple solution which demonstrates caching with the scenario of an Entity CRUD
* Follows layered architecture
* Uses Repository pattern and CQRS

# Technologies

* ASP.NET Core 3.1
* Entity Framework Core
* NCache (Enterprise Edition)
* MediatR

# Prerequisites

* A working SQL Server installation or an SQL Server database - update the Connection String in appsettings.json
* A working NCache cluster - update the client.nconf file with your IP Address 

# How do I run this?

The solution is built using ASP.NET Core (.NET 3.1) with a pipeline to upgrade to .NET 6 (soon), so for now you'd need a .NET Core (.NET 3.1) installed on your machine.

1. Clone the solution into your local repository
2. Open the solution in Visual Studio and set NCachedBookStore.Web as the startup project
3. Run the solution

or

1. Clone the solution into your local repository
2. Navigate to NCachedBookStore.Web directory and open a command prompt / Terminal 
3. Execute the command `dotnet run`

# License

The solution is completely open source and is licensed with MIT License.

# Show your Support 

Found this solution helpful and useful? You can do these to help this reach greater audience.

1. Leave a star on this repository :star:
2. Recommend this solution to your colleagues and dev community
3. Join my [Twitter family](https://twitter.com/referbruv). I regularly post awesome content on dev over there.
4. Join my [Facebook community](https://www.facebook.com/referbruv). I regularly post interesting content over there as well.
5. You can also buy me [a cup of great coffee :coffee:](https://www.buymeacoffee.com/referbruv)!

<a href="https://www.buymeacoffee.com/referbruv" target="_blank"><img src="https://cdn.buymeacoffee.com/buttons/default-orange.png" alt="Buy Me A Coffee" height="41" width="174"></a>

For more detailed articles and how-to guides, visit https://referbruv.com
