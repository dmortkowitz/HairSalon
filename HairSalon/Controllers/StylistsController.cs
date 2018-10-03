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
    public ActionResult Details (int id) 
    {
    Dictionary<string, object> model = new Dictionary<string, object> ();
    Stylist selectedStylist = Stylist.Find(id);
    List<Customer> stylistCustomers = selectedStylist.GetCustomers();
    model.Add ("stylist", selectedStylist);
    model.Add ("customers", stylistCustomers);
    return View (model);
    }

    [HttpGet ("/stylists/{id}/specialties")]
    public ActionResult Specialty (int id) 
    {
    Dictionary<string, object> model = new Dictionary<string, object> ();
    Stylist selectedStylist = Stylist.Find(id);
    List<Specialty> stylistSpecs = selectedStylist.GetSpecialties();
    model.Add ("stylist", selectedStylist);
    model.Add ("specialtys", stylistSpecs);
    return View(model);
    }


    [HttpGet("/stylists/{id}/specialties/new")]
    public ActionResult AddSpecialty(int id)
    {
      Stylist foundStylist = Stylist.Find(id);
      return View(foundStylist);
    }

    [HttpPost("/stylists/{id}/specialties/new")]
    public ActionResult AddSpecialty(int id, int specialtyId)
    {
      Stylist foundStylist = Stylist.Find(id);
      foundStylist.AddSpecialty(specialtyId);
      return RedirectToAction("Details", new {id = id});
    }


    [HttpPost("/stylists/{id}/delete")]
    public ActionResult DeleteStylist(int id)
    {
      Stylist newStylistId = Stylist.Find(id);
      newStylistId.Delete();
      List<Stylist> allStylists = Stylist.GetAll();
      return RedirectToAction("Index", allStylists);
    }

    // [HttpGet ("/stylists/{id}")]
    // public ActionResult DeleteAll (int id) 
    // {
    // Dictionary<string, object> model = new Dictionary<string, object> ();
    // Stylist selectedStylist = Stylist.Find(id);
    // // selectedStylist.DeleteAll();
    // // model.Add ("stylist", selectedStylist);
    // // model.Add ("customers", stylistCustomers);
    // return View (model);
    // }
  }
}