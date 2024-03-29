import { isNullOrUndefined } from 'util';

export class Parametros {
    NumeroDaPaginaAtual: number;
    TamanhoDaPagina: number;
    Filter?;
    constructor(
        tamanhoDaPagina: number,
        numeroDaPaginaAtual: number,
        filter?,
    ) {
        this.NumeroDaPaginaAtual = numeroDaPaginaAtual;
        this.TamanhoDaPagina = tamanhoDaPagina;
        if (!(isNullOrUndefined(filter) || '')) {
            this.Filter = filter;
        }

    }
}
