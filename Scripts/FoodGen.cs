using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodGen : MonoBehaviour
{
    public GameObject food;

    private void Awake()
    {
        Generate();
        Generate();
    }

    public void Generate()
    {
        int x = Random.Range(1, 28);
        int y = Random.Range(1, 28);
        GameObject foo = Instantiate(food, transform.position + new Vector3(x, y, 0), Quaternion.identity);
        foo.transform.parent = transform;
    }
}
