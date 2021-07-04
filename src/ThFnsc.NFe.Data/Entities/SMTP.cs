using System;
using ThFnsc.NFe.Data.Entities.Shared;

namespace ThFnsc.NFe.Data.Entities
{
    public class SMTP : BaseSoftDeleteEntity
    {
        public string Host { get; private set; }

        public int Port { get; private set; }

        public bool UseEncryption { get; private set; }

        public string Account { get; private set; }

        public string Username { get; private set; }

        public string Password { get; private set; }

        public string AccountName { get; private set; }

        public SMTP(
            string host,
            int port,
            bool useEncryption,
            string account,
            string username,
            string password,
            string accountName) =>
            Update(host, port, useEncryption, account, username, password, accountName);

        public void Update(
            string host,
            int port,
            bool useEncryption,
            string account,
            string username,
            string password,
            string accountName)
        {
            Host = host ?? throw new ArgumentNullException(nameof(host));
            Port = port;
            UseEncryption = useEncryption;
            Account = account ?? throw new ArgumentNullException(nameof(account));
            Username = username ?? throw new ArgumentNullException(nameof(username));
            Password = password ?? throw new ArgumentNullException(nameof(password));
            AccountName = accountName ?? throw new ArgumentNullException(nameof(accountName));
        }

        protected SMTP() { }
    }
}
