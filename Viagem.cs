using System;
using static System.Environment;

namespace apProjetoRotasTrem
{
    class Viagem
    {
        private const int tamanhoNomeOrigem = 16;
        private const int tamanhoNomeDestino = 16;
        private const int tamanhoDistancia = 3;
        private const int tamanhoPreco = 3;

        private string nomeOrigem, nomeDestino;
        private string distancia, preco;

        public Viagem(string linha)
        {
            nomeOrigem = linha.Substring(0, tamanhoNomeOrigem);
            nomeDestino = linha.Substring(tamanhoNomeOrigem, tamanhoNomeDestino);
            distancia = linha.Substring(tamanhoNomeDestino+tamanhoNomeOrigem, tamanhoDistancia);
            preco = linha.Substring(tamanhoNomeDestino + tamanhoNomeOrigem + tamanhoDistancia + 2, tamanhoPreco);
        }

        public string NomeOrigem { get => nomeOrigem; set => nomeOrigem = value; }
        public string NomeDestino { get => nomeDestino; set => nomeDestino = value; }
        public string Distancia { get => distancia; set => distancia = value; }
        public string Preco { get => preco; set => preco = value; }

        public override string ToString()
        {
            string ret = "";
            ret += nomeOrigem + NewLine; 
            ret += nomeDestino + NewLine; 
            ret += distancia + NewLine; 
            ret += preco + NewLine;
            return ret;
        }
    }
}
