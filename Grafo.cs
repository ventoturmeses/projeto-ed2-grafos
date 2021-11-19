using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace apProjetoRotasTrem
{
    class Grafo
    {
        private int maxVertices;
        private Vertice[] vertices;
        private int[,] adjMatrix;
        private int numVertices;
        private DataGridView dgv;


        /// DJIKSTRA
        private DistOriginal[] percurso;
        private int infinity = 1000000;
        private int verticeAtual;
        private int doInicioAteAtual;
        private int nTree;

        public Grafo(DataGridView dgv, int maxVertices)
        {
            this.maxVertices = maxVertices;
            this.dgv = dgv;
            vertices = new Vertice[this.maxVertices];
            adjMatrix = new int[this.maxVertices, this.maxVertices];
            numVertices = 0;
            nTree = 0;

            adjMatrix = new int[this.maxVertices, this.maxVertices];

            for (int j = 0; j < maxVertices; j++)
                for (int k = 0; k < maxVertices; k++)
                    adjMatrix[j, k] = infinity;

            percurso = new DistOriginal[this.maxVertices];
        }

        public void NovoVertice(Cidade c)
        {
            vertices[numVertices++] = new Vertice(c);

            if (dgv != null) // se foi passado como parâmetro um dataGridView para exibição
            {
                // se realiza o seu ajuste para a quantidade de vértices
                dgv.RowCount = numVertices + 1;
                dgv.ColumnCount = numVertices + 1;
                dgv.Columns[numVertices].Width = 45;
            }
        }

        public void NovaAresta(int inicio, int fim)
        {
            adjMatrix[inicio, fim] = 1;
            // adjMatrix[fim, inicio] = 1; ISSO GERA CICLOS!!!
        }
        public void NovaAresta(int inicio, int fim, int peso)
        {
            adjMatrix[inicio, fim] = peso;
        }

        public void ExibirVertice(int v, TextBox txt)
        {
            txt.Text += vertices[v].Cidade.Nome + " ";
        }

        public int SemSucessores() // encontra e retorna a linha de um vértice sem sucessores
        {
            bool temAresta;
            for (int linha = 0; linha < numVertices; linha++)
            {
                temAresta = false;
                for (int col = 0; col < numVertices; col++)
                    if (adjMatrix[linha, col] != infinity)
                    {
                        temAresta = true;
                        break;
                    }
                if (!temAresta)
                    return linha;
            }
            return -1;
        }

        public void ExibirAdjacencias()
        {
            dgv.RowCount = numVertices + 1;
            dgv.ColumnCount = numVertices + 1;
            for (int j = 0; j < numVertices; j++)
            {
                dgv[0, j + 1].Value = vertices[j].Cidade.Nome;
                dgv[j + 1, 0].Value = vertices[j].Cidade.Nome;
                for (int k = 0; k < numVertices; k++)
                {
                    if (adjMatrix[j, k] != infinity)
                    {
                        dgv[k + 1, j + 1].Style.BackColor = System.Drawing.Color.Yellow;
                        dgv[k + 1, j + 1].Value = Convert.ToString(adjMatrix[j, k]);
                    }
                    else
                    {
                        dgv[k + 1, j + 1].Value = "";
                        dgv[k + 1, j + 1].Style.BackColor = System.Drawing.Color.White;
                    }

                }
            }
        }


        public void RemoverVertice(int vertice)
        {
            if (dgv != null)
            {
                MessageBox.Show("Matriz de Adjacências antes de remover vértice " +
                              Convert.ToString(vertice));
                ExibirAdjacencias();
            }
            if (vertice != numVertices - 1)
            {
                for (int j = vertice; j < numVertices - 1; j++)   // remove vértice do vetor
                    vertices[j] = vertices[j + 1];

                // remove vértice da matriz
                for (int row = vertice; row < numVertices; row++)
                    MoverLinhas(row, numVertices - 1);
                for (int col = vertice; col < numVertices; col++)
                    MoverColunas(col, numVertices - 1);
            }
            numVertices--;
            if (dgv != null)
            {
                MessageBox.Show("Matriz de Adjacências após remover vértice " +
                       Convert.ToString(vertice));
                ExibirAdjacencias();
                MessageBox.Show("Retornando à ordenação");
            }
        }
        
        private void MoverLinhas(int row, int length)
        {
            if (row != numVertices - 1)
                for (int col = 0; col < length; col++)
                    adjMatrix[row, col] = adjMatrix[row + 1, col];  // desloca para excluir
        }
        private void MoverColunas(int col, int length)
        {
            if (col != numVertices - 1)
                for (int row = 0; row < length; row++)
                    adjMatrix[row, col] = adjMatrix[row, col + 1]; // desloca para excluir
        }






















        public String OrdenacaoTopologica()
        {
            if (dgv != null)
                ExibirAdjacencias();
            Stack<String> gPilha = new Stack<String>(); // para guardar a sequência de vértices
            int origVerts = numVertices;
            while (numVertices > 0)
            {
                int currVertex = SemSucessores();
                if (currVertex == -1)
                    return "Erro: grafo possui ciclos.";
                gPilha.Push(vertices[currVertex].Cidade.Nome);   // empilha vértice
                RemoverVertice(currVertex);
            }
            String resultado = "Sequência da Ordenação Topológica: ";
            while (gPilha.Count > 0)
                resultado += gPilha.Pop() + " ";    // desempilha para exibir
            return resultado;
        }


        private int ObterVerticeAdjacenteNaoVisitado(int v)
        {
            for (int j = 0; j <= numVertices - 1; j++)
                if ((adjMatrix[v, j] != infinity) && (!vertices[j].Visitado))
                    return j;
            return -1;
        }

        public void PercursoEmProfundidade(TextBox txt)
        {
            txt.Clear();
            Stack<int> gPilha = new Stack<int>();
            for (int j = 0; j <= numVertices - 1; j++)
                vertices[j].Visitado = false;
            vertices[0].Visitado = true;
            ExibirVertice(0, txt);
            gPilha.Push(0);
            int v;
            while (gPilha.Count > 0)
            {
                v = ObterVerticeAdjacenteNaoVisitado(gPilha.Peek());
                if (v == -1)
                    gPilha.Pop();
                else
                {
                    vertices[v].Visitado = true;
                    ExibirVertice(v, txt);
                    gPilha.Push(v);
                }
            }
            for (int j = 0; j <= numVertices - 1; j++)
                vertices[j].Visitado = false;
        }



        void ProcessarNo(int i, TextBox txt)
        {
            txt.Text += vertices[i].Cidade.Nome;
        }

        public void PercursoEmProfundidadeRec(int[,] adjMatrix, int numVerts, int part, TextBox txt)
        {
            int i;
            ProcessarNo(part, txt);
            vertices[part].Visitado = true;
            for (i = 0; i < numVerts; ++i)
                if (adjMatrix[part, i] != infinity && !vertices[i].Visitado)
                    PercursoEmProfundidadeRec(adjMatrix, numVerts, i, txt);
        }


        public void PercursoPorLargura(TextBox txt)
        {
            txt.Clear();
            Queue<int> gQueue = new Queue<int>();
            vertices[0].Visitado = true;
            ExibirVertice(0, txt);
            gQueue.Enqueue(0);
            int vert1, vert2;
            while (gQueue.Count > 0)
            {
                vert1 = gQueue.Dequeue();
                vert2 = ObterVerticeAdjacenteNaoVisitado(vert1);
                while (vert2 != -1)
                {
                    vertices[vert2].Visitado = true;
                    ExibirVertice(vert2, txt);
                    gQueue.Enqueue(vert2);
                    vert2 = ObterVerticeAdjacenteNaoVisitado(vert1);
                }
            }
            for (int i = 0; i < numVertices; i++)
                vertices[i].Visitado = false;
        }


        public void ArvoreGeradoraMinima(int primeiro, TextBox txt)
        {
            txt.Clear();
            Stack<int> gPilha = new Stack<int>(); // para guardar a sequência de vértices
            vertices[primeiro].Visitado = true;
            gPilha.Push(primeiro);
            int currVertex, ver;
            while (gPilha.Count > 0)
            {
                currVertex = gPilha.Peek();
                ver = ObterVerticeAdjacenteNaoVisitado(currVertex);
                if (ver == -1)
                    gPilha.Pop();
                else
                {
                    vertices[ver].Visitado = true;
                    gPilha.Push(ver);
                    ExibirVertice(currVertex, txt);
                    txt.Text += "-->";
                    ExibirVertice(ver, txt);
                    txt.Text += "  ";
                }
            }
            for (int j = 0; j <= numVertices - 1; j++)
                vertices[j].Visitado = false;
        }



        public string Caminho(int inicioDoPercurso, int finalDoPercurso, ListBox lista)
        {
            for (int j = 0; j < numVertices; j++)
                vertices[j].Visitado = false;

            vertices[inicioDoPercurso].Visitado = true;
            for (int j = 0; j < numVertices; j++)
            {
                // anotamos no vetor percurso a distância entre o inicioDoPercurso e cada vértice
                // se não há ligação direta, o valor da distância será infinity
                int tempDist = adjMatrix[inicioDoPercurso, j];
                percurso[j] = new DistOriginal(inicioDoPercurso, tempDist);
            }

            for (int nTree = 0; nTree < numVertices; nTree++)
            {
                // Procuramos a saída não visitada do vértice inicioDoPercurso com a menor distância
                int indiceDoMenor = ObterMenor();

                // e anotamos essa menor distância
                int distanciaMinima = percurso[indiceDoMenor].distancia;


                // o vértice com a menor distância passa a ser o vértice atual
                // para compararmos com a distância calculada em AjustarMenorCaminho()
                verticeAtual = indiceDoMenor;
                doInicioAteAtual = percurso[indiceDoMenor].distancia;

                // visitamos o vértice com a menor distância desde o inicioDoPercurso
                vertices[verticeAtual].Visitado = true;
                AjustarMenorCaminho(lista);
            }

            return ExibirPercursos(inicioDoPercurso, finalDoPercurso, lista);
        }


        public int ObterMenor()
        {
            int distanciaMinima = infinity;
            int indiceDaMinima = 0;
            for (int j = 0; j < numVertices; j++)
                if (!(vertices[j].Visitado) && (percurso[j].distancia < distanciaMinima))
                {
                    distanciaMinima = percurso[j].distancia;
                    indiceDaMinima = j;
                }
            return indiceDaMinima;
        }

        public void AjustarMenorCaminho(ListBox lista)
        {
            for (int coluna = 0; coluna < numVertices; coluna++)
                if (!vertices[coluna].Visitado)       // para cada vértice ainda não visitado
                {
                    // acessamos a distância desde o vértice atual (pode ser infinity)
                    int atualAteMargem = adjMatrix[verticeAtual, coluna];

                    // calculamos a distância desde inicioDoPercurso passando por vertice atual até
                    // esta saída
                    int doInicioAteMargem = doInicioAteAtual + atualAteMargem;

                    // quando encontra uma distância menor, marca o vértice a partir do
                    // qual chegamos no vértice de índice coluna, e a soma da distância
                    // percorrida para nele chegar
                    int distanciaDoCaminho = percurso[coluna].distancia;
                    if (doInicioAteMargem < distanciaDoCaminho)
                    {
                        percurso[coluna].verticePai = verticeAtual;
                        percurso[coluna].distancia = doInicioAteMargem;
                        ExibirTabela(lista);
                    }
                }
            lista.Items.Add("==================Caminho ajustado==============");
        }

        public void ExibirTabela(ListBox lista)
        {
            string dist = "";
            lista.Items.Add("Vértice\tVisitado?\tPeso\tVindo de");
            for (int i = 0; i < numVertices; i++)
            {
                if (percurso[i].distancia == infinity)
                    dist = "inf";
                else
                    dist = Convert.ToString(percurso[i].distancia);

                lista.Items.Add(vertices[i].Cidade.Nome + "\t" + vertices[i].Visitado +
                      "\t\t" + dist + "\t" + vertices[percurso[i].verticePai].Cidade.Nome);
            }
            lista.Items.Add("-----------------------------------------------------");
        }



        public string ExibirPercursos(int inicioDoPercurso, int finalDoPercurso, ListBox lista)
        {
            string linha = "", resultado = "";
            for (int j = 0; j < numVertices; j++)
            {
                linha += vertices[j].Cidade.Nome + "=";
                if (percurso[j].distancia == infinity)
                    linha += "inf";
                else
                    linha += percurso[j].distancia;
                string pai = vertices[percurso[j].verticePai].Cidade.Nome;
                linha += "(" + pai + ") ";
            }
            lista.Items.Add(linha);
            lista.Items.Add("");
            lista.Items.Add("");
            lista.Items.Add("Caminho entre " + vertices[inicioDoPercurso].Cidade.Nome +
                                       " e " + vertices[finalDoPercurso].Cidade.Nome);
            lista.Items.Add("");

            int onde = finalDoPercurso;
            Stack<string> pilha = new Stack<string>();

            int cont = 0;
            while (onde != inicioDoPercurso)
            {
                onde = percurso[onde].verticePai;
                pilha.Push(vertices[onde].Cidade.Nome);
                cont++;
            }

            while (pilha.Count != 0)
            {
                resultado += pilha.Pop();
                if (pilha.Count != 0)
                    resultado += " --> ";
            }

            if ((cont == 1) && (percurso[finalDoPercurso].distancia == infinity))
                resultado = "Não há caminho";
            else
                resultado += " --> " + vertices[finalDoPercurso].Cidade.Nome;
            return resultado;
        }








    }
}
