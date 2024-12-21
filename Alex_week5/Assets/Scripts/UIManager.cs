using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public Image hp3;
    public Image hp2;
    public Image hp1;
    public Image hp0;

    public TMP_Text gameOverText;

    void Update()
    {
        int playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().playerHP;
        int waveNumber = GameObject.FindFirstObjectByType<SpawnManager>().enemyWave;
        //This is a very Undertale way of doing this, but it works!
        if (playerHealth == 3)
        {
            hp3.enabled = true;
            hp2.enabled = false;
            hp1.enabled = false;
            hp0.enabled = false;
        }
        else if (playerHealth == 2)
        {
            hp3.enabled = false;
            hp2.enabled = true;
            hp1.enabled = false;
            hp0.enabled = false;
        }
        else if (playerHealth == 1)
        {
            hp3.enabled = false;
            hp2.enabled = false;
            hp1.enabled = true;
            hp0.enabled = false;
        }
        else if (playerHealth == 0)
        {
            hp3.enabled = false;
            hp2.enabled = false;
            hp1.enabled = false;
            hp0.enabled = true;
            gameOverText.text = "GAME OVER! YOU MADE IT TO WAVE " + waveNumber.ToString() + "! PRESS 'R' TO RESTART!";
        }
    }
}
