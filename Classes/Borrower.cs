namespace Uppgift2.Classes
{
    public class Borrower
    {
        
        public string Name { get; set; }
        public string LastName { get; set; }
        public string LoanedBooks { get; set; }
        public float SocialSecurityNumber { get; set; }
        public Borrower(string name, string lastname, string loanedbooks, float socialsecuritynumber) 
        { 
            Name = name;
            LastName = lastname;
            LoanedBooks = loanedbooks;
            SocialSecurityNumber = socialsecuritynumber;
        }
        
    }
}
