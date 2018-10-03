using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace HairSalon.Models
{
  public class Stylist
  {
    private string _name;
    private int _id;

    public Stylist(string stylistName, int stylistId = 0)
    {
      _name = stylistName;
      _id = stylistId;
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
        bool areIdsEqual = (this.GetId() == newStylist.GetId());
        bool areNamesEqual = (this.GetName() == newStylist.GetName());
        return (areIdsEqual && areNamesEqual);
      }
    }

    public override int GetHashCode()
    {
      return this.GetId().GetHashCode();
    }

    public string GetName()
    {
      return _name;
    }

    public int GetId()
    {
      return _id;
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO stylists (stylist_name) VALUES (@name);";

      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@name";
      name.Value = this._name;
      cmd.Parameters.Add(name);

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


    public static Stylist Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM stylists where id = @searchId;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = id;
      cmd.Parameters.Add(searchId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int stylistId = 0;
      string stylistName = "";
      while(rdr.Read())
      {
        stylistId = rdr.GetInt32(0);
        stylistName = rdr.GetString(1);
      }

      Stylist newStylist = new Stylist(stylistName, stylistId);
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return newStylist;
    }

        //     internal void AddItem(Item newItem)
        // {
        //     throw new NotImplementedException();
        // }

    public void Delete() 
    {
      MySqlConnection conn = DB.Connection ();
      conn.Open ();

      var cmd = conn.CreateCommand () as MySqlCommand;
      cmd.CommandText = @"DELETE FROM stylists WHERE id = @thisid; DELETE FROM customers WHERE stylist_id = @thisid; DELETE FROM stylist_specialty WHERE stylist_id = @thisid;";

      MySqlParameter thisId = new MySqlParameter();
      thisId.ParameterName = "@thisId";
      thisId.Value = this._id;
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
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE from stylists; DELETE from customers; DELETE from stylist_specialty;";

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
      cmd.CommandText = @"SELECT * FROM customers where stylist_id = @stylistId;";

      MySqlParameter newStylistId = new MySqlParameter();
      newStylistId.ParameterName = "@stylistId";
      newStylistId.Value = this.GetId();
      cmd.Parameters.Add(newStylistId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int id = 0;
      string customerName = "";
      int stylistId = 0;
      while(rdr.Read())
      {
        id = rdr.GetInt32(0);
        customerName = rdr.GetString(1);
        stylistId = rdr.GetInt32(2);
        Customer newCustomer = new Customer(customerName, stylistId, id);
        allStylistCustomers.Add(newCustomer);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allStylistCustomers;
    }
    public void AddSpecialty(int specialtyId)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO stylist_specialty (stylist_id, specialty_id) VALUES (@stylistId, @specialtyId);";

      cmd.Parameters.AddWithValue("@specialtyId", specialtyId);
      cmd.Parameters.AddWithValue("@stylistId", this._id);
      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public List<Specialty> GetSpecialties()
    {
      List<Specialty> allStylistSpecs = new List<Specialty> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT a.stylist_id, a.specialty_id, b.specialty_name, c.stylist_name FROM stylist_specialty a JOIN specialties b ON a.specialty_id = b.id JOIN stylists c ON a.stylist_id = c.id where a.stylist_id = @stylistId;";
      
      cmd.Parameters.AddWithValue("@stylistId", this._id);

      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while (rdr.Read())
      {
        int specialtyId = rdr.GetInt32(1);
        string specialtyName = rdr.GetString(2);
        Specialty foundSpecialty = new Specialty(specialtyName, specialtyId);
        allStylistSpecs.Add(foundSpecialty);
      }

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allStylistSpecs;
    }
  }
}