public class Program
{
    public static void Main(string[] args)
    {
        ExecutarControle();
    }

    static void ExecutarControle()
    {
        DateTime inicioDaVigencia = new DateTime(2024, 1, 1);
        DateTime fimDaVigencia = new DateTime(2024, 12, 31);
        int precoDaHoraInicial = 2;
        int precoDaHoraAdicional = 1;

        var tabelaDePrecos = new TabelaDePrecos(inicioDaVigencia, fimDaVigencia, precoDaHoraInicial, precoDaHoraAdicional);
        var estacionamento = new Estacionamento();

        Console.WriteLine("CONTROLE DE ESTACIONAMENTO");
        Console.WriteLine("--------------------------");

        string placa;
        do
        {
            Console.Write("Placa do veículo: ");
            placa = Console.ReadLine()?.Trim();

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

        estacionamento.MarcarEntrada(placa, dataDeEntrada, dataDeSaida, tabelaDePrecos);
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
