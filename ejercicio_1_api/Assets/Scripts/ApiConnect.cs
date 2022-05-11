using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using SimpleJSON;

public class ApiConnect : MonoBehaviour {

    const string PROYECT_ID = "623b791c49b1d8002e60cb0e";

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
