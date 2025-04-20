using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;



public class OyunDongusu : MonoBehaviour
{
    [Header("SORULAR")]
    public SorularList sorular;
    [Header("Soru Deðiþkenleri")]
    public TextMeshProUGUI soru_tmp;
    public TextMeshProUGUI buton_atmp, buton_btmp, buton_ctmp, buton_dtmp;
    void Start()
    {
        StartCoroutine(sorulariGetir());
    }

    
    void Update()
    {
        
    }
    
    IEnumerator sorulariGetir()
    {
        WWWForm form = new WWWForm();
        form.AddField("xd", "sorular");
        
        using UnityWebRequest www = UnityWebRequest.Post("http://localhost/kultur_yokusu/veritabani_islemler.php", form);
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("Sunucuya baðlanýrken hata oluþtu.");
            }
            else
            {
                Debug.Log("Basarili");
                sorular =JsonUtility.FromJson<SorularList>(www.downloadHandler.text);
            }
        }
    }
}
