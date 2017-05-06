using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO;
using BLL.Infrastructure;
using BLL.Interfaces;
using DAL.Entities;

namespace BLL.Services
{
    public class ValidateService : IValidateService
    {
        public void Validate(PositionDTO position)
        {
            if (position.OpenWeight < 0)
                throw new ValidationException("Open Weight of position cannot be less than zero", "");
        }

        public void Validate(PortfolioDTO portfolio)
        {
            if (portfolio.PercentWins < 0)
                throw new ValidationException("Percent Wins of portfolio cannot be less than zero", "");
        }


    }
}
