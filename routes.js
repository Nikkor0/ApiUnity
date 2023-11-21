const express = require('express');
const router = express.Router();
const dataController = require('./dataController');

// Endpoint para obtener todos los objetos
router.get('/objetos', dataController.getAllObjects);

// Endpoint para obtener un objeto por su ID
router.get('/objetos/:id', dataController.getObjectById);

// Endpoint para crear un nuevo objeto
router.post('/objetos', dataController.createObject);

// Endpoint para actualizar un objeto por su ID
router.put('/objetos/:id', dataController.updateObject);

// Endpoint para eliminar un objeto por su ID
router.delete('/objetos/:id', dataController.deleteObject);

module.exports = router;
