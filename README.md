
# Unit Testing with xUnit in ASP.NET Core
In order to test our application by using xUnit testing framework, we proceeded as follows: 
 - Create the xUnit project (required libraries are automatically installed)
 - Make project reference to the main project
 - Using `[Fact]`, `[Theory]`, and `[InlineData]` attributes. 
 - Also, we have created several tests to test our validation logic from the **AccountNumberValidation** class.
   
[Check the test code for `AccountNumberValidation`](https://github.com/fsmaili77/UnitTesting/commit/45a8dd926c5e917428c67365e5d5237d4136faee)

# Testing Controllers with Unit Tests and Moq in ASP.NET Core
As we are aware that controllers use **Dependency injection** in order to inject the interface into our controller, therefore the controller has a dependency on the repository logic through that injected interface.
In the case of writing tests for our controllers or any other class in a project, we should isolate those dependencies.
Some advantages of isolating dependencies in a test code:
 - We don’t have to initialize all dependencies to return correct values, thus making our test code much simplified
 - If our test fails and we didn’t isolate dependency, we can’t be sure whether it fails due to some error in a controller or in that dependency
 - When dependent code communicates with a real database, as our repository does, the test code could take more time to execute. This can happen due to connection issues or simply due to the time needed to fetch the data from the database.
We need to install the `Moq` library in order to isolate dependency in the test code.

[Check the test code for `Controllers`](https://github.com/fsmaili77/UnitTesting/commit/b3c7f261cdf7451e647a1f065db574d3158167ab)

# Integration Testing in ASP.NET Core
> Integration testing ensures that different components inside the application function correctly when working together.
> The main difference between integration testing and unit testing is that integration testing often includes application’s infrastructure components like database, file system, etc. When we work with unit tests, we mock these mentioned components. But with integration testing, we want to ensure that the whole app is working as expected with all of these components combined together.

After the preliminary intro for Integration testing, below we will outline the flow of adding such tests in our application:
- Creation of a new `xUnit` project for integration testing purposes
- Referencing to the main project
- Installation of two NuGet packages
  - `AspNetCore.Mvc.Testing` – this package provides the TestServer and an important class WebApplicationFactory to help us bootstrap our app in-memory
  - `Microsoft.EntityFrameworkCore.InMemory` – In-memory database provider


[Check the code for `Integration testing`](https://github.com/fsmaili77/UnitTesting/commit/e1412ea87c5b1709dc159a0ed7b5fd5e4cdc9d69)
### AntiForgeryToken validation
In the provided code for Integration testing, you may find
 instructions on how to extract AntiForgeryToken from HTML response and how to use it in our tests.

[Check the code for `AntiForgeryToken validation`](https://github.com/fsmaili77/UnitTesting/commit/4e0079b9c711224d058acdf2d021c11bbc36e562)

# Automated UI Tests with Selenium in ASP.NET Core
The flow of adding UI Tests in our application is as follows:
- Creation of a new `xUnit` project for Automated UI testing
- Installation of two NuGet packages
  - `Selenium.WebDriver`
  - `Selenium.WebDriver.ChromeDriver`
- Implementing IDisposable interface to our test class

> **Notice:** Before you start the Test Explorer window, you need to start your application without debugging (CTRL+F5) because a running server is required for UI tests to pass.

[Check the final version of the code with `Automated UI Tests` included](https://github.com/fsmaili77/UnitTesting)





