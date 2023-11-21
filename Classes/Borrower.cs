
namespace Uppgift2.Classes
{
    public class Borrower
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Book> LoanedBooks { get; set; }
        public double SocialSecurityNumber { get; set; }
        
        public Borrower(string firstName, string lastName, double socialSecuritynumber)
        {
            FirstName = firstName;
            LastName = lastName;
            SocialSecurityNumber = socialSecuritynumber;         
            

            LoanedBooks = new List<Book>();
        }

    }
  
}
