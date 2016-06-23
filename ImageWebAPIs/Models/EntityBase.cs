using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageWebAPIs.Models
{
    public abstract class EntityBase
    {

        public DateTime CreatedOn { get; protected set; }
        public DateTime UpdatedOn { get; set; }
    }
}
