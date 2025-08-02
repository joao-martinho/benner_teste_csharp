public class Program
{
    public void Main(string[] args)
    {
        ExecutarControle();
    }

    void ExecutarControle()
    {
        DateTime inicioDaVigencia = new DateTime(2024, 1, 1);
        DateTime fimDaVigencia = new DateTime(2024, 12, 31);
        int precoDaHoraInicial = 2;
        int precoDaHoraAdicional = 1;

        var tabelaDePrecos = new TabelaDePrecos(inicioDaVigencia, fimDaVigencia, precoDaHoraInicial, precoDaHoraAdicional);
        var estacionamento = new Estacionamento();

        Console.WriteLine("CONTROLE DE ESTACIONAMENTO");
        Console.WriteLine("==========================");

        Console.Write("Placa do veículo: ");
        string placa = Console.ReadLine();

        Console.Write("Horário de entrada (dd/mm/yyyy HH:mm:ss): ");
        DateTime dataDeEntrada = DateTime.Parse(Console.ReadLine()); // TODO: try/catch

        Console.Write("Horário de saída (dd/mm/yyyy HH:mm:ss): ");
        DateTime dataDeSaida = DateTime.Parse(Console.ReadLine()); // TODO: try/catch
    
        estacionamento.MarcarEntrada(placa, dataDeEntrada, dataDeSaida, tabelaDePrecos);
    }
}
