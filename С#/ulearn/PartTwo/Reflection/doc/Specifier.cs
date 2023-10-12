using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Documentation;

public class Specifier<T> : ISpecifier
{
    private Type type { get => typeof(T); }

    public string GetApiDescription()
    {
        var attributes = type.GetCustomAttributes(false).OfType<ApiDescriptionAttribute>().FirstOrDefault();
        return attributes?.Description;
    }

    public string[] GetApiMethodNames()
    {
        var methods = type.GetMethods()
            .Where(m => m.GetCustomAttributes(typeof(ApiMethodAttribute), false).Length > 0);
        return methods.Select(m => m.Name).ToArray();
    }

    public string GetApiMethodDescription(string methodName)
    {
        var method = type.GetMethod(methodName);
        var attribute = method?.GetCustomAttribute<ApiDescriptionAttribute>();
        return attribute?.Description;
    }

    public string[] GetApiMethodParamNames(string methodName)
    {
        var method = type.GetMethod(methodName);
        return method.GetParameters().Select(p => p.Name).ToArray();
    }

    public string GetApiMethodParamDescription(string methodName, string paramName)
    {
        var method = type.GetMethod(methodName);
        var parameter = method?.GetParameters().FirstOrDefault(p => p.Name == paramName);
        var attribute = parameter?.GetCustomAttribute<ApiDescriptionAttribute>();
        return attribute?.Description;
    }

    public ApiParamDescription GetApiMethodParamFullDescription(string methodName, string paramName)
    {
        var method = type.GetMethod(methodName);
        var parameter = method?.GetParameters().FirstOrDefault(p => p.Name == paramName);
        var e = parameter?.GetCustomAttribute<ApiIntValidationAttribute>();
        var attribute = parameter?.GetCustomAttribute<ApiDescriptionAttribute>();

        return new ApiParamDescription
        {
            ParamDescription = new CommonDescription(paramName, attribute?.Description),
            MinValue = e?.MinValue,
            MaxValue = e?.MaxValue,
            Required = parameter is not null
            && parameter.GetCustomAttribute<ApiRequiredAttribute>() != null
            && parameter.GetCustomAttribute<ApiRequiredAttribute>().Required
        };
    }

    public ApiMethodDescription GetApiMethodFullDescription(string methodName)
    {
        if (!GetApiMethodNames().Contains(methodName)) return null;
        var method = type.GetMethod(methodName);
        var parameter = method?.GetParameters();
        var paramDescriptions = new List<ApiParamDescription>();
        ApiParamDescription returnDescription = null;

        if (method.ReturnTypeCustomAttributes.IsDefined(typeof(Attribute), false))
        {
            returnDescription = new ApiParamDescription();
            if (method.ReturnTypeCustomAttributes.IsDefined(typeof(ApiRequiredAttribute), false))
            {
                returnDescription.ParamDescription = new CommonDescription();
                returnDescription.Required = method.ReturnTypeCustomAttributes
                    .GetCustomAttributes(false)
                    .OfType<ApiRequiredAttribute>()
                    .FirstOrDefault().Required;
            }
            if (method.ReturnTypeCustomAttributes.IsDefined(typeof(ApiIntValidationAttribute), false))
            {
                var e = method.ReturnTypeCustomAttributes.GetCustomAttributes(false)
                    .OfType<ApiIntValidationAttribute>().FirstOrDefault();
                returnDescription.MinValue = e.MinValue;
                returnDescription.MaxValue = e.MaxValue;
            }
        }
        foreach (var param in parameter)
        {
            paramDescriptions.Add(GetApiMethodParamFullDescription(methodName, param.Name));
        }
        return new ApiMethodDescription
        {
            MethodDescription = new CommonDescription(methodName, GetApiMethodDescription(methodName)),
            ParamDescriptions = paramDescriptions.ToArray(),
            ReturnDescription = returnDescription
        };
    }
}