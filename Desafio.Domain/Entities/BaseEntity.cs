using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Domain.Entities
{
    public abstract class BaseEntity
    {
        public virtual int Codigo { get; set; }
    }
}
