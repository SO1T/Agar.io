using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManager : MonoBehaviour
{
    public GameObject food;
    void Start()
    {
        StartCoroutine(InstantiateFood());
    }

    IEnumerator InstantiateFood()
    {
        while (true)
        {
            float x = Random.Range(-50, 50);
            float y = Random.Range(-50, 50);
            Instantiate(food, new Vector3(x, y, 0), Quaternion.identity, gameObject.transform);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
