namespace Uppgift2.Classes
{
    public class Menu
    {
        public void RunMenu() 
        {
            Library library = new Library();
                   
            bool endProgram = false;
            
            Console.WriteLine("Välkommen till Åshammars bibliotek!");
            Console.WriteLine("--------------------------------------------------");

            while (!endProgram) 
            {
                Console.WriteLine("Vad vill du göra? ");
                Console.WriteLine();
                Console.WriteLine("1. Lägg till nya böcker i biblioteket.");
                Console.WriteLine("2. Låna ut böcker till låntagare.");
                Console.WriteLine("3. Återlämna böcker.");
                Console.WriteLine("4. Visa tillgängliga böcker.");
                Console.WriteLine("5. Visa låntagare och deras lånade böcker.");
                Console.WriteLine("6. Lägg till nya låntagare.");
                Console.WriteLine("7. Avsluta programmet.");
                Console.Write("Menyval: ");
                int menuChoice = int.Parse(Console.ReadLine());

                switch (menuChoice)
                {
                    case 1:
                        //Lägg till nya böcker.
                        Console.WriteLine();
                        Console.Write("Titel på boken: ");
                        string title = Console.ReadLine();

                        Console.Write("Bokens författare: ");
                        string author = Console.ReadLine();

                        Console.Write("Bokens språk: ");
                        string language = Console.ReadLine();

                        Console.WriteLine("Välj en kategori");
                        Console.WriteLine("1. Djur");
                        Console.WriteLine("2. Historia");
                        Console.WriteLine("3. Thriller");
                        Console.Write("Kategori: ");
                        int categoryChoice = int.Parse(Console.ReadLine());

                        //TODO: CONTROL CORRECT NUMBER
                        
                        if (title == null) title = "";
                        if (author == null) author = "";
                        if (language == null) language = "";

                        Book newBook = new Book(title, author, language, true, categoryChoice);
                        newBook.Id = library.Books.Count == 0 ? 1 : library.Books.Count + 1;

                        library.Books.Add(newBook);
                        library.AddBookToJsonFile(library.Books);
                        break;
                    case 2:
                        //Låna ut böcker.
                        library.ShowAvaibleBooks();

                        Console.WriteLine();
                        Console.Write("Ange ID på boken som ska lånas ut: ");
                        int bookId = int.Parse(Console.ReadLine());

                        Console.WriteLine("Dessa låntagare finns: ");
                        foreach (Borrower borrower in library.Borrowers)
                        {
                            Console.WriteLine($"{borrower.FirstName} {borrower.LastName} med ID: {borrower.Id}");
                        }

                        Console.Write("Låntagarens id: ");

                        int borrowerId = int.Parse(Console.ReadLine());

                        library.BorrowBook(bookId, borrowerId);
                        break;
                    case 3:
                        //Återlämna böcker.

                        library.ShowBorrowers();

                        Console.Write("Ange låntagarens ID som ska återlämna bok/böcker: ");
                        borrowerId = int.Parse(Console.ReadLine());

                        Console.Write("Ange bokens ID: ");
                        bookId = int.Parse(Console.ReadLine());

                        library.ReturnBooks(borrowerId, bookId);
                        break;
                    case 4:
                        //Visa tillgängliga böcker.
                        Console.WriteLine();
                        library.ShowAvaibleBooks();
                        break;
                    case 5:
                        //Visa låntagare och deras böcker.
                        Console.WriteLine();
                        library.ShowBorrowers();
                        break;
                    case 6:
                        //Lägg till nya låntagare
                        Console.WriteLine();
                        Console.Write("Förnamn: ");
                        string newBorrowerName = Console.ReadLine();

                        Console.Write("Efternamn: ");
                        string? lastname = Console.ReadLine();

                        Console.Write("Personnummer: ");
                        double socialsecuritynumber = double.Parse(Console.ReadLine());

                        if (newBorrowerName == null) newBorrowerName = "";
                        if (lastname == null) lastname = "";
                        if (socialsecuritynumber == null) socialsecuritynumber = 0;

                        Borrower newBorrower = new Borrower(newBorrowerName, lastname, socialsecuritynumber);
                        newBorrower.Id = library.Borrowers.Count == 0 ? 1 : library.Borrowers.Count + 1;

                        library.Borrowers.Add(newBorrower);
                        library.AddBorrowerToJsonFile(library.Borrowers);
                        break;
                    case 7:
                        //Avslutar programmet.
                        endProgram = true;
                        Console.WriteLine("-----------------------------------------------");
                        Console.WriteLine("Progammet kommer avslutas, tack för denna gång! ");
                        Thread.Sleep(1000);
                        break;
                    default:
                        //Om man trycker på fel knappval i menyn.
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("---------------------------------------");
                        Console.WriteLine("Du valde ett ogiltigt val, försök igen.");
                        Console.WriteLine("---------------------------------------");
                        Console.WriteLine("Tryck enter för att komma vidare.");
                        Console.ReadKey();
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                }
            }

            

        }
    }
}
