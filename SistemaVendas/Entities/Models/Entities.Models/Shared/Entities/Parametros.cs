namespace SistemaVendas.Core.Shared.Entities
{
    public class Parametros
    {
		const int TamanhoMaximoDaPagina = 50;
		public int NumeroDaPaginaAtual { get; set; }
		private int _tamanhoDaPagina = 5;
		public int TamanhoDaPagina
		{
			get
			{
				return _tamanhoDaPagina;
			}
			set
			{
				_tamanhoDaPagina = (value > TamanhoMaximoDaPagina) ? TamanhoMaximoDaPagina : value;
			}
		}
	}
}
