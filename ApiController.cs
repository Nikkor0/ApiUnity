//apiController
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using System.Collections.Generic;
using static UIManager;

public class ApiController : MonoBehaviour
{
    private UIManager uiManager;

    private const string apiUrl = "http://localhost:3000/api/objetos"; // Reemplaza esto con la URL de tu API
    //public delegate void ObjectsFetchedHandler(string jsonResponse);
    //public static event ObjectsFetchedHandler OnObjectsFetched;

    public TextMeshProUGUI resultText;  // Asegúrate de que sea del tipo correcto

    public delegate void ObjectsFetchedHandler(List<Objeto> objetos);
    public static event ObjectsFetchedHandler OnObjectsFetched;

    [System.Serializable]
    private class ObjectsResponse
    {
        public List<Objeto> objetos;
    }




    void Start()
    {
        // Obtiene la referencia a UIManager
        uiManager = GetComponent<UIManager>();
    }



    public IEnumerator GetObjects()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(apiUrl))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error al obtener objetos: " + www.error);
            }
            else
            {
                string jsonResponse = www.downloadHandler.text;
                Debug.Log("Respuesta del servidor: " + jsonResponse);


                // Deserializa directamente la lista de objetos
                List<Objeto> objetos = JsonUtility.FromJson<List<Objeto>>("{\"objetos\":" + www.downloadHandler.text + "}");

                if (objetos != null && objetos.Count > 0)
                {
                    OnObjectsFetched?.Invoke(objetos);

                    // Muestra la información del primer objeto en el TextMeshProUGUI
                    Objeto primerObjeto = objetos[0];
                    string formattedText = $"El nombre del elemento es: \"{primerObjeto.nombre}\" su origen es de: \"{primerObjeto.origen}\" hay una cantidad de: {primerObjeto.cantidad} valorado en {primerObjeto.precio.ToString("F2")} millones";
                    resultText.text = formattedText;
                }
                else
                {
                    resultText.text = "No hay objetos disponibles.";
                }
            }
        }
    }

    public IEnumerator CreateObject(Objeto objeto)
    {
        // Convertir el objeto a JSON
        string json = JsonUtility.ToJson(objeto);

        // Crear la solicitud usando UnityWebRequest.Post
        using (UnityWebRequest www = UnityWebRequest.PostWwwForm(apiUrl, "POST"))
        {
            // Configurar el cuerpo de la solicitud con los datos JSON
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
            www.uploadHandler = new UploadHandlerRaw(bodyRaw);
            www.downloadHandler = new DownloadHandlerBuffer();

            // Establecer el encabezado de contenido JSON
            www.SetRequestHeader("Content-Type", "application/json");

            // Enviar la solicitud y esperar la respuesta
            yield return www.SendWebRequest();

            // Manejar la respuesta
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error al crear objeto: " + www.error);
            }
            else
            {
                string jsonResponse = www.downloadHandler.text;
                Debug.Log("Objeto creado: " + jsonResponse);

                // Convierte la respuesta JSON a un objeto Objeto
                Objeto objetoCreado = JsonUtility.FromJson<Objeto>(jsonResponse);

                // Formatea el texto para que sea más comprensible
                string formattedText = $"Nuevo mineral creado con ID: {objetoCreado.id}, Nombre: {objetoCreado.nombre}, Origen: {objetoCreado.origen}, Cantidad: {objetoCreado.cantidad}, Precio: {objetoCreado.precio}";

                // Asigna la respuesta formateada al componente Text en Unity
                resultText.text = formattedText;
            }
        }
    }

    public IEnumerator UpdateObject(int objectId, Objeto objeto)
    {
        string json = JsonUtility.ToJson(objeto);
        string updateUrl = apiUrl + "/" + objectId;
        using (UnityWebRequest www = UnityWebRequest.Put(updateUrl, json))
        {
            www.SetRequestHeader("Content-Type", "application/json");
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error al actualizar objeto: " + www.error);
            }
            else
            {
                string jsonResponse = www.downloadHandler.text;
                Debug.Log("Objeto actualizado: " + jsonResponse);

                // Convierte la respuesta JSON a un objeto Objeto
                Objeto objetoActualizado = JsonUtility.FromJson<Objeto>(jsonResponse);

                // Formatea el texto para que sea más comprensible
                string formattedText = $"El mineral con ID {objetoActualizado.id} ha sido actualizado con los siguientes valores: " +
                    $"Nombre: {objetoActualizado.nombre}, Origen: {objetoActualizado.origen}, Cantidad: {objetoActualizado.cantidad}, Precio: {objetoActualizado.precio}";

                // Asigna la respuesta formateada al componente Text en Unity
                resultText.text = formattedText;
            }
        }
    }

    public IEnumerator DeleteObject(int objectId)
    {
        string deleteUrl = apiUrl + "/" + objectId;
        using (UnityWebRequest www = UnityWebRequest.Delete(deleteUrl))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error al eliminar objeto: " + www.error);
            }
            else
            {
                Debug.Log("Objeto eliminado con éxito");

                // Formatea el texto para que sea más comprensible
                string formattedText = $"El mineral con ID {objectId} ha sido eliminado con éxito";

                // Asigna la respuesta formateada al componente Text en Unity
                resultText.text = formattedText;
            }
        }
    }


    public IEnumerator GetObjectByIdAndDisplayResult(int objectId)
    {
        if (resultText == null)
        {
            Debug.LogError("Error: el objeto de texto no está asignado en el inspector de Unity.");
            yield break;
        }



        // Construir la URL con el ID proporcionado
        string getUrl = apiUrl + "/" + objectId;

        using (UnityWebRequest www = UnityWebRequest.Get(getUrl))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error al obtener objeto por ID: " + www.error);
            }
            else
            {
                string jsonResponse = www.downloadHandler.text;
                Debug.Log("Objeto obtenido por ID: " + jsonResponse);

                // Convierte la respuesta JSON a un objeto Objeto
                Objeto objeto = JsonUtility.FromJson<Objeto>(jsonResponse);

                // Formatea el texto para que sea más comprensible
                string formattedText = $"El mineral obtenido con ID: {objeto.id} se le conoce como: {objeto.nombre} es de origen: {objeto.origen} hay: {objeto.cantidad} y está valorado en: {objeto.precio} millones";


                // Asigna la respuesta al componente Text en Unity
                resultText.text = formattedText; 
            }
        }
    }
}

[System.Serializable]
public class Objeto
{
    public int id;
    public string nombre;
    public string origen;
    public int cantidad;
    public float precio;
}
