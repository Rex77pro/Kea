const { rejects } = require('assert');
const { isUtf8 } = require('buffer');
const fs = require('fs');
const { resolve } = require('path');
const xml2js = require('xml2js');
const yaml = require('js-yaml');
const { promiseHooks } = require('v8');
const { type } = require('os');

class Parser{
    constructor(filePath) {
        this.filePath = filePath
    }

    XmlFileParser()
    {
        return new Promise((resolve, reject) => {
            fs.readFile(this.filePath, 'utf-8', (err, data) => {
                if (err) {
                    return reject(`Fejl ved indlæsning af fil: ${err.message}`);
                }

                xml2js.parseString(data, (err, result) =>{
                    if (err) {
                        return reject(`Fejl ved parsing af xml: ${err.message}`)
                    }
                    resolve(result);
                });
            });
        });
    };

    YmlFileParser()
    {
        return new Promise((resolve, rejects) => {
            fs.readFile(this.filePath, 'utf-8', (err, data) => {
                if (err) {
                    return reject(`Fejl ved indlæsning af fil: ${err.message}`);
                }
                try
                {
                    const result = yaml.load(data);
                    resolve(result);
                }
                catch (parseErr) {
                    rejects(`Fejl med parsing af Yaml: ${parseErr.message}`);
                }
            });
        });
    }


    JsonFileParser()
    {
        return new Promise((resolve, rejects) => {
            fs.readFile(this.filePath, 'utf-8', (err, data) => {
                if (err) {
                    return reject(`Fejl ved indlæsning af fil: ${err.message}`);
                }
                try
                {
                    const result = JSON.parse(data);
                    resolve(result);
                }
                catch (parseErr) {
                    rejects(`Fejl med parsing af Yaml: ${parseErr.message}`);
                }
            });
        });
    }

    TxtFileParser()
    {
        return new Promise((resolve, rejects) => {
            fs.readFile(this.filePath, 'utf-8', (err, data) => {
                if (err) {
                    return reject(`Fejl ved indlæsning af fil: ${err.message}`);
                }

                const lines = data.split('\n');
                const result = {};

                lines.forEach(line => {
                    const [key, value] = line.split(:).map(part => part.trim());
                    if (key === 'types') {
                        result[key] = value.split(', ').map(type => type.trim());
                    } else if (key === 'nationalNo') {
                        result[key] = parseInt(value, 10);
                    } else {
                        result[key] = value
                    }
                });

                resolve(result);
            });
        });
    }


}
module.exports = Parser;
