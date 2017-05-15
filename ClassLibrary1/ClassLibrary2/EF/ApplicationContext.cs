using System;
using System;
using System;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using DAL.Entities;
using DAL.Enums;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DALEF.EF
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public ApplicationContext(string connectionString) : base(connectionString) { }
        static ApplicationContext()
        {
            Database.SetInitializer(new StoreDbInitializer());
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Profile> Profiles { get; set; }
        public virtual DbSet<Portfolio> Portfolios { get; set; }
        public virtual DbSet<Position> Positions { get; set; }
        public virtual DbSet<Symbol> Symbols { get; set; }
        public virtual DbSet<Dividend> Dividends { get; set; }

    }

    public class StoreDbInitializer : DropCreateDatabaseIfModelChanges<ApplicationContext>
    {
        protected override void Seed(ApplicationContext db)
        {
            Role user = new Role { Name = "user" };
            Role admin = new Role { Name = "admin" };

            db.Roles.Add(user);
            db.Roles.Add(admin);

            User firstUser = new User
            {
                UserName = "firstUser",
                Email = "firstUser@gmail.com",
                PasswordHash = "Password"
            };

            db.Users.Add(firstUser);

            Dividend dividend1 = new Dividend { Id = 1, ShareholderName = "Shareholder1", Price = 23.12m };
            Dividend dividend2 = new Dividend { Id = 2, ShareholderName = "Shareholder2", Price = 234.34m };
            Dividend dividend3 = new Dividend { Id = 3, ShareholderName = "Shareholder3", Price = 56.12m };
            Dividend dividend4 = new Dividend { Id = 4, ShareholderName = "Shareholder4", Price = 99.99m };
            Dividend dividend5 = new Dividend { Id = 5, ShareholderName = "Shareholder5", Price = 5342.11m };
            Dividend dividend6 = new Dividend { Id = 6, ShareholderName = "Shareholder6", Price = 11.11m };

            #region Symbols Inizialize
            Symbol symbol1 = new Symbol
            {
                Id = 1,
                Name = "AAT",
                SymbolType = Symbols.Option,
                Dividends = new List<Dividend> { dividend1, dividend2 }
            };
            Symbol symbol2 = new Symbol
            {
                Id = 2,
                Name = "WIWTY",
                SymbolType = Symbols.Stock,
                Dividends = new List<Dividend> { dividend3, dividend4 }
            };
            Symbol symbol3 = new Symbol
            {
                Id = 3,
                Name = "PLSE",
                SymbolType = Symbols.Option,
                Dividends = new List<Dividend> { dividend5, dividend6 }
            };
            Symbol symbol4 = new Symbol { Id = 4, Name = "FXI", SymbolType = Symbols.Stock };
            Symbol symbol5 = new Symbol { Id = 5, Name = "DBA", SymbolType = Symbols.Option };
            Symbol symbol6 = new Symbol { Id = 6, Name = "UCTT", SymbolType = Symbols.Stock };
            Symbol symbol7 = new Symbol { Id = 7, Name = "CC", SymbolType = Symbols.Stock };
            Symbol symbol8 = new Symbol { Id = 8, Name = "ASMB", SymbolType = Symbols.Stock };

            db.Symbols.Add(symbol1);
            db.Symbols.Add(symbol2);
            db.Symbols.Add(symbol3);
            db.Symbols.Add(symbol4);
            db.Symbols.Add(symbol5);
            db.Symbols.Add(symbol6);
            db.Symbols.Add(symbol7);
            db.Symbols.Add(symbol8);
            #endregion

            #region Positions Inizialize
            Position position1 = new Position
            {
                Id = 1,
                SymbolId = 3,
                SymbolType = Symbols.Option,
                SymbolName = "PLSE",
                Name = "Pulse Biosciences CS",
                OpenDate = new DateTime(2015, 7, 20),
                OpenPrice = 128.32m,
                OpenWeight = 40,
                TradeType = TradeTypes.Long,
                TradeStatus = TradeStatuses.Open,
                Dividends = 57.3m,
                CloseDate = new DateTime(2016, 1, 12),
                ClosePrice = 218.32m,
                CurrentPrice = 99.53m,
                Gain = 87.12m,
                AbsoluteGain = 110.34m,
                MaxGain = 154.34m
            };
            Position position2 = new Position
            {
                Id = 2,
                SymbolId = 2,
                SymbolType = Symbols.Stock,
                SymbolName = "WIWTY",
                Name = "Witwatersrand Gold Rsrcs Ltd ",
                OpenDate = new DateTime(2009, 2, 24),
                OpenPrice = 4.00m,
                OpenWeight = 125,
                TradeType = TradeTypes.Long,
                TradeStatus = TradeStatuses.Open,
                Dividends = 0.00m,
                CloseDate = new DateTime(2012, 1, 12),
                ClosePrice = 5.60m,
                CurrentPrice = 3.64m,
                Gain = 40.0m,
                AbsoluteGain = 1.60m,
                MaxGain = 1.60m
            };
            Position position3 = new Position
            {
                Id = 3,
                SymbolId = 1,
                SymbolType = Symbols.Option,
                SymbolName = "AAT",
                Name = "AAT Corporation Limited",
                OpenDate = new DateTime(2017, 4, 28),
                OpenPrice = 43.20m,
                OpenWeight = 113,
                TradeType = TradeTypes.Long,
                TradeStatus = TradeStatuses.Wait,
                Dividends = 17.34m,
                CloseDate = new DateTime(2017, 5, 2),
                ClosePrice = 54.24m,
                CurrentPrice = 27.98m,
                Gain = 11.56m,
                AbsoluteGain = 9.45m,
                MaxGain = 14.34m
            };
            Position position4 = new Position
            {
                Id = 4,
                SymbolId = 4,
                SymbolType = Symbols.Stock,
                SymbolName = "FXI",
                Name = "iShares FTSE/XINHUA 25",
                OpenDate = new DateTime(2009, 8, 28),
                OpenPrice = 39.81m,
                OpenWeight = 65,
                TradeType = TradeTypes.Long,
                TradeStatus = TradeStatuses.Close,
                Dividends = 1.54m,
                CloseDate = new DateTime(2012, 1, 12),
                ClosePrice = 36.74m,
                CurrentPrice = 23.54m,
                Gain = 3.84m,
                AbsoluteGain = 3.65m,
                MaxGain = 3.65m
            };
            Position position5 = new Position
            {
                Id = 1,
                SymbolId = 3,
                SymbolType = Symbols.Option,
                SymbolName = "DBA",
                Name = "Powershares DB Agri Index",
                OpenDate = new DateTime(2009, 10, 30),
                OpenPrice = 25.57m,
                OpenWeight = 72,
                TradeType = TradeTypes.Short,
                TradeStatus = TradeStatuses.Open,
                Dividends = 0.00m,
                CloseDate = new DateTime(2012, 1, 12),
                ClosePrice = 29.25m,
                CurrentPrice = 12.56m,
                Gain = 14.39m,
                AbsoluteGain = 11.34m,
                MaxGain = 13.34m
            };

            db.Positions.Add(position1);
            db.Positions.Add(position2);
            db.Positions.Add(position3);
            db.Positions.Add(position4);
            db.Positions.Add(position5);
            #endregion

            #region Portfolios Inizialize
            Portfolio portfolio1 = new Portfolio
            {
                Id = 1,
                Name = "Strategic Investment Open Portfolio",
                Notes = "A portfolio is a grouping of financial assets such as stocks," +
                "bonds and cash equivalents, as well as their funds counterparts, " +
                "including mutual, exchange-traded and closed funds. Portfolios are held" +
                "directly by investors and/or managed by financial professionals. ",
                DisplayIndex = 1,
                LastUpdateDate = new DateTime(2017, 4, 28),
                Visibility = false,
                Quantity = 2,
                PercentWins = 73.23m,
                BiggestWinner = 234.32m,
                BiggestLoser = 12.65m,
                AvgGain = 186.65m,
                MonthAvgGain = 99.436m,
                PortfolioValue = 1532.42m,
                Positions = new List<Position> { position1, position2 }
            };

            Portfolio portfolio2 = new Portfolio
            {
                Id = 2,
                Name = "Strategic Investment Income Portfolio",
                Notes = "A portfolio is a grouping of financial assets such as stocks," +
                "bonds and cash equivalents, as well as their funds counterparts, " +
                "including mutual, exchange-traded and closed funds. Portfolios are held" +
                "directly by investors and/or managed by financial professionals. ",
                DisplayIndex = 2,
                LastUpdateDate = new DateTime(2017, 3, 12),
                Visibility = true,
                Quantity = 3,
                PercentWins = 93.23m,
                BiggestWinner = 534.32m,
                BiggestLoser = 123.46m,
                AvgGain = 316.65m,
                MonthAvgGain = 341.436m,
                PortfolioValue = 5532.42m,
                Positions = new List<Position> { position3, position4, position5 }
            };
            Portfolio portfolio3 = new Portfolio
            {
                Id = 3,
                Name = "HDFC Bank",
                DisplayIndex = 4,
                LastUpdateDate = new DateTime(2017, 3, 12),
                Visibility = true,
                Quantity = 3,
                PercentWins = 93.23m,
                BiggestWinner = 534.32m,
                BiggestLoser = 123.46m,
                AvgGain = 316.65m,
                MonthAvgGain = 341.436m,
                PortfolioValue = 5532.42m,
            };
            Portfolio portfolio4 = new Portfolio
            {
                Id = 4,
                Name = "IndusInd Bank",
                DisplayIndex = 5,
                LastUpdateDate = new DateTime(2017, 3, 12),
                Visibility = true,
                Quantity = 3,
                PercentWins = 93.23m,
                BiggestWinner = 534.32m,
                BiggestLoser = 123.46m,
                AvgGain = 316.65m,
                MonthAvgGain = 341.436m,
                PortfolioValue = 5532.42m,
            };
            Portfolio portfolio5 = new Portfolio
            {
                Id = 5,
                Name = "UltraTechCement",
                DisplayIndex = 3,
                LastUpdateDate = new DateTime(2017, 3, 12),
                Visibility = true,
                Quantity = 3,
                PercentWins = 93.23m,
                BiggestWinner = 534.32m,
                BiggestLoser = 123.46m,
                AvgGain = 316.65m,
                MonthAvgGain = 341.436m,
                PortfolioValue = 5532.42m,
            };

            db.Portfolios.Add(portfolio1);
            db.Portfolios.Add(portfolio2);
            db.Portfolios.Add(portfolio3);
            db.Portfolios.Add(portfolio4);
            db.Portfolios.Add(portfolio5);
            #endregion



            db.SaveChanges();
        }
    }
}
