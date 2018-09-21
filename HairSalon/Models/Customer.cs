using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using HairSalon;

namespace HairSalon.Models 
{
  public class Customer
  {
   
    private string _customerFirst;
    private string _customerLast;
    private int _custStylistId;
    private int _customerId;

    public Customer (string customerFirst, string customerLast, int stylistId = 0, int customerId = 0) 
    {
      _customerFirst = customerFirst;
      _customerLast = customerLast;
      _custStylistId = custStylistId;
      _customerId = customerId;
    }
  }
}
