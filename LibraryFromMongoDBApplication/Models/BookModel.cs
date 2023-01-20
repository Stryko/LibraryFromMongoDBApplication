using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;


namespace LibraryFromMongoDBApplication
{
    public class BookModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int YearOfPublication { get; set; }
        public int NumberOfPages { get; set; }
        public string CoverImage { get; set; }
        public int NumberOfCopies { get; set; }
        public int NumberOfAvailableCopies { get; set; }

        public override string ToString()
        {
            return String.Format("Id: {0}, Title: {1}, Author: {2}, YearOfPublication: {3}, NumberOfPages: {4}, CoverImage: {5}, NumberOfCopies: {6}",
                Id, Title, Author, YearOfPublication, NumberOfCopies, CoverImage, NumberOfCopies);
        }
    }
}
