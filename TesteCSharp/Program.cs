public class Program
{
    public static void Main(string[] args)
    {
        ExecutarControle();
    }

    static void ExecutarControle()
    {
        string ?opcao;

        do
        {
            Console.WriteLine("CONTROLE DE ESTACIONAMENTO");
            Console.WriteLine("==========================");
            Console.WriteLine("1) Marcar entrada");
            Console.WriteLine("2) Buscar veículo");
            Console.WriteLine("3) Sair");

            Console.Write("Opção: ");
            opcao = Console.ReadLine();
            switch (opcao)
            {
                case "1":
                    MarcarEntrada();
                    break;
                case "2":
                    Console.WriteLine(BuscarVeiculo());
                    break;
                case "3":
                    break;
                default:
                    Console.WriteLine("ERRO: opção inválida.");
                    break;
            }

        } while (opcao != "3");
        
    }

    static void MarcarEntrada()
    {
        DateTime inicioDaVigencia = new DateTime(2024, 1, 1);
        DateTime fimDaVigencia = new DateTime(2024, 12, 31);
        int precoDaHoraInicial = 2;
        int precoDaHoraAdicional = 1;

        var tabelaDePrecos = new TabelaDePrecos(inicioDaVigencia, fimDaVigencia, precoDaHoraInicial, precoDaHoraAdicional);
        var service = new Service();

        string placa;
            do
            {
                Console.Write("Placa do veículo: ");
                placa = Console.ReadLine()?.Trim()!;

                if (string.IsNullOrWhiteSpace(placa))
                {
                    Console.WriteLine("ERRO: a placa não pode estar vazia.");
                }

            } while (string.IsNullOrWhiteSpace(placa));

            DateTime dataDeEntrada = LerData("Horário de entrada (dd/MM/yyyy HH:mm:ss): ");
            DateTime dataDeSaida;

            do
            {
                dataDeSaida = LerData("Horário de saída (dd/MM/yyyy HH:mm:ss): ");

                if (dataDeSaida <= dataDeEntrada)
                {
                    Console.WriteLine("ERRO: a data de saída deve ser posterior à data de entrada.");
                }

            } while (dataDeSaida <= dataDeEntrada);

        service.MarcarEntrada(placa!, dataDeEntrada, dataDeSaida, tabelaDePrecos);
    }

    static string BuscarVeiculo()
    {
        Console.Write("Placa do veículo a buscar: ");
        string placa = Console.ReadLine()?.Trim()!;

        var service = new Service();
        var veiculo = service.BuscarVeiculo(placa);

        string retorno =
        "---------------" + "\n" +
        "Placa: " + veiculo.Placa + "\n" +
        "Horário de chegada: " + veiculo.HorarioDeChegada + "\n" +
        "Horário de saída: " + veiculo.HorarioDeSaida + "\n" +
        "Duração: " + veiculo.Duracao + "\n" +
        "Tempo cobrado (hora): " + veiculo.TempoCobrado + "\n" +
        "Preço: " + veiculo.Preco + "\n" +
        "Valor a pagar: " + veiculo.ValorAPagar + "\n" +
        "---------------";

        return retorno;
    }

    static DateTime LerData(string mensagem)
    {
        DateTime data;
        string formato = "dd/MM/yyyy HH:mm:ss";

        while (true)
        {
            Console.Write(mensagem);
            string? entrada = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(entrada))
            {
                Console.WriteLine("ERRO: o campo de data não pode estar vazio.");
                continue;
            }

            if (DateTime.TryParseExact(entrada, formato, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out data))
            {
                return data;
            }
            else
            {
                Console.WriteLine($"ERRO: formato inválido. Use o formato {formato}.");
            }
        }
    }
    
}
