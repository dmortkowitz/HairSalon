using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using HairSalon.Models;

namespace HairSalon.Tests
{
  [TestClass]
  public class CustomerTests : IDisposable
{
    public CustomerTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=david_mortkowitz_test; Allow User Variables=True;";
    } 

    [TestMethod]
    public void GetAll_CustomerListEmptyAtFirst_0()
    {
      //Arrange, Act
      int result = Customer.GetCustomers().Count;

      //Assert
      Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void Equals_ReturnsTrueForSameName_Customer()
    {
      //Arrange, Act
      Customer firstcustomer = new Customer("Bob");
      Customer secondCustomer = new Customer("Bob");

      //Assert
      Assert.AreEqual(firstcustomer, secondCustomer);
    }


    [TestMethod]
    public void Find_FindCustomerInDB_Customer()
    {
      //Arrange
      Customer testCustomer = new Customer("Bob", 1);
      testCustomer.Save();

      //Act
      Customer foundCustomer = Customer.Find(testCustomer.GetCustId());

      //Assert
      Assert.AreEqual(testCustomer, foundCustomer);
    }

    [TestMethod]
    public void Edit_UpdateCustomerName_Edit()
    {
      //Arrange
      Customer testCustomer = new Customer("Bob", 1);
      testCustomer.Save();
      testCustomer.Edit("Bill");

      //Act
      Customer foundCustomer = Customer.Find(testCustomer.GetCustId());
      string updateName = foundCustomer.GetCustName();

      //Assert
      Assert.AreEqual("Bill", updateName);
    }

    [TestMethod]
    public void Delete_DeletefromDatabase_Customer()
    {
      //Arrange
      Customer testCustomer = new Customer("Bob", 1);
      testCustomer.Save();

      //Act 
      testCustomer.Delete();
      List<Customer> allCustomers = Customer.GetCustomers();
      int totalCustomers = allCustomers.Count;

      //Assert
      Assert.AreEqual(0, totalCustomers);
    }

    public void Dispose()
    {
      Customer.DeleteAll();
      Stylist.DeleteAll();
    }
  }
}

