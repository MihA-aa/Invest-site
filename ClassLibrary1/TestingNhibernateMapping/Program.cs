using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;

namespace TestingNhibernateMapping
{
    class Program
    {
        static void Main(string[] args)
        {
            ISession Session = NHibernateSessionFactory.getSession("NewMyDB");
            StoreDbInitializer.Inizialize(Session);
        }
    }
}
