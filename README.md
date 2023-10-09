
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




