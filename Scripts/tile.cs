using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tile : MonoBehaviour
{
    public GameObject tiler;
    // Start is called before the first frame update
    void Start()
    {
        for(int x = 0; x < 50; x++)
        {
            for(int y = 0; y < 50; y++)
            {
                GameObject hi = Instantiate(tiler, transform.position + new Vector3(x, y, 0), Quaternion.identity);
                hi.transform.parent = transform;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
