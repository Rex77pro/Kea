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

   public static void ReadTxtFile()
    {
        using (FileStream fs = File.OpenRead(filePath + ".txt"))
        using (StreamReader reader = new StreamReader(fs))
        {
            // Opret en tom Pokémon
            Pokemon pokemon = new Pokemon();

            string? line;
            while ((line = reader.ReadLine()) != null)
            {
                // Split linjen ved ": "
                string[] parts = line.Split(": ", 2); // 2 sikrer, at vi kun splitter én gang

                if (parts.Length == 2)
                {
                    string key = parts[0].Trim().ToLower(); // Gør nøglen lille for nem sammenligning
                    string value = parts[1].Trim();

                    // Tildel værdier baseret på nøglen
                    switch (key)
                    {
                        case "national no":
                            pokemon.NationalNo = int.Parse(value);
                            break;
                        case "name":
                            pokemon.Name = value;
                            break;
                        case "types":
                            pokemon.Types = new List<string>(value.Split(", "));
                            break;
                    }
                }
            }

            // Udskriv Pokémon-data
            System.Console.WriteLine("Txt File");
            Console.WriteLine($"#{pokemon.NationalNo} - {pokemon.Name} ({string.Join(", ", pokemon.Types)})");
            System.Console.WriteLine();
        }
    }

    public static void ReadCsvFile()
    {
        using (FileStream fs = File.OpenRead(filePath + ".csv"))
        using (StreamReader reader = new StreamReader(fs))
        {
            reader.ReadLine();

            string? line = reader.ReadLine();
            if (line != null)
            {
                string[] values = line.Split(", ");

                Pokemon pokemon = new Pokemon
                {
                    NationalNo = int.Parse(values[0]),
                    Name = values[1],
                    Types = new List<string> { values[2] } 
                };
                System.Console.WriteLine("Csv File");
                System.Console.WriteLine($"#{pokemon.NationalNo} -- {pokemon.Name} -- {string.Join(", ", pokemon.Types)}");
                System.Console.WriteLine();
            }

        }
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