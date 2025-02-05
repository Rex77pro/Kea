const Parser = require('./Parser');
const Pokemon = require ('./Pokemon');

const filePath = 'C:\\Users\\smaur\\Code\\Kea\\SystemIntegration\\Assignments\\01a\\Files\\Pokemon';

const XmlParser = new Parser(filePath + '.xml');
const YamlParser = new Parser(filePath + '.yml');
const JsonParser = new Parser(filePath + '.json');
const TxtParser = new Parser(filePath + '.txt');
const CsvParser = new Parser(filePath + '.csv');


JsonParser.JsonFileParser()
    .then(data => {
        const pokemonData = data.pokemon;

        const newPokemon = new Pokemon({
            nationalNo: pokemonData.nationalNo,
            name: pokemonData.name,
            types: pokemonData.types
        });

        console.log();
        console.log("Json File");
        console.log(newPokemon.toString());
        console.log();
    })
    .catch(error => {
        console.error('Fejl: ', error);
    });

XmlParser.XmlFileParser()
    .then(data => {

        const pokemonData = data.pokemon;

        const newPokemon = new Pokemon({
            nationalNo: pokemonData.nationalNo[0],
            name: pokemonData.name[0],
            types: pokemonData.types[0].type
        });
        
        console.log("Xml File: ");
        console.log(newPokemon.toString());
        console.log()

        //console.log('Parsed XMl ', JSON.stringify(data, null, 2));
        
    })
    .catch(error =>{
        console.error('Fejl: ', error)
    });

YamlParser.YmlFileParser()
    .then(data => {
        const pokemonData = data.pokemon;

        const newPokemon = new Pokemon({
            nationalNo: pokemonData.nationalNo,
            name: pokemonData.name,
            types: pokemonData.types
        });

        console.log("Yaml File");
        console.log(newPokemon.toString());
        console.log();
    })
    .catch(error => {
        console.error('Fejl: ', error);
    });

TxtParser.TxtFileParser()
    .then(data => {
        const newPokemon = new Pokemon({
            nationalNo: data.nationalNo,
            name: data.name,
            types: data.types
        });

        console.log("TXT File:");
        console.log(newPokemon.toString());
        console.log();
    })
    .catch(error => {
        console.error('Fejl:', error);
    });

