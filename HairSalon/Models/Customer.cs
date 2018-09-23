using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using HairSalon;

namespace HairSalon.Models 
{
  public class Customer
  {
   
    private string _customerName;
    private int _stylistId;
    private int _id;

    public Customer (string customerName, int stylistId = 0, int id = 0) 
    {
      _customerName = customerName;
      _stylistId = stylistId;
      _id = id;
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
      return _customerName;
    }

    public int GetCustId()
    {
      return _id;
    }

    public int GetStylistId()
    {
      return _stylistId;
    }

    public static List<Customer> GetAll(int stylistId) 
    {
      List<Customer> allCustomers = new List<Customer> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM customers where stylistId = @stylistId;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      
      MySqlParameter searchStylistId = new MySqlParameter();
      searchStylistId.ParameterName = "@stylistId";
      searchStylistId.Value = stylistId;
      cmd.Parameters.Add(searchStylistId);
      while (rdr.Read()) 
      {
        int customerId = rdr.GetInt32(0);
        string customerName = rdr.GetString(1);
        int customerStylistId = rdr.GetInt32(2);
        Customer newCustomer = new Customer (customerName, customerStylistId, customerId);
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
      cmd.CommandText = @"INSERT INTO customers (customerName, stylistId) VALUES (@customerName, @stylistId);";

      MySqlParameter customerName = new MySqlParameter();
      customerName.ParameterName = "@customerName";
      customerName.Value = this._customerName;
      cmd.Parameters.Add(customerName);

      MySqlParameter stylistId = new MySqlParameter();
      stylistId.ParameterName = "@stylistId";
      stylistId.Value = this._stylistId;
      cmd.Parameters.Add(stylistId);
     
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
      cmd.CommandText = @"UPDATE customers SET customerName = @newCustomerName WHERE id = @searchId;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = _id;
      cmd.Parameters.Add(searchId);

      MySqlParameter customerName = new MySqlParameter();
      customerName.ParameterName = "@newCustomerName";
      customerName.Value = newCustomerName;
      cmd.Parameters.Add(customerName);

      cmd.ExecuteNonQuery();
      _customerName = newCustomerName;

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
    public static Customer Find(int id) //////check
    {
      MySqlConnection conn = DB.Connection(); 
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM customers WHERE id = @id;";
      MySqlParameter thisId = new MySqlParameter();
      thisId.ParameterName = "@id";
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

    // public int FindStylistId()
    // {
    //   int stylistId = this.GetStylistId();
    //   Stylist foundStylist = Stylist.Find(stylistId);
    //   return foundStylist.GetId();
    // }
    // public string FindStylistName()
    // {
    //   int stylistId = this.GetStylistId();
    //   Stylist foundStylist = Stylist.Find(stylistId);
    //   return foundStylist.GetName();
    // } 
  }
}
