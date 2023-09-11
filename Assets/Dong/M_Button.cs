using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class M_Button : MonoBehaviour
{

    public void Restart()
    {
        SceneManager.LoadScene("SumTest");
    }

    public void ReturnToTitle()
    {
        SceneManager.LoadScene("");
    }
}
