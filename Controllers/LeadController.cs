namespace DynamicsPortal.Controllers
{
    using DynamicsPortal.Business;
    using DynamicsPortal.Common;
    using DynamicsPortal.Models;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    [ApiController, Route("api/[controller]")]
    public class LeadController : ControllerBase
    {
        readonly ILeadManager leadManager;
        public LeadController(ILeadManager leadManager) => this.leadManager = leadManager;

        [HttpGet]
        public async Task<List<Lead>> GetListAsync() => await this.leadManager.GetListAsync(User.GetContactId());

        [HttpGet("{id}")]
        public async Task<Lead> GetByIdAsync([FromRoute] Guid id) => await this.leadManager.GetByIdAsync(id);

        [HttpPost]
        public async Task<Lead> CreateAsync([FromBody] Lead record)
        {
            record.ParentContactId = User.GetContactId();
            return await this.leadManager.CreateAsync(record);
        }

        [HttpPut]
        public async Task UpdateAsync([FromBody] Lead record) => await this.leadManager.UpdateAsync(record);

        [HttpDelete("{id}")]
        public async Task DeleteAsync([FromRoute] Guid id) => await this.leadManager.DeleteAsync(id);
    }
}
