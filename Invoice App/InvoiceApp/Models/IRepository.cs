using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WasmahTask.Models
{
    public interface IRepository<T>
    {
        T Get(int Id);
        T Add(T Item);
        T Update(T Item);
        T Delete(int Id);
    }
}
