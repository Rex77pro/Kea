// const Parser = require('./Parser');
// const Pokemon = require ('./Pokemon');

import { ParseCSV, ParseJson, ParseTxt, ParseXml, ParseYaml } from './ModuleParser.js';

const filePath = 'C:\\Users\\smaur\\Code\\Kea\\SystemIntegration\\Assignments\\01a\\Files\\Pokemon';
const CsvFilePath = filePath + '.csv';
const JsonFilePath = filePath + '.json';
const TxtFilePath = filePath + '.txt';
const XmlFilePath = filePath + '.xml';
const YamlFilePath = filePath + '.yml';

/* Parsers w. CommonJS
const XmlParser = new Parser(filePath + '.xml');
const YamlParser = new Parser(filePath + '.yml');
const JsonParser = new Parser(filePath + '.json');
const TxtParser = new Parser(filePath + '.txt');

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
*/

ParseCSV(CsvFilePath)
    .then(data => {
    console.log('Parsed CSV data:', data);
    })
    .catch(error => {
    console.error('Error parsing CSV:', error);
    });

ParseJson(JsonFilePath)
    .then(data => {
    console.log('Parsed Json data:', data);
    })
    .catch(error => {
    console.error('Error parsing Json:', error);
    });

ParseTxt(TxtFilePath)
    .then(data => {
    console.log('Parsed Txt data:', data);
    })
    .catch(error => {
    console.error('Error parsing Txt:', error);
    });

ParseXml(XmlFilePath)
    .then(data => {
    console.log('Parsed Xml data:', data);

    const typesArray = data.pokemon.types.type
    console.log('Types: ', typesArray)
    })
    .catch(error => {
    console.error('Error parsing Xml:', error);
    });

ParseYaml(YamlFilePath)
    .then(data => {
    console.log('Parsed Yaml data:', data);
    })
    .catch(error => {
    console.error('Error parsing Yaml:', error);
    });