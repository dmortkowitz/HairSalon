using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;

namespace HairSalon.Controllers
{
  public class CustomersController : Controller
  {

    [HttpGet("/customers")]
    public ActionResult Index()
    {
      List<Customer> allCustomers = Customer.GetAll();
      return View(allCustomers);
    }


    [HttpGet("/stylists/{styleid}/customers/new")]
    public ActionResult CreateForm(int styleId)
    {
      Dictionary <string,object> model = new Dictionary<string, object>{};
      Stylist stylist = Stylist.Find(styleId);
      return View(stylist);
    }

      
    [HttpPost ("/customers")]
    public ActionResult CreateCustomer (int styleId, string customerName) 
    {
      Dictionary<string, object> model = new Dictionary<string, object> ();
      Stylist foundStylist = Stylist.Find(styleId);
      Customer newCustomer = new Customer (customerName, styleId);
      newCustomer.Save();
      List<Customer> stylistCustomers = foundStylist.GetCustomers();
      model.Add ("customers", stylistCustomers);
      model.Add ("stylist", foundStylist);
      return View ("Details", model);
    }

    [HttpGet("/stylists/{styleId}/customers/{id}")]
    public ActionResult Details(int custStyleId, int customerId)
    {
      Customer customer = Customer.Find(customerId);
      Dictionary<string, object> model = new Dictionary<string, object>();
      Stylist stylist = Stylist.Find(custStyleId);
      model.Add("customer", customer);
      model.Add("stylist", stylist);
      return View("Details", model);
    }

    [HttpGet("/customers/{id}/update")]
    public ActionResult UpdateForm(int customerId)
    {
      Customer thisCustomer = Customer.Find(customerId);
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
