using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Xml;
using System.Xml.Serialization;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

static class FileReader{
    private static string filePath = @"..\..\..\..\Files\Pokemon";

    public static void ReadXmlFile()
    {
        using (FileStream fs = File.OpenRead(filePath + ".xml"))
        {
            Pokemon? pokemon = (Pokemon?)new XmlSerializer(typeof(Pokemon)).Deserialize(fs);

            System.Console.WriteLine("XML File");
            System.Console.WriteLine(pokemon?.Name);
            System.Console.WriteLine();

        }
    } 

    public static void ReadYamlFile()
    {
        var yamlContent = File.ReadAllText(filePath + ".yml");

        var deserializer = new DeserializerBuilder().Build();
        
        var yamlObject = deserializer.Deserialize<object>(yamlContent);

        var serializer = new SerializerBuilder()
            .Build();

        var yamlString = serializer.Serialize(yamlObject);

        System.Console.WriteLine("Yaml File");
        System.Console.WriteLine(yamlString);
        System.Console.WriteLine();
    }

    public static void ReadTextFile()
    {

    }

    public static void ReadCsvFile()
    {

    }

    public static void ReadJsonFile()
    {
        using (FileStream fs = File.OpenRead(filePath + ".json"))
        {
            Pokemon? pokemon = JsonSerializer.Deserialize<Pokemon>(fs);

            if (pokemon != null)
            {
                System.Console.WriteLine("Json File");
                System.Console.WriteLine(pokemon.Name);
                System.Console.WriteLine();
            }
            else
            {
                System.Console.WriteLine();
            }
        }
    }
}