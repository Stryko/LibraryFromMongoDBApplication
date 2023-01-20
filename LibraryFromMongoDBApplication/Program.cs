using MongoDB.Driver;
using LibraryFromMongoDBApplication;

DatabaseHandler dbHandler = new DatabaseHandler();
List<BookModel> booksList = await dbHandler.GetBooks();
List<UserModel> usersList = await dbHandler.GetUsers();

UserModel loggedUser = await dbHandler.TryLoginUser("admin","admin");


string connectionString = "mongodb+srv://stryko:KysuckyDeveloper963@library.h3gozkg.mongodb.net/?retryWrites=true&w=majority";
string databaseName = "Library";

string booksCollectionName = "books";
string customersCollectionName = "users";

var client = new MongoClient(connectionString);
var db = client.GetDatabase(databaseName);
var booksCollection = db.GetCollection<BookModel>(booksCollectionName);
var usersCollection = db.GetCollection<UserModel>(customersCollectionName);

var bookResults = await booksCollection.FindAsync(_ => true);
var userResults = await usersCollection.FindAsync(_ => true);

foreach (var book in bookResults.ToList())
{
    Console.WriteLine(book.ToString());
}

foreach (var user in userResults.ToList())
{
    Console.WriteLine(user.ToString());
}

var newBook0 = new BookModel { Title = "Kniha 1", Author = "David Junak", NumberOfCopies = 5, CoverImage = "", NumberOfPages = 50, YearOfPublication = 2010, NumberOfAvailableCopies = 5 };
var newBook1 = new BookModel { Title = "Kniha nieco", Author = "Adam Master", NumberOfCopies = 1, CoverImage = "", NumberOfPages = 500, YearOfPublication = 2020, NumberOfAvailableCopies = 1 };
var newBook2 = new BookModel { Title = "Nova kniha", Author = "Rantok Bistric", NumberOfCopies = 5, CoverImage = "", NumberOfPages = 20, YearOfPublication = 2022, NumberOfAvailableCopies = 5 };
var newBook3 = new BookModel { Title = "Testovanie kniha", Author = "Natural Siroky", NumberOfCopies = 4, CoverImage = "", NumberOfPages = 197, YearOfPublication = 1599, NumberOfAvailableCopies = 4 };
var newBook4 = new BookModel { Title = "Blabla", Author = "Niekto hlucny", NumberOfCopies = 23, CoverImage = "", NumberOfPages = 200, YearOfPublication = 1911, NumberOfAvailableCopies = 23 };
var newBook5 = new BookModel { Title = "Nieco nove", Author = "Neznamy", NumberOfCopies = 6, CoverImage = "", NumberOfPages = 355, YearOfPublication = 1999, NumberOfAvailableCopies = 6 };

/*
await booksCollection.InsertOneAsync(newBook0);
await booksCollection.InsertOneAsync(newBook1);
await booksCollection.InsertOneAsync(newBook2);
await booksCollection.InsertOneAsync(newBook3);
await booksCollection.InsertOneAsync(newBook4);
await booksCollection.InsertOneAsync(newBook5);
*/

var borroved = new List<BorrowedBookModel> { new BorrowedBookModel { IdBook = "", DTFrom = DateTime.Now, IsReturned = false } };

var address1 = new AddressModel { Address = "Aloise Rasina 565", City = "Olomouc", PostalNumber = "77900", State = "Czech republic" };
var address2 = new AddressModel { Address = "Zilinska 25", City = "Zilina", PostalNumber = "02440", State = "Slovak republic" };
var address3 = new AddressModel { Address = "Zlinska 111", City = "Zlin", PostalNumber = "1566", State = "Czech republic" };

var customer = new UserModel { Name = "David", Surname = "Paradny", Address = address1, IsBanned = false, IsApproved = true, Username = "stryko", Password = "stryko", NationalIdentificationNumber = "161dsad", IsLibrarian = false, BorrowedBooks = borroved };
var customer1 = new UserModel { Name = "Lukas", Surname = "Master", Address = address2, IsBanned = false, IsApproved = true, Username = "rantok", Password = "rantok", NationalIdentificationNumber = "1688dsad", IsLibrarian = false };
var customer2 = new UserModel { Name = "Adam", Surname = "Natural", Address = address2, IsBanned = false, IsApproved = true, Username = "natural", Password = "natural", NationalIdentificationNumber = "dsa145", IsLibrarian = false };
var librarian = new UserModel { Name = "Peter", Surname = "Zamestnany", Address = address3, IsBanned = false, IsApproved = true, Username = "admin", Password = "admin", NationalIdentificationNumber = "d16sa1d", IsLibrarian = true };

/*
await usersCollection.InsertOneAsync(customer);
await usersCollection.InsertOneAsync(customer1);
await usersCollection.InsertOneAsync(customer2);
await usersCollection.InsertOneAsync(librarian);
*/

Console.WriteLine("----------------------------------------------");
Console.ReadLine();


