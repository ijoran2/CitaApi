using CitaAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CitaAPI.Repositories
{
    public interface ICitasRepository
    {
        Task<IEnumerable<Citas>> Get();
        Task<Citas> Get(int Id);
        Task<Citas> Create(Citas Cita);
        Task Update(Citas Cita);
        Task Delete(int Id);
    }
}
