using Microsoft.EntityFrameworkCore;
using blogpessoal.Data;
using blogpessoal.Model;

namespace blogpessoal.Service.Implements
{
    public class AlunoService : IAlunoService
    {
       private readonly AppDbContext _context;

        //construtor 
        public AlunoService(AppDbContext context)
        {
            _context = context;
        }

        //async para indicar que esse metodo vai ser preenchido na pag
        public async Task<IEnumerable<Aluno>> GetAll()
        {   
            return await _context.Alunos
                .ToListAsync();
        }

        public async Task<Aluno?> GetById(long id)
        {
            try
            {
                var aluno = await _context.Alunos
                .FirstAsync(a => a.Id == id);
                return aluno;
            }
            catch
            {
                return null;
            }
        }

        public async Task<IEnumerable<Aluno>> GetByNome(string nome)
        {
            var Aluno = await _context.Alunos
                                .Where(a => a.Nome.Contains(nome))
                                .ToListAsync();
            return Aluno;
        }

        public async Task<Aluno?> Create(Aluno aluno)
        {
            await _context.Alunos.AddAsync(aluno);
            await _context.SaveChangesAsync();

            return aluno;
        }

         public async Task<Aluno?> Update(Aluno aluno)
        {
            var AlunoUpdate = await _context.Alunos.FindAsync(aluno.Id);

            if(AlunoUpdate is null)
            {
                return null;
            }
            _context.Entry(AlunoUpdate).State = EntityState.Detached;
            _context.Entry(aluno).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return aluno;
        }

        public async Task Delete(Aluno aluno)
        {
            _context.Remove(aluno);
            await _context.SaveChangesAsync();
        }

        
    }
}