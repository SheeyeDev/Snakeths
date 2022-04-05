using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generation : MonoBehaviour
{
    public int generationNumber = 0;
    public GameObject Everything;
    public GameObject[] everythings;
    public List<GameObject> best;
    public float[,,] bestbrain = new float[50,6,5];
    public float currentTime;
    public float maxTime;
    public int whichone;

    private void Awake()
    {
        nextGenerationOne();
    }
    void nextGenerationOne()
    {
        if (generationNumber > 0)
        {
            for (int x = 0; x < 550; x++)
            {
                if (everythings[x] != null)
                {
                    if (best.Count == 0)
                    {
                        best.Add(everythings[x]);
                    }
                    else
                    {
                        for (int c = 0; c < best.Count; c++)
                        {
                            if (everythings[x].transform.GetComponent<Everyting>().snake.points > best[c].transform.GetComponent<Everyting>().snake.points)
                            {
                                best.Insert(c, everythings[x]);
                                break;
                            }
                            else if (c == best.Count - 1)
                            {
                                if (best.Count < 50)
                                {
                                    best.Add(everythings[x]);
                                }
                            }
                        }
                    }
                }
            }
            for (int x = 0; x < 50; x++)
            {
                for (int xo = 0; xo < 6; xo++)
                {
                    for (int yo = 0; yo < 5; yo++)
                    {
                        bestbrain[x, xo, yo] = best[x].transform.GetComponent<Everyting>().snake.brain[xo, yo];
                    }
                }
            }
            for (int x = 0; x < 550; x++)
            {
                //if (everythings[x] != null)
                //{
                    Destroy(everythings[x]);
                //}
            }
            Debug.Log(best[0].transform.GetComponent<Everyting>().snake.points + "  " + best[0].transform.GetComponent<Everyting>().snake.died);
            best.Clear();
            for (int x = 0; x < 50; x++)
            {
                GameObject z = Instantiate(Everything, new Vector3(35 * x, 0, 0), Quaternion.identity);
                for (int xo = 0; xo < 6; xo++)
                {
                    for (int yo = 0; yo < 5; yo++)
                    {
                        z.transform.GetComponent<Everyting>().snake.brain[xo, yo] = bestbrain[x, xo, yo];
                    }
                }
                z.transform.GetComponent<Everyting>().snake.transform.GetComponent<Snakeph>().enabled = true;
                z.transform.name = ""+x;
                everythings[x] = z;
            }
            for (int yop = 0; yop <= 5; yop++)
            {
                for (int x = 0; x < 50; x++)
                    {
                    GameObject z = Instantiate(Everything, new Vector3(35 * (50 + x) + (35 * 50 * yop), 0, 0), Quaternion.identity);
                    for (int xo = 0; xo < 6; xo++)
                    {
                        for (int yo = 0; yo < 5; yo++)
                        {
                            z.transform.GetComponent<Everyting>().snake.brain[xo, yo] = (bestbrain[x, xo, yo] + bestbrain[Random.RandomRange(0, 49), xo, yo]) / 2;
                            int rand = Random.Range(0, 100);
                            if (rand >= 98)
                            {
                                z.transform.GetComponent<Everyting>().snake.brain[xo, yo] *= Random.RandomRange(0.1f, 5f);
                            }
                            else if (rand >= 95)
                            {
                                z.transform.GetComponent<Everyting>().snake.brain[xo, yo] *= Random.RandomRange(0.9f, 1.1f);
                            }
                        }
                    }
                    z.transform.GetComponent<Everyting>().snake.transform.GetComponent<Snakeph>().enabled = true;
                    z.transform.name = "" + (50 + x + 50 * yop);
                    everythings[50 + x + (50 * yop)] = z;
                }
            }
            for (int x = 0; x < 50; x++)
            {
                GameObject z = Instantiate(Everything, new Vector3(35 * (350+x), 0, 0), Quaternion.identity);
                for (int xo = 0; xo < 6; xo++)
                {
                    for (int yo = 0; yo < 5; yo++)
                    {
                        z.transform.GetComponent<Everyting>().snake.brain[xo, yo] = bestbrain[x, xo, yo];
                        int rand = Random.Range(0, 100);
                        if (rand > 85)
                        {
                            z.transform.GetComponent<Everyting>().snake.brain[xo, yo] *= Random.Range(-5f, 5f);
                        }
                        if (rand > 75)
                        {
                            z.transform.GetComponent<Everyting>().snake.brain[xo, yo] *= Random.Range(0.95f, 1.1f);
                        }
                    }
                }
                z.transform.name = "" + (350 + x);
                z.transform.GetComponent<Everyting>().snake.transform.GetComponent<Snakeph>().enabled = true;
                everythings[350+x] = z;
            }
            for (int x = 0; x < 50; x++)
            {
                GameObject z = Instantiate(Everything, new Vector3(35 * (400+x), 0, 0), Quaternion.identity);
                for (int xo = 0; xo < 6; xo++)
                {
                    for (int yo = 0; yo < 5; yo++)
                    {
                        z.transform.GetComponent<Everyting>().snake.brain[xo, yo] = bestbrain[x, xo, yo];
                        int rand = Random.Range(0, 100);
                        if (rand > 75)
                        {
                            z.transform.GetComponent<Everyting>().snake.brain[xo, yo] *= Random.Range(0.1f, 5f);
                        }
                    }
                }
                z.transform.name = "" + (400 + x);
                z.transform.GetComponent<Everyting>().snake.transform.GetComponent<Snakeph>().enabled = true;
                everythings[400+x] = z;
            }
            for (int x = 0; x < 50; x++)
            {
                GameObject z = Instantiate(Everything, new Vector3(35 * (450 + x), 0, 0), Quaternion.identity);
                for (int xo = 0; xo < 6; xo++)
                {
                    for (int yo = 0; yo < 5; yo++)
                    {
                        z.transform.GetComponent<Everyting>().snake.brain[xo, yo] = bestbrain[x, xo, yo];
                        int rand = Random.Range(0, 100);
                        if (rand > 98)
                        {
                            if (xo != 4)
                            {
                                z.transform.GetComponent<Everyting>().snake.brain[xo, yo] *= Random.Range(-10000f, 10000f);
                            }
                        }
                        z.transform.GetComponent<Everyting>().snake.brain[xo, yo] *= Random.Range(0.98f, 1.02f);
                    }
                }
                z.transform.name = "" + (450 + x);
                z.transform.GetComponent<Everyting>().snake.transform.GetComponent<Snakeph>().enabled = true;
                everythings[450 + x] = z;
            }
            for (int x = 0; x < 50; x++)
            {
                GameObject z = Instantiate(Everything, new Vector3(35 * (500 + x), 0, 0), Quaternion.identity);
                for (int xo = 0; xo < 6; xo++)
                {
                    for (int yo = 0; yo < 5; yo++)
                    {
                        z.transform.GetComponent<Everyting>().snake.brain[xo, yo] = bestbrain[x, xo, yo];
                        int rand = Random.Range(0, 100);
                        if (rand > 98)
                        {
                           z.transform.GetComponent<Everyting>().snake.brain[xo, yo] *= Random.Range(0.97f, 1.03f);
                        }
                    }
                }
                z.transform.name = "" + (500 + x);
                z.transform.GetComponent<Everyting>().snake.transform.GetComponent<Snakeph>().enabled = true;
                everythings[500 + x] = z;
            }
        }
        else
        {
            for (int x = 0; x < 550; x++)
            {
                GameObject z = Instantiate(Everything, new Vector3(35 * x, 0, 0), Quaternion.identity);
                for (int xo = 0; xo < 5; xo++)
                {
                    for (int yo = 0; yo < 5; yo++)
                    {
                        z.transform.GetComponent<Everyting>().snake.brain[xo, yo] = Random.Range(-10000f,10000f);
                    }
                }
                z.transform.GetComponent<Everyting>().snake.brain[4, 0] = Random.Range(1f, 15f); // changed from 15
                z.transform.GetComponent<Everyting>().snake.brain[4, 1] = Random.Range(0,10000);
                z.transform.GetComponent<Everyting>().snake.brain[4, 2] = Random.Range(0f, 100f);
                for (int zo = 0; zo < 4; zo++)
                {
                    z.transform.GetComponent<Everyting>().snake.brain[5, zo] = Random.Range(-1000f, 1000f);
                }
                z.transform.GetComponent<Everyting>().snake.transform.GetComponent<Snakeph>().enabled = true;
                everythings[x] = z;
            }
        }
        generationNumber += 1;
    }

    void nextGenerationTwo() //smaller population size, in case of lag
    {
            for (int x = 0; x < 550; x++)
            {
                if (everythings[x] != null)
                {
                    if (best.Count == 0)
                    {
                        best.Add(everythings[x]);
                    }
                    else
                    {
                        for (int c = 0; c < best.Count; c++)
                        {
                            if (everythings[x].transform.GetComponent<Everyting>().snake.points > best[c].transform.GetComponent<Everyting>().snake.points)
                            {
                                best.Insert(c, everythings[x]);
                                break;
                            }
                            else if (c == best.Count - 1)
                            {
                                if (best.Count < 50)
                                {
                                    best.Add(everythings[x]);
                                }
                            }
                        }
                    }
                }
            }
            for (int x = 0; x < 50; x++)
            {
                for (int xo = 0; xo < 6; xo++)
                {
                    for (int yo = 0; yo < 5; yo++)
                    {
                        bestbrain[x, xo, yo] = best[x].transform.GetComponent<Everyting>().snake.brain[xo, yo];
                    }
                }
            }
            for (int x = 0; x < 550; x++)
            {
                if (everythings[x] != null)
                {
                Destroy(everythings[x]);
                }
            }
        Debug.Log(best[0].transform.GetComponent<Everyting>().snake.points + "  " + best[0].transform.GetComponent<Everyting>().snake.died);
        if (!best[0].transform.GetComponent<Everyting>().snake.died)
        {
            maxTime = maxTime * 1.2f;
        }
        best.Clear();
            for (int x = 0; x < 50; x++)
            {
                GameObject z = Instantiate(Everything, new Vector3(35 * x, 0, 0), Quaternion.identity);
                for (int xo = 0; xo < 6; xo++)
                {
                    for (int yo = 0; yo < 5; yo++)
                    {
                        z.transform.GetComponent<Everyting>().snake.brain[xo, yo] = bestbrain[x, xo, yo];
                    }
                }
                z.transform.GetComponent<Everyting>().snake.transform.GetComponent<Snakeph>().enabled = true;
                z.transform.name = "" + x;
                everythings[x] = z;
            }
            for (int yop = 0; yop <= 1; yop++)
            {
                for (int x = 0; x < 50; x++)
                {
                    GameObject z = Instantiate(Everything, new Vector3(35 * (50 + x) + (35 * 50 * yop), 0, 0), Quaternion.identity);
                    for (int xo = 0; xo < 6; xo++)
                    {
                        for (int yo = 0; yo < 5; yo++)
                        {
                            z.transform.GetComponent<Everyting>().snake.brain[xo, yo] = (bestbrain[x, xo, yo] + bestbrain[Random.RandomRange(0, 49), xo, yo]) / 2;
                            int rand = Random.Range(0, 100);
                            if (rand >= 98)
                            {
                                z.transform.GetComponent<Everyting>().snake.brain[xo, yo] *= Random.RandomRange(0.1f, 5f);
                            }
                            else if (rand >= 95)
                            {
                                z.transform.GetComponent<Everyting>().snake.brain[xo, yo] *= Random.RandomRange(0.9f, 1.1f);
                            }
                        }
                    }
                    z.transform.GetComponent<Everyting>().snake.transform.GetComponent<Snakeph>().enabled = true;
                    z.transform.name = "" + (50 + x + 50 * yop);
                    everythings[50 + x + (50 * yop)] = z;
                }
            }
        generationNumber += 1;
    }

    void Update()
    {
        currentTime += 1 * Time.deltaTime;
        if (currentTime > maxTime) //automatically circles through generations.
        {
            nextGenerationTwo();
            currentTime = 0;
            if (generationNumber == 3)
            {
                maxTime = 35;
            }
            else if (generationNumber == 7)
            {
                maxTime = 65;
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            nextGenerationOne();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            nextGenerationTwo();
        }
    }
}
