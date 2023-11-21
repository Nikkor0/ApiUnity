// server.js
/*
const express = require('express');
const mysql = require('mysql2'); // Cambiado a mysql2

const app = express();

const connection = mysql.createConnection({
  host: 'localhost',
  user: 'root',
  password: '12345678',
  database: 'apiunity',
});

connection.connect((err) => {
  if (err) {
    console.error('Error al conectar a la base de datos:', err);
  } else {
    console.log('Conexión exitosa a la base de datos!');
    // Resto de tu código aquí
  }
});

// Resto de tu código...
*/

const express = require('express');
const mysql = require('mysql2'); // Cambiado a mysql2
const bodyParser = require('body-parser');
const routes = require('./routes');

const app = express();
const PORT = process.env.PORT || 3000;

app.use(bodyParser.json());

app.use('/api', routes);

app.listen(PORT, () => {
  console.log(`Server is running on port ${PORT}`);
});
