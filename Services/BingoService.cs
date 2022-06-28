using Methods.BingoVS.Models;

namespace BingoVS.Services;

public class BingoService : IBingoService
{
    public Bingo bingo = new Bingo();
    private static int countId = 1;

    public string Adiciona()
    {
        int id = countId++;
        List<Cartela> cartelas = new List<Cartela>();
        Bingo.Apostadores.Add(new Apostador(id, cartelas));
        return "Apostador adicionado com sucesso\nID: " + id.ToString(); ;
    }

    public string Remove(int id)
    {
        var apostadorAux = GetApostadorById(id);
        if (apostadorAux == null)
        {
            throw new InvalidDataException();
        }
        Bingo.Apostadores.Remove(apostadorAux);
        return "Apostador com ID " + id.ToString() + " removido com sucesso";
    }

    public Cartela AddCartela(int id)
    {
        var apostadorAux = GetApostadorById(id);
        if (apostadorAux == null)
        {
            throw new IndexOutOfRangeException();
        }
        else
        {
            Cartela cartela = new Cartela();
            apostadorAux.cartelas.Add(cartela);
            return cartela;
        }
    }

    public string Ranking()
    {
        List<List<int>> rankID = new List<List<int>>(Cartela.NumPorCartela + 1);
        for (int i = 0; i < Cartela.NumPorCartela + 1; i++)
        {
            rankID.Add(new List<int>());
        }

        foreach (Apostador apostador in Bingo.Apostadores)
        {
            rankID[apostador.maxAcertos].Add(apostador.id);
        }

        string st = "";

        for (int i = Cartela.NumPorCartela; i >= 0; i--)
        {
            if (rankID[i].Any())
            {
                st += i.ToString() + " acertos (IDs): ";
                for (int j = 0; j < rankID[i].Count; j++)
                {
                    st += rankID[i][j].ToString();
                    if (j < rankID[i].Count - 1)
                    {
                        st += ", ";
                    }
                    else
                    {
                        st += "\n";
                    }
                }
            }
        }

        return st;
    }

    public string Prob()
    {
        string st = "";
        double prob = (1.0 / Bingo.NumDisp.Count) * 100.0;
        foreach (Apostador apostador in Bingo.Apostadores)
        {
            st += "ID: " + apostador.id + "\tProbabilidade: ";
            if (apostador.maxAcertos == Cartela.NumPorCartela - 1)
            {
                st += prob.ToString() + "%\n";
            }
            else
            {
                st += "0%\n";
            }
        }
        return st;
    }

    public string SorteioNum()
    {
        if (!bingo.NumSorteados.Any())
        {
            if (Bingo.Apostadores.Count != 0)
            {
                if (bingo.ApostProntos())
                {
                    return Sorteio();
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
        else
        {
            return Sorteio();
        }
    }

    public string Sorteio()
    {
        int indice = bingo.Sorteador.Next(0, Bingo.NumDisp.Count - 1);
        int numSorteado = Bingo.NumDisp[indice];
        bingo.NumSorteados.Add(numSorteado);
        Bingo.NumDisp.RemoveAt(indice);

        foreach (Apostador ap in Bingo.Apostadores)
        {
            foreach (Cartela cartela in ap.cartelas)
            {
                _ = cartela.Verificar(numSorteado);
            }
            ap.AtMaxAcertos();
            foreach (Cartela cartela in ap.cartelas)
            {
                if (cartela.Vitoria())
                {
                    return "Numeros sorteados: " + bingo.StNumSort() + "\nID do apostador vencedor: " +
                        ap.id.ToString() + "\nCartela vencedora: " + cartela.ToString();
                }
            }
        }
        return "Numeros sorteados: " + bingo.StNumSort();
    }

    private Apostador GetApostadorById(int id)
    {
        return Bingo.Apostadores.Where(a => a.id == id).FirstOrDefault();
    }

    public List<Apostador> Apostadores()
    {
        return Bingo.Apostadores;
    }

}