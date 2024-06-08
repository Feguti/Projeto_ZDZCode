using Microsoft.EntityFrameworkCore;

namespace APITeste.Models
{
    public class DBContext
    {
        public DbSet<DadosFuncionario> DadosFuncionario { get; set; }
    }
}