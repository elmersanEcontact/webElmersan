using webEcontact.Models;

namespace webEcontact.Servicio.Interface
{
    public interface IContactList
    {
        Task<List<EC_Contactlist>> obtenerContactList();
    }
}
