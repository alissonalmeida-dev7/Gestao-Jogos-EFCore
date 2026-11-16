using GestaoJogosEFCore.Data;
using GestaoJogosEFCore.Models;
using Microsoft.EntityFrameworkCore;

namespace GestaoJogosEFCore.Repositories
{
    public class JogoRepository
    {
        private readonly DbContextOptions<GestaoJogosContext> _options;

        public JogoRepository(DbContextOptions<GestaoJogosContext> options)
        {
            _options = options;
        }
        
        public List<Jogo> Listar()
        {
            using var context = new GestaoJogosContext(_options);

            return context.Jogo
                // carrega a navegação de desenvolvedora junto com os jogos
                .Include(j => j.IdDesenvolvedoraNavigation)
                .OrderBy(j => j.IdJogo)
                // executa a consulta no banco de dados e materializa os resultados em uma lista
                .ToList();
        }

        // Busca um unico jogo por meio do id, retornando null caso não seja encontrado
        public Jogo? BuscarPorId(int id)
        {
            using var context = new GestaoJogosContext(_options);

            return context.Jogo 
                .Include(j => j.IdDesenvolvedoraNavigation)
                // Retorna o primeiro resultado ou o valor padrão (null)
                .FirstOrDefault(j => j.IdJogo == id); // Where
        }

         /*
            Create
            Read
            Update
            Delete
         */

        public void Adicionar(Jogo jogo)
        {
            using var context = new GestaoJogosContext(_options);

            context.Jogo.Add(jogo); 
            context.SaveChanges(); // funciona como o commit no SQL
        }

        public bool Atualizar(Jogo jogoAtualizado)
        {
            using var context = new GestaoJogosContext(_options);
            var jogoExistente = context.Jogo.Find(jogoAtualizado.IdJogo);

            if(jogoExistente is null)
            {
                return false;
            }

            jogoExistente.IdDesenvolvedora = jogoAtualizado.IdDesenvolvedora;
            jogoExistente.Nome = jogoAtualizado.Nome;
            jogoExistente.Genero = jogoAtualizado.Genero;
            jogoExistente.AnoLancamento = jogoAtualizado.AnoLancamento;
            jogoExistente.Preco = jogoAtualizado.Preco;

            context.SaveChanges();
            return true;
        }

        public bool Excluir(int id)
        {
            using var context = new GestaoJogosContext(_options);
            var jogoExistente = context.Jogo.Find(id);

            if(jogoExistente is null)
            {
                return false;
            }

            context.Jogo.Remove(jogoExistente);
            context.SaveChanges();
            return true;
        }

        public List<Desenvolvedora> ListarDesenvolverdoras()
        {
            using var context = new GestaoJogosContext(_options);

            return context.Desenvolvedora
                .OrderBy(d => d.Nome) // Executa a consulta no banco de dados e materializa os resultados em uma lista
                .ToList();
        }

        public void AdicionarDesenvolvedora(Desenvolvedora desenvolvedora)
        {
            using var context = new GestaoJogosContext(_options);

            context.Desenvolvedora.Add(desenvolvedora);
            context.SaveChanges();
        }

    }
}