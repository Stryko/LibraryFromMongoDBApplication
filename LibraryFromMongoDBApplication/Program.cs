using MongoDB.Driver;
using LibraryFromMongoDBApplication;


string connectionString = "mongodb+srv://stryko:KysuckyDeveloper963@library.h3gozkg.mongodb.net/?retryWrites=true&w=majority";
string databaseName = "Library";

string booksCollectionName = "books";
string customersCollectionName = "customers";

var client = new MongoClient(connectionString);
var db = client.GetDatabase(databaseName);
var booksCollection = db.GetCollection<BookModel>(booksCollectionName);
var customersCollection = db.GetCollection<CustomerModel>(customersCollectionName);

/*
await bookCollection.InsertOneAsync(newBook);
*/

var bookResults = await booksCollection.FindAsync(_ => true);
var customerResults = await customersCollection.FindAsync(_ => true);

var newBook = new BookModel
{
    Title = "Kniha rozumu",
    Author = "Neznamy",
    NumberOfCopies = 10,
    CoverImage = "",
    NumberOfPages = 50,
    YearOfPublication = 2000
};
