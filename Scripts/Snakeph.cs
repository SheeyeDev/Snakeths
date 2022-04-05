using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snakeph : MonoBehaviour
{
    public float speed;
    float wait;
    public Vector3 direction;
    public int points;
    public GameObject tail;
    public Transform children;
    List<Rigidbody2D> children2 = new List<Rigidbody2D>();
    public bool died;
    public FoodGen generator;
    public float[,] brain = new float[6,5];
    public LayerMask mask;
    float widzimisie;
    public float max = 0;
    public float moveleft;
    public float moveright;
    public float moveup;
    public float movedown;
    public float brain1;
    public float brain2;
    public int addon;
    public float hunger;
    private int thisdir;
    private int previousdir;
    private Rigidbody2D rb;
    private void Awake()
    {
        rb = transform.GetComponent<Rigidbody2D>();
        speed = Random.Range(23f, 27f);
        widzimisie = 50000;
    }
    void Update()
    {
        if (!died)
        {
            if (hunger >= 625)
            {
                died = true;
            }
            hunger += Time.deltaTime * speed;
            wait += speed * Time.deltaTime;
            for (int x = 0; x < 4; x++)
            {
                brain[0, x] = 0;
                brain[0, x] += brain[5, x] * hunger * brain[4,2];
            }

            widzimisie += 1 * Time.deltaTime;
            if (widzimisie > brain[4,0])
            {
                widzimisie = 0;
                addon = Random.Range(0, 3);
            }
            brain[0, addon] += brain[4, 1];
            sprawdz(-Vector3.right, 0);
            sprawdz(Vector3.up, 1);
            sprawdz(-Vector3.up, 2);
            sprawdz(Vector3.right, 3);
            direction = Vector3.Normalize(-Vector3.right);
            brain[0, previousdir] = -999999999999999;
            max = brain[0, 0];
            moveleft = brain[0, 0];
            thisdir = 3;
            moveright = brain[0, 3];
            movedown=brain[0,2];
            moveup = brain[0, 1];
            if (brain[0,1]>max)
            {
                 direction = Vector3.Normalize(Vector3.up);
                 max = brain[0, 1];
                thisdir = 2;
            }
            if (brain[0, 2] > max)
            {
                direction = -Vector3.Normalize(Vector3.up);
                max = brain[0, 2];
                thisdir = 1;
            }
            if (brain[0, 3] > max)
            {
                 direction = Vector3.Normalize(Vector3.right);
                 max = brain[0, 3];
                thisdir = 0;
            }
            if (wait > 1)
            {
                wait = 0;
                Move();
                previousdir = thisdir;
            }
        }
    }

    void sprawdz(Vector3 dir, int op)
    {
        RaycastHit2D hit;
        hit = Physics2D.Raycast(transform.position, dir * 50f, 50f, mask);
        if (hit.collider!=null)
        {
            if (hit.transform.tag == "foo")
            {
                for (int x = 1; x < 4; x++)
                {
                    brain[0, op] += brain[x, 0] * (50-Vector3.Magnitude(transform.position - new Vector3(hit.point.x, hit.point.y, transform.position.z)));
                }
            }
            if (hit.transform.tag == "wall")
            {
                for (int x = 1; x < 4; x++)
                {
                    brain[0, op] += brain[x, 1] * (50 - Vector3.Magnitude(transform.position - new Vector3(hit.point.x, hit.point.y, transform.position.z)));
                }
            }
            if (hit.transform.tag == "tail")
            {
                for (int x = 1; x < 4; x++)
                {
                    brain[0, op] += brain[x, 2] * (50 - Vector3.Magnitude(transform.position - new Vector3(hit.point.x, hit.point.y, transform.position.z)));
                }
            }
        }
    }

    private void Move()
    {
        //Vector3 prevpos = transform.position;
        //rb.MovePosition(transform.position + direction);
        if (children.childCount > 0)
        {
            for(int x = children.childCount-1; x >=0; x--)
            {
                if (children.GetChild(x).GetComponent<tail>().offset == 0)
                {
                    if (x == 0)
                    {
                        children.GetChild(x).transform.position = transform.position;
                        //children2[x].MovePosition(prevpos);
                    }
                    else
                    {
                        children.GetChild(x).transform.position = children.GetChild(x - 1).transform.position;
                        //children2[x].MovePosition(children.GetChild(x - 1).transform.position);
                    }
                }
                else
                {
                    children.GetChild(x).GetComponent<tail>().offset -= 1;
                    //children.GetChild(x).GetComponent<BoxCollider2D>().isTrigger = false;
                }
            }
        }
        transform.position = transform.position + direction;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "foo")
        {
            Destroy(other.gameObject);
            points += 1;
            hunger = 0;
            GameObject x = Instantiate(tail, transform.position, Quaternion.identity);
            x.transform.parent = children;
            x.transform.name = "tailor";
            children2.Add(x.transform.GetComponent<Rigidbody2D>());
            if (children.childCount == 1)
            {
                x.transform.GetComponent<BoxCollider2D>().enabled = false;
            }
            generator.Generate();
        }
        if (other.gameObject.tag == "wall")
        {
            died = true;
            transform.parent.name = "dead";
            transform.GetComponent<BoxCollider2D>().enabled = false;
            Destroy(children.gameObject);
        }
        if (other.gameObject.tag == "tail")
        {
            if (other.transform.GetComponent<tail>().offset <= 0)
            {
                if (other.transform != children.GetChild(0)&&wait>0.5f)
                {
                    transform.GetComponent<SpriteRenderer>().color = Color.blue;
                    died = true;
                    transform.parent.name = "dead";
                    transform.GetComponent<BoxCollider2D>().enabled = false;
                    Destroy(children.gameObject);
                }
            }
        }
    }
}
