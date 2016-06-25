using System;

namespace ImageWebAPIs.Models
{
    public class Token : EntityBase
    {
        public int Id { get; protected set; }
        public string AuthToken { get; set; }
        public  int UserId { get; set; }

        public DateTime? IssuedOn { get; set; }
        public DateTime? ExpiresOn { get; set; }

        public virtual Client User { get; private set; }
    }
}