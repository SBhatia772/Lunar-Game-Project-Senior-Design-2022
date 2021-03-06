using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class landSite : MonoBehaviour
{

    GameObject p;
    PlayerController player;
    //GameObject ignition;
    //GameObject gas;

    public int points;
    // Start is called before the first frame update
    void Start()
    {
        p = GameObject.Find("Lunar Lander");
        player = p.GetComponent<PlayerController>();
        //ignition = GameObject.Find("Ignition");
        //gas = GameObject.Find("Gas Propulltion 0");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //print("COllision!");

        PlayerController.landingSite = true;

        if (collision.gameObject.Equals(p) && player.gameOver == false)
        {
            StartCoroutine(Wait(collision));
        }
    }


    IEnumerator Wait(Collision2D collision)
    {
        yield return new WaitForSeconds(3f);


        Vector2 playerDirection = new Vector2(0, collision.transform.up.y);

        if (player.velocity >= 0.2f || Mathf.Cos(Vector2.Angle(playerDirection, Vector2.down)) >= -0.4f)
        {
            player.gameOver = true;
            print("Game Over!");
            

        }

        else if ( player.gameOver == false)
        {
            print("Points!");
            player.score += points;
            player.gameOver = true;


            yield return new WaitForSeconds(0.5f);

            //EditorApplication.isPlaying = false;
        }
    }


}
