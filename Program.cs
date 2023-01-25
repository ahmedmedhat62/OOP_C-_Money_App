using oop2._2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

{

    List<Wallet> wallets = new List<Wallet>();
    
    while (true)
    {
        Console.WriteLine("press 1 for registration or press 2 for login");
        var n = Console.ReadLine();
        if (int.TryParse(n, out var output))
        {
            if (output == 1)
            {
                Console.Clear();
                Console.WriteLine("write your email");
                var email = Console.ReadLine();
                Console.WriteLine("write your password");
                var pass = Console.ReadLine();
                Console.WriteLine("write your birthdate");
                DateTime birth = DateTime.Parse(Console.ReadLine());
                UserRegistration user = new UserRegistration(email, pass, birth, output);
                if (user.Email == UserRegistration.activeUser.Email)
                {
                    Console.WriteLine("Registration successful, Welcome " + user.Email);
                }
                Console.Clear();
            }
            else if (output == 2)
            {
                Console.Clear();
                Console.WriteLine("Enter your email:");
                string email = Console.ReadLine();
                Console.WriteLine("Enter your password:");
                string password = Console.ReadLine();
                if (UserRegistration.Login(email, password))
                {
                    while (true)
                    {
                        
                        
                        Console.WriteLine("You are now logged in");


                        Console.WriteLine("1- Logout");
                        Console.WriteLine("2- Show wallets");
                        Console.WriteLine("3- Create a wallet");
                        Console.WriteLine("4- Choose a Wallet");
                        Console.WriteLine("5- Delete a wallet");
                        Console.WriteLine("6- add income");
                        Console.WriteLine("7- add expense");
                        Console.WriteLine("8- show statistics");
                        string f = Console.ReadLine();
                        if (f == "1")
                        {
                            UserRegistration.logout();
                            break;

                        }
                        else if (f == "2")
                        {
                            while (true)
                            {
                                Console.WriteLine("Existing wallets: ");
                                for (int i = 0; i < wallets.Count; i++)
                                {
                                    Console.WriteLine(i + 1 + "- " + wallets[i].Currency + " " + wallets[i].CurrentAmount);
                                }
                                Console.WriteLine("666- Go back to logged in menu ");
                                int input = int.Parse(Console.ReadLine());
                                if (input == 666)
                                {
                                    break;
                                }

                            }
                        }
                        else if (f == "3")
                        {
                            Console.WriteLine("Enter starting amount:");
                            int amount = int.Parse(Console.ReadLine());
                            Console.WriteLine("Choose currency:");
                            var names = Enum.GetNames(typeof(currency));
                            for (int i = 0; i < names.Length; i++)
                            {
                                Console.WriteLine(i + 1 + " - " + names[i]);
                            }
                            int currencyChoice = int.Parse(Console.ReadLine());
                            currency selectedCurrency = (currency)Enum.Parse(typeof(currency), names[currencyChoice - 1]);
                            Wallet newWallet = new Wallet(amount, selectedCurrency);
                            wallets.Add(newWallet);
                            Console.WriteLine("Wallet added");

                        }
                        else if (f == "4")
                        {

                            for (int i = 0; i < wallets.Count; i++)
                            {

                                Console.WriteLine(i + 1 + " " + wallets[i].Currency + " " + wallets[i].CurrentAmount);
                            }
                            int c = int.Parse(Console.ReadLine());
                            c--;
                            Wallet.activeWallet = wallets[c];



                        }

                        else if (f == "5")
                        {
                            for (int i = 0; i < wallets.Count; i++)
                            {

                                Console.WriteLine(i + 1 + "- " + wallets[i].Currency + " " + wallets[i].CurrentAmount);
                            }
                            int d = int.Parse(Console.ReadLine());
                            d--;
                            wallets.RemoveAt(d);
                            Console.WriteLine("Wallet removed");


                        }
                        else if (f == "6")
                        {
                            Console.WriteLine("enter the amount of money");
                            int add = int.Parse(Console.ReadLine());
                            Console.WriteLine("enter the category:");
                            var names = Enum.GetNames(typeof(IncomeCategory));
                            for (int i = 0; i < names.Length; i++)
                            {
                                Console.WriteLine(i + 1 + " - " + names[i]);
                            }
                            int catNum = int.Parse(Console.ReadLine());
                            if (catNum > 0 && catNum <= names.Length)
                            {
                                IncomeCategory cat = (IncomeCategory)Enum.Parse(typeof(IncomeCategory), names[catNum - 1]);
                                Console.WriteLine("Enter a date:");
                                string dateString = Console.ReadLine();
                                if (!DateTime.TryParse(dateString, out DateTime dt))
                                    Console.WriteLine("Invalid date, please enter a valid date");
                                else
                                    Wallet.activeWallet.AddMoneyToActiveWallet(add, cat, dt);
                            }
                            else
                            {
                                Console.WriteLine("Invalid category number, please enter a valid number");
                            }
                        }


                        else if (f == "7")
                        {
                            Console.WriteLine("enter the amount of money");
                            int add = int.Parse(Console.ReadLine());
                            Console.WriteLine("enter the category:");
                            var names = Enum.GetNames(typeof(ExpenseCategory));
                            for (int i = 0; i < names.Length; i++)
                            {
                                Console.WriteLine(i + 1 + " - " + names[i]);
                            }
                            int catNum = int.Parse(Console.ReadLine());
                            if (catNum > 0 && catNum <= names.Length)
                            {
                                ExpenseCategory cat = (ExpenseCategory)Enum.Parse(typeof(ExpenseCategory), names[catNum - 1]);
                                Console.WriteLine("Enter a date:");
                                string dateString = Console.ReadLine();
                                if (!DateTime.TryParse(dateString, out DateTime dt))
                                    Console.WriteLine("Invalid date, please enter a valid date");
                                else
                                    Wallet.activeWallet.RemoveMoneyFromActiveWallet(add, cat, dt);
                            }
                            else
                            {
                                Console.WriteLine("Invalid category number, please enter a valid number");
                            }
                        }

                        else if (f == "8")
                        {
                            Console.WriteLine("enter the first date");
                            string fdateString = Console.ReadLine();
                            DateTime fdt = DateTime.Parse(fdateString);
                            Console.WriteLine("enter the second date");
                            string sdateString = Console.ReadLine();
                            DateTime sdt = DateTime.Parse(sdateString);
                            List<Operations> operations = Wallet.activeWallet.GetOperationsBetweenDates(fdt, sdt);
                            for (int i = 0; i < operations.Count; i++)
                            {
                                Console.WriteLine("Amount: " + operations[i].amount);
                                Console.WriteLine("Category: " + operations[i].category);
                                Console.WriteLine("Transaction time: " + operations[i].TransactionTime);
                            }
                        }
                    }

                }
                else
                {
                    Console.WriteLine("Invalid email or password");
                }
            }
            else
            {
                Console.WriteLine("Invalid input, Please Try Again");
            }
        }
        else
        {
            Console.WriteLine("Invalid input, Please Try Again");
        }

    }
}
