public class Veiculo
{
    // placa do veículo, serve como ID
    public string Placa { get; set; } = string.Empty;
    public DateTime HorarioDeChegada { get; set; }
    public DateTime HorarioDeSaida { get; set; }
    public TimeSpan Duracao { get; set; }
    public string TempoCobrado { get; set; } = string.Empty;
    public string Preco { get; set; } = string.Empty;
    public string ValorAPagar { get; set; } = string.Empty;

    // construtor com todos os argumentos
    public Veiculo(string placa, DateTime dataDeEntrada, DateTime dataDeSaida, TimeSpan duracao, string tempoCobrado, string preco, string valorAPagar)
    {
        Placa = placa;
        HorarioDeChegada = dataDeEntrada;
        HorarioDeSaida = dataDeSaida;
        Duracao = duracao;
        TempoCobrado = tempoCobrado;
        Preco = preco;
        ValorAPagar = valorAPagar;
    }

    // construtor sem argumentos
    public Veiculo()
    {
        // intencionalmente vazio
    }
}
