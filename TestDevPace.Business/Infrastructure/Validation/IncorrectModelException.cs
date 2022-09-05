using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDevPace.Business.Infrastructure.Validation
{
    public class IncorrectModelException : Exception
    {
        public string? Property { get; protected set; }
        public IncorrectModelException(string message) : base(message) { }
        public IncorrectModelException(string message, string prop) : base(message)
        {
            Property = prop;
        }
    }
}
