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

    public bool gameEnding = false;

    public static bool landingSite = false;

    public bool landed = false;

    //GameObject ignition;

    GameObject gas;
    GameObject ignition;
    GameObject explosion;
    GameObject gameOverScreen;
    GameObject lander;
    GameObject quitButton;
    GameObject retryButton;



    float getFuel()
    {
        return fuel;
    }
    // Start is called before the first frame update
    void Start()
    {
        gameOver = false;
        quitButton = GameObject.Find("Quit");
        quitButton.GetComponent<Renderer>().enabled = false;
        quitButton.SetActive(false);

        retryButton = GameObject.Find("Retry");
        retryButton.GetComponent<Renderer>().enabled = false;
        retryButton.SetActive(false);

        lander = GameObject.Find("Lunar Lander");
        lander.SetActive(true);
        gameOverScreen = GameObject.Find("GameOver Screen");
        gas = GameObject.Find("Gas Propulltion 0");
        ignition = GameObject.Find("Ignition");
        explosion = GameObject.Find("Explosion");
        rb = GameObject.Find("Lunar Lander").GetComponent<Rigidbody2D>();
        rb.AddForce(transform.right * 50f);
        explosion.GetComponent<Renderer>().enabled = false;
        gameOverScreen.GetComponent<Renderer>().enabled = false;
    }



   
    // Update is called once per frame
    void FixedUpdate()
    {

        velocity = rb.velocity.magnitude;

        if (Input.GetKey(KeyCode.Space) && fuel > 0 && gameEnding == false)
        {
            rb.AddForce(transform.up * 0.200f);

            fuel -= 0.1f;

            StartCoroutine(WaitRoutine());
            
        }
        else
        {
            ignition.GetComponent<Renderer>().enabled = false;
            gas.GetComponent<Renderer>().enabled = false;
        }



        if (Input.GetKey(KeyCode.A) && gameEnding == false)
        {
            
            transform.Rotate(new Vector3(0, 0, 3f));
        }

        if(Input.GetKey(KeyCode.D) && gameEnding == false)
        { 
            transform.Rotate(new Vector3(0, 0, -3f));
        }


        if(transform.position.y >= 6.79f)
        {
            transform.position = new Vector3(transform.position.x, 6.15f, transform.position.z);
        }

        if(transform.position.x <= -11.63f)
        {
            transform.position = new Vector3(-11f, transform.position.y, transform.position.z);
        }

        if (transform.position.x >= 7.46f)
        {
            transform.position = new Vector3(7.15f, transform.position.y, transform.position.z);
        }


        if (fuel < 0)
            fuel = 0;

       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        Vector2 playerDirection = new Vector2(0, transform.up.y);

        if(collision.gameObject.name.Equals("Site 1") || collision.gameObject.name.Equals("Site 2") || collision.gameObject.name.Equals("Site 3") || collision.gameObject.name.Equals("Site 4") || collision.gameObject.name.Equals("Site 5") || collision.gameObject.name.Equals("Site 6") || collision.gameObject.name.Equals("Site 7"))
        {
            landingSite = true;
        }

        if (velocity >= 0.2f || Mathf.Cos(Vector2.Angle(playerDirection, Vector2.down)) >= -0.4f)
        {
            gameOver = true;
            //print("Game Over!");

            gameOverScreen.GetComponent<Renderer>().enabled = true;

            StartCoroutine(Wait());

        }
        else if (landingSite == false)
        {
            StartCoroutine(CloseGame());
        }
        else if (landingSite == true)
        {
            Debug.Log("Close Game Coroutine Started!");
            gameOver = false;
            gameEnding = true;
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
        
        lander.SetActive(false);
        quitButton.SetActive(true);
        retryButton.SetActive(true);

        gameOverScreen.GetComponent<Renderer>().enabled = true;
        quitButton.GetComponent<Renderer>().enabled = true;
        retryButton.GetComponent<Renderer>().enabled = true;
        
        
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

        Vector2 playerDirection = new Vector2(0, transform.up.y);

        if (velocity >= 0.2f || Mathf.Cos(Vector2.Angle(playerDirection, Vector2.down)) >= -0.4f)
        {
            gameOver = true;
            
            score = 0;  

            StartCoroutine(Wait());
        }

        Debug.Log(landingSite.ToString());  
        Debug.Log("gameOver: " + gameOver.ToString());

        yield return new WaitForSeconds(1f);

        if (gameOver == false && landingSite == false) //we haven't reached a landing site but we have landed
        {
            score += 100;
            //print("Normal points");

            yield return new WaitForSeconds(1f);

            //show you win screen
        }
        else if (landingSite == true)
        {
            yield return new WaitForSeconds(1f);
            Debug.Log("Success");
            UnityEditor.EditorApplication.ExitPlaymode();
        }

        
    }
}
