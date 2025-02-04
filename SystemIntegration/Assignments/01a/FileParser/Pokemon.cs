using System.Text.Json.Serialization;
using System.Xml.Serialization;

[XmlRoot (ElementName = "pokemon")]
public class Pokemon
{
    [JsonPropertyName("nationalNo")]
    [XmlElement ("nationalNo")]
    public int? NationalNo { get; set;}

    [JsonPropertyName("name")]
    [XmlElement ("name")]
    public string? Name { get; set; }

    [JsonPropertyName("types")]
    [XmlArray (ElementName = "types")]
    [XmlArrayItem (ElementName = "type")]
    public List<string>? Types { get; set; }
}