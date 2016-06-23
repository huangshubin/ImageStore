using System;

namespace ImageWebAPIs.Models
{
    public class Token : EntityBase
    {
        public int Id { get; protected set; }
        public string AuthToken { get; set; }
        public DateTime IssuedOn { get; set; }
        public DateTime ExpriesOn { get; set; }
        public virtual int UserId { get; set; }
        public virtual Client User { get; private set; }
    }
}