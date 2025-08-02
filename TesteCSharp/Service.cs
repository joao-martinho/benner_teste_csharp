public class Service
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

        int valorAPagarInt;
        if (tempoCobrado == 0)
        {
            valorAPagarInt = tabelaDePrecos.PrecoDaHoraInicial / 2;
        }
        else
        {
            valorAPagarInt = tabelaDePrecos.PrecoDaHoraInicial + (tempoCobrado - 1) * tabelaDePrecos.PrecoDaHoraAdicional;
        }

        string valorAPagar = valorAPagarInt.ToString();
        string preco = tabelaDePrecos.PrecoDaHoraInicial.ToString();

        var veiculo = new Veiculo(
            placa,
            horarioDeChegada,
            horarioDeSaida,
            duracao,
            preco,
            tempoCobradoStr,
            valorAPagar
        );

        EscreverCsv(veiculo);
    }

    public Veiculo BuscarVeiculo(string placa)
    {
        Console.WriteLine("service");
        string horarioDeChegadaStr = "";
        string horarioDeSaidaStr = "";
        string duracaoStr = "";
        string tempoCobrado = "";
        string preco = "";
        string valorAPagar = "";


        foreach (var linha in File.ReadLines("dados.csv"))
        {
            var partes = linha.Split(',');

            if (partes[0].Trim() == placa)
            {
                Console.WriteLine("dentro do if");
                horarioDeChegadaStr = partes[1];
                horarioDeSaidaStr = partes[2];
                duracaoStr = partes[3];
                tempoCobrado = partes[4];
                preco = partes[5];
                valorAPagar = partes[6];
                Console.WriteLine(valorAPagar);

                break;
            }
        }

        DateTime horarioDeChegada = DateTime.Parse(horarioDeChegadaStr);
        DateTime horarioDeSaida = DateTime.Parse(horarioDeSaidaStr);
        TimeSpan duracao = TimeSpan.Parse(duracaoStr);

        var veiculo = new Veiculo(placa, horarioDeChegada, horarioDeSaida, duracao, tempoCobrado, preco, valorAPagar);

        return veiculo;
    }

    private void EscreverCsv(Veiculo veiculo)
    {
        string linhaCsv = string.Join(
            veiculo.Placa,
            veiculo.HorarioDeChegada.ToString("dd/MM/yyyy HH:mm:ss"),
            veiculo.HorarioDeSaida.ToString("dd/MM/yyyy HH:mm:ss"),
            veiculo.Duracao.ToString(@"hh\:mm\:ss"),
            veiculo.TempoCobrado,
            "R$ " + veiculo.Preco + ".00",
            "R$ " + veiculo.ValorAPagar + ".00"
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
