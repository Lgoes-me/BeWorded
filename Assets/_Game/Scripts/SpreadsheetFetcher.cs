using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;

public class SpreadsheetFetcher
{
    public List<T> FetchSpreadsheet<T>(string data) where T : new()
    {
        var spreadsheet = ToListOfStrings(data);

        if (spreadsheet.Count == 0)
            throw new InvalidOperationException("Planilha está vazia");

        var propertyInfos = PropertyInfos<T>(spreadsheet);
        return Response<T>(spreadsheet, propertyInfos);
    }

    private List<List<string>> ToListOfStrings(string data)
    {
        //Separator de linha "\r\n"
        //Separator de coluna "\t"

        return data
            .Split("\r\n")
            .Where(s => !string.IsNullOrWhiteSpace(s))
            .Select(s => s.Split('\t').ToList())
            .ToList();
    }

    private static Dictionary<int, PropertyInfo> PropertyInfos<T>(List<List<string>> spreadsheet) where T : new()
    {
        var properties = typeof(T).GetProperties().ToList();
        var dict = new Dictionary<int, PropertyInfo>();

        //Cabecalho pode ter colunas a mais por retrocompatibilidade,
        //mas processo deve acabar se tiverem colunas faltando
        foreach (var property in properties)
        {
            var header = spreadsheet[0];

            if (header.Contains(property.Name))
            {
                dict.Add(header.FindIndex(v => v == property.Name), property);
            }
            else
            {
                throw new InvalidOperationException(
                    "Propriedade do objeto não encontrada no cabeçalho da planilha");
            }
        }

        return dict;
    }

    private static List<T> Response<T>(
        List<List<string>> spreadsheet,
        Dictionary<int, PropertyInfo> propertyInfos) where T : new()
    {
        var response = new List<T>();

        //Ignora o cabeçalho
        for (var index = 1; index < spreadsheet.Count; index++)
        {
            var item = new T();
            List<string> parameters = spreadsheet[index];

            for (var i = 0; i < parameters.Count; i++)
            {
                var value = parameters[i];

                if (propertyInfos.TryGetValue(i, out var propertyInfo))
                {
                    Type safeType = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;
                    object safeValue = string.IsNullOrWhiteSpace(value)
                        ? null
                        : Convert.ChangeType(value, safeType, CultureInfo.InvariantCulture);

                    propertyInfo.SetValue(item, safeValue);
                }
            }

            response.Add(item);
        }

        return response;
    }
}