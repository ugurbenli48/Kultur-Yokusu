using Microsoft.Unity.VisualStudio.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.UIElements;

public static class rastgeleSýrala
{
    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = UnityEngine.Random.Range(0, n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}

public class OyunDongusu : MonoBehaviour
{
    [Header("SORULAR")]
    public SorularList sorular;
    [Header("Soru Deðiþkenleri")]
    public TextMeshProUGUI soru_tmp;
    public TextMeshProUGUI buton_atmp, buton_btmp, buton_ctmp, buton_dtmp;

    public int soruNo;
    BirimSoruModel birimSoruModel;
    public Color dogruRenk, yanlisRenk;
    public UnityEngine.UI.Image[] butonImages;
    AudioSource sesKaynagi;
    public AudioClip dogruSesi, yanlisSesi;
    public List<int> sorularSýrasý;
    private Color orjinalRenk;
    public GameObject gameOver_P;
    public CanvasGroup soruPanel;
    public CanvasGroup[] butonCg;
    JokerSistemi jokerSistemi;
    public CanvasGroup[] butonJoker;

    void Start()
    {
        orjinalRenk = butonImages[0].color;
        sesKaynagi = GetComponent<AudioSource>();
        jokerSistemi = GetComponent<JokerSistemi>();
        StartCoroutine(sorulariGetir());
        soruNo = 0;
    }
    void Update()
    {
        
    }

    void sorularýSormaSirasi()
    {
        for (int i = 0; i < sorular.butunSorular.Count; i++)
        {
            sorularSýrasý.Add(i);
        }
        sorularSýrasý.Shuffle();
    }
    public void yuzde50Kullan()
    {
        StartCoroutine(JokerKaybol(0));
        jokerSistemi.yuzdeliJokerKullan(birimSoruModel.dogruCevab);
    }
    void soruSor(int soruNo)
    {
        foreach (var btn in butonImages)
        {
            btn.color = orjinalRenk;
        }

        birimSoruModel = sorular.butunSorular[sorularSýrasý[soruNo]];

        soru_tmp.text = birimSoruModel.soru;
        buton_atmp.text = birimSoruModel.a_Cevab;
        buton_btmp.text = birimSoruModel.b_Cevab;
        buton_ctmp.text = birimSoruModel.d_Cevab;
        buton_dtmp.text = birimSoruModel.c_Cevab;
    }
    public void kontrolEt(int basilanCevap) {
        birimSoruModel = sorular.butunSorular[sorularSýrasý[soruNo]];
        if(birimSoruModel.dogruCevab == basilanCevap)
        {
            Debug.Log("Dogru");
            sesKaynagi.PlayOneShot(dogruSesi);
            butonImages[birimSoruModel.dogruCevab-1].color = dogruRenk;
            StartCoroutine(DogruCevapBeklet());
        }
        else
        {
            Debug.Log("Yanlis");
            sesKaynagi.PlayOneShot(yanlisSesi);
            butonImages[basilanCevap - 1].color = yanlisRenk;
            
            if(!ciftCevap)
            {
                butonImages[birimSoruModel.dogruCevab - 1].color = dogruRenk;
                gameOver();
            }
           else
            {
                ciftCevap = false;
            }
           
        }

    }
    public void pasKullan()
    {
        StartCoroutine(JokerKaybol(1));
        StartCoroutine(pasKaybol());
    }
    private void gameOver()
    {
        gameOver_P.SetActive(true);
        StartCoroutine(gameOverAktif());
    }
    public bool ciftCevap;
    public void ciftCevapKullan()
    {
        ciftCevap = true;
        StartCoroutine(JokerKaybol(2));
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
                sorular = JsonUtility.FromJson<SorularList>(www.downloadHandler.text);
                sorularýSormaSirasi();
                soruSor(soruNo);
            }
        }
    }
    IEnumerator DogruCevapBeklet()
    {
        yield return new WaitForSeconds(2f);
         
        StartCoroutine(SoruKaybol());

        yield return new WaitForSeconds(1f);

        soruNo++;
        soruSor(soruNo);
        StartCoroutine(SoruYukle());
    }
    IEnumerator pasKaybol()
    {
        StartCoroutine(SoruKaybol());
        yield return new WaitForSeconds(1f);
        soruNo++;
        soruSor(soruNo);
        StartCoroutine(SoruYukle());
    }
    IEnumerator gameOverAktif()
    {
        CanvasGroup cg = gameOver_P.GetComponent<CanvasGroup>();
        cg.alpha = 0;
        cg.interactable = false;
        cg.blocksRaycasts = false;

        float duration = 1f;
        float timer = 0;

        while (timer < duration)
        {
            timer += Time.unscaledDeltaTime;
            cg.alpha = Mathf.Lerp(0, 1, timer / duration);
            yield return null;
        }

        cg.alpha = 1;
        cg.interactable = true;
        cg.blocksRaycasts = true;
        Time.timeScale = 0;
    }
    IEnumerator JokerKaybol(int i)
    {
        float duration = 2f;
        float timer = 0;

        while (timer < duration)
        {

            timer += Time.unscaledDeltaTime;

            butonJoker[i].alpha = Mathf.Lerp(1, 0, timer / duration);

            yield return null;
        }
        butonJoker[i].interactable = false;
    }
    IEnumerator SoruKaybol()
    {
        float duration = 1f;
        float timer = 0;

        while (timer < duration)
        {
            
            timer += Time.unscaledDeltaTime;
            soruPanel.alpha = Mathf.Lerp(1, 0, timer / duration);

            foreach (CanvasGroup cg in butonCg)
            {
                if(cg.alpha > 0)
                {
                    cg.alpha = Mathf.Lerp(1, 0, timer / duration);
                }
            }

            yield return null;
        }

        soruPanel.alpha = 0;
        foreach (CanvasGroup cg in butonCg)
        {
            cg.alpha = 0;
        }
    }
    IEnumerator SoruYukle()
    {
       
        CanvasGroup soruCg = soruPanel.GetComponent<CanvasGroup>();
        CanvasGroup[] butonCg = new CanvasGroup[butonImages.Length];
     
        for (int i = 0; i < butonImages.Length; i++)
        {
            butonCg[i] = butonImages[i].GetComponent<CanvasGroup>();
            butonCg[i].blocksRaycasts=true;
        }

        float duration = 2f; 
        float timer = 0;

        while (timer < duration)
        {
            timer += Time.unscaledDeltaTime;

            soruCg.alpha = Mathf.Lerp(0, 1, timer / duration);

            for (int i = 0; i < butonCg.Length; i++)
            {
                butonCg[i].alpha = Mathf.Lerp(0, 1, timer / duration);
            }

            yield return null;
        }

        soruCg.alpha = 1;
        for (int i = 0; i < butonCg.Length; i++)
        {
            butonCg[i].alpha = 1;
        }

        soruCg.interactable = true;
        for (int i = 0; i < butonCg.Length; i++)
        {
            butonCg[i].interactable = true;
        }
    }
}
