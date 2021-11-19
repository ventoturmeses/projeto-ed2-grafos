using System;
using System.IO;
using System.Windows.Forms;
using static System.Environment;

namespace apProjetoRotasTrem
{
    class Cidade : IComparable<Cidade>, IRegistro
    {
        private const int tamanhoIndice = 2;
        private const int tamanhoNome = 16;
        private const int tamanhoX = 5;
        private const int tamanhoY = 5;

        private int indice;
        private string nome;
        private string coordenadaX, coordenadaY;

        private const int tamanhoRegistro = tamanhoIndice +
                                            tamanhoNome +
                                            tamanhoX +
                                            tamanhoY;

        public Cidade()
        {
            Indice = 0;
            Nome = "";
            CoordenadaX = "";
            CoordenadaY = "";
        }

        public Cidade(int indice, string nome, string coordenadaX, string coordenadaY)
        {
            Indice = indice;
            Nome = nome;
            CoordenadaX = coordenadaX;
            CoordenadaY = coordenadaY;
        }
        public Cidade(string linha)
        {
            Indice = Convert.ToInt32(linha.Substring(0, tamanhoIndice));
            Nome = linha.Substring(tamanhoIndice, tamanhoNome);
            CoordenadaX = linha.Substring(tamanhoIndice + tamanhoNome, tamanhoX);
            CoordenadaY = linha.Substring(tamanhoIndice + tamanhoNome + tamanhoX + 1, tamanhoY);
        }
        public int Indice { get => indice; set => indice = value; }
        public string Nome { get => nome; set => nome = value.PadRight(tamanhoNome, ' ').Substring(0, tamanhoNome); }
        public string CoordenadaX { get => coordenadaX; set => coordenadaX = value.PadRight(tamanhoX, ' ').Substring(0, tamanhoX); }
        public string CoordenadaY { get => coordenadaY; set => coordenadaY = value.PadRight(tamanhoY, ' ').Substring(0, tamanhoY); }

        public int CompareTo(Cidade c)
        {
            return indice - c.indice;
        }

        public int TamanhoRegistro { get => tamanhoRegistro; }

        public void LerRegistro(BinaryReader arquivo, long qualRegistro)
        {
            if (arquivo != null)
                try
                {
                    long qtosBytes = qualRegistro * TamanhoRegistro;
                    arquivo.BaseStream.Seek(qtosBytes, SeekOrigin.Begin);
                    Indice = arquivo.ReadInt32();

                    char[] nome = new char[tamanhoNome];
                    nome = arquivo.ReadChars(tamanhoNome);

                    char[] coordX = new char[tamanhoX];
                    coordX = arquivo.ReadChars(tamanhoX);

                    char[] coordY = new char[tamanhoY];
                    coordY = arquivo.ReadChars(tamanhoY);

                    string nomeLido = "";

                    for (int i = 0; i < tamanhoNome; i++)
                        nomeLido += nome[i];
                    Nome = nomeLido;

                    nomeLido = "";
                    for (int i = 0; i < tamanhoX; i++)
                        nomeLido += coordX[i];
                    CoordenadaX = nomeLido;

                    nomeLido = "";
                    for (int i = 0; i < tamanhoY; i++)
                        nomeLido += coordY[i];
                    CoordenadaY = nomeLido;
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

                char[] nome = new char[tamanhoNome];
                for (int i = 0; i < tamanhoNome; i++)
                    nome[i] = this.nome[i];

                char[] coordX = new char[tamanhoX];
                for (int i = 0; i < tamanhoX; i++)
                    coordX[i] = coordenadaX[i];

                char[] coordY = new char[tamanhoY];
                for (int i = 0; i < tamanhoY; i++)
                    coordY[i] = coordenadaY[i];

                arquivo.Write(nome);
                arquivo.Write(coordX);
                arquivo.Write(coordY);
            }
        }

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
