using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;
public class Account
{
    public string Name { get; set; }
    public double Balance { get; set; }

    public Account(string name = "Unnamed Account", double balance = 0.0)
    {
        this.Name = name;
        this.Balance = balance;
    }

    public bool Deposit(double amount)
    {
        if (amount < 0)
            return false;
        else
        {
            Balance += amount;
            return true;
        }
    }

    public bool Withdraw(double amount)
    {
        if (Balance - amount >= 0)
        {
            Balance -= amount;
            return true;
        }
        else
        {
            return false;
        }
    }
    public static double operator +(Account lhs, Account rhs)
    {
        return lhs.Balance + rhs.Balance;
    }
    public override string ToString()
    {
        return Name + " : Balance = " + Balance;
    }

}
class SavingsAccount : Account
{
    public SavingsAccount(string name = "Unnamed Account", double balance = 0.0, double interestrate = 0.05) : base(name, balance)
    {
        InterestRate = interestrate;
    }

    public double InterestRate { get; set; }

    public new void Withdraw(double amount)
    {
        base.Withdraw(Balance + (Balance * InterestRate));
    }



}
class CheckingAccount : Account
{
    public CheckingAccount(string name = "Unnamed Account", double balance = 0.0, double fee = 1.5) : base(name, balance)
    {
        Fee = fee;
    }

    public double Fee { get; set; }
    public new bool Withdraw(double amount)
    {
        double totalAmount = amount + Fee;
        if (Balance >= totalAmount)
        {
            Balance -= totalAmount;
            return true;
        }
        return false;
    }
}
//class TrustAccount : Account
//{
//    private List<DateTime> withdrawalDates = new List<DateTime>();
//    private const int MaxWithdrawals = 3;
//    public TrustAccount(string name = "Unnamed Account", double balance = 0.0, double interestrate = 0.05) : base(name, balance)
//    {
//        InterestRate = interestrate;
//    }

//    public double InterestRate { get; set; }

//    public new bool Deposit(double amount)
//    {
//        if (amount >= 5000)
//        {
//            Balance += amount + 50;

//        }
//        else
//        {
//            Balance += amount;
//        }
//        return true;
//    }
//    public new bool Withdraw(double amount)
//    {
//        DateTime now = DateTime.Now;
//        withdrawalDates.RemoveAll(date => date.Year < now.Year); 

//        if (withdrawalDates.Count >= MaxWithdrawals)
//        {
//            Console.WriteLine($"{Name}: Withdrawal limit reached for this year.");
//            return false;
//        }
//        if (amount > Balance * 0.2)
//        {
//            Console.WriteLine($"{Name}: Cannot withdraw more than 20% of balance.");
//            return false;
//        }
//        if (Balance >= amount)
//        {
//            Balance -= amount;
//            withdrawalDates.Add(now);
//            return true;
//        }
//        return false;
//    }

class TrustAccount : SavingsAccount
{
    private int withdrawCount = 0;
    private const int Withdrawals = 3;
public TrustAccount(string name = "Unnamed Account", double balance = 0.0, double interestrate = 0.05) : base(name, balance , interestrate)
    {
    //InterestRate = interestrate;
}
//public double InterestRate { get; set; }
public new bool Deposit(double amount)
    {
        if (amount >= 5000)
        {
            Balance += amount + 50;

        }
        else
        {
            Balance += amount;
        }
        return true;
    }
    public new bool Withdraw(double amount)
    {
        if (withdrawCount >= Withdrawals)
        {
            Console.WriteLine($"{Name}: Withdrawal limit reached.");
            return false;
        }
        if (amount > Balance * 0.2)
        {
            Console.WriteLine($"{Name}: Cannot withdraw more than 20% of balance.");
            return false;
        }
        if (Balance >= amount)
        {
            Balance -= amount + (amount*InterestRate);
            withdrawCount++;
            return true;
        }
        return false;
    }
}

public class AccountUtil
{
    // Utility helper functions for Account class
    public static void Deposit(List<Account> accounts, double amount)
    {
        Console.WriteLine("\n=== Depositing to Accounts =================================");
        foreach (var acc in accounts)
        {
            if (acc.Deposit(amount))
                Console.WriteLine($"Deposited {amount} to {acc}");
            else
                Console.WriteLine($"Failed Deposit of {amount} to {acc}");
        }
    }

    public static void Withdraw(List<Account> accounts, double amount)
    {
        Console.WriteLine("\n=== Withdrawing from Accounts ==============================");
        foreach (var acc in accounts)
        {
            if (acc.Withdraw(amount))
                Console.WriteLine($"Withdrew {amount} from {acc}");
            else
                Console.WriteLine($"Failed Withdrawal of {amount} from {acc}");
        }
    }

