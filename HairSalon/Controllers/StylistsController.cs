using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;

namespace HairSalon.Controllers 
{
  public class StylistsController : Controller 
  {

    [HttpGet ("/stylists")]
    public ActionResult Index() 
    {
      List<Stylist> allStylists = Stylist.GetAll();
      return View (allStylists);
    }

    [HttpGet ("/stylists/new")]
    public ActionResult CreateForm() 
    {
      return View();
    }

    [HttpPost ("/stylists")]
    public ActionResult Create (string stylistName) 
    {
      Stylist newStylist = new Stylist(stylistName);
      newStylist.Save();
      List<Stylist> allStylists = Stylist.GetAll();
      return RedirectToAction("Index", allStylists);
    }


    [HttpGet ("/stylists/{id}")]
    public ActionResult Details (int stylistId) 
    {
    Dictionary<string, object> model = new Dictionary<string, object> ();
    Stylist selectedStylist = Stylist.Find(stylistId);
    List<Customer> stylistCustomers = selectedStylist.GetCustomers();
    model.Add ("stylist", selectedStylist);
    model.Add ("customers", stylistCustomers);
    return View (model);
    }
  }
}