﻿using PostSharp.Aspects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INPerformanceTest.Extensions
{
    internal static class Formatter
    {
        public static void AppendTypeName(this StringBuilder stringBuilder, Type declaringType)
        {
            stringBuilder.Append(declaringType.FullName);
            if (declaringType.IsGenericType)
            {
                var genericArguments = declaringType.GetGenericArguments();
                AppendGenericArguments(stringBuilder, genericArguments);
            }
        }

        public static void AppendGenericArguments(this StringBuilder stringBuilder, Type[] genericArguments)
        {
            stringBuilder.Append('<');
            for (var i = 0; i < genericArguments.Length; i++)
            {
                if (i > 0)
                {
                    stringBuilder.Append(", ");
                }

                stringBuilder.Append(genericArguments[i].Name);
            }
            stringBuilder.Append('>');
        }

        public static void AppendArguments(this StringBuilder stringBuilder, Arguments arguments)
        {
            stringBuilder.Append('(');
            for (var i = 0; i < arguments.Count; i++)
            {
                if (i > 0)
                {
                    stringBuilder.Append(", ");
                }

                stringBuilder.Append(arguments[i]);
            }
            stringBuilder.Append(')');
        }
    }
}