    // Helper functions for SavingsAccount
    public static void DepositSavings(List<Account> accounts, double amount)
    {
        Console.WriteLine("\n=== Depositing to Savings Accounts =================================");
        foreach (var acc in accounts)
        {
            if (acc.Deposit(amount))
                Console.WriteLine($"Deposited {amount} to {acc}");
            else
                Console.WriteLine($"Failed Deposit of {amount} to {acc}");
        }
    }

    public static void WithdrawSavings(List<Account> accounts, double amount)
    {
        Console.WriteLine("\n=== Withdrawing from Savings Accounts ==============================");
        foreach (var acc in accounts)
        {
            if (acc.Withdraw(amount))
                Console.WriteLine($"Withdrew {amount} from {acc}");
            else
                Console.WriteLine($"Failed Withdrawal of {amount} from {acc}");
        }
    }

    // Helper functions for CheckingAccount
    public static void DepositChecking(List<Account> accounts, double amount)
    {
        Console.WriteLine("\n=== Depositing to Checking Accounts =================================");
        foreach (var acc in accounts)
        {
            if (acc.Deposit(amount))
                Console.WriteLine($"Deposited {amount} to {acc}");
            else
                Console.WriteLine($"Failed Deposit of {amount} to {acc}");
        }
    }

    public static void WithdrawChecking(List<Account> accounts, double amount)
    {
        Console.WriteLine("\n=== Withdrawing from Checking Accounts ==============================");
        foreach (var acc in accounts)
        {
            if (acc.Withdraw(amount))
                Console.WriteLine($"Withdrew {amount} from {acc}");
            else
                Console.WriteLine($"Failed Withdrawal of {amount} from {acc}");
        }
    }

    // Helper functions for TrustAccount
    public static void DepositTrust(List<Account> accounts, double amount)
    {
        Console.WriteLine("\n=== Depositing to Trust Accounts =================================");
        foreach (var acc in accounts)
        {
            if (acc.Deposit(amount))
                Console.WriteLine($"Deposited {amount} to {acc}");
            else
                Console.WriteLine($"Failed Deposit of {amount} to {acc}");
        }
    }

    public static void WithdrawTrust(List<Account> accounts, double amount)
    {
        Console.WriteLine("\n=== Withdrawing from Trust Accounts ==============================");
        foreach (var acc in accounts)
        {
            if (acc.Withdraw(amount))
                Console.WriteLine($"Withdrew {amount} from {acc}");
            else
                Console.WriteLine($"Failed Withdrawal of {amount} from {acc}");
        }
    }
}

class Program
{
    static void Main()
    {
        // Accounts
        var accounts = new List<Account>();
        accounts.Add(new Account());
        accounts.Add(new Account("Larry"));
        accounts.Add(new Account("Moe", 2000));
        accounts.Add(new Account("Curly", 5000));

        AccountUtil.Deposit(accounts, 1000);
        AccountUtil.Withdraw(accounts, 2000);

        // Savings
        var savAccounts = new List<Account>();
        savAccounts.Add(new SavingsAccount());
        savAccounts.Add(new SavingsAccount("Superman"));
        savAccounts.Add(new SavingsAccount("Batman", 2000));
        savAccounts.Add(new SavingsAccount("Wonderwoman", 5000, 5.0));

        AccountUtil.DepositSavings(savAccounts, 1000);
        AccountUtil.WithdrawSavings(savAccounts, 2000);

        // Checking
        var checAccounts = new List<Account>();
        checAccounts.Add(new CheckingAccount());
        checAccounts.Add(new CheckingAccount("Larry2"));
        checAccounts.Add(new CheckingAccount("Moe2", 2000));
        checAccounts.Add(new CheckingAccount("Curly2", 5000));

        AccountUtil.DepositChecking(checAccounts, 1000);
        AccountUtil.WithdrawChecking(checAccounts, 2000);
        AccountUtil.WithdrawChecking(checAccounts, 2000);

        // Trust
        var trustAccounts = new List<Account>();
        trustAccounts.Add(new TrustAccount());
        trustAccounts.Add(new TrustAccount("Superman2"));
        trustAccounts.Add(new TrustAccount("Batman2", 2000));
        trustAccounts.Add(new TrustAccount("Wonderwoman2", 5000, 5.0));

        AccountUtil.DepositTrust(trustAccounts, 1000);
        AccountUtil.DepositTrust(trustAccounts, 6000);
        AccountUtil.WithdrawTrust(trustAccounts, 2000);
        AccountUtil.WithdrawTrust(trustAccounts, 3000);
        AccountUtil.WithdrawTrust(trustAccounts, 500);


        Account account1 = new Account("Ahmed", 3000);
        Account account2 = new Account("Solly", 5000);
        double totalBalance = account1 + account2;
        Console.WriteLine("----Testing-----");
        Console.WriteLine($"Total Balance of {account1.Name} and {account2.Name} is: {totalBalance}");

        Console.WriteLine();
    }
}


