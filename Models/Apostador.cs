namespace Methods.BingoVS.Models
{
    public class Apostador
    {
        public readonly int id;
        public List<Cartela> cartelas;
        public int maxAcertos;

        public Apostador(int id, List<Cartela> cartelas)
        {
            this.id = id;
            this.cartelas = cartelas;
            this.maxAcertos = 0;
        }

        public void AtMaxAcertos()
        {
            if (this.cartelas.Count != 0)
            {
                foreach (Cartela cartela in this.cartelas)
                {
                    if (cartela.Acertos > maxAcertos)
                    {
                        this.maxAcertos = cartela.Acertos;
                    }
                }
            }
        }
    }
}
