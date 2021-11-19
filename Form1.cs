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

        ArvoreDeBusca<Cidade> arvoreCidades;
        ArvoreDeBusca<Viagem> arvoreViagens;


        public Form1()
        {
            InitializeComponent();
            arvoreCidades = new ArvoreDeBusca<Cidade>();
            arvoreViagens = new ArvoreDeBusca<Viagem>();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //cidades
            string[] arqCidades = File.ReadAllLines("C:/temp/Cidades.txt");
            // armazena uma linha do arquivo em cada posição do vetor

            int qtasCidades = arqCidades.Length; // quantas cidades existem no vetor
            cidades = new Cidade[qtasCidades];
            // instancia o vetor de cidades com a capacidade para o número de linhas lidas no arquivo

            for (int i = 0; i < qtasCidades; i++)
               cidades[i] = new Cidade(arqCidades[i]);
            cidades = cidades.OrderBy(c => c.Nome).ToArray();




            // para cada posição do vetor cidades, é instanciado um novo objeto da classe cidade,
            // que será construído baseado na linha do arquivo que está no vetor de string

            // viagens
            string[] arqViagens = File.ReadAllLines("C:/temp/Viagens.txt");
            // armazena uma linha do arquivo em cada posição do vetor

            int qtasViagens = arqViagens.Length; // quantas cidades existem no vetor
            viagens = new Viagem[qtasViagens];
            // instancia o vetor de viagens com a capacidade para o número de linhas lidas no arquivo

            for (int i = 0; i < qtasViagens; i++)
                viagens[i] = new Viagem(arqViagens[i]);
            viagens = viagens.OrderBy(v => v.NomeOrigem).ToArray();
            // para cada posição do vetor viagens, é instanciado um novo objeto da classe viagem,
            // que será construído baseado na linha do arquivo que está no vetor de string

            oGrafo = new Grafo(dgvGrafo, qtasCidades);
            // objeto grafo é criado com o número máximo de vertices equivalente à quantidade de cidades lidas

            for (int i = 0; i < qtasCidades; i++)
                oGrafo.NovoVertice(cidades[i]); // cada vértice será uma cidade

            for (int i = 0; i < qtasViagens; i++)
            {
                // para cada viagem, será criada uma aresta,
                // sendo a origem o índice da cidade de origem,
                // o destino o índice da cidade de destino,
                // e o peso será a distância entre as cidades 

                int indiceO = IndiceDaCidade(viagens[i].NomeOrigem);
                int indiceD = IndiceDaCidade(viagens[i].NomeDestino);

                if (indiceO < 0 || indiceD < 0)
                {
                    // caso algum dos índices sejam menores que zero significa que
                    // algo deu errado na leitura dos arquivos e construção da matriz

                    MessageBox.Show("ERRO NA CONSTRUÇÃO DA MATRIZ!");
                    return;
                }

                oGrafo.NovaAresta(indiceO, indiceD, int.Parse(viagens[i].Distancia)); // aresta criada
            }

            int IndiceDaCidade(string nome) 
            {
                // método para retornar o índice
                // da cidade baseado em seu nome

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
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            lsbSaida.Items.Clear();
            lsbSaida.Items.Add("");
            lsbSaida.Items.Add("Menores caminhos:");
            lsbSaida.Items.Add("");
            int ori = int.Parse(txtOrigem.Text);
            int des = int.Parse(txtDestino.Text);
            lsbSaida.Items.Add(oGrafo.Caminho(ori, des, lsbSaida));
            lsbSaida.Items.Add("");
        }
    }
}
