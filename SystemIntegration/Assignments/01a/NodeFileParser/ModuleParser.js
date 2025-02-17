import fs from 'fs/promises'
import xml2js from 'xml2js'
import yaml from 'js-yaml'

export async function ParseCSV(filePath){
    try {
        const data = await fs.readFile(filePath, 'utf-8');
        const lines = data.split('\n').filter(line => line.trim() !== '');

        if (lines.length === 0) {
            return [];
        }

        const headers = lines[0].split(',').map(header => header.trim());
        const result = [];

        for (let i = 1; i < lines.length; i++) {
            const values = lines[i].split(',').map(value => value.trim());
            const obj = {};

            headers.forEach((header, index) => {
                obj[header] = values[index];
              });

            result.push(obj);
        }       

        return result;
    } catch (err) {
        throw new Error(`Fejl ved læsning af CSV-fil: ${err.message}`);
    }
}

export async function ParseJson(filePath) {
    try {
        const data = await fs.readFile(filePath, 'utf-8');
        const result = JSON.parse(data)
        return result;
    } 
    catch (err) {
        throw new Error(`Fejl ved læsning af Json-fil: ${err.message}`);
    }    
}

export async function ParseTxt(filePath){
    try {
        const data = await fs.readFile(filePath, 'utf-8');
        const result = data;
        return result;
    } 
    catch (err) {
        throw new Error(`Fejl ved læsning af Txt-fil: ${err.message}`);
    }
}


export async function ParseXml(filePath){
    try {
        const data = await fs.readFile(filePath, 'utf-8');
        const parser = new xml2js.Parser({explicitArray: false});
        const result = await parser.parseStringPromise(data)
        return result;
    } 
    catch (err) {
        throw new Error(`Fejl ved læsning af Xml-fil: ${err.message}`);
    }
}

export async function ParseYaml(filePath){
    try {
        const data = await fs.readFile(filePath, 'utf-8');
        const result = yaml.load(data);
        return result;
    } 
    catch (err) {
        throw new Error(`Fejl ved læsning af Yaml-fil: ${err.message}`);
    }
}
