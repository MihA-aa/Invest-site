﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BLL.DTO;
using BLL.Helpers;
using BLL.Infrastructure;
using BLL.Interfaces;
using DAL.Entities;

namespace BLL.Services
{
    public class ValidateService : IValidateService
    {
        public void Validate(PositionDTO position)
        {
            if (position.OpenWeight < 1 || position.OpenWeight >10000)
                throw new ValidationException("The Weight value must be greater than 0 and less than or equal to 10,000.", HelperService.GetMemberName((PositionDTO c) => c.OpenWeight)/*"OpenWeight"*/);
        }

        public void Validate(PortfolioDTO portfolio)
        {
            if (portfolio.PercentWins < 0)
                throw new ValidationException("Percent Wins of portfolio cannot be less than zero", HelperService.GetMemberName((PortfolioDTO c) => c.PercentWins));
        }

        public void Validate(ViewDTO view)
        {
            if (view.MoneyPrecision < 0 || view.MoneyPrecision > 8)
                throw new ValidationException("The Money Precision value must be greater or equal to 0 and less than or equal to 8", HelperService.GetMemberName((ViewDTO c) => c.MoneyPrecision));
            if (view.PercentyPrecision < 0 || view.PercentyPrecision > 8)
                throw new ValidationException("The Percenty Precision value must be greater or equal to 0 and less than or equal to 8", HelperService.GetMemberName((ViewDTO c) => c.MoneyPrecision));
        }

        public void Validate(UserDTO userDto)
        {
            if (!Regex.IsMatch(userDto.Login, Resource.Resource.loginPattern))
                throw new ValidationException(Resource.Resource.LoginValidateMessage, HelperService.GetMemberName((UserDTO c) => c.Login));
            if (!Regex.IsMatch(userDto.Password, Resource.Resource.passwordPattern))
                throw new ValidationException(Resource.Resource.PasswordValidateMessage, HelperService.GetMemberName((UserDTO c) => c.Password));
        }
        public void ValidateOnlyLogin(UserDTO userDto)
        {
            if (!Regex.IsMatch(userDto.Login, Resource.Resource.loginPattern))
                throw new ValidationException(Resource.Resource.LoginValidateMessage, HelperService.GetMemberName((UserDTO c) => c.Login));
        }

    }
}
