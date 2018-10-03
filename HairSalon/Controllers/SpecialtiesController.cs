using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;

namespace HairSalon.Controllers
{
  public class SpecialtiesController : Controller
  {
  
  [HttpGet("/specialties")]
    public ActionResult Index()
    {
      List<Specialty> allSpecs = Specialty.GetAll();
      return View(allSpecs);
    }
    
    [HttpGet("/specialties/{id}")]
    public ActionResult Details(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object> ();
      Specialty foundSpecialty = Specialty.Find(id);
      List <Stylist> stylists = foundSpecialty.GetAllStylists(); //add to model
      model.Add("specialty", foundSpecialty);
      model.Add("stylists", stylists);
      return View(model);
    }
    [HttpGet("/specialties/new")]
    public ActionResult CreateForm()
    {
      return View();
    }
    [HttpPost("/specialties")]
    public ActionResult Create(string specialtyName)
    {
      Specialty newSpecialty = new Specialty(specialtyName);
      newSpecialty.Save();
      List<Specialty> allSpecialties = Specialty.GetAll();
      return RedirectToAction("Index", allSpecialties);
    }


    // [HttpGet("/specialty/{id}/update")]
    // public ActionResult UpdateForm(int id)
    // {
    //   Specialty thisSpecialty = Specialty.Find(id);
    //   return View(thisSpecialty);
    // }

    // [HttpPost("/specialty/{id}/update")]
    // public ActionResult Update(int id, string newSpecialtyName)
    // {
    //   Specialty thisSpecialty = Specialty.Find(id);
    //   thisSpecialty.Edit(newSpecialtyName);
    //   return RedirectToAction("Index", new {id = newSpecialtyName.GetId()});
    // }

    [HttpPost("/specialties/{id}/delete")]
    public ActionResult DeleteSpecialty(int id)
    {
      Specialty newSpecialty = Specialty.Find(id);
      newSpecialty.Delete();
      return RedirectToAction("index");
    }
  
  }
}
