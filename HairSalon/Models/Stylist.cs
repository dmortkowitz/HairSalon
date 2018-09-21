using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace HairSalon.Models
{
  public class Stylist
  {
    private string _stylistName;
    private int _id;

    public Stylist(string stylistName, int styleId)
    {
      _stylistName = stylistName;
      _id = styleId;
    }

    public Stylist(string stylistName)
    {
      _stylistName = stylistName;
    }

    public override bool Equals(System.Object otherStylist)
    {
      if (!(otherStylist is Stylist))
      {
        return false;
      }
      else
      {
        Stylist newStylist = (Stylist) otherStylist; 
        bool areIdsEqual = (this.GetStyleId() == newStylist.GetStyleId());
        bool areNamesEqual = (this.GetStyleName() == newStylist.GetStyleName());
        return (areIdsEqual && areNamesEqual);
      }
    }
    public override int GetHashCode()
    {
      return this.GetStyleId().GetHashCode();
    }
    public string GetStyleName()
    {
      return _stylistName;
    }
    public int GetStyleId()
    {
      return _id;
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO stylists (stylistName) VALUES (@stylistName);";

      MySqlParameter stylistName = new MySqlParameter();
      stylistName.ParameterName = "@stylistName";
      stylistName.Value = this._stylistName;
      cmd.Parameters.Add(stylistName);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public static List<Stylist> GetAll()
    {
      List<Stylist> allStylists = new List<Stylist> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM stylists;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int stylistId = rdr.GetInt32(0);
        string stylistName = rdr.GetString(1);
        Stylist newStylist = new Stylist(stylistName, stylistId);
        allStylists.Add(newStylist);
      }
      conn.Close();
      if (conn != null)
      {
          conn.Dispose();
      }
      return allStylists;
    }


    public static Stylist Find(int styleId)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM stylists WHERE id = @searchId;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = styleId;
      cmd.Parameters.Add(searchId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int customerStylistId = 0;
      string customerStylistName = "";
      while(rdr.Read())
      {
        customerStylistId = rdr.GetInt32(0);
        customerStylistName = rdr.GetString(1);
      }
        Stylist newStylist = new Stylist(customerStylistName, customerStylistId);
        conn.Close();
        if (conn != null)
      {
        conn.Dispose();
      }
      return newStylist;
    }

    internal void AddCustomer(Customer newCustomer)
    {
        throw new NotImplementedException();
    }

    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM stylists;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
          conn.Dispose();
      }
    }

      public List<Customer> GetCustomers()
   {
      List<Customer> allStylistCustomers = new List<Customer> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM customers WHERE id = @styleId;";

      MySqlParameter newStylistId = new MySqlParameter();
      newStylistId.ParameterName = "@styleId";
      newStylistId.Value = this.GetStyleId();
      cmd.Parameters.Add(newStylistId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int custId = 0;
      string description = "";
      int styleId = 0;
      while(rdr.Read())
      {
        custId = rdr.GetInt32(0);
        description = rdr.GetString(1);
        styleId = rdr.GetInt32(2);
        Customer newCustomer = new Customer(description, styleId, custId);
        allStylistCustomers.Add(newCustomer);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allStylistCustomers;
    }
  }
}