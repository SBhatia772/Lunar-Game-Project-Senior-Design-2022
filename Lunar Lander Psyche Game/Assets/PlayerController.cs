using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;

    public int score;

    public float velocity;

    public float fuel = 1000;

    public bool gameOver = false;

    public static bool landingSite = false;

    public bool landed = false;

    //GameObject ignition;

    GameObject gas;
    GameObject ignition;
    GameObject explosion;



    float getFuel()
    {
        return fuel;
    }
    // Start is called before the first frame update
    void Start()
    {
        gas = GameObject.Find("Gas Propulltion 0");
        ignition = GameObject.Find("Ignition");
        explosion = GameObject.Find("Explosion");
        rb = GameObject.Find("Lunar Lander").GetComponent<Rigidbody2D>();
        rb.AddForce(transform.right * 50f);
        explosion.GetComponent<Renderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

        velocity = rb.velocity.magnitude;

        if (Input.GetKey(KeyCode.Space) && fuel > 0)
        {
            rb.AddForce(transform.up * 0.050f);

            fuel -= 0.1f;

            StartCoroutine(WaitRoutine());
            
        }
        else
        {
            ignition.GetComponent<Renderer>().enabled = false;
            gas.GetComponent<Renderer>().enabled = false;
        }

        if(Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0, 0, 1f);
        }

        if(Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0, 0, -1f);
        }


        if(transform.position.y >= 6f)
        {
            transform.position = new Vector3(transform.position.x, 5.847f, transform.position.z);
        }

        if(transform.position.x <= -14.25f)
        {
            transform.position = new Vector3(-14.13f, transform.position.y, transform.position.z);
        }

        if (transform.position.x >= 10.69f)
        {
            transform.position = new Vector3(10.3f, transform.position.y, transform.position.z);
        }


        if (fuel < 0)
            fuel = 0;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (velocity >= 0.2f || Mathf.Cos(Vector2.Angle(transform.up, Vector2.down)) >= -0.5f)
        {
            gameOver = true;
            print("Game Over!");

            StartCoroutine(Wait());

        }
        else if (landingSite == false && landed == false)
        {
            landed = true;
            StartCoroutine(CloseGame());
        }
        
    }

    IEnumerator Wait()
    {
        gas.GetComponent<Renderer>().enabled = false;
        rb.GetComponent<Renderer>().enabled = false;
        ignition.GetComponent<Renderer>().enabled = false;
        explosion.GetComponent<Renderer>().enabled = true;

        yield return new WaitForSeconds(0.1f);

        explosion.GetComponent<Renderer>().enabled = false;

        EditorApplication.isPlaying = false;
    }



    IEnumerator WaitRoutine()
    {

        ignition.GetComponent<Renderer>().enabled = true;

        yield return new WaitForSeconds(1f);

        ignition.GetComponent<Renderer>().enabled = false;

        gas.GetComponent<Renderer>().enabled = true;
        
    }

    IEnumerator CloseGame()
    {

        yield return new WaitForSeconds(2f);

        if (velocity >= 0.2f || Vector2.Dot(transform.up, Vector2.down) >= 0)
        {
            gameOver = true;
            print("Game Over!");
            EditorApplication.isPlaying = false;

        }

        yield return new WaitForSeconds(3f);
        if (gameOver == false && landingSite == false) //we haven't reached a landing site but we have landed
        {
            score += 100;
            print("Normal points");

            yield return new WaitForSeconds(1f);

            EditorApplication.isPlaying = false;
        }

        
    }
}
