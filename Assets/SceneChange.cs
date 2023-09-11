using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public GameObject panel;
    int count = 0;
    enum SceneName
    {
        Title,
        Main
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void Description()
    {
        if (count == 0)
        {
            panel.SetActive(true);
            count++;
        }
        else if(count == 1)
        {
            panel.SetActive(false);
            count = 0;
        }
    
    }
}
