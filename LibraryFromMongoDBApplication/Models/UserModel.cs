using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace LibraryFromMongoDBApplication
{
    public class UserModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string NationalIdentificationNumber { get; set; }
        public AddressModel Address { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsApproved { get; set; }
        public bool IsBanned { get; set; }
        public bool IsLibrarian { get; set; }
        public List<BorrowedBookModel>? BorrowedBooks { get; set; }

        public override string ToString()
        {
            return String.Format("Id: {0}, Name: {1}, Surname: {2}, NationalIdentificationNumber: {3}, Address: {4}, Username: {5}, IsApproved: {6}, IsBanned: {7}, IsLibrarian: {8}, BorrowedBooks: {10}",
                Id, Name, Surname, NationalIdentificationNumber, Address.ToString(), Username, Password, IsApproved, IsBanned, IsLibrarian);
        }
    }

    
}
