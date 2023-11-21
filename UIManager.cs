// UIManager.cs
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public ApiController apiController;
    public TextMeshProUGUI resultText; // Asigna el componente Text desde la jerarquía de Unity

    public TMP_InputField idInputField;
    public TMP_InputField nombreInputField;
    public TMP_InputField origenInputField;
    public TMP_InputField cantidadInputField;
    public TMP_InputField precioInputField;


    [System.Serializable]
    public class ObjetoResponse
    {
        public List<Objeto> objetos;
    }


    private void Start()
    {
        // Suscribe al evento
        ApiController.OnObjectsFetched += HandleObjectsFetched;
    }

    private void OnDestroy()
    {
        // Desuscribe al evento para evitar fugas de memoria
        ApiController.OnObjectsFetched -= HandleObjectsFetched;
    }


    public void SalirDelJuego()
    {
        Application.Quit();
    }



    public string ResultText
    {
        get { return resultText.text; }
        set { resultText.text = value; }
    }


    public void OnGetAllValuesButtonClicked()
    {
        apiController.StartCoroutine(apiController.GetObjects());
    }


    private void HandleObjectsFetched(List<Objeto> objetos)
    {
        Debug.Log($"Número de objetos recibidos: {objetos.Count}");


        if (objetos != null && objetos.Count > 0)
        {
            Objeto primerObjeto = objetos[0];
            string formattedText = $"El nombre del elemento es: \"{primerObjeto.nombre}\" su origen es de: \"{primerObjeto.origen}\" hay una cantidad de: {primerObjeto.cantidad} valorado en {primerObjeto.precio.ToString("F2")} millones";
            resultText.text = formattedText;
        }
        else
        {
            resultText.text = "No hay objetos disponibles.";
        }
    }


    public void OnCreateButtonClicked()
    {
        // Obtener valores de los campos de entrada
        int id = int.Parse(idInputField.text);
        string nombre = nombreInputField.text;
        string origen = origenInputField.text;
        int cantidad = int.Parse(cantidadInputField.text);
        float precio = float.Parse(precioInputField.text);

        // Crear un nuevo objeto
        Objeto nuevoObjeto = new Objeto
        {
            id = id,
            nombre = nombre,
            origen = origen,
            cantidad = cantidad,
            precio = precio
        };

        // Llamar al método CreateObject de ApiController
        apiController.StartCoroutine(apiController.CreateObject(nuevoObjeto));
    }

    public void OnReadButtonClicked()
    {
        // Obtener el ID del InputField y convertirlo a entero
        if (int.TryParse(idInputField.text, out int objectIdToRead))
        {
            // Llama a GetObjectById con el ID proporcionado
            apiController.StartCoroutine(apiController.GetObjectByIdAndDisplayResult(objectIdToRead));
        }
        else
        {
            Debug.LogError("Por favor, ingrese un ID válido para obtener información del objeto.");
        }

    }

    public void OnUpdateButtonClicked()
    {
        // Obtener valores de los campos de entrada
        int id = int.Parse(idInputField.text); // Asegúrate de que idInputField tenga el valor correcto
        string nombre = nombreInputField.text;
        string origen = origenInputField.text;
        int cantidad = int.Parse(cantidadInputField.text);
        float precio = float.Parse(precioInputField.text);

        // Crear un objeto actualizado con los valores de los InputFields
        Objeto objetoActualizado = new Objeto
        {
            id = id, // Asigna el id obtenido del InputField
            nombre = nombre,
            origen = origen,
            cantidad = cantidad,
            precio = precio
        };

        // Llamar al método UpdateObject de ApiController
        apiController.StartCoroutine(apiController.UpdateObject(id, objetoActualizado));
    }

    public void OnDeleteButtonClicked()
    {
        // Obtén el ID del InputField y conviértelo a entero
        if (int.TryParse(idInputField.text, out int objectIdToDelete))
        {
            // Llama a DeleteObject con el ID proporcionado
            apiController.StartCoroutine(apiController.DeleteObject(objectIdToDelete));
        }
        else
        {
            Debug.LogError("Por favor, ingrese un ID válido para eliminar el objeto.");
        }
    }
}
