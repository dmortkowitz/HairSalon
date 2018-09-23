using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;

namespace HairSalon.Controllers
{
  public class CustomersController : Controller
  {

    [HttpGet("/stylists/{stylistId}/")]
    public ActionResult Index(int stylistId)
    {
      Stylist stylist = Stylist.Find(stylistId);
      List<Customer> allCustomers = Customer.GetAll(stylistId);
      return View(allCustomers);
    }


    [HttpGet("/stylists/{stylistId}/customers/new")]
    public ActionResult CreateForm(int stylistId)
    {
      //int testStylistId = int.Parse("stylistId");
      // Dictionary <string,object> model = new Dictionary<string, object>{};
      Stylist stylist = Stylist.Find(stylistId);
      return View(stylist);
    }
    
    [HttpGet("/stylists/{stylistId}/customers/{id}")]
    public ActionResult Details(int stylistId, int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Stylist stylist = Stylist.Find(stylistId);
      Customer customer = Customer.Find(id);
      model.Add("customer", customer);
      model.Add("stylist", stylist);
      return View(model);
    }
      
    // [HttpPost ("/customers")]
    // public ActionResult CreateCustomer (int stylistId, string customerName) 
    // {
    //   // int testStylistId = int.Parse("stylistId");
    //   // string testCustomerName = "customerName";
    //   Dictionary<string, object> model = new Dictionary<string, object> ();
    //   Stylist foundStylist = Stylist.Find(stylistId);
    //   Customer newCustomer = new Customer (customerName, stylistId);
    //   newCustomer.Save();
    //   List<Customer> stylistCustomers = foundStylist.GetCustomers();
    //   model.Add ("customers", stylistCustomers);
    //   model.Add ("stylist", foundStylist);
    //   return RedirectToAction("index", model);
    // }

    [HttpPost ("/customers")]
    public ActionResult CreateCustomer () 
    {
      int testStylistId = int.Parse(Request.Form["stylistId"]);
      string testCustomerName = Request.Form["customerName"];
      Dictionary<string, object> model = new Dictionary<string, object> ();
      Stylist foundStylist = Stylist.Find(testStylistId);
      Customer newCustomer = new Customer (testCustomerName, testStylistId);
      newCustomer.Save();
      List<Customer> stylistCustomers = foundStylist.GetCustomers();
      // model.Add ("customers", stylistCustomers);
      // model.Add ("stylist", foundStylist);
      return RedirectToAction("index", new {stylistId = testStylistId});
    }
    // [HttpGet("/stylists/{stylistId}/customers/{id}")]
    // public ActionResult Details(int stylistId, int id)
    // {
    //   Customer customer = Customer.Find(id);
    //   Dictionary<string, object> model = new Dictionary<string, object>();
    //   Stylist stylist = Stylist.Find(stylistId);
    //   model.Add("customer", customer);
    //   model.Add("stylist", stylist);
    //   return View("Details", model);
    // }

    [HttpGet("/customers/{id}/update")]
    public ActionResult UpdateForm(int id)
    {
      Customer thisCustomer = Customer.Find(id);
      return View(thisCustomer);
    }

    [HttpPost("/customers/{id}/update")]
    public ActionResult Update(int id, string customerName)
    {
      Customer thisCustomer = Customer.Find(id);
      thisCustomer.Edit(customerName);
      return RedirectToAction("Index");
    }

    [HttpGet("/customers/{id}/delete")]
    public ActionResult DeleteCustomer(int id)
    {
      Customer newCustomer = Customer.Find(id);
      newCustomer.Delete();
      return RedirectToAction("index");
    }
  }
}
