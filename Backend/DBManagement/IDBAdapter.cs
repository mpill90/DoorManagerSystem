using Shared;
using System.Collections.Generic;

namespace Api.DBManagement
{
    public interface IDBAdapter
    {
        public void Open();
        public void Create(Door door);
        public IEnumerable<Door> Read();
        public Door Read(int id);
        public void Update(Door door);
        public void Delete(int id);
    }
}
