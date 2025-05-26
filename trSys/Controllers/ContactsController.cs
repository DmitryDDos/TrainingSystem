using Microsoft.AspNetCore.Mvc;

public class ContactsController : Controller
{
    public IActionResult Index()
    {
        return View("~/Views/Contact/Contacts.cshtml");
    }
}