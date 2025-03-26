using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetKontrol : MonoBehaviour
{
    public static bool internet;
    void Start()
    {
        StartCoroutine(internetDurum());
    }
    IEnumerator internetDurum()
    {
        WWWForm form = new WWWForm();

        using UnityWebRequest www = UnityWebRequest.Post("http://localhost/kultur_yokusu/netkontrol.txt", form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(www.error);
            internet = false;
        }
        else
        {
            internet = true;
            Debug.Log("Ýnternet Var!");
        }
        yield return new WaitForSeconds(2);
        StartCoroutine(internetDurum());
    }
}
