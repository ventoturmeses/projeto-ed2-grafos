using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace apProjetoRotasTrem
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnOrdTop_Click(object sender, EventArgs e)
        {
            Cidade c1 = new Cidade(1, "Campinas", 0.212, 0.821);
            Cidade c2 = new Cidade(2, "São Paulo", 0.652, 0.123);
            Cidade c3 = new Cidade(3, "Osasco", 0.100, 0.573);
            Cidade c4 = new Cidade(4, "Jundiaí", 0.852, 0.999);

            Grafo oGrafo = new Grafo(dgvGrafo, 4);
            oGrafo.NovoVertice(c1);
            oGrafo.NovoVertice(c2);
            oGrafo.NovoVertice(c3);
            oGrafo.NovoVertice(c4);
            oGrafo.NovaAresta(0, 3);
            oGrafo.NovaAresta(2, 1);
            oGrafo.NovaAresta(3, 1);
            oGrafo.NovaAresta(3, 2);

            oGrafo.ExibirAdjacencias();
            // oGrafo.OrdenacaoTopologica(txtSaida);
        }
    }
}
