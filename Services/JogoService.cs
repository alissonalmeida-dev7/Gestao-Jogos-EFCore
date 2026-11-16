using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestaoJogosEFCore.Models;
using GestaoJogosEFCore.Repositories;

namespace GestaoJogosEFCore.Services
{
    public class JogoService
    {
        private readonly JogoRepository _repository;

        public JogoService(JogoRepository repository)
        {
            _repository = repository;
        }

        // public List<Jogo> ListarTodos()
        // {
        //     return _repository.Listar();
        // }
        public List<Jogo> ListarTodosJogos() => _repository.Listar();
        public Jogo? BuscarPorId(int id) => _repository.BuscarPorId(id);
        public void CadastrarJogo(Jogo jogo) => _repository.Adicionar(jogo);
        public bool AtualizarJogo(Jogo jogoAtualizado) => _repository.Atualizar(jogoAtualizado);
        public bool ExcluirJogo(int id) => _repository.Excluir(id);
        public List<Desenvolvedora> ListarDesenvolvedoras() => _repository.ListarDesenvolverdoras();
        public void CadastrarDesenvolvedora(Desenvolvedora desenvolvedora) => _repository.AdicionarDesenvolvedora(desenvolvedora);
        
    }
}