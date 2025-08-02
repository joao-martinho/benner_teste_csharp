public class Estacionamento
{
    public void MarcarEntrada(string placa, DateTime horarioDeChegada, DateTime horarioDeSaida, TabelaDePrecos tabelaDePrecos)
    {

        // medida defensiva caso o método seja acessado de fora de Program.cs
        if (horarioDeSaida <= horarioDeChegada)
        {
            throw new ArgumentException("ERRO: o horário de saída deve ser posterior ao horário de chegada.");
        }

        TimeSpan duracao = CalcularDuracao(horarioDeChegada, horarioDeSaida);
        int tempoCobrado = CalcularTempoCobrado(horarioDeChegada, horarioDeSaida);

        string tempoCobradoStr = tempoCobrado == 0 ? "0.5" : tempoCobrado.ToString();

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
        string linhaCsv = string.Join(",",
            veiculo.Placa,
            veiculo.HorarioDeChegada.ToString("dd/MM/yyyy HH:mm:ss"),
            veiculo.HorarioDeSaida.ToString("dd/MM/yyyy HH:mm:ss"),
            veiculo.Duracao.ToString(@"hh\:mm\:ss"),
            veiculo.TempoCobrado,
            veiculo.Preco,
            veiculo.ValorAPagar
        );

        try
        {
            using var escritor = new StreamWriter("dados.csv", append: true);
            escritor.WriteLine(linhaCsv);
        }
        catch (IOException e)
        {
            Console.WriteLine($"Erro ao salvar os dados no arquivo: {e.Message}");
        }
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
