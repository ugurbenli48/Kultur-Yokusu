using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yonetim : MonoBehaviour
{
    public GameObject kayit_P, anamenu_P;
    void Start()
    {
        if(!PlayerPrefs.HasKey("kayitDurumu"))
        {
            kayit_P.SetActive(true);
            anamenu_P.SetActive(false);
            PlayerPrefs.GetInt("kayitDurumu", 1);
        }
        else
        {
            kayit_P.SetActive(false);
            anamenu_P.SetActive(true);
        }
    }
    public void devam_B()
    {
        PlayerPrefs.SetInt("kayitDurumu", 1);
        kayit_P.SetActive(false);
        anamenu_P.SetActive(true);
    }

    
    void Update()
    {
        
    }
}
