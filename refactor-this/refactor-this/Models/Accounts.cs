using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace refactor_this.Models
{
    public class Account
    {
        // TODO: attempt to make this "isNew" field redundant, by making calling code more sensible
        private bool isNew;

        // TODO: confirm the best way to make sure that Id does get set with new value for a new account
        // Mayve it does already? Or there is an attribute to help with this? Or I should seperate out a 
        // AccountOnCreationDto and AccountOnUpdateDto to make things really clear??
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Number { get; set; }

        public float Amount { get; set; }

        // TODO: read above comments on what to do about this isNew and Id generation..
        public Account()
        {

            isNew = true;
        }

        public Account(Guid id)
        {
            isNew = false;
            Id = id;
            // TODO: would be better off letting a database model handle stuff like creating new ids..
        }
    }
}