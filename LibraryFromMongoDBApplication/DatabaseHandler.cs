using LibraryFromMongoDBApplication;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace LibraryFromMongoDBApplication
{
    public class DatabaseHandler
    {
        private const string CONNECTION_STRING = "mongodb+srv://stryko:KysuckyDeveloper963@library.h3gozkg.mongodb.net/?retryWrites=true&w=majority";
        private const string DATABASE = "Library";

        private const string booksCollectionName = "books";
        private const string usersCollectionName = "users";

        private IMongoCollection<T> ConnectToMongo<T>(in string collection)
        {
            var client = new MongoClient(CONNECTION_STRING);
            var db = client.GetDatabase(DATABASE);
            return db.GetCollection<T>(collection);
        }

        #region zakladne vratenie vsetkych obj

        /// <summary>
        /// Vrati vsetky knihy v kolekcii books
        /// </summary>
        /// <returns></returns>
        public async Task<List<BookModel>> GetBooks()
        {
            List<BookModel> result = new List<BookModel>();

            var booksCollection = ConnectToMongo<BookModel>(booksCollectionName);
            var bookResults = await booksCollection.FindAsync(_ => true);
            return bookResults.ToList();
        }

        /// <summary>
        /// Vrati vsetkych uzivatelov v kolekcii, aj zakaznikov aj knihovnikov
        /// </summary>
        /// <returns></returns>
        public async Task<List<UserModel>> GetUsers()
        {
            List<UserModel> result = new List<UserModel>();

            var usersCollection = ConnectToMongo<UserModel>(usersCollectionName);
            var userResults = await usersCollection.FindAsync(_ => true);
            return userResults.ToList();
        }

        #endregion

        #region login

        /// <summary>
        /// Prihlasenie uzivatela, vrati null ak neexistuje ucet
        /// Ak existuje vrati usera
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<UserModel> TryLoginUser(string username, string password)
        {
            var usersCollection = ConnectToMongo<UserModel>(usersCollectionName);
            var userResults = await usersCollection.FindAsync(_ => true);

            foreach (var user in userResults.ToList())
            {
                if (user.Username == username && user.Password == password)
                    return user;
            }

            return null;
        }

        #endregion

        #region operacie uzivatela

        /// <summary>
        /// Kontrola ci existuje uzivatel s danym uzivatelskym menom
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<bool> CheckUserExists(string username)
        {
            var usersCollection = ConnectToMongo<UserModel>(usersCollectionName);
            var userResults = await usersCollection.FindAsync(_ => true);

            foreach (var user in userResults.ToList())
            {
                if (user.Username == username)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Vytvorenie noveho uzivatela
        /// Ak existuje ucet vrati null
        /// Ak neexistuje ucet vrati toho uzivatela
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<UserModel> CreateNewUser(UserModel user)
        {
            if (await CheckUserExists(user.Username))
                return null;

            var usersCollection = ConnectToMongo<UserModel>(usersCollectionName);
            var userResults = await usersCollection.FindAsync(_ => true);
            await usersCollection.InsertOneAsync(user);

            return await GetUserByUsername(user.Username);
        }

        /// <summary>
        /// Vrati uzivatela podla uzivatelskeho mena
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<UserModel> GetUserByUsername(string username)
        {
            var usersCollection = ConnectToMongo<UserModel>(usersCollectionName);
            var userResults = await usersCollection.FindAsync(_ => true);

            foreach (var user in userResults.ToList())
            {
                if (user.Username == username)
                    return user;
            }

            return null;
        }

        /// <summary>
        /// Ak si uzivatel updatuje data alebo aj knihovnik
        /// Ak updatuje knihovnik tak sa posle ci je knihovnik...
        /// Ak nie je tak sa vykona odschvalenie uctu
        /// </summary>
        /// <returns></returns>
        public Task UpdateUserData(UserModel user, bool isLibrarian)
        {
            var usersCollection = ConnectToMongo<UserModel>(usersCollectionName);

            if (isLibrarian) user.IsApproved = false;

            var filter = Builders<UserModel>.Filter.Eq("Id", user.Id);
            return usersCollection.ReplaceOneAsync(filter, user);
        }

        /// <summary>
        /// Vymazanie uzivatela
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task DeleteUser(UserModel user)
        {
            var usersCollection = ConnectToMongo<UserModel>(usersCollectionName);
            return usersCollection.DeleteOneAsync(x => x.Id == user.Id);
        }

        #endregion

        #region operacie knihy

        /// <summary>
        /// Vytvorenie novej knihy
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        public async Task<BookModel> AddNewBook(BookModel book)
        {
            if (await CheckIfBookExists(book.Title))
                return null;

            var booksCollection = ConnectToMongo<BookModel>(booksCollectionName);
            var booksResults = await booksCollection.FindAsync(_ => true);
            await booksCollection.InsertOneAsync(book);

            return await GetBookByTitle(book.Title);
        }

        /// <summary>
        /// Kontrola ci existuje kniha
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<bool> CheckIfBookExists(string title)
        {
            var booksCollection = ConnectToMongo<BookModel>(booksCollectionName);
            var booksResults = await booksCollection.FindAsync(_ => true);

            foreach (var book in booksResults.ToList())
            {
                if (book.Title == title)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Vrati uzivatela podla uzivatelskeho mena
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<BookModel> GetBookByTitle(string title)
        {
            var booksCollection = ConnectToMongo<BookModel>(booksCollectionName);
            var booksResults = await booksCollection.FindAsync(_ => true);

            foreach (var book in booksResults.ToList())
            {
                if (book.Title == title)
                    return book;
            }

            return null;
        }

        /// <summary>
        /// Update knihy
        /// </summary>
        /// <returns></returns>
        public Task UserBook(BookModel book)
        {
            var booksCollection = ConnectToMongo<BookModel>(booksCollectionName);
            var filter = Builders<BookModel>.Filter.Eq("Id", book.Id);
            return booksCollection.ReplaceOneAsync(filter, book);
        }

        /// <summary>
        /// Vymazanie  knihy
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task DeleteBook(BookModel book)
        {
            //TODO:: databazovo zavolat funkciu, ktora skontroluje ci je nejaka ta kniha pozicana...

            var booksCollection = ConnectToMongo<BookModel>(booksCollectionName);
            return booksCollection.DeleteOneAsync(x => x.Id == book.Id);
        }

        #endregion

        #region pozicanie knihy

        /// <summary>
        /// Pozicanie knihy uzivatelom
        /// </summary>
        /// <returns></returns>
        private Task BorrowBook(UserModel user, string bookId)
        {
            //TODO::kontrola je dost kopii?
            //TODO::kontrola ci neprekroci maximum pozicanych knih

            BorrowedBookModel newBorrowedBook = new BorrowedBookModel {
                DTFrom = DateTime.Now,
                IsReturned = false,
                IdBook = bookId
            };

            if (user.BorrowedBooks == null) 
                user.BorrowedBooks = new List<BorrowedBookModel>();

            user.BorrowedBooks.Add(newBorrowedBook);

            return UpdateUserData(user, true);
        }

        /// <summary>
        /// Vratenie knihy
        /// </summary>
        /// <returns></returns>
        private Task ReturnBook(UserModel user, string bookId)
        {
            user.BorrowedBooks.Find(x => x.IdBook == bookId).IsReturned = true;

            return UpdateUserData(user, true);
        }

        #endregion

        #region operacie knihovnika

        /// <summary>
        /// Schvalenie uzivatela
        /// </summary>
        /// <returns></returns>
        private Task ApproveUser(UserModel user)
        {
            user.IsApproved = true;
            return UpdateUserData(user, true);
        }

        /// <summary>
        /// Odschvalenie uzivatela
        /// </summary>
        /// <returns></returns>
        private Task UnapproveUser(UserModel user)
        {
            user.IsApproved = false;
            return UpdateUserData(user, true);
        }

        /// <summary>
        /// Zabanovanie uzivatela
        /// </summary>
        /// <returns></returns>
        private Task BanUser(UserModel user)
        {
            user.IsBanned = true;
            return UpdateUserData(user, true);
        }

        /// <summary>
        /// Odbanovanie uzivatela
        /// </summary>
        /// <returns></returns>
        private Task UnbanUser(UserModel user)
        {
            user.IsBanned = false;
            return UpdateUserData(user, true);
        }

        #endregion
    }
}
