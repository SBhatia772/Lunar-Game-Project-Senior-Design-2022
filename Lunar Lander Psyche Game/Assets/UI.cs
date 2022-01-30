using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{

    PlayerController player;

    public Text fuelCounterText;

    public Text speedCounterText;

    public Text scoreCounterText;

    float fuel;

    float speed;

    float score;

    void Start()
    {
        GameObject p = GameObject.Find("Lunar Lander");
        player = p.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
        fuel = player.fuel;
        fuelCounterText.text = "Fuel: " + fuel.ToString();
        speed = player.velocity; 
        speedCounterText.text = "Speed: " + speed.ToString();
        score = player.score;
        scoreCounterText.text = "Score: " + score.ToString();
        
    }
}
