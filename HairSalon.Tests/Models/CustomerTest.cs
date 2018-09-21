using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon.Models;

namespace HairSalon.Tests 
{

  [TestClass]
  public class CustomerTest : IDisposable 
  {
    public void Dispose () 
    {
      Item.DeleteAll ();
    }
    public CustomerTests () 
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=david_mortkowitz_test;";
    }
  }
}