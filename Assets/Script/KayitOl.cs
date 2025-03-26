using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
public class KayitOl : MonoBehaviour
{
    public TextMeshProUGUI hata_T;
    public TMP_InputField nick_IF;

    void Start()
    {
        hata_T.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void kontrol()
    {
        if (NetKontrol.internet)
        {
            if (!nick_IF.text.Equals(""))
            {
                StartCoroutine(kayitOl());
            }
            else
            {      
                textYazdir("Lütfen Boþ Býrakmayýn");
            }
        }
        else
        {
            textYazdir("Ýnternet Yok");
        }
    }
    IEnumerator kayitOl()
    {
        WWWForm form = new WWWForm();
        form.AddField("xd", "kayitol");
        form.AddField("nick", nick_IF.text);

        using UnityWebRequest www = UnityWebRequest.Post("http://localhost/kultur_yokusu/veritabani_islemler.php", form);
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                textYazdir("Sunucuya baðlanýrken hata oluþtu.");
            }
            else
            {
                textYazdir(www.downloadHandler.text);
            }
        }
    }
    void textYazdir(string mesaj)
    {
        hata_T.gameObject.SetActive(true);
        hata_T.text = mesaj;
        Invoke("sifirla", 2.0f);
    }
    void sifirla()
    {
        hata_T.text = "";
    }
}
