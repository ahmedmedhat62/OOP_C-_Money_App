using oop2._2;

public class Wallet
{
    public decimal StartingAmount { get; set; }
    public currency Currency { get; set; }
    public decimal CurrentAmount { get; set; }

    public static Wallet activeWallet;

    private List<Operations> operations = new List<Operations>();
    public Wallet(decimal startingAmount, currency currency)
    {
        StartingAmount = startingAmount;
        CurrentAmount = startingAmount;
        Currency = currency;
        Console.WriteLine("wallet created");
    }

    public void SelectAsActive()
    {
        activeWallet = this;
    }

    public void AddMoneyToActiveWallet(int amount, IncomeCategory category, DateTime transactionTime)
    {
        if (activeWallet != null)
        {
            activeWallet.CurrentAmount += amount;
            operations.Add(new Income(amount, category, transactionTime));
        }
    }
    public void RemoveMoneyFromActiveWallet(int amount, ExpenseCategory category, DateTime transactionTime)
    {
        if (activeWallet != null && activeWallet.CurrentAmount >= amount)
        {
            activeWallet.CurrentAmount -= amount;
            operations.Add(new Expense(amount, category, transactionTime));
        }
    }

    public List<Operations> GetOperationsBetweenDates(DateTime startDate, DateTime endDate)
    {
        var operationsBetweenDates = operations.Where(o => o.TransactionTime.Date >= startDate.Date && o.TransactionTime.Date <= endDate.Date).ToList();
        return operationsBetweenDates;
    }

}
