using System.IO;

namespace oop2._2

{
    using System.IO;
    internal class UserRegistration
    {
        public static UserRegistration activeUser;
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime Birthdate { get; set; }
        public List<Wallet> Wallets { get; set; }

        public UserRegistration(string email, string password, DateTime birthdate, int op)
        {
            
            if (op == 1)
            {
                if (IsValidEmail(email) && !EmailExists(email))
                {
                    Email = email;
                }
                else
                {
                    Console.WriteLine("Email already exists");
                    return;
                }

            }

            else if (op == 2)
            {
                if (IsValidEmail(email))
                {
                    Email = email;
                    Wallets = new List<Wallet>();
                }
                else
                {
                    Console.WriteLine("Invalid email");
                }
            }

            if (IsValidPassword(password))
            {
                Password = password;
            }
            else
            {
                Console.WriteLine("Password is too weak");
            }

            if (IsValidBirthdate(birthdate))
            {
                Birthdate = birthdate;
            }
            else
            {
                Console.WriteLine("Invalid birthdate");
            }
            if (IsValidEmail(email) && IsValidPassword(password) && IsValidBirthdate(birthdate))
            {
                Email = email;
                Password = password;
                Birthdate = birthdate;
                activeUser = this;
              
                Wallets = new List<Wallet>();
                SaveUser();
            }

        }

        private bool EmailExists(string email)
        {

            //Read the user's data from the file
            string filePath = "users.txt";
            if (!File.Exists(filePath))
            {
                Console.WriteLine("File does not exist!");
                return false;
            }

            string[] userData = File.ReadAllLines(filePath);
            foreach (string line in userData)
            {
                string[] data = line.Split(',');
                if (data[0] == email)
                {
                    return true;
                }
            }
            return false;

        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private bool IsValidPassword(string password)
        {
            int minLength = 8;
            if (password.Length < minLength)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool IsValidBirthdate(DateTime birthdate)
        {
            if (birthdate > DateTime.Now)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private void SaveUser()
        {
            string filePath = "users.txt";
            string userData = Email + "," + Password + "," + Birthdate;
            for (int i = 0; i < Wallets.Count; i++)
            {
                userData += "," + Wallets[i].CurrentAmount + "," + Wallets[i].Currency;
            }
            File.AppendAllText(filePath, userData + Environment.NewLine);
        }
        public static bool Login(string email, string password)
        {
            //Read the user's data from the file
            string filePath = "users.txt";
            if (!File.Exists(filePath))
            {
                Console.WriteLine("File does not exist!");
                return false;
            }

            string[] userData = File.ReadAllLines(filePath);
            foreach (string line in userData)
            {
                string[] data = line.Split(',');
                if (data[0] == email && data[1] == password)
                {
                    activeUser = new UserRegistration(data[0], data[1], DateTime.Parse(data[2]), 0);
                    Console.WriteLine("Welcome " + data[0]);
                    return true;
                }
            }
            Console.WriteLine("Invalid email or password");
            return false;
        }


        public static void logout()
        {
            activeUser = null;
            Console.WriteLine("logged out");

        }
    }
}