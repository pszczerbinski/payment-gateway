namespace PaymentGateway
{
    using System;

    /// <summary>
    /// Defines the database settings.
    /// </summary>
    public interface IDatabaseSettings
    {
        /// <summary>
        /// Gets or sets the database name on the host.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the user for the read-write user to the database.
        /// </summary>
        string Username { get; set; }

        /// <summary>
        /// Gets or sets the password for the read-write user to the database.
        /// </summary>
        string Password { get; set; }

        /// <summary>
        /// Gets or sets the comma separated list of hosts in the format {server}:{port}.
        /// </summary>
        string Hosts { get; set; }

        /// <summary>
        /// Validates the database settings.
        /// </summary>
        void Validate();
    }

    public class DatabaseSettings : IDatabaseSettings
    {
        public string Name { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Hosts { get; set; }

        public void Validate()
        {
            var msg = "The following parameters need to be provided. They can be set from app secrets or as environment vars.\r\n.";
            var err = false;
            var envPrefix = "DatabaseSettings:";

            if (string.IsNullOrWhiteSpace(this.Name))
            {
                err = true;
                msg += envPrefix + nameof(this.Name) + ", ";
            }

            if (string.IsNullOrWhiteSpace(this.Username))
            {
                err = true;
                msg += envPrefix + nameof(this.Username) + ", ";
            }

            if (string.IsNullOrWhiteSpace(this.Password))
            {
                err = true;
                msg += envPrefix + nameof(this.Password) + ", ";
            }

            if (string.IsNullOrWhiteSpace(this.Hosts))
            {
                err = true;
                msg += envPrefix + nameof(this.Hosts) + ".";
            }

            if (err)
            {
                throw new ArgumentException(msg);
            }
        }
    }
}
