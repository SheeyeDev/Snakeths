using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodStuck : MonoBehaviour
{
    public FoodGen generator;
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "tail")
        {
            generator.Generate();
            Destroy(transform.gameObject);
        }
    }
}
