using System;
using System.Collections.Generic;
using DAL.ApplicationManager;
using DAL.Entities;
using DAL.Enums;
using Microsoft.AspNet.Identity;
using NHibernate;
using NHibernate.AspNet.Identity;

namespace DALEF.EF
{
    public static class StoreDbInitializer
    {
        public static void Inizialize(ISession Session)
        {
            #region Customer Inizialize
            Customer WallStreetDaily = new Customer
            {
                Id = 1,
                Name = "Wall Street Daily"
            };
            Customer FleetStreetPublication = new Customer { Id = 2, Name = "Fleet Street Publication" };
            Customer DailyEdge = new Customer { Id = 3, Name = "Daily Edge" };
            Customer HeidiShubert = new Customer { Id = 6, Name = "Heidi Shubert" };
            Customer WeissResearch = new Customer { Id = 4, Name = "Weiss Research" };
            Customer WSD = new Customer { Id = 5, Name = "WSD Custom Strategy" };

            Session.Save(DailyEdge);
            Session.Save(HeidiShubert);
            Session.Save(WallStreetDaily);
            Session.Save(WeissResearch);
            Session.Save(WSD);
            Session.Save(FleetStreetPublication);
            #endregion

            #region Portfolios Inizialize
            Portfolio portfolio1 = new Portfolio
            {
                Id = 1,
                Name = "Strategic Investment Open Portfolio",
                Notes = "A portfolio is a grouping of financial assets such as stocks,",
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
                Customer = WallStreetDaily
            };

            Portfolio portfolio2 = new Portfolio
            {
                Id = 2,
                Name = "Strategic Investment Income Portfolio",
                Notes = "A portfolio is a grouping of financial assets such as stocks,",
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
                Customer = WallStreetDaily
                //Positions = new List<Position> { position3, position4, position5 }
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
                Customer = WallStreetDaily
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
                Customer = FleetStreetPublication
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
                Customer = FleetStreetPublication
            };

            Session.Save(portfolio1);
            Session.Save(portfolio2);
            Session.Save(portfolio3);
            Session.Save(portfolio4);
            Session.Save(portfolio5);
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
                LastUpdatePrice = 218.32m,
                Portfolio = portfolio1
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
                LastUpdatePrice = 218.32m,
                Portfolio = portfolio1
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
                LastUpdatePrice = 53.32m,
                Portfolio = portfolio2
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
                LastUpdatePrice = 53.32m,
                Portfolio = portfolio2
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
                MaxGain = 13.34m,
                Portfolio = portfolio2
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

            Session.Save(position1);
            Session.Save(position10);
            Session.Save(position11);
            Session.Save(position12);
            Session.Save(position13);
            Session.Save(position14);
            Session.Save(position15);
            Session.Save(position16);
            Session.Save(position17);
            Session.Save(position18);
            Session.Save(position19);
            Session.Save(position20);
            Session.Save(position21);
            Session.Save(position22);
            Session.Save(position23);
            Session.Save(position24);
            Session.Save(position25);
            Session.Save(position26);
            Session.Save(position27);
            Session.Save(position28);
            Session.Save(position29);
            Session.Save(position2);
            Session.Save(position3);
            Session.Save(position4);
            Session.Save(position5);
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

            Session.Save(None);
            Session.Save(Money);
            Session.Save(Linked);
            Session.Save(Date);
            Session.Save(DateAndTime);
            Session.Save(Percent);

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

            Session.Save(DateFormat);
            Session.Save(MoneyFormat);
            Session.Save(PercentFormat);
            Session.Save(NoneFormat);
            Session.Save(LineFormat);
            #endregion

            #region ViewTemplate Inizialize
            ViewTemplate viewTemplate1 = new ViewTemplate
            {
                Id = 1,
                Name = "Preview all",
                Positions = TemplatePositions.All,
                ShowPortfolioStats = true,
                SortOrder = Sorting.ASC,
                Customer = WallStreetDaily
            };

            ViewTemplate viewTemplate2 = new ViewTemplate
            {
                Id = 2,
                Name = "Default",
                Positions = TemplatePositions.OpenOnly,
                ShowPortfolioStats = false,
                SortOrder = Sorting.DESC,
                Customer = WallStreetDaily
            };

            Session.Save(viewTemplate1);
            Session.Save(viewTemplate2);
            #endregion
            
            #region Columns Inizialize

            Column Name = new Column
            {
                Id = 1,
                Name = "Name",
                Format = LineFormat,
                //ViewTemplateColumns = new List<ViewTemplateColumn> { viewTemplateColumn1, viewTemplateColumn21 }
            };
            Column SymbolName = new Column
            {
                Id = 2,
                Name = "Symbol Name",
                Format = LineFormat,
                //ViewTemplateColumns = new List<ViewTemplateColumn> { viewTemplateColumn2 }
            };
            Column OpenPrice = new Column
            {
                Id = 3,
                Name = "Open Price",
                Format = MoneyFormat,
                //ViewTemplateColumns = new List<ViewTemplateColumn> { viewTemplateColumn3 }
            };
            Column OpenDate = new Column
            {
                Id = 4,
                Name = "Open Date",
                Format = DateFormat,
                //ViewTemplateColumns = new List<ViewTemplateColumn> { viewTemplateColumn4 }
            };
            Column OpenWeight = new Column
            {
                Id = 5,
                Name = "Open Weight",
                Format = NoneFormat,
                //ViewTemplateColumns = new List<ViewTemplateColumn> { viewTemplateColumn5 }
            };
            Column CurrentPrice = new Column
            {
                Id = 6,
                Name = "Current Price",
                Format = MoneyFormat,
                //ViewTemplateColumns = new List<ViewTemplateColumn> { viewTemplateColumn6 }
            };
            Column ClosePrice = new Column
            {
                Id = 7,
                Name = "Close Price",
                Format = MoneyFormat,
               // ViewTemplateColumns = new List<ViewTemplateColumn> { viewTemplateColumn7 }
            };
            Column CloseDate = new Column
            {
                Id = 8,
                Name = "Close Date",
                Format = DateFormat,
                //ViewTemplateColumns = new List<ViewTemplateColumn> { viewTemplateColumn8 }
            };
            Column TradeType = new Column
            {
                Id = 9,
                Name = "Trade Type",
                Format = NoneFormat,
               // ViewTemplateColumns = new List<ViewTemplateColumn> { viewTemplateColumn9 }
            };
            Column TradeStatus = new Column
            {
                Id = 10,
                Name = "Trade Status",
                Format = NoneFormat,
                //ViewTemplateColumns = new List<ViewTemplateColumn> { viewTemplateColumn10 }
            };
            Column Dividends = new Column
            {
                Id = 11,
                Name = "Dividends",
                Format = MoneyFormat,
                //ViewTemplateColumns = new List<ViewTemplateColumn> { viewTemplateColumn11 }
            };
            Column Gain = new Column
            {
                Id = 12,
                Name = "Gain",
                Format = MoneyFormat,
                //ViewTemplateColumns = new List<ViewTemplateColumn> { viewTemplateColumn12 }
            };
            Column AbsoluteGain = new Column
            {
                Id = 13,
                Name = "Absolute Gain",
                Format = PercentFormat,
                //ViewTemplateColumns = new List<ViewTemplateColumn> { viewTemplateColumn13 }
            };
            Column MaxGain = new Column
            {
                Id = 14,
                Name = "Max Gain",
                Format = MoneyFormat,
                //ViewTemplateColumns = new List<ViewTemplateColumn> { viewTemplateColumn14 }
            };
            Column LastUpdateDate = new Column
            {
                Id = 15,
                Name = "Last Update Date",
                Format = DateFormat,
                //ViewTemplateColumns = new List<ViewTemplateColumn> { viewTemplateColumn15 }
            };

            Column LastUpdatePrice = new Column
            {
                Id = 16,
                Name = "Last Update Price",
                Format = MoneyFormat,
                //ViewTemplateColumns = new List<ViewTemplateColumn> { viewTemplateColumn16 }
            };

            Session.Save(Name);
            Session.Save(SymbolName);
            Session.Save(OpenPrice);
            Session.Save(OpenDate);
            Session.Save(OpenWeight);
            Session.Save(CurrentPrice);
            Session.Save(ClosePrice);
            Session.Save(CloseDate);
            Session.Save(TradeType);
            Session.Save(TradeStatus);
            Session.Save(Dividends);
            Session.Save(Gain);
            Session.Save(AbsoluteGain);
            Session.Save(MaxGain);
            Session.Save(LastUpdateDate);
            Session.Save(LastUpdatePrice);
            #endregion
            
            #region ViewTemplateColumns Inizialize
            ViewTemplateColumn viewTemplateColumn1 = new ViewTemplateColumn
            {
                Id = 1,
                Name = "Name",
                ColumnEntiy = Name,
                ViewTemplateId = 1,
                DisplayIndex = 1,
                ColumnFormat = Linked,
                ColumnId = 1,
                ColumnFormatId = 3,
                ViewTemplate = viewTemplate1,
                //ViewTemplatesForSorting = new List<ViewTemplate> { viewTemplate1 }
            };
            ViewTemplateColumn viewTemplateColumn2 = new ViewTemplateColumn
            {
                Id = 2,
                Name = "Symbol",
                ColumnEntiy = SymbolName,
                ViewTemplateId = 1,
                DisplayIndex = 2,
                ColumnFormat = None,
                ColumnId = 2,
                ColumnFormatId = 1,
                ViewTemplate = viewTemplate1
            };
            ViewTemplateColumn viewTemplateColumn3 = new ViewTemplateColumn
            {
                Id = 4,
                Name = "Open Price",
                ColumnEntiy = OpenPrice,
                ViewTemplateId = 1,
                DisplayIndex = 4,
                ColumnFormat = Money,
                ColumnId = 3,
                ColumnFormatId = 2,
                ViewTemplate = viewTemplate1
            };
            ViewTemplateColumn viewTemplateColumn4 = new ViewTemplateColumn
            {
                Id = 5,
                Name = "Open Date",
                ColumnEntiy = OpenDate,
                ViewTemplateId = 1,
                DisplayIndex = 5,
                ColumnFormat = Date,
                ColumnId = 4,
                ColumnFormatId = 4,
                ViewTemplate = viewTemplate1
            };
            ViewTemplateColumn viewTemplateColumn5 = new ViewTemplateColumn
            {
                Id = 3,
                Name = "Weight",
                ColumnEntiy = OpenWeight,
                ViewTemplateId = 1,
                DisplayIndex = 3,
                ColumnFormat = None,
                ColumnId = 5,
                ColumnFormatId = 1,
                ViewTemplate = viewTemplate1
            };
            ViewTemplateColumn viewTemplateColumn6 = new ViewTemplateColumn
            {
                Id = 6,
                Name = "Current Price",
                ColumnEntiy = CurrentPrice,
                ViewTemplateId = 1,
                DisplayIndex = 6,
                ColumnFormat = Linked,
                ColumnId = 6,
                ColumnFormatId = 3,
                ViewTemplate = viewTemplate1
            };
            ViewTemplateColumn viewTemplateColumn7 = new ViewTemplateColumn
            {
                Id = 7,
                Name = "Close Price",
                ColumnEntiy = ClosePrice,
                ViewTemplateId = 1,
                DisplayIndex = 7,
                ColumnFormat = None,
                ColumnId = 7,
                ColumnFormatId = 1,
                ViewTemplate = viewTemplate1
            };
            ViewTemplateColumn viewTemplateColumn8 = new ViewTemplateColumn
            {
                Id = 8,
                Name = "Close Date",
                ColumnEntiy = CloseDate,
                ViewTemplateId = 1,
                DisplayIndex = 8,
                ColumnFormat = DateAndTime,
                ColumnId = 8,
                ColumnFormatId = 6,
                ViewTemplate = viewTemplate1
            };
            ViewTemplateColumn viewTemplateColumn9 = new ViewTemplateColumn
            {
                Id = 9,
                Name = "Trade Type",
                ColumnEntiy = TradeType,
                ViewTemplateId = 1,
                DisplayIndex = 9,
                ColumnFormat = None,
                ColumnId = 9,
                ColumnFormatId = 1,
                ViewTemplate = viewTemplate1
            };
            ViewTemplateColumn viewTemplateColumn10 = new ViewTemplateColumn
            {
                Id = 10,
                Name = "Trade Status",
                ColumnEntiy = TradeStatus,
                ViewTemplateId = 1,
                DisplayIndex = 10,
                ColumnFormat = None,
                ColumnId = 10,
                ColumnFormatId = 1,
                ViewTemplate = viewTemplate1
            };
            ViewTemplateColumn viewTemplateColumn11 = new ViewTemplateColumn
            {
                Id = 11,
                Name = "Dividends",
                ColumnEntiy = Dividends,
                ViewTemplateId = 1,
                DisplayIndex = 11,
                ColumnFormat = Money,
                ColumnId = 11,
                ColumnFormatId = 2,
                ViewTemplate = viewTemplate1
            };
            ViewTemplateColumn viewTemplateColumn12 = new ViewTemplateColumn
            {
                Id = 12,
                Name = "Absolute Gain",
                ColumnEntiy = AbsoluteGain,
                ViewTemplateId = 1,
                DisplayIndex = 12,
                ColumnFormat = Percent,
                ColumnId = 13,
                ColumnFormatId = 7,
                ViewTemplate = viewTemplate1
            };
            ViewTemplateColumn viewTemplateColumn13 = new ViewTemplateColumn
            {
                Id = 13,
                Name = "Max Gain",
                ColumnEntiy = MaxGain,
                ViewTemplateId = 1,
                DisplayIndex = 13,
                ColumnFormat = Money,
                ColumnId = 14,
                ColumnFormatId = 2,
                ViewTemplate = viewTemplate1
            };
            ViewTemplateColumn viewTemplateColumn14 = new ViewTemplateColumn
            {
                Id = 14,
                Name = "Gain",
                ColumnEntiy = Gain,
                ViewTemplateId = 1,
                DisplayIndex = 14,
                ColumnFormat = Linked,
                ColumnId = 12,
                ColumnFormatId = 3,
                ViewTemplate = viewTemplate1
            };
            ViewTemplateColumn viewTemplateColumn15 = new ViewTemplateColumn
            {
                Id = 15,
                Name = "Last Update Date",
                ColumnEntiy = LastUpdateDate,
                ViewTemplateId = 1,
                DisplayIndex = 15,
                ColumnFormat = DateAndTime,
                ColumnId = 15,
                ColumnFormatId = 6,
                ViewTemplate = viewTemplate1
            };

            ViewTemplateColumn viewTemplateColumn16 = new ViewTemplateColumn
            {
                Id = 16,
                Name = "Last Update Price",
                ColumnEntiy = LastUpdatePrice,
                ViewTemplateId = 1,
                DisplayIndex = 16,
                ColumnFormat = Linked,
                ColumnId = 16,
                ColumnFormatId = 2,
                ViewTemplate = viewTemplate1
            };
            ViewTemplateColumn viewTemplateColumn21 = new ViewTemplateColumn
            {
                Id = 17,
                Name = "Name",
                ColumnEntiy = Name,
                ViewTemplateId = 2,
                DisplayIndex = 1,
                ColumnFormat = None,
                ColumnId = 1,
                ColumnFormatId = 1,
                ViewTemplate = viewTemplate2,
                //ViewTemplatesForSorting = new List<ViewTemplate> { viewTemplate2 }
            };
            Session.Save(viewTemplateColumn1);
            Session.Save(viewTemplateColumn2);
            Session.Save(viewTemplateColumn3);
            Session.Save(viewTemplateColumn4);
            Session.Save(viewTemplateColumn5);
            Session.Save(viewTemplateColumn6);
            Session.Save(viewTemplateColumn1);
            Session.Save(viewTemplateColumn7);
            Session.Save(viewTemplateColumn8);
            Session.Save(viewTemplateColumn9);
            Session.Save(viewTemplateColumn10);
            Session.Save(viewTemplateColumn11);
            Session.Save(viewTemplateColumn12);
            Session.Save(viewTemplateColumn13);
            Session.Save(viewTemplateColumn14);
            Session.Save(viewTemplateColumn15);
            Session.Save(viewTemplateColumn16);
            Session.Save(viewTemplateColumn21);
            #endregion
            
            #region View Inizialize

            ViewForTable previewAllView = new ViewForTable
            {
                Id = 1,
                Name = "Preview All View",
                ShowName = true,
                DateFormat = DateFormats.MonthDayYear,
                MoneyPrecision = 2,
                PercentyPrecision = 4,
                ViewTemplate = viewTemplate1,
                ViewTemplateId = viewTemplate1.Id,
                Customer = WallStreetDaily
            };

            ViewForTable defaultView = new ViewForTable
            {
                Id = 2,
                Name = "Default View",
                ShowName = false,
                DateFormat = DateFormats.DayMonthNameYear,
                MoneyPrecision = 1,
                PercentyPrecision = 2,
                ViewTemplate = viewTemplate2,
                ViewTemplateId = viewTemplate2.Id,
                Customer = WallStreetDaily
            };

            Session.Save(previewAllView);
            Session.Save(defaultView);
            #endregion

            #region User Inizialize
            var userManager = new ApplicationUserManager(new UserStore<UserEntity>(Session));
            var roleManager = new ApplicationRoleManager(new RoleStore<Role>(Session));
            List<Role> identityRoles = new List<Role>
            {
                new Role() {Name = "Admin"},
                new Role() {Name = "User"},
                new Role() {Name = "Employee"}
            };

            foreach (Role role in identityRoles)
            {
                roleManager.Create(role);
            }
            UserEntity admin = new UserEntity { Email = "Admin", UserName = "Admin" };
            userManager.Create(admin, "Password");
            userManager.AddToRole(admin.Id, "Admin");
            userManager.AddToRole(admin.Id, "Employee");

            var clientProfile = new Profile
            {
                Id = admin.Id,
                Login = admin.UserName,
                Customer = WallStreetDaily,
                CustomerId = WallStreetDaily.Id
            };
            WallStreetDaily.Profiles.Add(clientProfile);
            #endregion

            #region Records Inizialize
            Record record1 = new Record
            {
                UserId = "1aaa023d-e950-47fc-9c3f-54fbffcc99cf",
                Entity = Entities.Position,
                Operation = Operations.Create,
                Successfully = true,
                EntityId = 1,
                DateTime = new DateTime(2017, 4, 1)
            };
            Record record2 = new Record
            {
                UserId = "1aaa023d-e950-47fc-9c3f-54fbffcc99cf",
                Entity = Entities.Position,
                Operation = Operations.Delete,
                Successfully = false,
                EntityId = 1,
                DateTime = new DateTime(2017, 4, 3)
            };
            Record record3 = new Record
            {
                UserId = "2da9e5e9-ee3e-473c-a131-c39050b26760",
                Entity = Entities.Portfolio,
                Operation = Operations.Update,
                Successfully = true,
                EntityId = 2,
                DateTime = new DateTime(2017, 4, 4)
            };
            Session.Save(record1);
            Session.Save(record2);
            Session.Save(record3);
            #endregion

            Session.Flush();

            #region View and function inizialize

            ISQLQuery createSymbolView = Session.CreateSQLQuery(
                "CREATE VIEW SymbolView AS " +
                "SELECT S.SymbolID, S.Symbol, S.Name, C.Symbol AS CurrencySymbol " +
                "FROM  HistoricalDataNew.dbo.Symbol AS S INNER JOIN " +
                "HistoricalDataNew.dbo.Currencies AS C ON S.CurrencyId = C.CurrencyId");

            ISQLQuery createSymbolDividends = Session.CreateSQLQuery(
                "CREATE VIEW SymbolDividends AS " +
                "SELECT    DividendAmount, SymbolID, TradeDate " +
                "FROM   HistoricalDataNew.dbo.Dividend");

            ISQLQuery createTradeSybolInformation = Session.CreateSQLQuery(
                "CREATE VIEW TradeSybolInformation AS " +
                "SELECT SymbolID, TradeDate, TradeIndex "+
                "FROM   HistoricalDataNew.dbo.IndexData " +
                "UNION " +
                "SELECT SymbolID, TradeDate, CAST(TradeOpen AS money) AS TradeIndex " +
                "FROM   HistoricalDataNew.dbo.StockData");

            ISQLQuery createGetPriceInDateInterval = Session.CreateSQLQuery(

                "CREATE FUNCTION[dbo].[getPriceDividendForSymbolInDateInterval](@dateFrom datetime, @dateTo datetime, @symbolId int) "+
                "RETURNS "+
                "@report TABLE(TradeDate datetime, Price MONEY, Dividends MONEY) "+
                "AS BEGIN "+
                    "INSERT INTO @report SELECT DISTINCT tr.TradeDate, tr.TradeIndex AS Price, ISNULL(( "+
                                                    "SELECT SUM(d.DividendAmount) "+

                                                    "FROM SymbolDividends d "+

                                                       "WHERE d.SymbolId = tr.SymbolId AND d.SymbolID = @symbolId AND "+

                                                       "d.TradeDate = tr.TradeDate),0) AS Dividends "+

                        "FROM TradeSybolInformation tr "+
                        "WHERE tr.SymbolID = @symbolId  AND tr.TradeDate >= @dateFrom AND tr.TradeDate <= @dateTo "+

                        "ORDER BY tr.TradeDate "+

                    "RETURN "+
                "END ");

            ISQLQuery creategetMaxMinGain = Session.CreateSQLQuery(
                "CREATE FUNCTION[dbo].[getMaxMinGainForSymbolInDateInterval](@dateFrom datetime, @dateTo datetime, @symbolId int) "+
                "RETURNS "+
                "@report TABLE(TradeDate datetime, Price MONEY, Dividends MONEY) "+
                "AS BEGIN "+
                        "DECLARE @tab TABLE(TradeDate datetime, Price MONEY, Dividends MONEY) "+
        
                        "INSERT INTO @tab SELECT *FROM getPriceDividendForSymbolInDateInterval(@dateFrom, @dateTo, @symbolId) "+
        
                        "INSERT INTO @report SELECT TOP 1 * FROM @tab "+
                        "WHERE Price + Dividends = (SELECT MAX(Price + Dividends) FROM @tab) "+

		                "INSERT INTO @report SELECT TOP 1 * FROM @tab "+
                        "WHERE Price + Dividends = (SELECT MIN(Price + Dividends) FROM @tab) "+

	                "RETURN "+
                "END");


            createSymbolView.ExecuteUpdate();
            createSymbolDividends.ExecuteUpdate();
            createTradeSybolInformation.ExecuteUpdate();
            createGetPriceInDateInterval.ExecuteUpdate();
            creategetMaxMinGain.ExecuteUpdate();

            #endregion
        }
    }
}
