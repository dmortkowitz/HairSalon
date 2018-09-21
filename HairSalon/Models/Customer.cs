using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using HairSalon;

namespace HairSalon.Models 
{
  public class Customer
  {
   
    private string _customerFirst;
    private int _styleId;
    private int _id;

    public Customer (string customerFirst, int styleId = 0, int customerId = 0) 
    {
      _customerFirst = customerFirst;
      _styleId = styleId;
      _id = customerId;
    }

    public override bool Equals(System.Object otherCustomer)
    {
      if (!(otherCustomer is Customer))
      {
        return false;
      }
      else
      {
        Customer newCustomer = (Customer) otherCustomer;
        bool areIdsEqual = (this.GetCustId() == newCustomer.GetCustId());
        bool areDescriptionsEqual = (this.GetCustName() == newCustomer.GetCustName());
        return (areIdsEqual && areDescriptionsEqual);
      }
    }

    public override int GetHashCode()
    {
      return this.GetCustName().GetHashCode();
    }

    public string GetCustName() 
    {
      return _customerFirst;
    }

    public int GetCustId()
    {
      return _id;
    }

    public int GetStyleId()
    {
      return _styleId;
    }

    public static List<Customer> GetAll() 
    {
      List<Customer> allCustomers = new List<Customer> {};
      MySqlConnection conn = DB.Connection();
      conn.Open ();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM customers;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      
      while (rdr.Read()) 
      {
        int customerId = rdr.GetInt32(0);
        string customerDescription = rdr.GetString(1);
        int customerStylistID = rdr.GetInt32(2);
        Customer newCustomer = new Customer (customerDescription, customerStylistID, customerId);
        allCustomers.Add(newCustomer);
      }
      conn.Close();
      if (conn != null) 
      {
        conn.Dispose();
      }
      return allCustomers;
    }
    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO customers (customerFirst, custStylistId) VALUES (@customerName, @styleId);";

      MySqlParameter customerFirst = new MySqlParameter();
      customerFirst.ParameterName = "@customerName";
      customerFirst.Value = this._customerFirst;
      cmd.Parameters.Add(customerFirst);

      MySqlParameter custStylistId = new MySqlParameter();
      custStylistId.ParameterName = "@styleId";
      custStylistId.Value = this._styleId;
      cmd.Parameters.Add(custStylistId);
     
      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId; 
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    
    public void Edit(string newCustomerName)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE customers SET customerFirst = @newCustomerName WHERE id = @searchId;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = _id;
      cmd.Parameters.Add(searchId);

      MySqlParameter customerFirst = new MySqlParameter();
      customerFirst.ParameterName = "@newCustomerName";
      customerFirst.Value = newCustomerName;
      cmd.Parameters.Add(customerFirst);

      cmd.ExecuteNonQuery();
      _customerFirst = newCustomerName;

      conn.Close();
      if (conn != null)
      {
          conn.Dispose();
      }
    } 

    public void Delete() 
    {
      MySqlConnection conn = DB.Connection ();
      conn.Open ();

      var cmd = conn.CreateCommand () as MySqlCommand;
      cmd.CommandText = @"DELETE FROM customers WHERE id= @thisid;";

      MySqlParameter thisId = new MySqlParameter();
      thisId.ParameterName = "@thisId";
      thisId.Value = _id;
      cmd.Parameters.Add(thisId);

      cmd.ExecuteNonQuery ();

      conn.Close ();
      if (conn != null) 
      {
        conn.Dispose ();
      }
    }
    
    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM customers;";

      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
          conn.Dispose();
      }
    }
    public static Customer Find(int id)
    {
      MySqlConnection conn = DB.Connection(); 
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM customers WHERE id = @thisId;";
      MySqlParameter thisId = new MySqlParameter();
      thisId.ParameterName = "@thisId";
      thisId.Value = id;
      cmd.Parameters.Add(thisId);
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int custId = 0;
      string custName = "";
      int styleId = 0;
      while (rdr.Read())
      {
        custId = rdr.GetInt32(0);
        custName = rdr.GetString(1);
        styleId = rdr.GetInt32(2);
      }
       Customer foundCustomer = new Customer(custName, styleId, custId); 
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return foundCustomer;
    }

    public int FindStylistId()
    {
      int custStylistId = this.GetStyleId();
      Stylist foundStylist = Stylist.Find(custStylistId);
      return foundStylist.GetStyleId();
    }
    public string FindStylistName()
    {
      int stylistId = this.GetStyleId();
      Stylist foundStylist = Stylist.Find(stylistId);
      return foundStylist.GetStyleName();
    } 
  }
}
