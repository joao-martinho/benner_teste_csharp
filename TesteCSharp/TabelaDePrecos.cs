public class TabelaDePrecos
{
    public DateTime InicioDaVigencia { get; set; }
    public DateTime FimDaVigencia { get; set; }

    public int PrecoDaHoraInicial { get; set; }
    public int PrecoDaHoraAdicional { get; set; }

    // construtor com todos os argumentos
    public TabelaDePrecos(DateTime inicioDaVigencia, DateTime fimDaVigencia, int precoDaHoraInicial, int precoDaHoraAdicional)
    {
        InicioDaVigencia = inicioDaVigencia;
        FimDaVigencia = fimDaVigencia;
        PrecoDaHoraInicial = precoDaHoraInicial;
        PrecoDaHoraAdicional = precoDaHoraAdicional;
    }

    // construtor sem argumentos
    public TabelaDePrecos()
    {
        // intencionalmente vazio
    }
}
