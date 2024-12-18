namespace BackendAPI.Models
{
    public class Resultados
    {
        public int TotalJogos { get; set; }
        public int TotalPontos { get; set; }
        public int MediaJogos { get; set; }
        public int BestScore { get; set; }
        public int WrostScore { get; set; }
        public int Records { get; set; }
        public string DataInicial { get; set; }
        public string DataFinal { get; set; }

    }
}
