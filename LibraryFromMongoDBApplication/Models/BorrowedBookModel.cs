using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryFromMongoDBApplication
{
    public class BorrowedBookModel
    {
        public string IdBook { get; set; }
        public DateTime DTFrom { get; set; }
        public bool IsReturned { get; set; }
    }
}
