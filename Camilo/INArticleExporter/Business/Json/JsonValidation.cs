using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Confluent.Kafka;
using Imprensa.Business.Exceptions;
using INPerformanceTest.Business.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using NJsonSchema;
using ILoggerFactory = INPerformanceTest.Interfaces.ILoggerFactory;

namespace Imprensa
{
    namespace Business
    {
        public class JsonValidation : IJsonValidation
        {
            private readonly ILogger logger;
            private readonly ILoggerFactory loggerFactory;
            private NJsonSchema.JsonSchema jSchema;

            private string schema = "{" + "  \"$schema\": \"http://json-schema.org/draft-06/schema#\"," + "  \"$ref\": \"#/definitions/Materia\"," + "  \"definitions\": {" + "    \"Materia\": {" + "      \"type\": \"object\"," + "      \"additionalProperties\": true," + "      \"properties\": {" + "        \"versao\": {" + "          \"type\": \"string\"" + "        }," + "        \"textoHTML\": {" + "          \"type\": \"string\"" + "        }," + "        \"textoEstruturado\": {" + "          \"$ref\": \"#/definitions/TextoEstruturado\"" + "        }," + "        \"estrutura\": {" + "          \"$ref\": \"#/definitions/Estrutura\"" + "        }," + "        \"metadados\": {" + "          \"$ref\": \"#/definitions/Metadados\"" + "        }" + "      }," + "      \"required\": [" + "        \"estrutura\"," + "        \"metadados\"," + "        \"textoHTML\"," + "        \"versao\"" + "      ]," + "      \"title\": \"Materia\"" + "    }," + "    \"Estrutura\": {" + "      \"type\": \"object\"," + "      \"additionalProperties\": true," + "      \"properties\": {" + "        \"identificacao\": {" + "          \"type\": \"string\"," + "          \"minLength\": 2," + "          \"maxLength\": 150," + "        }," + "        \"titulo\": {" + "          \"type\": [" + "            \"string\"," + "            \"null\"" + "          ]," + "          \"default\": null" + "        }," + "        \"subtitulo\": {" + "          \"type\": [" + "            \"string\"," + "            \"null\"" + "          ]," + "          \"default\": null" + "        }," + "        \"dataTexto\": {" + "          \"type\": [" + "            \"string\"," + "            \"null\"" + "          ]," + "          \"default\": null" + "        }," + "        \"ementa\": {" + "          \"type\": [" + "            \"string\"," + "            \"null\"" + "          ]," + "          \"default\": null" + "        }," + "        \"resumo\": {" + "          \"type\": [" + "            \"string\"," + "            \"null\"" + "          ]," + "          \"default\": null" + "        }," + "        \"assinaturas\": {" + "          \"type\": [" + "            \"array\"," + "            \"null\"" + "          ]," + "          \"default\": null," + "          \"items\": {" + "            \"$ref\": \"#/definitions/Assinatura\"" + "          }" + "        }" + "      }," + "      \"required\": [" + "        \"identificacao\"" + "      ]," + "      \"title\": \"Estrutura\"" + "    }," + "    \"Assinatura\": {" + "      \"type\": \"object\"," + "      \"additionalProperties\": true," + "      \"properties\": {" + "        \"assinante\": {" + "          \"type\": \"string\"" + "        }," + "        \"cargo\": {" + "          \"type\": [" + "            \"string\"," + "            \"null\"" + "          ]," + "          \"default\": null" + "        }" + "      }," + "      \"required\": []," + "      \"title\": \"Assinatura\"" + "    }," + "    \"Metadados\": {" + "      \"type\": \"object\"," + "      \"additionalProperties\": true," + "      \"properties\": {" + "        \"idMateria\": {" + "          \"type\": \"integer\"" + "        }," + "        \"idOficio\": {" + "          \"type\": [" + "            \"integer\"," + "            \"null\"" + "          ]," + "          \"default\": null" + "        }," + "        \"idXML\": {" + "          \"type\": [" + "            \"string\"," + "            \"null\"" + "          ]," + "          \"default\": null" + "        }," + "        \"ordem\": {" + "          \"type\": \"string\"" + "        }," + "        \"imprensaOficial\": {" + "          \"type\": \"string\"" + "        }," + "        \"idImprensaOficial\": {" + "          \"type\": \"integer\"" + "        }," + "        \"materiasComposicaoTexto\": {" + "          \"type\": \"array\"," + "          \"minItems\": 1," + "          \"items\": {" + "            \"$ref\": \"#/definitions/MateriaComposicaoTexto\"" + "          }" + "        }," + "        \"tipoAto\": {" + "          \"type\": \"string\"" + "        }," + "        \"idTipoAto\": {" + "          \"type\": \"integer\"" + "        }," + "        \"origem\": {" + "          \"$ref\": \"#/definitions/Origem\"" + "        }," + "        \"destaque\": {" + "          \"$ref\": \"#/definitions/Destaque\"" + "        }," + "        \"materiasRelacionadas\": {" + "          \"type\": [" + "            \"array\"," + "            \"null\"" + "          ]," + "          \"default\": null," + "          \"items\": {" + "            \"$ref\": \"#/definitions/MateriaRelacionada\"" + "          }" + "        }," + "        \"publicacao\": {" + "          \"$ref\": \"#/definitions/Publicacao\"" + "        }" + "      }," + "      \"required\": [" + "        \"idImprensaOficial\"," + "        \"idMateria\"," + "        \"idTipoAto\"," + "        \"idXML\"," + "        \"imprensaOficial\"," + "        \"ordem\"," + "        \"origem\"," + "        \"publicacao\"," + "        \"tipoAto\"," + "        \"materiasComposicaoTexto\"" + "      ]," + "      \"title\": \"Metadados\"" + "    }," + "    \"Destaque\": {" + "      \"type\": [" + "        \"object\"," + "        \"null\"" + "      ]," + "      \"default\": null," + "      \"additionalProperties\": true," + "      \"properties\": {" + "        \"tipo\": {" + "          \"type\": \"string\"" + "        }," + "        \"prioridade\": {" + "          \"type\": \"integer\"" + "        }," + "        \"texto\": {" + "          \"type\": \"string\"" + "        }," + "        \"nomeImagem\": {" + "          \"type\": [" + "            \"string\"," + "            \"null\"" + "          ]," + "          \"default\": null" + "        }," + "        \"fonteImagem\": {" + "          \"type\": [" + "            \"string\"," + "            \"null\"" + "          ]," + "          \"default\": null" + "        }" + "      }," + "      \"required\": [" + "        \"prioridade\"," + "        \"texto\"," + "        \"tipo\"" + "      ]," + "      \"title\": \"Destaque\"" + "    }," + "    \"MateriaComposicaoTexto\": {" + "      \"type\": \"object\"," + "      \"additionalProperties\": true," + "      \"properties\": {" + "        \"idMateria\": {" + "          \"type\": \"integer\"" + "        }," + "        \"idOficio\": {" + "          \"type\": [" + "            \"integer\"," + "            \"null\"" + "          ]," + "          \"default\": null" + "        }," + "        \"ordem\": {" + "          \"type\": [" + "            \"string\"," + "            \"null\"" + "          ]," + "          \"default\": null" + "        }" + "      }," + "      \"required\": [" + "        \"idMateria\"" + "      ]," + "      \"title\": \"MateriaComposicaoTexto\"" + "    }," + "    \"MateriaRelacionada\": {" + "      \"type\": \"object\"," + "      \"additionalProperties\": true," + "      \"properties\": {" + "        \"idXML\": {" + "          \"type\": \"string\"" + "        }," + "        \"ordem\": {" + "          \"type\": [" + "            \"string\"," + "            \"null\"" + "          ]," + "          \"default\": null" + "        }" + "      }," + "      \"required\": [" + "        \"idXML\"" + "      ]," + "      \"title\": \"MateriaRelacionada\"" + "    }," + "    \"Origem\": {" + "      \"type\": \"object\"," + "      \"additionalProperties\": true," + "      \"properties\": {" + "        \"idOrigem\": {" + "          \"type\": \"integer\"" + "        }," + "        \"nomeOrigem\": {" + "          \"type\": \"string\"" + "        }," + "        \"idSiorg\": {" + "          \"type\": [" + "            \"integer\"," + "            \"null\"" + "          ]," + "          \"default\": null" + "        }," + "        \"origemPai\": {" + "          \"$ref\": \"#/definitions/Origem\"" + "        }," + "        \"uf\": {" + "          \"type\": [" + "            \"string\"," + "            \"null\"" + "          ]," + "          \"default\": null" + "        }," + "        \"nomeMunicipio\": {" + "          \"type\": [" + "            \"string\"," + "            \"null\"" + "          ]," + "          \"default\": null" + "        }," + "        \"idMunicipioIbge\": {" + "          \"type\": [" + "            \"integer\"," + "            \"null\"" + "          ]," + "          \"default\": null" + "        }" + "      }," + "      \"required\": [" + "        \"idOrigem\"," + "        \"nomeOrigem\"" + "      ]," + "      \"title\": \"Origem\"" + "    }," + "    \"Publicacao\": {" + "      \"type\": \"object\"," + "      \"additionalProperties\": true," + "      \"properties\": {" + "        \"retificacaoTecnica\": {" + "          \"type\": \"boolean\"" + "        }," + "        \"jornal\": {" + "          \"$ref\": \"#/definitions/Jornal\"" + "        }," + "        \"dataPublicacao\": {" + "          \"type\": \"string\"," + "          \"format\": \"date\"" + "        }," + "        \"numeroPaginaPdf\": {" + "          \"type\": \"integer\"" + "        }," + "        \"urlVersaoOficialPdf\": {" + "          \"type\": [" + "            \"string\"," + "            \"null\"" + "          ]," + "          \"default\": null," + "          \"format\": \"uri\"," + "          \"qt-uri-protocols\": [" + "            \"http\"," + "            \"https\"" + "          ]" + "        }," + "        \"urlVersaoOficialHtml\": {" + "          \"type\": [" + "            \"string\"," + "            \"null\"" + "          ]," + "          \"default\": null," + "          \"format\": \"uri\"," + "          \"qt-uri-protocols\": [" + "            \"http\"," + "            \"https\"" + "          ]" + "        }" + "      }," + "      \"required\": [" + "        \"dataPublicacao\"," + "        \"jornal\"," + "        \"numeroPaginaPdf\"," + "        \"retificacaoTecnica\"" + "      ]," + "      \"title\": \"Publicacao\"" + "    }," + "    \"Jornal\": {" + "      \"type\": \"object\"," + "      \"additionalProperties\": true," + "      \"properties\": {" + "        \"ano\": {" + "          \"type\": [" + "            \"string\"," + "            \"null\"" + "          ]," + "          \"default\": null" + "        }," + "        \"idJornal\": {" + "          \"type\": \"integer\"" + "        }," + "        \"jornal\": {" + "          \"type\": \"string\"" + "        }," + "        \"numeroEdicao\": {" + "          \"type\": \"string\"" + "        }," + "        \"idSecao\": {" + "          \"type\": [" + "            \"integer\"," + "            \"null\"" + "          ]," + "          \"default\": null" + "        }," + "        \"secao\": {" + "          \"type\": [" + "            \"string\"," + "            \"null\"" + "          ]," + "          \"default\": null" + "        }," + "        \"isExtra\": {" + "          \"type\": \"boolean\"" + "        }," + "        \"isSuplemento\": {" + "          \"type\": \"boolean\"" + "        }" + "      }," + "      \"required\": [" + "        \"idJornal\"," + "        \"isExtra\"," + "        \"isSuplemento\"," + "        \"jornal\"," + "        \"numeroEdicao\"" + "      ]," + "      \"title\": \"Jornal\"" + "    }," + "    \"TextoEstruturado\": {" + "      \"type\": [" + "        \"object\"," + "        \"string\"," + "        \"null\"" + "      ]," + "      \"default\": null," + "      \"additionalProperties\": true," + "      \"title\": \"TextoEstruturado\"" + "    }" + "  }" + "}";

            public JsonValidation(ILogger logger, ILoggerFactory loggerFactory)
            {
                this.logger = logger;
                this.loggerFactory = loggerFactory;
            }

            public async Task Validate(string json)
            {
                try
                {
                    if (this.jSchema == null)
                        this.jSchema = await NJsonSchema.JsonSchema.FromJsonAsync(schema);

                    var jobj = JObject.Parse(json);

                    var errors = this.jSchema.Validate(json);

                    if (errors.Count > 0)
                    {
                        if (errors.Count > 0)
                            throw new InvalidJsonException(errors, json);
                    }
                }
                catch (InvalidJsonException jsonException)
                {
                    ILogger validationLogger = loggerFactory.CreateLogger("ValidationLogger");

                    validationLogger.LogInformation($"Erros: {jsonException.Message}");
                    validationLogger.LogInformation($"Json: {jsonException.Json}");
                    throw jsonException;

                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error");

                    throw ex;
                }
            }
        }
    }
}
