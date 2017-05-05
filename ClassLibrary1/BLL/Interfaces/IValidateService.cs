using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO;
using DAL.Entities;

namespace BLL.Interfaces
{
    interface IValidateService
    {
        bool IsValid(PositionDTO position);
        bool IsValid(PortfolioDTO portfolio);
    }
}
