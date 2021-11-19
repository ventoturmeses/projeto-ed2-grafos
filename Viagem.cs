using System;
using System.IO;
using static System.Environment;
using System.Windows.Forms;

namespace apProjetoRotasTrem
{
    class Viagem : IComparable<Viagem>, IRegistro
    {
        private const int tamanhoIndice = 2;
        private const int tamanhoNomeOrigem = 16;
        private const int tamanhoNomeDestino = 16;
        private const int tamanhoDistancia = 4;
        private const int tamanhoPreco = 3;

        private int indice;
        private string nomeOrigem, nomeDestino;
        private string distancia, preco;

        private const int tamanhoRegistro = tamanhoIndice +
                                            tamanhoNomeOrigem +
                                            tamanhoNomeDestino +
                                            tamanhoDistancia +
                                            tamanhoPreco;

        public Viagem()
        {
            Indice = 0;
            NomeOrigem = "";
            NomeDestino = "";
            Distancia = "";
            Preco = "";
        }

        public Viagem(int indice, string nomeOrigem, string nomeDestino, string distancia, string preco)
        {
            Indice = indice;
            NomeOrigem = nomeOrigem;
            NomeDestino = nomeDestino;
            Distancia = distancia;
            Preco = preco;
        }

        public Viagem(string linha)
        {
            Indice = Convert.ToInt32(linha.Substring(0, tamanhoIndice));
            NomeOrigem = linha.Substring(tamanhoIndice, tamanhoNomeOrigem);
            NomeDestino = linha.Substring(tamanhoIndice + tamanhoNomeOrigem, tamanhoNomeDestino);
            Distancia = linha.Substring(tamanhoIndice + tamanhoNomeDestino + tamanhoNomeOrigem, tamanhoDistancia);
            Preco = linha.Substring(tamanhoIndice + tamanhoNomeDestino + tamanhoNomeOrigem + tamanhoDistancia, tamanhoPreco);
        }


        public int Indice { get => indice; set => indice = value; }
        public string NomeOrigem { get => nomeOrigem; set => nomeOrigem = value.PadRight(tamanhoNomeOrigem, ' ').Substring(0, tamanhoNomeOrigem); }
        public string NomeDestino { get => nomeDestino; set => nomeDestino = value.PadRight(tamanhoNomeDestino, ' ').Substring(0, tamanhoNomeDestino); }
        public string Distancia { get => distancia; set => distancia = value.PadRight(tamanhoDistancia, ' ').Substring(0, tamanhoDistancia); }
        public string Preco { get => preco; set => preco = value.PadRight(tamanhoPreco, ' ').Substring(0, tamanhoPreco); }

        public int CompareTo(Viagem v)
        {
            return indice - v.indice;
        }

        public int TamanhoRegistro { get => tamanhoRegistro; }
        public void LerRegistro(BinaryReader arquivo, long qualRegistro)
        {
            if (arquivo != null)
                try
                {
                    long qtosBytes = qualRegistro * TamanhoRegistro;
                    arquivo.BaseStream.Seek(qtosBytes, SeekOrigin.Begin);
                    this.Indice = arquivo.ReadInt32();

                    char[] nomeOrigem = new char[tamanhoNomeOrigem];
                    nomeOrigem = arquivo.ReadChars(tamanhoNomeOrigem);

                    char[] nomeDestino = new char[tamanhoNomeDestino];
                    nomeDestino = arquivo.ReadChars(tamanhoNomeDestino);

                    char[] distancia = new char[tamanhoDistancia];
                    distancia = arquivo.ReadChars(tamanhoDistancia);

                    char[] preco = new char[tamanhoPreco];
                    preco = arquivo.ReadChars(tamanhoPreco);

                    string nomeLido = "";

                    for (int i = 0; i < tamanhoNomeOrigem; i++)
                        nomeLido += nomeOrigem[i];
                    NomeOrigem = nomeLido;

                    nomeLido = "";
                    for (int i = 0; i < tamanhoNomeDestino; i++)
                        nomeLido += nomeDestino[i];
                    NomeDestino = nomeLido;

                    nomeLido = "";
                    for (int i = 0; i < tamanhoDistancia; i++)
                        nomeLido += distancia[i];
                    Distancia = nomeLido;

                    nomeLido = "";
                    for (int i = 0; i < tamanhoPreco; i++)
                        nomeLido += preco[i];
                    Preco = nomeLido;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
        }

        public void GravarRegistro(BinaryWriter arquivo)
        {
            if (arquivo != null)
            {
                arquivo.Write(Indice);

                char[] nomeOrigem = new char[tamanhoNomeOrigem];
                for (int i = 0; i < tamanhoNomeOrigem; i++)
                    nomeOrigem[i] = this.nomeOrigem[i];

                char[] nomeDestino = new char[tamanhoNomeDestino];
                for (int i = 0; i < tamanhoNomeDestino; i++)
                    nomeDestino[i] = this.nomeDestino[i];

                char[] distancia = new char[tamanhoDistancia];
                for (int i = 0; i < tamanhoDistancia; i++)
                    distancia[i] = this.distancia[i];

                char[] preco = new char[tamanhoPreco];
                for (int i = 0; i < tamanhoPreco; i++)
                    preco[i] = this.preco[i];

                arquivo.Write(nomeOrigem);
                arquivo.Write(nomeDestino);
                arquivo.Write(distancia);
                arquivo.Write(preco);
            }
        }

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
