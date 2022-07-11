using System.Collections.Generic;

namespace BankAPITest.Services.IRepositories
{

    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Get(int id);
        IEnumerable<TEntity> GetAll();

    }

}

