using Newtonsoft.Json.Linq;
using System.Text.Json;
using System.Text.Json.Nodes;


namespace Uppgift2.Classes
{
    public class Library
    {
        public const string ListOfBooks = "C:/Users/linda/source/repos/Uppgift2/Files/listofbooks.json";
        public const string ListOfBorrowers = "C:/Users/linda/source/repos/Uppgift2/Files/listofborrowers.json";

        public List<Book> Books { get; set; }
        public List<Borrower> Borrowers { get; set; }


        public Library()
        {
            Books = GetBooks();
            Borrowers = GetBorrowers();
        }


        public List<Borrower> GetBorrowers()
        {
            try
            {
                if (!File.Exists(ListOfBorrowers)) { return new List<Borrower>(); }
                string jsonData = File.ReadAllText(ListOfBorrowers);

                return JsonSerializer.Deserialize<List<Borrower>>(jsonData);
            }
            catch (Exception ex)
            {
                //WRITE ERROR MESSAGE TO LOGFILE

                return new List<Borrower>();
            }
        }
        public List<Book> GetBooks()
        {
            try
            {
                if (!File.Exists(ListOfBooks)) { return new List<Book>(); }
                string jsonData = File.ReadAllText(ListOfBooks);

                return JsonSerializer.Deserialize<List<Book>>(jsonData);
            }
            catch (Exception ex)
            {
                //WRITE ERROR MESSAGE TO LOGFILE

                return new List<Book>();
            }
        }

