import express from 'express';
import { ParseCSV, ParseJson, ParseTxt, ParseXml, ParseYaml } from '../../01a/NodeFileParser/ModuleParser.js';

const filePath = 'C:\\Users\\smaur\\Code\\Kea\\SystemIntegration\\Assignments\\01a\\Files\\Pokemon';
const CsvFilePath = filePath + '.csv';
const JsonFilePath = filePath + '.json';
const TxtFilePath = filePath + '.txt';
const XmlFilePath = filePath + '.xml';
const YamlFilePath = filePath + '.yml';

const app = express();
app.use(express.json());

// app.get('/', async (req, res) => {
//     res.send({ data: "From Node" });    
// });

    app.get('/nodedata', async (req, res) => {
        try {
            const [csvData, jsonData, txtData, xmlData, yamlData] = await Promise.all([
                ParseCSV(CsvFilePath),
                ParseJson(JsonFilePath),
                ParseTxt(TxtFilePath),
                ParseXml(XmlFilePath),
                ParseYaml(YamlFilePath)
            ]);

            const data = {csvData, jsonData, txtData, xmlData, yamlData}

            // const data = await ParseCSV(CsvFilePath); Til at sende en enkelt parsed Fil
            res.send({ data });
        } catch (error) {
            console.error('Error parsing CSV:', error);
            res.status(500).send({ error: error.toString() });
        }
    });

    app.post('/receiveData', (req, res) => {
        const receivedData = req.body;
        console.log('Data modtaget fra C#: ', receivedData);
        res.send({status: 'Data modtaget'})
    })

const PORT = 8080;
app.listen(PORT, () => console.log('http://127.0.0.1:8080/nodedata | http://127.0.0.1:8080/receiveData '));