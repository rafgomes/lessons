

class IDispanchange {

   
}

class Padrao extends IDispanchange{
   
}

class DispachaProdutosDigitais extends IDispanchange{
    Disparchar() {
        console.log("Enviar por Email ");
    }
}

class DispachaProdutoCorreio extends IDispanchange{
    Disparchar() {
        console.log("Enviar por Correio ");
    }
}


const padrao = new Padrao();
const produtoDigital = new DispachaProdutosDigitais();
const correios = new DispachaProdutoCorreio()
const lista = [padrao, produtoDigital, correios];

lista.forEach( item => item.Disparchar());


