using Microsoft.AspNetCore.Mvc;
using webEcontact.Models;
using webEcontact.Servicio.Interface;
using webEcontact.Servicio.Repositorio;

namespace webEcontact.Controllers
{
    public class ContactListController : Controller
    {
        private readonly IContactList _contactList;
        private readonly IConfiguration _config;

        public ContactListController(IContactList contactList,
            IConfiguration configuration)
        {
            _contactList = contactList;
            _config = configuration;
        }

        public async Task<IActionResult> Index()
        {
            List<EC_Contactlist> lContactlist = new List<EC_Contactlist>();
            lContactlist = await _contactList.obtenerContactList();
            // Aquí puedes llamar al servicio que obtiene las listas de contactos
            

            return View(lContactlist);
        }
    }
}
