/*
const db = require('./db');

// Obtener todos los objetos
exports.getAllObjects = (req, res) => {
  db.query('SELECT * FROM objetos', (err, results) => {
    if (err) {
      console.error('Error fetching objects:', err);
      res.status(500).json({ error: 'Error fetching objects' });
      return;
    }
    res.json(results);
  });
};

// Obtener un objeto por su ID
exports.getObjectById = (req, res) => {
  const objectId = req.params.id;
  db.query('SELECT * FROM objetos WHERE id = ?', [objectId], (err, results) => {
    if (err) {
      console.error('Error fetching object by ID:', err);
      res.status(500).json({ error: 'Error fetching object' });
      return;
    }
    if (results.length === 0) {
      res.status(404).json({ error: 'Object not found' });
      return;
    }
    res.json(results[0]);
  });
};

// Crear un nuevo objeto
exports.createObject = (req, res) => {
  const { nombre, origen, cantidad, precio } = req.body;
  db.query(
    'INSERT INTO objetos (nombre, origen, cantidad, precio) VALUES (?, ?, ?, ?)',
    [nombre, origen, cantidad, precio],
    (err, results) => {
      if (err) {
        console.error('Error creating object:', err);
        res.status(500).json({ error: 'Error creating object' });
        return;
      }
      res.json({ id: results.insertId, nombre, origen, cantidad, precio });
    }
  );
};

// Actualizar un objeto por su ID
exports.updateObject = (req, res) => {
  const objectId = req.params.id;
  const { nombre, origen, cantidad, precio } = req.body;
  db.query(
    'UPDATE objetos SET nombre = ?, origen = ?, cantidad = ?, precio = ? WHERE id = ?',
    [nombre, origen, cantidad, precio, objectId],
    (err, results) => {
      if (err) {
        console.error('Error updating object:', err);
        res.status(500).json({ error: 'Error updating object' });
        return;
      }
      if (results.affectedRows === 0) {
        res.status(404).json({ error: 'Object not found' });
        return;
      }
      res.json({ id: objectId, nombre, origen, cantidad, precio });
    }
  );
};

// Eliminar un objeto por su ID
exports.deleteObject = (req, res) => {
  const objectId = req.params.id;
  db.query('DELETE FROM objetos WHERE id = ?', [objectId], (err, results) => {
    if (err) {
      console.error('Error deleting object:', err);
      res.status(500).json({ error: 'Error deleting object' });
      return;
    }
    if (results.affectedRows === 0) {
      res.status(404).json({ error: 'Object not found' });
      return;
    }
    res.json({ message: 'Object deleted successfully' });
  });
};
*/

const db = require('./db');

// Obtener todos los objetos
exports.getAllObjects = (req, res) => {
  try {
    db.query('SELECT * FROM objetos', (err, results) => {
      if (err) {
        console.error('Error fetching objects:', err);
        res.status(500).json({ error: 'Error fetching objects' });
        return;
      }
      res.json(results);
    });
  } catch (error) {
    console.error('Unexpected error:', error);
    res.status(500).json({ error: 'Unexpected error' });
  }
};

// Obtener un objeto por su ID
exports.getObjectById = (req, res) => {
  try {
    const objectId = req.params.id;
    db.query('SELECT * FROM objetos WHERE id = ?', [objectId], (err, results) => {
      if (err) {
        console.error('Error fetching object by ID:', err);
        res.status(500).json({ error: 'Error fetching object' });
        return;
      }
      if (results.length === 0) {
        res.status(404).json({ error: 'Object not found' });
        return;
      }
      res.json(results[0]);
    });
  } catch (error) {
    console.error('Unexpected error:', error);
    res.status(500).json({ error: 'Unexpected error' });
  }
};

// Crear un nuevo objeto
exports.createObject = (req, res) => {
  try {
    const { nombre, origen, cantidad, precio } = req.body;
    db.query(
      'INSERT INTO objetos (nombre, origen, cantidad, precio) VALUES (?, ?, ?, ?)',
      [nombre, origen, cantidad, precio],
      (err, results) => {
        if (err) {
          console.error('Error creating object:', err);
          res.status(500).json({ error: 'Error creating object' });
          return;
        }
        res.json({ id: results.insertId, nombre, origen, cantidad, precio });
      }
    );
  } catch (error) {
    console.error('Unexpected error:', error);
    res.status(500).json({ error: 'Unexpected error' });
  }
};

// Actualizar un objeto por su ID
exports.updateObject = (req, res) => {
  try {
    const objectId = req.params.id;
    const { nombre, origen, cantidad, precio } = req.body;
    db.query(
      'UPDATE objetos SET nombre = ?, origen = ?, cantidad = ?, precio = ? WHERE id = ?',
      [nombre, origen, cantidad, precio, objectId],
      (err, results) => {
        if (err) {
          console.error('Error updating object:', err);
          res.status(500).json({ error: 'Error updating object' });
          return;
        }
        if (results.affectedRows === 0) {
          res.status(404).json({ error: 'Object not found' });
          return;
        }
        res.json({ id: objectId, nombre, origen, cantidad, precio });
      }
    );
  } catch (error) {
    console.error('Unexpected error:', error);
    res.status(500).json({ error: 'Unexpected error' });
  }
};

// Eliminar un objeto por su ID
exports.deleteObject = (req, res) => {
  try {
    const objectId = req.params.id;
    db.query('DELETE FROM objetos WHERE id = ?', [objectId], (err, results) => {
      if (err) {
        console.error('Error deleting object:', err);
        res.status(500).json({ error: 'Error deleting object' });
        return;
      }
      if (results.affectedRows === 0) {
        res.status(404).json({ error: 'Object not found' });
        return;
      }
      res.json({ message: 'Object deleted successfully' });
    });
  } catch (error) {
    console.error('Unexpected error:', error);
    res.status(500).json({ error: 'Unexpected error' });
  }
};
