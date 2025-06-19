using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JokerSistemi : MonoBehaviour
{
    public List<string> ihtimaller;
    public GameObject[] secenekler;
    public int dogruCevap;
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
    public void yuzdeliJokerKullan(int dogruCevap)
    {
        ihtimaller = new List<string>()
        {
            "12","13","23",
            "02","03","23",
            "01","03","13",
            "01","02","12"
        };
        string secilen;
        int random = 0;

        switch (dogruCevap) { 
            case 1:
                {
                    random = Random.Range(0, 3);
                    break;
                }
            case 2:
                {
                    random = Random.Range(3, 6);
                    break;
                }
            case 3:
                {
                    random = Random.Range(6, 9);
                    break;
                }
            case 4:
                {
                    random = Random.Range(9, 12);
                    break;
                }
        }
        secilen = ihtimaller[random];

        for (int i = 0; i < secilen.Length; i++)
        {
            string gecici = secilen.Substring(i, 1);
            int secenekIndex = int.Parse(gecici);
            StartCoroutine(KaybolmaAnimasyonu(secenekler[secenekIndex].GetComponent<CanvasGroup>(), 2f));
        }
    }
    IEnumerator KaybolmaAnimasyonu(CanvasGroup cg, float duration)
    {
        float timer = 0;

        while (timer < duration)
        {
            timer += Time.unscaledDeltaTime;
            cg.alpha = Mathf.Lerp(1, 0, timer / duration);
            cg.blocksRaycasts = false;
            yield return null;
        }

        cg.alpha = 0;
        cg.blocksRaycasts = false;
    }
}
