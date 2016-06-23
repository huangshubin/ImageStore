using ImageWebAPIs.AuthEntityFramework;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ImageWebAPIs.Models
{
    public class Client : EntityBase
    {
        public int Id { get; protected set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public DateTime DateRegistered { get; set; }

        public bool Active { get; set; }

        public virtual ICollection<Image> Images { get; private set; }

      
       
    }
}