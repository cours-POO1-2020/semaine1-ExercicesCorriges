using System;
using System.Diagnostics;
using System.Threading;

namespace BankAccountNS
{

    public class Program
    { 
        public const int MaxAmount = 100;

        public static void Main(string[] args)
        {
            /*
             * Avant modifications, ce programme cree un compte en banque au nom de Mr. Bryan Walton et y depose 12.25
             * Il execute ensuite 10 fois une sequence de deux operations:
             *  - Un credit d'un montant aletoire entre 0 et 100 dollars (MaxAmount)
             *  - Un debit d'un montant aletoire entre 0 et 100 dollars (MaxAmount)            
             * A chacune de ces operations il refuse un nombre negatif ou un debit superieur a la balance du compte
             * Il affiche la blance apres chaque operation
             *toto
             */



            //BankAccount ba = new BankAccount("Mr. Bryan Walton", 12.25);
            //1) changement de nom
            BankAccount ba = new BankAccount("M. Julien Brunet", 12.25);

            Random rand = new Random();
            // for (int i = 0; i < 10 ; i++)
            //2) on ajoute 5 operations pour en avoir 15
            for (int i = 0; i < 15; i++)
            {

                ba.Credit(Math.Round(rand.NextDouble() * MaxAmount,2));
                Thread.Sleep(500);

                //Je ne veux que 10 credits mais 15 debits
                if (i < 10)
                {
                    ba.Debit(Math.Round(rand.NextDouble() * MaxAmount, 2));
                    Thread.Sleep(500);
                }

            }

            Console.ReadKey();
        }
    }


    /// <summary>
    /// Bank account demo class.
    /// </summary>
    public  class BankAccount
    {
        private readonly string _CustomerName;
        private double _Balance;

        private BankAccount() { }

        public BankAccount(string customerName, double balance)
        {
            _CustomerName = customerName;
            _Balance = balance;
            Console.WriteLine($"{CustomerName} just opened a new account and has credited: {Balance} $." );
        }

        public string CustomerName
        {
            get { return _CustomerName; }
        }

        public double Balance
        {
            get { return Math.Round(_Balance,2); }
        }

        public void Debit(double amount)
        {
            Console.WriteLine("---Start operation---");
            Console.WriteLine($"Trying to Debit {amount} $ on {this.CustomerName} account");
            if (amount > Balance)
            {
                Console.ForegroundColor  = ConsoleColor.Red;
                Console.WriteLine($"Impossible to debit more than existing account balance.");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("---operation aborted---");
                Console.WriteLine();
                return;
            }

            if (amount < 0)
            {
                Console.WriteLine($"Impossible to debit negative values.");
                Console.WriteLine("---operation aborted---");
                Console.WriteLine();
                return;
            }

            //Gestion de 1$ de taxe par debit
            _Balance -= amount;
            _Balance -= 1;
            Console.WriteLine($"Successfull: Balance is {Balance} $ on {this.CustomerName} account, (1$ tax applied).");
            Console.WriteLine("---End operation---");
            Console.WriteLine();
        }

        public void Credit(double amount)
        {
            Console.WriteLine("---Start operation---");
            Console.WriteLine($"Trying to credit {amount} $ on {this.CustomerName} account");
            if (amount < 0)
            {
                Console.WriteLine($"Impossible to credit negative values.");
                Console.WriteLine("---operation aborted---");
                Console.WriteLine("---operation aborted---");
                Console.WriteLine();
                return;
            }
            //3) on ajoute la gestion du credit de 20 $ minimum
            if (amount < 20)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Minimum credit is 20$.");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("---operation aborted---");
                Console.WriteLine();
                return;
            }

            //Gestion de 1$ de taxe par credit
            _Balance += amount;
            _Balance -= 1;

            Console.WriteLine($"Successfull: Balance is {Balance} $ on {this.CustomerName} account, (1$ tax applied).");

            Console.WriteLine("---End operation---");
            Console.WriteLine();

        }


    }
}
