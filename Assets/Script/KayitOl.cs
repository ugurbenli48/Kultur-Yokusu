using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.SceneManagement;
public class KayitOl : MonoBehaviour
{
    public TextMeshProUGUI hata_T;
    public TMP_InputField nick_IF;
    public GameObject kayit_P, anamenu_P;
    
    void Start()
    {
        hata_T.gameObject.SetActive(false);

        if (!PlayerPrefs.HasKey("kayitDurumu"))
        {
            kayit_P.SetActive(true);
            anamenu_P.SetActive(false);
        }
        else
        {
            kayit_P.SetActive(false);
            anamenu_P.SetActive(true);
        }
    }

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
                StartCoroutine(devam_B());
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
         public IEnumerator devam_B()
    {
        PlayerPrefs.SetInt("kayitDurumu", 1);
        yield return new WaitForSeconds(3f);
        kayit_P.SetActive(false);
        anamenu_P.SetActive(true);
    }
    public void yenioyun_B ()
    {
        sahneDegistirici.sahneDegis(1);
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
