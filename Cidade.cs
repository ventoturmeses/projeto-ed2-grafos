using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apProjetoRotasTrem
{
    class Cidade
    {
        private int indice;
        private string nome;
        private double coordenadaX, coordenadaY;

        public Cidade(int indice, string nome, double coordenadaX, double coordenadaY)
        {
            this.indice = indice;
            this.nome = nome;
            this.coordenadaX = coordenadaX;
            this.coordenadaY = coordenadaY;
        }

        public int Indice { get => indice; set => indice = value; }
        public string Nome { get => nome; set => nome = value; }
        public double CoordenadaX { get => coordenadaX; set => coordenadaX = value; }
        public double CoordenadaY { get => coordenadaY; set => coordenadaY = value; }
    }
}
