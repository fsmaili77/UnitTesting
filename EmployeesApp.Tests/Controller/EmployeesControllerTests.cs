using EmployeesApp.Contracts;
using EmployeesApp.Controllers;
using EmployeesApp.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesApp.Tests.Controller
{
    public class EmployeesControllerTests
    {
        // Declare a private field to hold a mock instance of IEmployeeRepository.
        private readonly Mock<IEmployeeRepository> _mockRepo;

        // Declare a private field to hold an instance of the EmployeesController.
        private readonly EmployeesController _controller;

        // Constructor for the test class.
        public EmployeesControllerTests()
        {
            // Initialize the mock repository with Moq.
            _mockRepo = new Mock<IEmployeeRepository>();

            // Create an instance of the EmployeesController, passing the mock repository as a parameter.
            _controller = new EmployeesController(_mockRepo.Object);
        }

        // Test method for the Index action that checks if it returns a view.
        [Fact]
        public void Index_ActionExecutes_ReturnViewForIndex()
        {
            // Call the Index action on the controller.
            var result = _controller.Index();

            // Assert that the result is of type ViewResult.
            Assert.IsType<ViewResult>(result);
        }

        // Test method for the Index action that checks if it returns the expected number of employees.
        [Fact]
        public void Index_ActionExecutes_ReturnsExactNumberOfEmployees()
        {
            // Setup the mock repository to return a list of two Employee objects when GetAll is called.
            _mockRepo.Setup(repo => repo.GetAll())
                .Returns(new List<Employee>() { new Employee(), new Employee() });

            // Call the Index action on the controller.
            var result = _controller.Index();

            // Assert that the result is of type ViewResult.
            var viewResult = Assert.IsType<ViewResult>(result);

            // Assert that the model in the ViewResult is of type List<Employee>.
            var employees = Assert.IsType<List<Employee>>(viewResult.Model);

            // Assert that there are exactly 2 employees in the list.
            Assert.Equal(2, employees.Count);
        }

        [Fact]
        public void Index_ActionExecuted_ReturnViewForCreate()
        {
            // Call the Index action on the controller.
            var result = _controller.Index();

            // Assert that the result is of type ViewResult, indicating that it's a view.
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Index_InvalidModelState_ReturnView()
        {
            // Add a model error to the controller's ModelState dictionary.
            _controller.ModelState.AddModelError("Name", "Name is required");

            // Create an Employee object with specified properties.
            var empoloyee = new Employee { Age = 25, AccountNumber = "225-6589789451-12" };

            // Call the Create action on the controller, passing the employee as a parameter.
            var result = _controller.Create(empoloyee);

            // Assert that the result is of type ViewResult, indicating that it's a view.
            var viewResult = Assert.IsType<ViewResult>(result);

            // Assert that the model in the ViewResult is of type Employee.
            var testEmployee = Assert.IsType<Employee>(viewResult.Model);

            // Assert that the AccountNumber and Age properties in the testEmployee match the values in the original employee object.
            Assert.Equal(empoloyee.AccountNumber, testEmployee.AccountNumber);
            Assert.Equal(empoloyee.Age, testEmployee.Age);
        }

        [Fact]
        public void Create_InvalidModelState_CreateEmployeeNeverExecutes()
        {
            // Add a model error to the controller's ModelState indicating that the "Name" field is required.
            _controller.ModelState.AddModelError("Name", "Name is required");

            // Create an Employee object with the "Age" property set to 65, but without a "Name."
            var empoloyee = new Employee { Age = 65 };

            // Call the Create action on the controller, passing the employee as a parameter.
            _controller.Create(empoloyee);

            // Verify that the "CreateEmployee" method of the mock repository was never executed.
            _mockRepo.Verify(x => x.CreateEmployee(It.IsAny<Employee>()), Times.Never);
        }

        [Fact]
        public void Create_ModelStateValid_CreateEmployeeCalledOnce()
        {
            // Declare a variable to capture the employee passed to the "CreateEmployee" method.
            Employee? emp = null;

            // Set up the mock repository to capture the input parameter using a callback.
            _mockRepo.Setup(r => r.CreateEmployee(It.IsAny<Employee>()))
                .Callback<Employee>(x => emp = x);

            // Create an Employee object with valid properties.
            var employee = new Employee
            {
                Name = "Test Employee",
                Age = 32,
                AccountNumber = "123-5435789603-21"
            };

            // Call the Create action on the controller, passing the employee as a parameter.
            _controller.Create(employee);

            // Verify that the "CreateEmployee" method of the mock repository was called exactly once.
            _mockRepo.Verify(x => x.CreateEmployee(It.IsAny<Employee>()), Times.Once);

            // Assert that the captured employee parameter matches the input employee's properties.
            Assert.Equal(emp.Name, employee.Name);
            Assert.Equal(emp.Age, employee.Age);
            Assert.Equal(emp.AccountNumber, employee.AccountNumber);
        }

        [Fact]
        public void Create_ActionExecuted_RedirectsToIndexAction()
        {
            // Create an Employee object with valid properties.
            var employee = new Employee
            {
                Name = "Test Employee",
                Age = 45,
                AccountNumber = "123-4356874310-43"
            };

            // Call the Create action on the controller, passing the employee as a parameter.
            var result = _controller.Create(employee);

            // Assert that the result is of type RedirectToActionResult, indicating a redirection.
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);

            // Assert that the action name in the redirection is "Index."
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

    }

}
