using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace HairSalon.Models
{
  public class Stylist
  {
    private string _stylistFirst;
    private string _stylistLast;
    private int _stylistId;

    public Stylist (string stylistFirst, string stylistLast, int stylistId)
    {
      _stylistFirst = stylistFirst;
      _stylistLast = stylistLast;
      _stylistId = stylistId;
    }
  }
}