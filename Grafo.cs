using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace apProjetoRotasTrem
{
    class Grafo
    {
        private Vertice[] vertices;
        private int maxVertices;
        private int[,] adjMatrix;
        private int numVertices;
        private DataGridView dgv;

        public Grafo(DataGridView dgv, int maxVertices)
        {
            this.dgv = dgv;
            this.maxVertices = maxVertices;
            vertices = new Vertice[this.maxVertices];
            numVertices = 0;

            adjMatrix = new int[maxVertices, maxVertices];
            for (int j = 0; j < maxVertices; j++)
                for (int k = 0; k < maxVertices; k++)
                    adjMatrix[j, k] = 0;
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

        public int SemSucessores() // encontra e retorna a linha de um vértice sem sucessores
        {
            bool temAresta;
            for (int linha = 0; linha < numVertices; linha++)
            {
                temAresta = false;
                for (int col = 0; col < numVertices; col++)
                    if (adjMatrix[linha, col] > 0)
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
                dgv.Rows[j + 1].Cells[0].Value = vertices[j].Cidade.Nome;
                dgv.Rows[0].Cells[j + 1].Value = vertices[j].Cidade.Nome;
                for (int k = 0; k < numVertices; k++)
                    dgv.Rows[j + 1].Cells[k + 1].Value = Convert.ToString(adjMatrix[j, k]);
            }
        }

        public void OrdenacaoTopologica(TextBox txt)
        {
            Stack<string> gPilha = new Stack<string>(); //guarda a sequência de vértices
            while (numVertices > 0)
            {
                int currVertice = SemSucessores();
                if (currVertice == -1)
                {
                    txt.Text = "Erro: grafo possui ciclos.";
                    return;
                }
                gPilha.Push(vertices[currVertice].Cidade.Nome); // empilha vértice
                RemoverVertice(currVertice);
            }
            string resultado = "Sequência da Ordenação Topológica: ";
            while (gPilha.Count > 0)
                resultado += gPilha.Pop() + " "; // desempilha para exibir
            txt.Text = resultado;
            return;
        }

        public void RemoverVertice(int vert)
        {
            if (dgv != null)
            {
                MessageBox.Show($"Matriz de Adjacências antes de remover vértice {vert + 1}");
                ExibirAdjacencias();
            }
            if (vert != numVertices - 1)
            {
                for (int j = vert; j < numVertices - 1; j++)// remove vértice do vetor
                    vertices[j] = vertices[j + 1];
                // remove vértice da matriz
                for (int row = vert; row < numVertices; row++)
                    MoverLinhas(row, numVertices - 1);
                for (int col = vert; col < numVertices; col++)
                    MoverColunas(col, numVertices - 1);
            }
            numVertices--;
            if (dgv != null)
            {
                MessageBox.Show($"Matriz de Adjacências após remover vértice {vert + 1}");
                ExibirAdjacencias();
                MessageBox.Show("Retornando à ordenação");
            }
        }

        private void MoverLinhas(int row, int length)
        {
            if (row != numVertices - 1)
                for (int col = 0; col < length; col++)
                    adjMatrix[row, col] = adjMatrix[row + 1, col]; // desloca para excluir
        }

        private void MoverColunas(int col, int length)
        {
            if (col != numVertices - 1)
                for (int row = 0; row < length; row++)
                    adjMatrix[row, col] = adjMatrix[row, col + 1]; // desloca para excluir
        }
    }
}
