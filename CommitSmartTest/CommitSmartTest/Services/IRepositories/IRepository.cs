using System.Collections.Generic;

namespace CommitSmartTest.Services.IRepositories
{

    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Get(int id);
        IEnumerable<TEntity> GetAll();

    }

}

