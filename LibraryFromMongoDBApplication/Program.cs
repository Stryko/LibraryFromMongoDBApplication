using MongoDB.Driver;
using LibraryFromMongoDBApplication;


string connectionString = "mongodb+srv://stryko:KysuckyDeveloper963@library.h3gozkg.mongodb.net/?retryWrites=true&w=majority";
string databaseName = "Library";

string bookCollectionName = "books";

var client = new MongoClient(connectionString);
var db = client.GetDatabase(databaseName);
var bookCollection = db.GetCollection<BookModel>(bookCollectionName);

var newBook = new BookModel
{
    Title = "Kniha rozumu",
    Author = "Neznamy",
    NumberOfCopies = 10,
    CoverImage = "",
    NumberOfPages = 50,
    YearOfPublication = 2000
};

await bookCollection.InsertOneAsync(newBook);