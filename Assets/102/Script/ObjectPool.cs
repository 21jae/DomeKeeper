using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject prefab; // 풀링할 프리팹
    public int poolSize = 10; // 풀 크기

    private Queue<GameObject> objectPool = new Queue<GameObject>();

    private void Start()
    {
        InitializePool();
    }

    // 풀 초기화
    private void InitializePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            objectPool.Enqueue(obj);
        }
    }

    // 오브젝트를 풀에서 가져오기
    public GameObject GetObjectFromPool(Vector3 position, Quaternion rotation)
    {
        if (objectPool.Count > 0)
        {
            GameObject obj = objectPool.Dequeue();
            obj.transform.position = position;
            obj.transform.rotation = rotation;
            obj.SetActive(true);
            return obj;
        }
        else
        {
            // 풀이 비어있을 경우 추가로 생성
            GameObject obj = Instantiate(prefab, position, rotation);
            return obj;
        }
    }

    // 오브젝트를 풀로 반환하기
    public void ReturnObjectToPool(GameObject obj)
    {
        obj.SetActive(false);
        objectPool.Enqueue(obj);
    }
}