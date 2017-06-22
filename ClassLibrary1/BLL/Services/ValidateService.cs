using System;
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
                throw new ValidationException(Resource.Resource.PositionOpenWeightValidate, HelperService.GetMemberName((PositionDTO c) => c.OpenWeight));
        }

        public void Validate(PortfolioDTO portfolio)
        {
            if (portfolio.PercentWins < 0)
                throw new ValidationException(Resource.Resource.PortfolioPercentWinsValidate, HelperService.GetMemberName((PortfolioDTO c) => c.PercentWins));
        }

        public void Validate(ViewDTO view)
        {
            var exceptionList = new List<ValidationException>();
            if (view.MoneyPrecision < 0 || view.MoneyPrecision > 8)
                exceptionList.Add(new ValidationException(Resource.Resource.MoneyPrecisionValidate, HelperService.GetMemberName((ViewDTO c) => c.MoneyPrecision)));
            if (view.PercentyPrecision < 0 || view.PercentyPrecision > 8)
                exceptionList.Add(new ValidationException(Resource.Resource.PercentyPrecisionValidate, HelperService.GetMemberName((ViewDTO c) => c.PercentyPrecision)));
            CheckForExceptions(exceptionList);
        }

        public void Validate(UserDTO userDto)
        {
            var exceptionList = new List<ValidationException>();
            if (!Regex.IsMatch(userDto.Login, Resource.Resource.loginPattern))
                exceptionList.Add(new ValidationException(Resource.Resource.LoginValidateMessage, HelperService.GetMemberName((UserDTO c) => c.Login)));
            if (!Regex.IsMatch(userDto.Password, Resource.Resource.passwordPattern))
                exceptionList.Add(new ValidationException(Resource.Resource.PasswordValidateMessage, HelperService.GetMemberName((UserDTO c) => c.Password)));
            CheckForExceptions(exceptionList);
        }

        private static void CheckForExceptions(IEnumerable<ValidationException> exceptionList)
        {
            if (exceptionList.Any())
            {
                string message = "";
                string properties = "";
                foreach (var exception in exceptionList)
                {
                    properties += exception.Property + "|";
                    message += exception.Message + "|";
                }
                throw new ValidationException(message, properties);
            }
        }

        public void ValidateOnlyLogin(UserDTO userDto)
        {
            if (!Regex.IsMatch(userDto.Login, Resource.Resource.loginPattern))
                throw new ValidationException(Resource.Resource.LoginValidateMessage, HelperService.GetMemberName((UserDTO c) => c.Login));
        }
    }
}
