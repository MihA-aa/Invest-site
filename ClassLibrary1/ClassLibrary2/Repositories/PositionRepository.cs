using ClassLibrary1.Entities;
using ClassLibrary1.Interfaces;
using ClassLibrary2.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace ClassLibrary2.Repositories
{
    public class PositionRepository : GenericRepository<Position>
    {
        public PositionRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
