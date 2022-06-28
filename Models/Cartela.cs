namespace Methods.BingoVS.Models
{
    public class Cartela
    {
        public static readonly int NumMin = 1;
        public static readonly int NumMax = 100;
        public static readonly int NumPorCartela = 9;

        private static Random Sorteador = new Random();

        private readonly List<int> Numeros;
        private readonly List<bool> Verificacoes;
        public int Acertos;

        public Cartela()
        {
            int totalNumPossiv = Cartela.NumMax - Cartela.NumMin + 1;
            List<int> numDisp = new List<int>(totalNumPossiv);
            for (int i = Cartela.NumMin; i <= Cartela.NumMax; i++)
            {
                numDisp.Add(i);
            }

            this.Numeros = new List<int>(Cartela.NumPorCartela);
            this.Verificacoes = new List<bool>(Cartela.NumPorCartela);
            for (int i = 0; i < Cartela.NumPorCartela; i++)
            {
                int indice = Cartela.Sorteador.Next(0, numDisp.Count - 1);

                this.Numeros.Add(numDisp[indice]);
                this.Verificacoes.Add(false);

                numDisp.RemoveAt(indice);
            }

            this.Acertos = 0;
        }

        public bool Vitoria()
        {
            if (this.Acertos == Cartela.NumPorCartela)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Verificar(int num)
        {
            if (this.Numeros.Contains(num))
            {
                int indice = this.Numeros.IndexOf(num);
                if (!this.Verificacoes[indice])
                {
                    this.Acertos++;
                    this.Verificacoes[indice] = true;
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public override string ToString()
        {
            string st = "";
            for (int i = 0; i < this.Numeros.Count; i++)
            {
                int num = this.Numeros[i];
                if (i < this.Numeros.Count - 1)
                {
                    st += num.ToString() + ", ";
                }
                else
                {
                    st += num.ToString();
                }
            }
            return st;
        }
    }
}
