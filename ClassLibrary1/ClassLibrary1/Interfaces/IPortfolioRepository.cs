﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IPortfolioRepository : IRepository<Portfolio>
    {
        void AddPositionToPortfolio(Position position, int portfolioId);
        void ChangePortfolioDisplayIndex(int id, int displayIndex);
        bool IsExist(int id);
        void UpdatePortfolioNameAndNotes(Portfolio portfolio);
        void RecalculatePortfolioValue(int id);
        IQueryable<Portfolio> GetPortfolioQuery(int id);
    }
}
