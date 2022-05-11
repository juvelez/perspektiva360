using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using SimpleJSON;

public class ApiConnect : MonoBehaviour {

    /// <summary>
    /// Proyect ID used to retrieve the information to the API
    /// </summary>
    const string PROYECT_ID = "623b791c49b1d8002e60cb0e";

    /// <summary>
    /// To store the reference for the output textfield
    /// </summary>
    public Text outputTextField;

    /// <summary>
    /// METHOD 1
    /// IT is .NET based more mature, but requieres more dependencies, is not compatible with Coroutines
    /// </summary>
    
    /*
    [Serializable]
    public class DataInfo {
        public string txt_inicio2;
    }

    private DataInfo GetInfoMethod1() {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(String.Format("https://api.tikal.pro/textosPersonalizados/porProyecto?proyecto_id={0}", PROYECT_ID));
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream());
        string jsonResponse = reader.ReadToEnd();
        DataInfo info = JsonUtility.FromJson<DataInfo>(jsonResponse);
        outputTextField.text = info.txt_inicio2;
        return info;
    }
    */

    /// <summary>
    /// METHOD 2
    /// Using Unity Engine Wrappers, more optimized for games and multimedia apps, it's compatible with CoRutines
    /// This method is called by the Button "Ejecutar"
    /// </summary>
    /// 
    public void GetInfoMethod2() {
        WWWForm form = new WWWForm();
        form.AddField("proyecto_id", PROYECT_ID);
        outputTextField.text = "Conectando...";
        StartCoroutine(getData(form, (bool res, string data) => {
            if (res) {
                Debug.Log(data);
                outputTextField.text = "El contenido de la variable es:\n\n" + data;
            } else {
                Debug.Log("Error retrieving data");
                outputTextField.text = "Error obteniendo la información";
            }
        }));
    }

    /// <summary>
    /// Async execution of the API data retrieving 
    /// </summary>
    /// <param name="form">Data/parameters to send to the API query</param>
    /// <param name="onFinish">Calleback to excecute when the data arrives</param>
    /// <returns></returns>
    static IEnumerator getData(WWWForm form, System.Action<bool,string> onFinish) {

        bool res;
        string varibleContent = "...";

        using (UnityWebRequest request = UnityWebRequest.Post("https://api.tikal.pro/textosPersonalizados/porProyecto", form)) {
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success) {
                res = false;
                Debug.Log("Error getting data:" + request.error);
            } else {
                res = true;
                Debug.Log("Data arrived safe and sound:");
                Debug.Log(request.downloadHandler.text);

                JSONNode itemsData = JSON.Parse(request.downloadHandler.text);
                JSONNode items = itemsData["resultado"]["textosPersonalizados"];

                //TODO: Verify the correct variable name, the request was to consult "txt_inicio2"
                //      but that variable does not exists in the retrieved data, there is a variable called "txt_inicio3"
                //      this situation must be verified.

                foreach (JSONNode item in items) {
                    if (item["variable"] == "txt_inicio3") {
                        varibleContent = item["texto"];
                    }
                }

            }

            onFinish(res, varibleContent);
        }
    }

}
