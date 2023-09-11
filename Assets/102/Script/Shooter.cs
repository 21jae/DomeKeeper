using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] public GameObject missile;
    [SerializeField] public GameObject target;

    [SerializeField] public float spd;
    [SerializeField] public int shot;
    [SerializeField] public float Dtime;

    public void Update()
    {
        target = GameObject.FindGameObjectWithTag("Monster");
        Dtime += Time.deltaTime;
        if(Dtime > 0.3f) 
        { 

        Shot();
        }
    }
    public void Shot()
    {
        StartCoroutine(CreateMissile());
    }

    IEnumerator CreateMissile()
    {
        int _shot = shot;
        while (_shot > 0)
        {
            
            _shot--;
            GameObject bullet = Instantiate(missile, transform);
            bullet.GetComponent<BezierMissile>().master = gameObject;
            bullet.GetComponent<BezierMissile>().enemy = target;
            Dtime = 0f;
            if (target != null)
            {
                SoundManager.instance.PlayAutoTower();
            }
        }
        yield return null;
    }
}
