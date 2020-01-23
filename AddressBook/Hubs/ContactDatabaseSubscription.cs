using AddressBook.Model;
using AddressBook.Shared.Contracts.Business;
using System;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base.Enums;
using TableDependency.SqlClient.Base.EventArgs;

namespace AddressBook.Api.Hubs
{
    public class ContactDatabaseSubscription
    {
        private bool disposedValue = false;
        private readonly IContactService _contactService;
        private readonly ContactHub _contactHub;
        private SqlTableDependency<Contact> _tableDependency;

        public ContactDatabaseSubscription(IContactService contactService, ContactHub contactHub)
        {
            _contactService = contactService;
            _contactHub = contactHub;
        }

        public void Configure(string connectionString)
        {
            _tableDependency = new SqlTableDependency<Contact>(connectionString, null, null, null, null);
            _tableDependency.OnChanged += Changed;
            _tableDependency.Start();

            Console.WriteLine("Waiting for receiving notifications...");
        }

        private async void Changed(object sender, RecordChangedEventArgs<Contact> e)
        {
            if (e.ChangeType != ChangeType.None)
            {
                await _contactHub.SendMessage("Contacts update", await _contactService.FindAllAsync());
            }
        }

        #region IDisposable

        ~ContactDatabaseSubscription()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _tableDependency.Stop();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
