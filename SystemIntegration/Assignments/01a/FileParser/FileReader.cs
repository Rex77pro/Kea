using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Xml;
using System.Xml.Serialization;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

static class FileReader
{
    private static string filePath = @"..\..\..\..\Files\Pokemon";

    public static void ReadXmlFile()
    {
        using (FileStream fs = File.OpenRead(filePath + ".xml"))
        {
            Pokemon? pokemon = (Pokemon?)new XmlSerializer(typeof(Pokemon)).Deserialize(fs);

            System.Console.WriteLine("XML File");
            System.Console.WriteLine(pokemon?.NationalNo);
            System.Console.WriteLine(pokemon?.Name);
            Console.WriteLine("Types: " + string.Join(", ", pokemon.Types ?? new List<string>()));
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
                        case "nationalno":
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
                System.Console.WriteLine($"#{pokemon.NationalNo} - {pokemon.Name} - {string.Join(", ", pokemon.Types)}");
                System.Console.WriteLine();
            }

        }
    }

    public static async void ReadJsonFile()
    {
        string fullPath = filePath + ".json";

        if(!File.Exists(fullPath))
        {
            Console.WriteLine("Filen blev ikke fundet: " + fullPath);
            return;
        }

        try 
        {
            using FileStream fs = File.OpenRead(fullPath);
            var opts = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            Pokemon? pokemon = await JsonSerializer.DeserializeAsync<Pokemon>(fs, opts);

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
        } catch (Exception ex)
        {
            Console.WriteLine("Der opstod en fejl under parsing af JSON: " + ex.Message);
        }
        
    }

    public static async Task JsonFileReader()
    {
        string fullPath = filePath + ".json";

        if(!File.Exists(fullPath))
        {
            Console.WriteLine("Filen blev ikke fundet: " + fullPath);
            return;
        }

        try
            {
                using FileStream fs = File.OpenRead(fullPath);
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                // Brug JsonDocument til at læse hele JSON-strukturen
                using JsonDocument doc = await JsonDocument.ParseAsync(fs);
                JsonElement root = doc.RootElement;

                // Tjek om "pokemon"-noden findes
                if (root.TryGetProperty("pokemon", out JsonElement pokemonElement))
                {
                    // Deserialiser "pokemon"-noden til et Pokemon-objekt
                    Pokemon? pokemon = JsonSerializer.Deserialize<Pokemon>(pokemonElement.GetRawText(), options);

                    if (pokemon != null)
                    {
                        Console.WriteLine("JSON File:");
                        Console.WriteLine($"National No: {pokemon.NationalNo}");
                        Console.WriteLine($"Name: {pokemon.Name}");
                        Console.WriteLine("Types: " + string.Join(", ", pokemon.Types ?? new List<string>()));
                    }
                    else
                    {
                        Console.WriteLine("Kunne ikke deserialisere 'pokemon'-noden.");
                    }
                }
                else
                {
                    Console.WriteLine("Nøglen 'pokemon' blev ikke fundet i JSON-filen.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Der opstod en fejl under parsing af JSON: " + ex.Message);
            }
    }
}