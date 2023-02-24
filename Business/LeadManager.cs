namespace DynamicsPortal.Business
{
    using DynamicsPortal.Models;
    using Microsoft.PowerPlatform.Dataverse.Client;
    using Microsoft.Xrm.Sdk;
    using Microsoft.Xrm.Sdk.Query;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class LeadManager : ILeadManager
    {
        readonly ServiceClient client;
        public LeadManager(ServiceClient client) => this.client = client;
        private static Lead MapToLead(Entity entity)
        {
            var result = new Lead
            {
                Id = entity.GetAttributeValue<Guid>("leadid"),
                Subject = entity.GetAttributeValue<string>("subject"),
                FirstName = entity.GetAttributeValue<string>("firstname"),
                LastName = entity.GetAttributeValue<string>("lastname"),
                JobTitle = entity.GetAttributeValue<string>("jobtitle"),
               // BusinessPhone= entity.GetAttributeValue<int>("telephone1"),
                EmailAddress = entity.GetAttributeValue<string>("emailaddress1"),
            };

            return result;
        }
        public async Task<List<Lead>> GetListAsync(Guid contactId)
        {
            var query = new QueryExpression 
            {
                EntityName = "lead",
                ColumnSet = new ColumnSet("leadid", "subject", "firstname", "lastname", "jobtitle", "emailaddress1"),
                Criteria = new FilterExpression()
            };

            query.Criteria.AddCondition("parentcontactid", ConditionOperator.Equal, contactId);
            var entityCollection = await client.RetrieveMultipleAsync(query);
            var list = entityCollection.Entities.Select(entity => MapToLead(entity)).ToList();
            return list;
        }

        public async Task<Lead> GetByIdAsync(Guid id)
        {
            var entity = await client.RetrieveAsync("lead", id, new ColumnSet("leadid", "subject", "firstname", "lastname", "jobtitle", "emailaddress1"));
            var record = MapToLead(entity);
            return record;
        }

        public async Task<Lead> CreateAsync(Lead record)
        {
            var entity = new Entity("lead");
            entity["parentcontactid"] = new EntityReference("contact", record.ParentContactId);
            entity["subject"] = record.Subject;
            entity["firstname"] = record.FirstName;
            entity["lastname"] = record.LastName;
           // entity["telephone1"] = record.BusinessPhone;
           entity["jobtitle"] = record.JobTitle;
            entity["emailaddress1"] = record.EmailAddress;
            record.Id = await client.CreateAsync(entity);
            return record;
        }

        public async Task UpdateAsync(Lead record)
        {
            var entity = await client.RetrieveAsync("lead", record.Id.Value, new ColumnSet(true));
            entity["subject"] = record.Subject;
            entity["firstname"] = record.FirstName;
            entity["lastname"] = record.LastName;
             entity["jobtitle"] = record.JobTitle;
            entity["emailaddress1"] = record.EmailAddress;
            entity["emailaddress1"] = record.EmailAddress;
            await client.UpdateAsync(entity);
        }
        public async Task DeleteAsync(Guid id)
        {
            await client.DeleteAsync("lead", id);
        }
    }
}
