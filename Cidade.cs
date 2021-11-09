using System;
using static System.Environment;

namespace apProjetoRotasTrem
{
    class Cidade
    {
        private const int tamanhoNome = 16;
        private const int tamanhoX = 5;
        private const int tamanhoY = 5;

        private string nome;
        private string coordenadaX, coordenadaY;

        public Cidade(string nome, string coordenadaX, string coordenadaY)
        {
            this.nome = nome;
            this.coordenadaX = coordenadaX;
            this.coordenadaY = coordenadaY;
        }      
        public Cidade(string linha)
        {
            nome = linha.Substring(0, tamanhoNome);
            coordenadaX = linha.Substring(tamanhoNome, tamanhoX).Trim();
            coordenadaY = linha.Substring(tamanhoNome + tamanhoX + 1, tamanhoY).Trim();
        }

        public string Nome { get => nome; set => nome = value; }
        public string CoordenadaX { get => coordenadaX; set => coordenadaX = value; }
        public string CoordenadaY { get => coordenadaY; set => coordenadaY = value; }

        public override string ToString()
        {
            string ret = "";
            ret += nome + NewLine;
            ret += coordenadaX + NewLine;
            ret += coordenadaY + NewLine;
            return ret;
        }
    }
}
