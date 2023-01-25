namespace oop2._2
{
    public enum ExpenseCategory
    {
        Food,
        Taxi,
        Restaurants,
        Sport,
        Rent,
        Investments,
        Clothes,
        Fun,
        Other,
        Medicine
    }

    public enum IncomeCategory
    {
        Salary,
        Scholarship,
        Other
    }

    public enum currency
    {
        Usd,
        Eur,
        Rub
    }

    public class Operations
    {
        public Operations(int amount, string category, DateTime transactionTime)
        {
            this.amount = amount;
            this.category = category;
            TransactionTime = transactionTime;
        }

        public int amount { get; set; }
        public string category { get; set; }
        public DateTime TransactionTime { get; set; }
    }

    public class Income : Operations
    {
        public Income(int amount, IncomeCategory category, DateTime transactionTime) : base(amount, category.ToString(), transactionTime)
        {
        }
    }

    public class Expense : Operations
    {
        public Expense(int amount, ExpenseCategory category, DateTime transactionTime) : base(amount, category.ToString(), transactionTime)
        {
        }
    }
}
