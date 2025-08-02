public class Estacionamento
{
    public void MarcarEntrada(string placa, DateTime horarioDeChegada, DateTime horarioDeSaida, TabelaDePrecos tabelaDePrecos)
    {
        TimeSpan duracao = CalcularDuracao(horarioDeChegada, horarioDeSaida);
        int tempoCobrado = CalcularTempoCobrado(horarioDeChegada, horarioDeSaida);

        string tempoCobradoStr = tempoCobrado == 0 ? "0,5" : tempoCobrado.ToString();

        int valorAPagar;
        if (tempoCobrado == 0)
        {
            valorAPagar = tabelaDePrecos.PrecoDaHoraInicial / 2;
        }
        else
        {
            valorAPagar = tabelaDePrecos.PrecoDaHoraInicial + (tempoCobrado - 1) * tabelaDePrecos.PrecoDaHoraAdicional;
        }

        var veiculo = new Veiculo(
            placa,
            horarioDeChegada,
            horarioDeSaida,
            duracao,
            tempoCobradoStr,
            valorAPagar
        );

        EscreverCsv(veiculo);
    }

    private void EscreverCsv(Veiculo veiculo)
    {
        string pastaDestino = Path.Combine(Directory.GetCurrentDirectory(), "Data");
        string caminhoCsv = Path.Combine(pastaDestino, "dados.csv");

        string linhaCsv = string.Join(",",
            veiculo.Placa,
            veiculo.HorarioDeChegada.ToString("dd/MM/yyyy HH:mm"),
            veiculo.HorarioDeSaida.ToString("dd/MM/yyyy HH:mm"),
            veiculo.Duracao.ToString("HH:mm:ss"),
            veiculo.TempoCobrado,
            veiculo.Preco,
            veiculo.ValorAPagar
        );

        using var escritor = new StreamWriter(caminhoCsv, append: true);
        escritor.WriteLine(linhaCsv);
    }

    private TimeSpan CalcularDuracao(DateTime horarioDeChegada, DateTime horarioDeSaida)
    {
        return (horarioDeSaida - horarioDeChegada).Duration();
    }

    private int CalcularTempoCobrado(DateTime horarioDeChegada, DateTime horarioDeSaida)
    {
        TimeSpan duracao = horarioDeSaida - horarioDeChegada;
        int horasCompletas = (int)duracao.TotalHours;

        if (duracao.Minutes > 10)
        {
            horasCompletas++;
        }

        return horasCompletas;
    }
}
