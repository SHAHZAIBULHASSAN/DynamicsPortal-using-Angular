namespace DynamicsPortal.Business
{
    using DynamicsPortal.Models;
    using System.Threading.Tasks;

    public interface IContactManager
    {
        Task<Contact> GetContactAsync(string emailAddress, string password);
    }
}