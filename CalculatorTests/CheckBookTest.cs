using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Calculator.CheckBook;
using System.Linq;
using System.Collections.ObjectModel;

namespace CalculatorTests
{
    [TestClass]
    public class CheckBookTest
    {
        [TestMethod]
        public void FillsUpProperly()
        {
            var ob = new CheckBookVM();

            Assert.IsNull(ob.Transactions);

            ob.Fill();

            Assert.AreEqual(12, ob.Transactions.Count);
        }

        [TestMethod]
        public void CountofEqualsMoshe()
        {
            var ob = new CheckBookVM();
            ob.Fill();

            var count = ob.Transactions.Where( t => t.Payee == "Moshe" ).Count();

            Assert.AreEqual(4, count);
        }

        [TestMethod]
        public void SumOfMoneySpentOnFood()
        {
            var ob = new CheckBookVM();
            ob.Fill();

            var category = "Food";

            var food = ob.Transactions.Where(t=> t.Tag == category );

            var total = food.Sum(t => t.Amount);

            Assert.AreEqual(261, total);

        }

        [TestMethod]
        public void Group()
        {
            var ob = new CheckBookVM();
            ob.Fill();

            var total = ob.Transactions.GroupBy(t => t.Tag).Select(g => new { g.Key, Sum=g.Sum( t=> t.Amount ) });

            Assert.AreEqual(261, total.First().Sum);
            Assert.AreEqual(300, total.Last().Sum);
        }

        [TestMethod]
        public void averageTransPerTag()
        {
            var ob = new CheckBookVM();
            ob.Fill();
            var average = ob.Transactions.GroupBy(tag => tag.Tag).Select(s => new { s.Key, avg = s.Average(tag => tag.Amount) });
            Assert.AreEqual(32.625, average.First().avg);
            Assert.AreEqual(75, average.Last().avg);
        }

        [TestMethod]
        public void payForFood()
        {
            var ob = new CheckBookVM();
            ob.Fill();
            var category = "Food";
            var total1 = ob.Transactions.Where(tag => tag.Payee == "Moshe" & tag.Tag == category).Sum(tag => tag.Amount);

            Assert.AreEqual(130, total1);

            var total2 = ob.Transactions.Where(tag => tag.Payee == "Tim" & tag.Tag == category).Sum(tag => tag.Amount);
            Assert.AreEqual(0, total2);

            var total3 = ob.Transactions.Where(tag => tag.Payee == "Bracha" & tag.Tag == category).Sum(tag => tag.Amount);
            Assert.AreEqual(131, total3);
        }

        [TestMethod]
        public void payForPayee()
        {
            var ob = new CheckBookVM();
            ob.Fill();
            var pay1 = ob.Transactions.Where(tag => tag.Payee == "Moshe").Sum(tag => tag.Amount);

            Assert.AreEqual(130, pay1);

            var pay2 = ob.Transactions.Where(tag => tag.Payee == "Tim").Sum(tag => tag.Amount);
            Assert.AreEqual(300, pay2);

            var pay3 = ob.Transactions.Where(tag => tag.Payee == "Bracha").Sum(tag => tag.Amount);
            Assert.AreEqual(131, pay3);

        }

        [TestMethod]
        public void aprilTransactions()
        {
            var ob = new CheckBookVM();
            ob.Fill();
            var start_date = DateTime.Parse("4/5/2015");
            var end_date= DateTime.Parse("4/8/2015");
            var trans = ob.Transactions.Where(tag => (tag.Date >= start_date.Date) & (tag.Date < end_date.Date)).Count();
            Assert.AreEqual(6, trans);
        }

        [TestMethod]
        public void moneyOnAuto()
        {
            var ob = new CheckBookVM();
            ob.Fill();
            var category = "Auto";

            var auto_checking = ob.Transactions.Where(tag => tag.Tag == category & tag.Account == "Checking");

            var total_checking = auto_checking.Sum(tag => tag.Amount);

            Assert.AreEqual(150, total_checking);
            var auto_credit = ob.Transactions.Where(t => t.Tag == category & t.Account == "Credit");

            var total_credit = auto_credit.Sum(t => t.Amount);

            Assert.AreEqual(150, total_credit);
            Assert.IsTrue(total_checking == total_credit);
        }
        
        [TestMethod]
        public void transOfAccounts()
        {
            var ob = new CheckBookVM();
            ob.Fill();

            var date_start = DateTime.Parse("4/5/2015");
            var date_end = DateTime.Parse("4/8/2015");
            var trans_checking = ob.Transactions.Where(tag => (tag.Date >= date_start.Date) & (tag.Date < date_end.Date) & tag.Account == "Checking").Count();

            Assert.AreEqual(3, trans_checking);
            var trans_credit = ob.Transactions.Where(tag => (tag.Date >= date_start.Date) & (tag.Date < date_end.Date) & tag.Account == "Credit").Count();
            Assert.AreEqual(3, trans_credit);

        }
        
        [TestMethod]
        public void datesAccountWasUsed()
        {
            var ob = new CheckBookVM();
            ob.Fill();
           // List <DateTime> list = new List <DateTime>();
            var trans_checking = ob.Transactions.Where(tag => tag.Account == "Checking");
            var date_checking = trans_checking.OrderBy(tag => tag.Date).Count();
            //list.Add();
            Assert.AreEqual(6, date_checking);
            var trans_credit = ob.Transactions.Where(tag => tag.Account == "Credit");
            var date_credit = trans_credit.OrderBy(tag => tag.Date).Count();
            Assert.AreEqual(6, date_credit);

        }
    }
}
