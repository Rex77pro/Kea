const Parser = require('./Parser');

const filePath = 'C:\\Users\\smaur\\Code\\Kea\\SystemIntegration\\Assignments\\01a\\Files\\Pokemon';

const XmlParser = new Parser(filePath + '.xml');

XmlParser.XmlFileParser()
    .then(data => {
        console.log('Parsed XMl ', JSON.stringify(data, null, 2));
    })
    .catch(error =>{
        console.error('Fejl: ', error)
    });
