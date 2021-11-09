using System;
using System.IO;
using System.Windows.Forms;
using System.Linq;

namespace apProjetoRotasTrem
{
    public partial class Form1 : Form
    {
        Grafo oGrafo;
        Cidade[] cidades;
        Viagem[] viagens;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LerArquivos();
        }

        private void LerArquivos()
        {
            StreamReader arqCidades = new StreamReader("C:/temp/Cidades.txt");
            int qtasCidades = File.ReadAllLines("C:/temp/Cidades.txt").Length;

            cidades = new Cidade[qtasCidades];
            for (int i = 0; i < qtasCidades; i++)
            {
                string linha = arqCidades.ReadLine();
                cidades[i] = new Cidade(linha);
            }

            StreamReader arqViagens = new StreamReader("C:/temp/Viagens.txt");
            int qtasViagens = File.ReadAllLines("C:/temp/Viagens.txt").Length;

            viagens = new Viagem[qtasViagens];
            for (int i = 0; i < qtasViagens; i++)
            {
                string linha = arqViagens.ReadLine();
                viagens[i] = new Viagem(linha);
            }

            oGrafo = new Grafo(dgvGrafo, qtasCidades);

            for (int i = 0; i < cidades.Length; i++)
                oGrafo.NovoVertice(cidades[i]);

            for (int i = 0; i < viagens.Length; i++)
            {
                int indiceO = IndiceDaCidade(viagens[i].NomeOrigem);
                int indiceD = IndiceDaCidade(viagens[i].NomeDestino);

                if (indiceO < 0 || indiceD < 0)
                {
                    MessageBox.Show("ERRO NA CONSTRUÇÃO DA MATRIZ!");
                    return;
                }

                oGrafo.NovaAresta(indiceO, indiceD);
            }

            arqCidades.Close(); arqViagens.Close();

            int IndiceDaCidade(string nome)
            {
                for (int i = 0; i < cidades.Length; i++)
                {
                    if (cidades[i].Nome == nome)
                        return i;
                }
                return -1;
            }
        }

        private void btnOrdTop_Click(object sender, EventArgs e)
        {
            oGrafo.ExibirAdjacencias();
            // for (int i = 0; i < viagens.Length; i++)
                // MessageBox.Show(viagens[i].ToString());
            // for (int i = 0; i < cidades.Length; i++)
                // MessageBox.Show(cidades[i].ToString());
            // oGrafo.OrdenacaoTopologica(txtSaida);
        }
    }
}
