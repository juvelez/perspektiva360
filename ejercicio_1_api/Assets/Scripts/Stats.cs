using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Stats {

    public static bool enabled = true;
    public static int turns = 0;
    public static int dicesRollings = 0;
    public static int dicesResults = 0;
    public static int dicesPairs = 0;
    public static int dicesFullPairs = 0;
    public static int piecesMovements = 0;
    public static int piecesSentToJail = 0;
    public static int piecesTakeouts = 0;
    public static int heavenArrivals = 0;
    public static int playersBlown = 0;
    public static int dataPostings = 0;

    public static int goodPostings = 0;
    public static int badPostings = 0;
    public static MonoBehaviour sender;

    public static void addTurn() {
        turns++;
        updateDataFile();
    }

    private static void updateDataFile() {
        if (enabled) {

            dataPostings++;

            WWWForm form = new WWWForm();
            form.AddField("Turns", turns.ToString());
            form.AddField("DicesRollings", dicesRollings.ToString());
            bool res = sendData(form);
        }
    }

    public static bool sendData(WWWForm form) {
        bool sentOk = true;
        sender.StartCoroutine(uploadData(form, (bool res) => {
            sentOk = res;
        }));
        return sentOk;
    }

    static IEnumerator uploadData(WWWForm form, System.Action<bool> onFinish) {
        bool res = true;
        using (UnityWebRequest www = UnityWebRequest.Post("https://www.3dparques.com/statistics/stats.php", form)) {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success) {
                res = false;
                badPostings++;
                Debug.Log("Error sending data:" + www.error + ", badPostings:" + badPostings);
            } else {
                res = true;
                goodPostings++;
                Debug.Log("Form upload complete!, goodPostings:" + goodPostings);
            }
            onFinish(res);
        }
    }

}
