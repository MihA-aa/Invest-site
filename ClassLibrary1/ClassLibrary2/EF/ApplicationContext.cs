using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using DAL.Entities;
using DAL.Enums;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DALEF.EF
{
    public class ApplicationContext : IdentityDbContext<User>, IDbContextFactory<ApplicationContext>
    {
        public ApplicationContext(/*string connectionString*/) : base(/*connectionString*/"DefaultConnection") { }
        static ApplicationContext()
        {
            Database.SetInitializer(new StoreDbInitializer());
        }

        public ApplicationContext Create()
        {
            return new ApplicationContext();
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Profile> Profiles { get; set; }
        public virtual DbSet<Portfolio> Portfolios { get; set; }
        public virtual DbSet<Position> Positions { get; set; }
        public virtual DbSet<Format> Formats { get; set; }
        public virtual DbSet<ColumnFormat> ColumnFormats { get; set; }
        public virtual DbSet<ViewTemplate> ViewTemplates { get; set; }
        public virtual DbSet<View> Views { get; set; }
        public virtual DbSet<ViewTemplateColumn> ViewTemplateColumns { get; set; }
        public virtual DbSet<Column> Columns { get; set; }
    }

    public class StoreDbInitializer : DropCreateDatabaseIfModelChanges<ApplicationContext>
    {
        protected override void Seed(ApplicationContext db)
        {
            #region User Inizialize
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
                CurrentPrice = 99.53m,
                Gain = 87.12m,
                AbsoluteGain = 110.34m,
                MaxGain = 154.34m,
                LastUpdateDate = new DateTime(2016, 1, 1),
                LastUpdatePrice = 218.32m
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
                CurrentPrice = 3.64m,
                Gain = 40.0m,
                AbsoluteGain = 1.60m,
                MaxGain = 1.60m,
                LastUpdateDate = new DateTime(2016, 1, 1),
                LastUpdatePrice = 218.32m
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
                Gain = 11.56m,
                AbsoluteGain = 9.45m,
                MaxGain = 14.34m,
                LastUpdateDate = new DateTime(2016, 5, 1),
                LastUpdatePrice = 53.32m
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
                Gain = 3.84m,
                AbsoluteGain = 3.65m,
                MaxGain = 3.65m,
                LastUpdateDate = new DateTime(2011, 10, 11),
                LastUpdatePrice = 53.32m
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

            var position10 = (Position)position1.Clone();
            var position11 = (Position)position1.Clone();
            var position12 = (Position)position1.Clone();
            var position13 = (Position)position1.Clone();
            var position14 = (Position)position1.Clone();
            var position15 = (Position)position1.Clone();
            var position16 = (Position)position1.Clone();
            var position17 = (Position)position1.Clone();
            var position18 = (Position)position1.Clone();
            var position19 = (Position)position1.Clone();

            var position21 = (Position)position2.Clone();
            var position20 = (Position)position2.Clone();
            var position22 = (Position)position2.Clone();
            var position23 = (Position)position2.Clone();
            var position24 = (Position)position2.Clone();
            var position25 = (Position)position2.Clone();
            var position26 = (Position)position2.Clone();
            var position27 = (Position)position2.Clone();
            var position28 = (Position)position2.Clone();
            var position29 = (Position)position2.Clone();

            db.Positions.Add(position1);
            db.Positions.Add(position10);
            db.Positions.Add(position11);
            db.Positions.Add(position12);
            db.Positions.Add(position13);
            db.Positions.Add(position14);
            db.Positions.Add(position15);
            db.Positions.Add(position16);
            db.Positions.Add(position17);
            db.Positions.Add(position18);
            db.Positions.Add(position19);
            db.Positions.Add(position20);
            db.Positions.Add(position21);
            db.Positions.Add(position22);
            db.Positions.Add(position23);
            db.Positions.Add(position24);
            db.Positions.Add(position25);
            db.Positions.Add(position26);
            db.Positions.Add(position27);
            db.Positions.Add(position28);
            db.Positions.Add(position29);
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
                Positions = new List<Position>
                {
                    position1, position10, position11, position12,
                    position13, position14, position15, position16,
                    position17, position18, position19, position2,
                    position20, position21, position22,
                    position23, position24, position25, position26,
                    position27, position28, position29,
                }
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

            #region ColumnFormat Inizialize
            ColumnFormat None = new ColumnFormat
            {
                Id = 1,
                Name = "None"
            };
            
            ColumnFormat Money = new ColumnFormat
            {
                Id = 2,
                Name = "Money"
            };
            ColumnFormat Linked = new ColumnFormat
            {
                Id = 3,
                Name = "Linked"
            };
            ColumnFormat Date = new ColumnFormat
            {
                Id = 4,
                Name = "Date"
            };
            ColumnFormat LinkedCloseDate = new ColumnFormat
            {
                Id = 5,
                Name = "LinkedCloseDate"
            };
            ColumnFormat DateAndTime = new ColumnFormat
            {
                Id = 6,
                Name = "DateAndTime"
            };
            ColumnFormat Percent = new ColumnFormat
            {
                Id = 7,
                Name = "Percent"
            };
            
            db.ColumnFormats.Add(None);
            db.ColumnFormats.Add(Money);
            db.ColumnFormats.Add(Linked);
            db.ColumnFormats.Add(Date);
            db.ColumnFormats.Add(LinkedCloseDate);
            db.ColumnFormats.Add(DateAndTime);
            db.ColumnFormats.Add(Percent);

            #endregion

            #region Formats Inizialize

            Format DateFormat = new Format
            {
                Id = 1,
                Name = "Date Format",
                ColumnFormats = new List<ColumnFormat> { Date, Linked, DateAndTime }
            };
            Format MoneyFormat = new Format
            {
                Id = 2,
                Name = "Money Format",
                ColumnFormats = new List<ColumnFormat> { None, Money, Linked }
            };
            Format PercentFormat = new Format
            {
                Id = 3,
                Name = "Percent Format",
                ColumnFormats = new List<ColumnFormat> { None, Percent }
            };
            Format NoneFormat = new Format
            {
                Id = 4,
                Name = "None Format",
                ColumnFormats = new List<ColumnFormat> { None }
            };
            Format LineFormat = new Format
            {
                Id = 5,
                Name = "Line Format",
                ColumnFormats = new List<ColumnFormat> { None, Linked }
            };

            db.Formats.Add(DateFormat);
            db.Formats.Add(MoneyFormat);
            db.Formats.Add(PercentFormat);
            db.Formats.Add(NoneFormat);
            db.Formats.Add(LineFormat);
            #endregion

            #region Columns Inizialize

            Column Name = new Column
            {
                Id = 1,
                Name = "Name",
                Format = LineFormat
            };
            Column SymbolName = new Column
            {
                Id = 2,
                Name = "Symbol Name",
                Format = LineFormat
            };
            Column OpenPrice = new Column
            {
                Id = 3,
                Name = "Open Price",
                Format = MoneyFormat
            };
            Column OpenDate = new Column
            {
                Id = 4,
                Name = "Open Date",
                Format = DateFormat
            };
            Column OpenWeight = new Column
            {
                Id = 5,
                Name = "Open Weight",
                Format = NoneFormat
            };
            Column CurrentPrice = new Column
            {
                Id = 6,
                Name = "Current Price",
                Format = MoneyFormat
            };
            Column ClosePrice = new Column
            {
                Id = 7,
                Name = "Close Price",
                Format = MoneyFormat
            };
            Column CloseDate = new Column
            {
                Id = 8,
                Name = "Close Date",
                Format = DateFormat
            };
            Column TradeType = new Column
            {
                Id = 9,
                Name = "Trade Type",
                Format = NoneFormat
            };
            Column TradeStatus = new Column
            {
                Id = 10,
                Name = "Trade Status",
                Format = NoneFormat
            };
            Column Dividends = new Column
            {
                Id = 11,
                Name = "Dividends",
                Format = MoneyFormat
            };
            Column Gain = new Column
            {
                Id = 12,
                Name = "Gain",
                Format = MoneyFormat
            };
            Column AbsoluteGain = new Column
            {
                Id = 13,
                Name = "Absolute Gain",
                Format = PercentFormat
            };
            Column MaxGain = new Column
            {
                Id = 14,
                Name = "Max Gain",
                Format = MoneyFormat
            };
            db.Columns.Add(Name);
            db.Columns.Add(SymbolName);
            db.Columns.Add(OpenPrice);
            db.Columns.Add(OpenDate);
            db.Columns.Add(OpenWeight);
            db.Columns.Add(CurrentPrice);
            db.Columns.Add(ClosePrice);
            db.Columns.Add(CloseDate);
            db.Columns.Add(TradeType);
            db.Columns.Add(TradeStatus);
            db.Columns.Add(Dividends);
            db.Columns.Add(Gain);
            db.Columns.Add(AbsoluteGain);
            db.Columns.Add(MaxGain);
            #endregion

            #region ViewTemplateColumns Inizialize
            ViewTemplateColumn viewTemplateColumn1 = new ViewTemplateColumn
            {
                Id = 1,
                Name = "Name",
                Column = Name,
                ViewTemplateId = 1,
                DisplayIndex = 1
            };
            ViewTemplateColumn viewTemplateColumn2 = new ViewTemplateColumn
            {
                Id = 2,
                Name = "Symbol",
                Column = SymbolName,
                ViewTemplateId = 1,
                DisplayIndex = 2
            };
            ViewTemplateColumn viewTemplateColumn3 = new ViewTemplateColumn
            {
                Id = 4,
                Name = "Open Price",
                Column = OpenPrice,
                ViewTemplateId = 1,
                DisplayIndex = 4
            };
            ViewTemplateColumn viewTemplateColumn4 = new ViewTemplateColumn
            {
                Id = 5,
                Name = "Open Date",
                Column = OpenDate,
                ViewTemplateId = 1,
                DisplayIndex = 5
            };
            ViewTemplateColumn viewTemplateColumn5 = new ViewTemplateColumn
            {
                Id = 3,
                Name = "Weight",
                Column = OpenWeight,
                ViewTemplateId = 1,
                DisplayIndex = 3
            };
            ViewTemplateColumn viewTemplateColumn6 = new ViewTemplateColumn
            {
                Id = 6,
                Name = "Current Price",
                Column = CurrentPrice,
                ViewTemplateId = 1,
                DisplayIndex = 6
            };
            ViewTemplateColumn viewTemplateColumn7 = new ViewTemplateColumn
            {
                Id = 7,
                Name = "Close Price",
                Column = ClosePrice,
                ViewTemplateId = 1,
                DisplayIndex = 7
            };
            ViewTemplateColumn viewTemplateColumn8 = new ViewTemplateColumn
            {
                Id = 8,
                Name = "Close Date",
                Column = CloseDate,
                ViewTemplateId = 1,
                DisplayIndex = 8
            };
            ViewTemplateColumn viewTemplateColumn9 = new ViewTemplateColumn
            {
                Id = 9,
                Name = "Trade Type",
                Column = TradeType,
                ViewTemplateId = 1,
                DisplayIndex = 9
            };
            ViewTemplateColumn viewTemplateColumn10 = new ViewTemplateColumn
            {
                Id = 10,
                Name = "Trade Status",
                Column = TradeStatus,
                ViewTemplateId = 1,
                DisplayIndex = 10
            };
            ViewTemplateColumn viewTemplateColumn11 = new ViewTemplateColumn
            {
                Id = 11,
                Name = "Dividends",
                Column = Dividends,
                ViewTemplateId = 1,
                DisplayIndex = 11
            };
            ViewTemplateColumn viewTemplateColumn12 = new ViewTemplateColumn
            {
                Id = 12,
                Name = "Absolute Gain",
                Column = AbsoluteGain,
                ViewTemplateId = 1,
                DisplayIndex = 12
            };
            ViewTemplateColumn viewTemplateColumn13 = new ViewTemplateColumn
            {
                Id = 13,
                Name = "Max Gain",
                Column = MaxGain,
                ViewTemplateId = 1,
                DisplayIndex = 13
            };
            ViewTemplateColumn viewTemplateColumn14 = new ViewTemplateColumn
            {
                Id = 14,
                Name = "Gain",
                Column = Gain,
                ViewTemplateId = 1,
                DisplayIndex = 14
            };
            ViewTemplateColumn viewTemplateColumn21 = new ViewTemplateColumn
            {
                Id = 15,
                Name = "Name",
                Column = Name,
                ViewTemplateId = 2,
                DisplayIndex = 1
            };
            db.ViewTemplateColumns.Add(viewTemplateColumn1);
            db.ViewTemplateColumns.Add(viewTemplateColumn2);
            db.ViewTemplateColumns.Add(viewTemplateColumn3);
            db.ViewTemplateColumns.Add(viewTemplateColumn4);
            db.ViewTemplateColumns.Add(viewTemplateColumn5);
            db.ViewTemplateColumns.Add(viewTemplateColumn6);
            db.ViewTemplateColumns.Add(viewTemplateColumn1);
            db.ViewTemplateColumns.Add(viewTemplateColumn7);
            db.ViewTemplateColumns.Add(viewTemplateColumn8);
            db.ViewTemplateColumns.Add(viewTemplateColumn9);
            db.ViewTemplateColumns.Add(viewTemplateColumn10);
            db.ViewTemplateColumns.Add(viewTemplateColumn11);
            db.ViewTemplateColumns.Add(viewTemplateColumn12);
            db.ViewTemplateColumns.Add(viewTemplateColumn13);
            db.ViewTemplateColumns.Add(viewTemplateColumn14);
            db.ViewTemplateColumns.Add(viewTemplateColumn21);
            #endregion

            #region ViewTemplate Inizialize
            ViewTemplate viewTemplate1 = new ViewTemplate
            {
                Id = 1,
                Name = "Preview all",
                Positions = TemplatePositions.All,
                ShowPortfolioStats = true,
                SortOrder = Sorting.ASC,
                Columns = new List<ViewTemplateColumn>
                {
                    viewTemplateColumn1, viewTemplateColumn2, viewTemplateColumn3, viewTemplateColumn4,
                    viewTemplateColumn5, viewTemplateColumn6, viewTemplateColumn6, viewTemplateColumn7,
                    viewTemplateColumn8, viewTemplateColumn9, viewTemplateColumn10, viewTemplateColumn11,
                    viewTemplateColumn12, viewTemplateColumn13, viewTemplateColumn13, viewTemplateColumn14
                }
            };

            ViewTemplate viewTemplate2 = new ViewTemplate
            {
                Id = 2,
                Name = "Default",
                Positions = TemplatePositions.OpenOnly,
                ShowPortfolioStats = false,
                SortOrder = Sorting.DESC,
                Columns = new List<ViewTemplateColumn>
                {viewTemplateColumn21}
            };

            db.ViewTemplates.Add(viewTemplate1);
            db.ViewTemplates.Add(viewTemplate2);
            #endregion
            
            #region View Inizialize

            View previewAllView = new View
            {
                Id = 1,
                Name = "Preview All View",
                ShowName = true,
                DateFormat = DateFormats.MonthDayYear,
                MoneyPrecision = 2,
                PercentyPrecision = 4,
                ViewTemplate = viewTemplate1,
                Portfolio = portfolio1
            };

            View defaultView = new View
            {
                Id = 2,
                Name = "Default View",
                ShowName = false,
                DateFormat = DateFormats.DayMonthNameYear,
                MoneyPrecision = 1,
                PercentyPrecision = 2,
                ViewTemplate = viewTemplate2,
                Portfolio = portfolio2
            };

            db.Views.Add(previewAllView);
            db.Views.Add(defaultView);
            #endregion
            try
            {
                db.SaveChanges();
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateConcurrencyException ex)
            {
                Console.WriteLine(ex.InnerException);
            }
            catch (System.Data.Entity.Core.EntityCommandCompilationException ex)
            {
                Console.WriteLine(ex.InnerException);
            }
            catch (System.Data.Entity.Core.UpdateException ex)
            {
                Console.WriteLine(ex.InnerException);
            }

            catch (System.Data.Entity.Infrastructure.DbUpdateException ex) //DbContext
            {
                Console.WriteLine(ex.InnerException);
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
                throw;
            }





        }
    }
}
