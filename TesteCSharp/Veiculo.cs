public class Veiculo
{
    // placa do veículo, serve como ID
    public string Placa { get; set; }
    public DateTime HorarioDeChegada { get; set; }
    public DateTime HorarioDeSaida { get; set; }
    public TimeSpan Duracao { get; set; }
    public string TempoCobrado { get; set; }
    public string Preco { get; set; }
    public string ValorAPagar { get; set; }

    // construtor com todos os argumentos
    public Veiculo(string placa, DateTime dataDeEntrada, DateTime dataDeSaida, TimeSpan duracao, string tempoCobrado, int valorAPagar)
    {
        Placa = placa;
        HorarioDeChegada = dataDeEntrada;
        HorarioDeSaida = dataDeSaida;
        Duracao = duracao;
        TempoCobrado = tempoCobrado;
        Preco = "R$ " + 2 + ",00";
        ValorAPagar = "R$ " + valorAPagar + ",00";
    }

    // construtor sem argumentos
    public Veiculo()
    {
        // intencionalmente vazio
    }
}
