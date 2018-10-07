using Common.Application;

namespace EnterprisePatterns.Api.Customers
{
    public class Customer
    {
        public virtual long Id { get; set; }
        public virtual string OrganizationName { get; set; }

        public Customer()
        {
        }

        public virtual bool hasFullName()
        {
            return !string.IsNullOrWhiteSpace(this.OrganizationName);
        }

        public virtual Notification validateForSave()
        {
            Notification notification = new Notification();

            if (this == null)
            {
                notification.addError("The student is null");
            }

            if (!this.hasFullName())
            {
                notification.addError("The customer doesn´t have a valid organization name");
            }

            return notification;
        }
    }
}
