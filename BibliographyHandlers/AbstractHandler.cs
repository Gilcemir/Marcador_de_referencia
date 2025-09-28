namespace Marcador_de_referencia.BibliographyHandlers
{
    public abstract class AbstractHandler : IHandler
    {
        private IHandler? _next;

        public virtual BibliographyResult Handle(string body)
        {
            if (_next != null)
            {
                return _next.Handle(body);
            }

            return new BibliographyResult(body, ERefType.book);
        }

        public IHandler SetNext(IHandler handler)
        {
            _next = handler;

            return _next;
        }
    }
}
