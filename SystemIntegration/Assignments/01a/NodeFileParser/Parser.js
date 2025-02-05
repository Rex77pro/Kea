const { rejects } = require('assert');
const { isUtf8 } = require('buffer');
const fs = require('fs');
const { resolve } = require('path');
const xml2js = require('xml2js')

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
                })
            })
        })
    }
}

module.exports = Parser;
