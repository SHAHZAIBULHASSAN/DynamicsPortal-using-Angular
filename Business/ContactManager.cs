namespace DynamicsPortal.Business
{
    using DynamicsPortal.Models;
    using Microsoft.PowerPlatform.Dataverse.Client;
    using Microsoft.Xrm.Sdk.Query;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class ContactManager : IContactManager
    {
        readonly ServiceClient client;
        public ContactManager(ServiceClient client) => this.client = client;

        public async Task<Contact> GetContactAsync(string emailAddress, string password)
        {
            var query = new QueryExpression("contact")
            {
                ColumnSet = new ColumnSet("contactid", "fullname"),
                Criteria = new FilterExpression()
            };

            query.Criteria.AddCondition("emailaddress1", ConditionOperator.Equal, emailAddress);
            query.Criteria.AddCondition("ss_password", ConditionOperator.Equal, password);

            var entityCollection = await client.RetrieveMultipleAsync(query);

            if (entityCollection == null || entityCollection.Entities?.Count <= 0)
            {
                return null;
            }

            var result = entityCollection.Entities.Select(entity => new Contact
            {
                Id = entity.GetAttributeValue<Guid>("contactid"),
                Name = entity.GetAttributeValue<string>("fullname"),
                EmailAddress = emailAddress
            })?.First();

            return result;
        }
    }
}
