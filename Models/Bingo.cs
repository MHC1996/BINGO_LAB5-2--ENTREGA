namespace Methods.BingoVS.Models
{
    public class Bingo
    {
        public static List<Apostador> Apostadores = new List<Apostador>();
        public List<int> NumSorteados = new List<int>();
        public static List<int> NumDisp = GeraNum();
        public static int ContaID = 0;

        public Random Sorteador = new Random();


        private static List<int> GeraNum()
        {
            List<int> lista = new List<int>();
            for (int i = Cartela.NumMin; i <= Cartela.NumMax; i++)
            {
                lista.Add(i);
            }
            return lista;
        }

        public bool ApostProntos()
        {
            foreach (Apostador apostador in Apostadores)
            {
                if (!apostador.cartelas.Any())
                {
                    return false;
                }
            }
            return true;
        }

        public string StNumSort()
        {
            string st = "";
            for (int i = 0; i < this.NumSorteados.Count; i++)
            {
                int num = this.NumSorteados[i];
                if (i < this.NumSorteados.Count - 1)
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
