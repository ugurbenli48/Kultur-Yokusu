using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sahneDegistirici : MonoBehaviour
{
   public static void sahneDegis(int sahne_id)
    {
        SceneManager.LoadScene(sahne_id,LoadSceneMode.Single);
    }
}
