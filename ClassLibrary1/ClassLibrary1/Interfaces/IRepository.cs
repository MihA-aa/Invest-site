using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll<T>();
        T Get(int id);
        IEnumerable<T> Find<T>(Func<T, Boolean> predicate);
        void Create<T>(T item);
        void Update<T>(T item);
        void Delete(int id);
    }
}
