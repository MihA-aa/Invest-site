﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IPositionRepository : IRepository<Position>
    {
        bool CheckIfPositionExists(int id);
        void ChangePositionParams(Position position);
    }
}
