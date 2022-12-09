namespace Componentes
{
    public class Ferramentas
    {
        internal string MetodoSomenteParaMeuAssembly() //nao pode ser acessado pelo assembly Aula15
        {
            return "Este metodo so pode ser acessado dentro deste assembly componentes";
        }

        public string MetodoParaTodosQueUtilizarOAssembly()
        {
            return "Este metodo é para todos";
        }

    }
}