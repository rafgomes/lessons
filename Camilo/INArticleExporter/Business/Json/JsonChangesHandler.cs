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
using Imprensa.Business;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;

public class JsonChangesHandler : IJsonChangesHandler
{
    private readonly ILogger logger;

    public JsonChangesHandler(ILogger logger)
    {
        this.logger = logger;
    }

    public string GetJson(string json)
    {
        var jsonFix = new JsonElementFix(logger);

        var jsonResult = jsonFix.GetJson(json);

        var jsonReplace = new JsonReplace(jsonResult);

        jsonResult = jsonReplace.GetJson();

        return jsonResult;
    }
}

