using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using HairSalon.Models;

namespace HairSalon.Tests
{
  [TestClass]
  public class SpecialtyTests : IDisposable
{
    public SpecialtyTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=david_mortkowitz_test; Allow User Variables=True;";
    } 

    [TestMethod]
    public void GetAll_SpecialtyListEmptyAtFirst_0()
    {
      //Arrange, Act
      int result = Specialty.GetAll().Count;

      //Assert
      Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void Equals_ReturnsTrueForSameName_Specialty()
    {
      //Arrange, Act
      Specialty firstSpecialty = new Specialty("Haircut", 1);
      Specialty secondSpecialty = new Specialty("Haircut", 1);

      //Assert
      Assert.AreEqual(firstSpecialty, secondSpecialty);
    }

    [TestMethod]
    public void Save_SavesSpecialtyToDatabase_SpecialtyList()
    {
      //Arrange
      Specialty testSpecialty = new Specialty("Haircut");
      testSpecialty.Save();

      //Act
      List<Specialty> result = Specialty.GetAll();
      List<Specialty> testList = new List<Specialty>{testSpecialty};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }



    [TestMethod]
    public void Find_FindsSpecialtyInDatabase_Specialty()
    {
      //Arrange
      Specialty testSpecialty = new Specialty("Haircut");
      testSpecialty.Save();

      //Act
      Specialty foundSpecialty = Specialty.Find(testSpecialty.GetId());

      //Assert
      Assert.AreEqual(testSpecialty, foundSpecialty);
    }

    public void Dispose()
    {
      // Customer.DeleteAll();
      Stylist.DeleteAll();
    }
  }
}

