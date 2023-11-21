const mysql = require('mysql2');

const db = mysql.createConnection({
  host: 'localhost',
  user: 'root', // Tu nombre de usuario
  password: '12345678', // Tu contraseña
  database: 'apiunity', // Tu nombre de base de datos
});

db.connect((err) => {
  if (err) {
    console.error('Error connecting to database:', err);
    return;
  }
  console.log('Connected to database!');
});

module.exports = db;
