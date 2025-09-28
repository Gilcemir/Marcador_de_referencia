namespace Marcador_de_referencia.BibliographyHandlers
{
    public interface IHandler
    {
        IHandler SetNext(IHandler handler);

        BibliographyResult Handle(string body);
    }
}
