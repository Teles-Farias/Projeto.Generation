using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using blogpessoal.Model;

namespace blogpessoal.Service
{
    public interface IAlunoService
    {
         Task<IEnumerable<Aluno>> GetAll();

        Task<Aluno?> GetById(long id);

        Task<IEnumerable<Aluno>> GetByNome(string nome);

        Task<Aluno?> Create(Aluno aluno);

        Task<Aluno?> Update(Aluno aluno);

        Task Delete(Aluno aluno);
    }
}