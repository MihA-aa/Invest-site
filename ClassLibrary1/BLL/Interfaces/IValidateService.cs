﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO;
using DAL.Entities;

namespace BLL.Interfaces
{
    public interface IValidateService
    {
        void Validate(PositionDTO position);
        void Validate(UserDTO userDto);
        void Validate(ViewDTO view);
        void ValidateOnlyLogin(UserDTO userDto);
    }
}