        public void AddBorrowerToJsonFile(List<Borrower> borrowers, double socialsecuritynumber)
        {
            try
            {
                while (true) 
                {
                    if (socialsecuritynumber < 9 || socialsecuritynumber > 12)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine();
                        Console.WriteLine("-----------------------------------------------");
                        Console.WriteLine("Du måste ange minst 10 siffror i personnummret.");
                        Console.WriteLine("-----------------------------------------------");
                        Thread.Sleep(3000);
                        Console.Clear();
                        return;
                    }
                    else 
                    {
                        JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };
                        string jsonString = JsonSerializer.Serialize(borrowers, options);
                        File.WriteAllText(ListOfBorrowers, jsonString);

                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine();
                        Console.WriteLine("Det finns en ny registrerad låntagare i biblioteket.");
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                //WRITE ERROR MESSAGE TO LOGFILE
            }
        }
        public void AddBookToJsonFile(List<Book> books, int categoryChoice)
        {
            try
            {
                while (true)
                {
                    if (categoryChoice < 0 || categoryChoice > 4)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("----------------------------------------------");
                        Console.WriteLine("Du har angett ej ett giltigt val. Försök igen.");
                        Console.WriteLine("----------------------------------------------");
                        Console.ForegroundColor = ConsoleColor.White;
                        return;
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("------------------------------------");
                        Console.WriteLine("Boken är registrerad i biblioteket.");
                        Console.WriteLine("------------------------------------");
                        Console.ForegroundColor = ConsoleColor.White;
                        Thread.Sleep(3000);
                        Console.Clear();
                        break;
                    }
                }
                JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };
                string jsonString = JsonSerializer.Serialize(books, options);
                File.WriteAllText(ListOfBooks, jsonString);

            }
            catch (Exception e)
            {
                //WRITE ERROR MESSAGE TO LOGFILE
            }
        }
        public void BorrowBook(int bookId, int borrowerId)
        {
            
            Book updatedBook = Books.Find(book => book.Id == bookId);
            if (updatedBook == null)
            {
                //TODO: TELL USER THERE IS NO BOOK WITH THAT TITLE!!
                return;
            }

            Borrower borrower = Borrowers.Find(borrower => borrower.Id == borrowerId);
            if (borrower == null)
            {
                //TODO: TELL USER THERE IS NO BORROWER WITH THAT ID!!
                return;
            }
           
            updatedBook.IsAvailable = false;
            borrower.LoanedBooks.Add(updatedBook);

            JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };

            string jsonBookString = JsonSerializer.Serialize(Books, options);
            File.WriteAllText(ListOfBooks, jsonBookString);

            string jsonBorrowerString = JsonSerializer.Serialize(Borrowers, options);
            File.WriteAllText(ListOfBorrowers, jsonBorrowerString);

            Console.WriteLine($"Boken {updatedBook.Title} är utlåndad till låntagare {borrower.FirstName} {borrower.LastName}");
        }
        public void ReturnBooks(int borrowerId, int bookId)
        {

            Book returnBook = Books.Find(book => book.Id == bookId);
            Borrower borrower = Borrowers.Find(borrower => borrower.Id == borrowerId);
            if (returnBook == null && borrower == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Det finns inga böcker i biblioteket.\nDet finns inga låntagare registrerade.");
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }

            
           

            returnBook.IsAvailable = true;
            borrower.LoanedBooks.Remove(returnBook);

            JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };

            string jsonReturnBookString = JsonSerializer.Serialize(Books, options);
            File.WriteAllText(ListOfBooks, jsonReturnBookString);

            Borrower loanedBooks = Borrowers.Find(LoanedBooks => LoanedBooks == LoanedBooks);

            string jsonBorrowerString = JsonSerializer.Serialize(Borrowers, options);
            
            Console.WriteLine($"Boken {returnBook.Title}, {returnBook.Id} är återlämnad.");

        }              
        public void ShowAvaibleBooks( )
        {
            if (Books.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("--------------------------------------------------");
                Console.WriteLine("Det finns inga böcker att visa.\nLägg till nya böcker i biblioteket för att se böcker i listan.");
                Console.WriteLine("--------------------------------------------------");
                Console.ForegroundColor = ConsoleColor.White;
                Thread.Sleep(3000);
                Console.Clear();
                return;
            }

            List<Book> availableBooks = Books.Where(book => book.IsAvailable).ToList();
            Console.WriteLine();
            Console.WriteLine("Dessa böcker finns tillgängliga: ");
            foreach (Book book in availableBooks)
            {
                Console.WriteLine("-----------------------------------------------");
                Console.WriteLine($"Titel: {book.Title}\n" +
                    $"Författare: {book.Author}\n" +
                    $"Språk: {book.Language}\n" +
                    $"Bokens id: {book.Id}\n" +
                    $"Kategori: {book.GetCategoryName()}");
            }
            Console.WriteLine();
            Console.WriteLine("---------------------------------");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Tryck Enter för att komma vidare.");
            Console.ForegroundColor = ConsoleColor.White;
            while (true)
            {
                ConsoleKey key = Console.ReadKey(true).Key;
                if (key.Equals(ConsoleKey.Enter))
                {
                    break;
                }
            }
        }
       

        public void ShowBorrowers()
        {
            if (Borrowers.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("--------------------------------------------------");
                Console.WriteLine("Det finns inga registrerade låntagare.\nVar vänlig och registrera nya låntagare.");
                Console.WriteLine("--------------------------------------------------");
                Console.ForegroundColor = ConsoleColor.White;
                Thread.Sleep(5000);
                return;
                //Console.Clear();
            }
            else
            {
                Console.WriteLine("------Låntagare------");
                foreach (Borrower borrower in Borrowers)
                {
                    Console.WriteLine($"Förnamn: {borrower.FirstName}\n" +
                        $"Efternamn: {borrower.LastName}\n" +
                        $"Personnumer: {borrower.SocialSecurityNumber}\n" +
                        $"Biblioteks id: {borrower.Id}");
    
                    if (borrower.LoanedBooks.Count > 0)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Lånade böcker:");
                        foreach (Book book in borrower.LoanedBooks) 
                        {
                            Console.WriteLine($"Titel: {book.Title}\n" +
                            $"Författare: {book.Author}\n" + 
                            $"Språk: {book.Language}\n" +
                            $"Bokens id: {book.Id}\n" +
                            $"Kategori: {book.GetCategoryName()}"
                            );
                            Console.WriteLine();
                        }
                    }
                    Console.WriteLine("------------------------------------------");
                    Console.WriteLine();
                }
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Tryck Enter för att komma vidare i programmet.");
                Console.ForegroundColor = ConsoleColor.White;
                

                //Gör så att man kan komma vidare i programmet vid Enter knapptryckning, fungerar inte med någon annan knapptryckning. 
                while (true)
                {
                    ConsoleKey key = Console.ReadKey(true).Key;
                    if (key.Equals(ConsoleKey.Enter))
                    {
                        break;
                    }
                }
                //Console.Clear();
            }
        }

       

    }
}
