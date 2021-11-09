using System;

namespace apProjetoRotasTrem
{
    class Vertice
    {
        private Cidade cidade;
        private bool visitado;

        public Vertice(Cidade cidade)
        {
            this.cidade = cidade;
            visitado = false;
        }

        public bool Visitado { get => visitado; set => visitado = value; }
        public Cidade Cidade { get => cidade; set => cidade = value; }
    }
}
