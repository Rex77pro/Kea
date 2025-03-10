using System;
using static FileReader;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpClient();
builder.Services.AddOpenApi();

var app = builder.Build();

app.UseHttpsRedirection();

app.MapGet("/nodedata", async ([FromServices] IHttpClientFactory httpClientFactory) =>
{
    var client = httpClientFactory.CreateClient();
    // Brug den korrekte URL til din Node-server
    var response = await client.GetAsync("http://127.0.0.1:8080/nodedata");
    if (response.IsSuccessStatusCode)
    {
        var data = await response.Content.ReadAsStringAsync();
        return Results.Ok(data);
    }
    else
    {
        return Results.Problem("Fejl ved hentning af data fra Node-serveren");
    }
})
.WithName("GetNodeData");

app.MapGet("/senddata", async ([FromServices] IHttpClientFactory httpClientFactory) => 
{
    var jsonData = await FileReader.JsonFileReader();
    var xmlData = FileReader.ReadXmlFile();
    var csvData = FileReader.ReadCsvFile();
    var txtData = FileReader.ReadTxtFile();
    var yamlData = FileReader.ReadYamlFile();

    var combinedData = new {
        timestamp = DateTime.UtcNow.ToString("o"),
        jsonData,
        xmlData,
        csvData,
        txtData,
        yamlData
    };

    var client = httpClientFactory.CreateClient();

    var response = await client.PostAsJsonAsync("http://127.0.0.1:8080/receiveData", combinedData);

    if (response.IsSuccessStatusCode)
    {
        return Results.Ok("Data sendt succesfuldt til Node-serveren!");
    }
    else
    {
        return Results.Problem("Fejl ved afsendelse af data til Node-serveren");
    }
})
.WithName("SendDataToNode");

app.Run();
