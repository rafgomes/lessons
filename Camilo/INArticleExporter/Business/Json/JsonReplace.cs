public class JsonReplace
{
    private string json;

    public JsonReplace(string json)
    {
        this.json = json;
    }

    public string GetJson()
    {
        Replaces();

        return this.json;
    }

    private void Replaces()
    {
        this.json = this.json.Replace("O P R E S I D E N T E D A R E P Ú B L I C A", "O PRESIDENTE DA REPÚBLICA");
        this.json = this.json.Replace("O PRESIDENTE DA REPÚBLICAO", "O PRESIDENTE DA REPÚBLICA O");
        this.json = this.json.Replace("O PRESIDENTE DA REPÚBLICAA", "O PRESIDENTE DA REPÚBLICA A");

        this.json = this.json.Replace("\"assinatura\" :", "").Replace("\"assinatura\":", "");

        // Me.json = Me.json.Replace(".jpg", "")

        this.json = this.json.Replace("Destaques Do Diário Oficial da União", "DESTAQUE_DOU");
        this.json = this.json.Replace("Concursos e Seleções", "CONCURSO_SELECAO");
        this.json = this.json.Replace("Destaques Especiais", "DESTAQUE_ESPECIAL");
    }
}
