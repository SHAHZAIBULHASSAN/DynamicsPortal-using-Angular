using DynamicsPortal.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DynamicsPortal.Business
{
    public interface ILeadManager
    {
        Task<Lead> GetByIdAsync(Guid id);
        Task<Lead> CreateAsync(Lead record);
        Task DeleteAsync(Guid id);
        Task<List<Lead>> GetListAsync(Guid contactId);
        Task UpdateAsync(Lead record);
    }
}