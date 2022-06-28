using Methods.BingoVS.Models;

namespace BingoVS.Services;
public interface IBingoService
{
    public string Adiciona();
    public string Remove(int id);
    public Cartela AddCartela(int id);
    public string Ranking();
    public string Prob();
    public string SorteioNum();
    public string Sorteio();
}