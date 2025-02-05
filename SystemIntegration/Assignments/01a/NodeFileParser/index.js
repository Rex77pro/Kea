const Parser = require('./Parser');

const filePath = 'C:\\Users\\smaur\\Code\\Kea\\SystemIntegration\\Assignments\\01a\\Files\\Pokemon';

const XmlParser = new Parser(filePath + '.xml');

XmlParser.XmlFileParser()
    .then(data => {

        const pokemon = data.pokemon;
        const name = pokemon.name[0];
        const nationalNo = pokemon.nationalNo[0];
        const types = pokemon.types[0].type.join(", ");
        const resultString = `Pokemon: ${name} (#${nationalNo}), Types: ${types}`;
        
        console.log()
        console.log("Xml File: ");
        console.log(resultString);
        console.log()

        //console.log('Parsed XMl ', JSON.stringify(data, null, 2));
        
    })
    .catch(error =>{
        console.error('Fejl: ', error)
    });
