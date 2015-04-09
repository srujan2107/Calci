using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.CheckBook
{
    public class Transaction: BaseVM
    {
        private CheckBookVM _VM;
        public CheckBookVM VM
        {
            get { return _VM; }
            set { _VM = value; OnPropertyChanged(); }
        }
        
        private DateTime _Date;
        public DateTime Date
        {
            get { return _Date; }
            set { _Date = value; OnPropertyChanged(); }
        }

        private string _Payee;
        public string Payee
        {
            get { return _Payee; }
            set { _Payee = value; OnPropertyChanged(); }
        }

        private string _Account;
        public string Account
        {
            get { return _Account; }
            set { _Account = value; OnPropertyChanged(); if (VM != null) VM.OnPropertyChanged("Accounts"); }
        }

        private double _Amount;
        public double Amount
        {
            get { return _Amount; }
            set { _Amount = value; OnPropertyChanged(); }
        }

        private string _Tag;
        public string Tag
        {
            get { return _Tag; }
            set { _Tag = value; OnPropertyChanged(); }
        }
        

    }

    public class CheckBookVM: BaseVM
    {
        private ObservableCollection<Transaction> _Transactions;
        public ObservableCollection<Transaction> Transactions
        {
            get { return _Transactions; }
            set { _Transactions = value; OnPropertyChanged(); OnPropertyChanged("Accounts"); }
        }

        public IEnumerable<string> Accounts
        {
            get { return Transactions.Select(t=> t.Account).Distinct(); }
        }
        

        public void Fill()
        {
            Transactions = new ObservableCollection<Transaction>( new[] {
                new Transaction { VM=this, Date= DateTime.Now.AddDays(-1), Account="Checking", Payee="Moshe", Amount=30, Tag="Food" },
                new Transaction { VM=this, Date= DateTime.Now.AddDays(-3), Account="Checking", Payee="Tim", Amount=130, Tag="Auto" },
                new Transaction { VM=this, Date= DateTime.Now.AddDays(-4), Account="Checking", Payee="Moshe", Amount=35, Tag="Food" },
                new Transaction { VM=this, Date= DateTime.Now.AddDays(-5), Account="Checking", Payee="Bracha", Amount=35, Tag="Food" },
                new Transaction { VM=this, Date= DateTime.Now.AddDays(-6), Account="Checking", Payee="Tim", Amount=20, Tag="Auto" },
                new Transaction { VM=this, Date= DateTime.Now.AddDays(-1), Account="Credit", Payee="Moshe", Amount=30, Tag="Food" },
                new Transaction { VM=this, Date= DateTime.Now.AddDays(-2), Account="Credit", Payee="Bracha", Amount=30.5, Tag="Food" },
                new Transaction { VM=this, Date= DateTime.Now.AddDays(-3), Account="Credit", Payee="Tim", Amount=130, Tag="Auto" },
                new Transaction { VM=this, Date= DateTime.Now.AddDays(-4), Account="Credit", Payee="Moshe", Amount=35, Tag="Food" },
                new Transaction { VM=this, Date= DateTime.Now.AddDays(-5), Account="Credit", Payee="Bracha", Amount=35, Tag="Food" },
                new Transaction { VM=this, Date= DateTime.Now.AddDays(-2), Account="Checking", Payee="Bracha", Amount=30.5, Tag="Food" },
                new Transaction { VM=this, Date= DateTime.Now.AddDays(-6), Account="Credit", Payee="Tim", Amount=20, Tag="Auto" },
            });
        }
    }
}
