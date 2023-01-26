using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConsumindoApiAluno.Entities.Services
{
    public class AlunoServices
    {
        public async Task<Aluno> Integracao(int id)
        {
            HttpClient httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"https://localhost:7068/Aluno/{id}");
            var jsonString = await response.Content.ReadAsStringAsync();

            Aluno jsonObject = JsonConvert.DeserializeObject<Aluno>(jsonString);

            if (jsonObject != null)
            {
                return jsonObject;
            }
            else
            {
                return new Aluno
                {
                    Verificacao = true
                };
            }

        }
    }
}
