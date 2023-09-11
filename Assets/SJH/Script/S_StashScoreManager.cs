using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class S_StashScoreManager : MonoBehaviour
{
    [SerializeField] Text redjemScore;
    [SerializeField] Text bluejemScore;
    [SerializeField] Text greenjemScore;

    [SerializeField] GameObject stash;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        redjemScore.text = stash.GetComponent<S_JemstoneStash>().redjemScore.ToString();
        bluejemScore.text = stash.GetComponent<S_JemstoneStash>().bluejemScore.ToString();
        greenjemScore.text = stash.GetComponent<S_JemstoneStash>().greenjemScore.ToString();
    }
}
