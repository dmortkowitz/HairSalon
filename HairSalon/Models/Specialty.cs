using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace HairSalon.Models
{
  public class Specialty
  {
    private string _name;
    private int _id; 

    public Specialty(string specName, int specId = 0)
    {
      _name = specName; 
      _id = specId;
    }
  
    public override bool Equals(System.Object otherSpecialty)
    {
      if (!(otherSpecialty is Specialty))
      {
        return false; 
      }
      else
      {
        Specialty newSpecialty = (Specialty) otherSpecialty; 
        bool areIdsEqual = (this.GetId() == newSpecialty.GetId());
        bool areNamesEqual = (this.GetName() == newSpecialty.GetName());
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
      cmd.CommandText = @"INSERT INTO specialties (specialty_name) VALUES (@specialtyName);";

      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@specialtyName";
      name.Value = this._name;
      cmd.Parameters.Add(name);

      cmd.ExecuteNonQuery();
      _id = (int)cmd.LastInsertedId;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static List<Specialty> GetAll()
    {
      List<Specialty> allSpecs = new List<Specialty> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM specialties;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int specId = rdr.GetInt32(0);
        string specName = rdr.GetString(1);
        Specialty newSpec = new Specialty(specName, specId);
        allSpecs.Add(newSpec);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allSpecs;
    }
    public static Specialty Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand; 
      cmd.CommandText = @"SELECT * FROM specialties WHERE id = @searchId;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = id;
      cmd.Parameters.Add(searchId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int specId = 0;
      string specName = "";
      while(rdr.Read())
      {
        specId = rdr.GetInt32(0);
        specName = rdr.GetString(1);
      }

      Specialty newSpec = new Specialty(specName, specId);
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return newSpec;
    }
    public List<Stylist> GetStylists()
    {
      List<Stylist> allStylist = new List<Stylist> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM stylists where stylist_id = @specId;";
      //UPDATE SQL FOR JOIN

      MySqlParameter newSpecId = new MySqlParameter();
      newSpecId.ParameterName = "@specId"; //Must match controller
      newSpecId.Value = this.GetId();
      cmd.Parameters.Add(newSpecId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int id = 0;
      string stylistName = "";
      // int specId = 0;
      while(rdr.Read())
      {
        id = rdr.GetInt32(0);
        stylistName = rdr.GetString(1);
        // specId = rdr.GetInt32(2);
        Stylist newStylist = new Stylist(stylistName, id); 
        allStylist.Add(newStylist);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allStylist;
    }

    public void Delete() 
    {
      MySqlConnection conn = DB.Connection ();
      conn.Open ();

      var cmd = conn.CreateCommand () as MySqlCommand;
      cmd.CommandText = @"DELETE FROM specialties WHERE id = @thisid;";

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
      cmd.CommandText = @"DELETE from specialties; DELETE from stylist_specialty;";

      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

      public List<Stylist> GetAllStylists()
      {
      List <Stylist> stylists = new List <Stylist>{};
      MySqlConnection conn = DB.Connection();
      conn.Open();

      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT stylists.id, stylists.stylist_name from stylist_specialty JOIN stylists ON stylist_specialty.stylist_id = stylists.id where stylist_specialty.specialty_id = @id;";

      cmd.Parameters.AddWithValue("@id", this._id);

      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while (rdr.Read())
      {
        int stylistId = rdr.GetInt32(0);
        string stylistName = rdr.GetString(1);
        Stylist foundStylist = new Stylist(stylistName, stylistId);
        stylists.Add(foundStylist);
      }

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return stylists;
    }
    
  }
}
