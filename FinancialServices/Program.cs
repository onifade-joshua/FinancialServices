using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace FinancialServices
{
    internal class Program
    {
        private const string path = @"C:\Users\joshua\Documents\AccountOpening.json";
        private static int userIdCounter = 1;
        private static int accountIdCounter = 1;

        static void Main(string[] args)
        {
            bool AccountOpening = true;
            List<UserTable> userAccounts = new List<UserTable>();

            while (AccountOpening)
            {
                UserTable UserDetails = GetUserDetails();
                UserDetails.UserId = GenerateUniqueUserId();
                AccountTable UserProfile = GetAccountDetails();
                UserProfile.AccountId = GenerateUniqueAccountId();

                // Allow the user to deposit or withdraw
                Console.WriteLine("Do you want to deposit or withdraw? (deposit/withdraw)");
                string transactionType = Console.ReadLine().ToLower();
                if (transactionType == "deposit")
                {
                    TransactionTable PaymentMethod = GetTransactionDetails("Deposit");
                    Deposit(UserProfile, PaymentMethod.Amount);
                }
                else if (transactionType == "withdraw")
                {
                    TransactionTable PaymentMethod = GetTransactionDetails("Withdraw");
                    Withdraw(UserProfile, PaymentMethod.Amount);
                }
                else
                {
                    Console.WriteLine("Invalid transaction type.");
                }

                string json = JsonSerializer.Serialize(UserProfile);
                SaveUserDetailsToFile(json);

                userAccounts.Add(UserDetails);

                Console.WriteLine("Do you want to open another account? (yes/no)");
                string response = Console.ReadLine().ToLower();
                if (response != "yes")
                {
                    AccountOpening = false;
                }
            }

            // Display account details
            foreach (var userAccount in userAccounts)
            {
                Console.WriteLine($"User ID: {userAccount.UserId}");
                Console.WriteLine($"Account ID: {userAccount.AccountId}");
                Console.WriteLine($"Account Type: {userAccount.AccountType}");
                Console.WriteLine($"Account Balance: {userAccount.Balance}");
                Console.WriteLine();
            }
        }

        static UserTable GetUserDetails()
        {
            UserTable userDetails = new UserTable();

            Console.WriteLine("Enter First Name?");
            userDetails.FirstName = Console.ReadLine();

            Console.WriteLine("Enter Last Name?");
            userDetails.LastName = Console.ReadLine();

            Console.WriteLine("Enter date of birth?");
            userDetails.DateOfBirth = Console.ReadLine();

            Console.WriteLine("Enter Gmail address?");
            userDetails.Gmail = Console.ReadLine();

            Console.WriteLine("Enter phone number?");
            userDetails.PhoneNumber = Console.ReadLine();

            Console.WriteLine("Enter house address?");
            userDetails.Address = Console.ReadLine();

            Console.WriteLine("Enter user name is?");
            userDetails.UserName = Console.ReadLine();

            Console.WriteLine("Please enter your unique passcode?");
            userDetails.Password = Console.ReadLine();


            return userDetails;
        }

        static AccountTable GetAccountDetails()
        {
            AccountTable accountDetails = new AccountTable();
            Console.WriteLine("Welcome to SamJoDa Microfinance Bank Ltd");

            // Choose between Savings and Current accounts
            Console.WriteLine("Choose Account Type (Savings/Current):");
            accountDetails.AccountType = Console.ReadLine();

            // Prompt user for other account details
            Console.WriteLine("Account number of Receiver");
            accountDetails.AccountNumber = Console.ReadLine();

            return accountDetails;
        }

        static TransactionTable GetTransactionDetails(string transactionType)
        {
            TransactionTable transactionDetails = new TransactionTable();

            Console.WriteLine($"{transactionType} Feed!");

            // Set the transaction type based on the parameter
            transactionDetails.TransactionType = transactionType;

            Console.WriteLine("Transaction ID:");
            transactionDetails.TransactionId = Console.ReadLine();

            while (true)
            {
                Console.WriteLine($"How much are you {transactionType.ToLower()}?");
                string amountStr = Console.ReadLine();
                if (decimal.TryParse(amountStr, out decimal amount))
                {
                    transactionDetails.Amount = amount;
                    break; // Exit the loop if valid amount is provided
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid numeric amount.");
                }
            }

            //Console.WriteLine("Description:");
            //transactionDetails.Description = Console.ReadLine();

            return transactionDetails;
        }


        static void Deposit(AccountTable account, decimal amount)
        {
            account.Balance += amount;
            Console.WriteLine($"You have successfully deposited {amount:C}. Your new balance is: {account.Balance}");
        }

        static void Withdraw(AccountTable account, decimal amount)
        {
            if (amount <= account.Balance)
            {
                account.Balance -= amount;
                Console.WriteLine($"You have successfully withdrawn {amount:C}. Your new balance is: {account.Balance}");
            }
            else
            {
                Console.WriteLine("Insufficient funds for withdrawal.");
            }
        }

        static string GenerateUniqueUserId()
        {
            return $"U{userIdCounter++}";
        }

        static string GenerateUniqueAccountId()
        {
            return $"A{accountIdCounter++}";
        }

        static void SaveUserDetailsToFile(string userDetailsJson)
        {
            try
            {
                File.AppendAllText(path, userDetailsJson + Environment.NewLine);
                Console.WriteLine("User details saved to AccountOpening.json");
            }
            catch (Exception me)
            {
                Console.WriteLine("Error: " + me.Message);
            }
        }
    }
}
