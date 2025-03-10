using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Xml;
using System.Xml.Serialization;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

public static class FileReader
{
    private static string filePath = @"..\..\..\..\Files\Pokemon";

    public static Pokemon ReadXmlFile()
    {
        string fullPath = @"C:\Users\smaur\Code\Kea\SystemIntegration\Assignments\01a\Files\Pokemon.xml";
        using (FileStream fs = File.OpenRead(fullPath))
        {
            Pokemon? pokemon = (Pokemon?)new XmlSerializer(typeof(Pokemon)).Deserialize(fs);

            System.Console.WriteLine("XML File");
            System.Console.WriteLine(pokemon?.NationalNo);
            System.Console.WriteLine(pokemon?.Name);
            Console.WriteLine("Types: " + string.Join(", ", pokemon.Types ?? new List<string>()));
            System.Console.WriteLine();
            return pokemon;
        }
    } 

    public static Pokemon? ReadYamlFile()
    {
        string fullPath = @"C:\Users\smaur\Code\Kea\SystemIntegration\Assignments\01a\Files\Pokemon.yml";
        var yamlContent = File.ReadAllText(fullPath);

        // Byg en deserializer med en naming convention (f.eks. CamelCase) for at matche dine property-navne
        var deserializer = new DeserializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .Build();
        
        // Deserialiser YAML-indholdet direkte til et Pokemon-objekt
        PokemonWrapper? wrapper = deserializer.Deserialize<PokemonWrapper>(yamlContent);
        Pokemon? pokemon = wrapper?.Pokemon;

        Console.WriteLine("Yaml File");
        if (pokemon != null)
        {
            Console.WriteLine($"NationalNo: {pokemon.NationalNo}");
            Console.WriteLine($"Name: {pokemon.Name}");
            Console.WriteLine("Types: " + string.Join(", ", pokemon.Types ?? new List<string>()));
        }
        else
        {
            Console.WriteLine("Kunne ikke deserialisere YAML-filen til et Pokemon-objekt.");
        }
        Console.WriteLine();
        
        return pokemon;
    }

   public static Pokemon ReadTxtFile()
    {
        string fullPath = @"C:\Users\smaur\Code\Kea\SystemIntegration\Assignments\01a\Files\Pokemon.txt";
        using (FileStream fs = File.OpenRead(fullPath))
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
            return pokemon;
        }
    }

    public static Pokemon ReadCsvFile()
    {
        string fullPath = @"C:\Users\smaur\Code\Kea\SystemIntegration\Assignments\01a\Files\Pokemon.csv";
        using (FileStream fs = File.OpenRead(fullPath))
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
                return pokemon;
            }

        }
        return null;
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

    public static async Task<Pokemon> JsonFileReader()
    {
        // string fullPath = filePath + ".json";
        string fullPath = @"C:\Users\smaur\Code\Kea\SystemIntegration\Assignments\01a\Files\Pokemon.json";

        if(!File.Exists(fullPath))
        {
            Console.WriteLine("Filen blev ikke fundet: " + fullPath);
            return null;
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
                    return pokemon;
                }
                else
                {
                    Console.WriteLine("Nøglen 'pokemon' blev ikke fundet i JSON-filen.");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Der opstod en fejl under parsing af JSON: " + ex.Message);
                return null;
            }
    }
}