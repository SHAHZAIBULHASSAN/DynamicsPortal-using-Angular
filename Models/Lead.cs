namespace DynamicsPortal.Models
{
    using System;

    public class Lead
    {
        public Guid? Id { get; set; }
        public Guid ParentContactId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string JobTitle { get; set; }
        public string Subject { get; set; }
       // public int BusinessPhone { get; set; }
    }
}
