using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryFromMongoDBApplication
{
    public class AddressModel
    {
        public string Address { get; set; }
        public string City { get; set; }
        public string PostalNumber { get; set; }
        public string State { get; set; }

        public override string ToString()
        {
            return String.Format("[Address: {0}, {1}, {2}, {3}]", Address, City, PostalNumber, State);
        }
    }
}
