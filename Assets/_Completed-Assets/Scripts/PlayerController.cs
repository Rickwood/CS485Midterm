using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    public float speed;
    public Text countText;
    public Text winText;
    public bool isGameOver = false;

    private Rigidbody2D rb2d;
    private int count;
    private bool gameOver = false;

    private void Start()
    {
        GameObject.FindGameObjectWithTag("Background").GetComponent<AudioSource>().Play();
        rb2d = GetComponent<Rigidbody2D>();
        count = 0;
        SetCountText();
        winText.text = "";
    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        if (gameOver == false)
            rb2d.AddForce(movement * speed);
        else
        {
            rb2d.velocity = Vector3.zero;
            rb2d.velocity = Vector3.zero;
        }
    }

    private void Update()
    {
        if (gameOver == true)
        {
            rb2d.velocity = Vector3.zero;
            rb2d.velocity = Vector3.zero;
            GameOver();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            gameObject.GetComponent<AudioSource>().Play();
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }
        if (other.gameObject.CompareTag("Hazard"))
        {
            GameObject.FindGameObjectWithTag("Background").GetComponent<AudioSource>().Stop();
            GameObject.FindGameObjectWithTag("Hazard").GetComponent<AudioSource>().Play();
            gameOver = true;
            isGameOver = true;
            winText.text = "You Lose!!  Don't touch the boulders to survive!  Please try again!! \r\n Press 'Esc' to return to the Menu";
        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 12)
        {
            GameObject.FindGameObjectWithTag("Background").GetComponent<AudioSource>().Stop();
            winText.text = "YOU WIN!!!  YOU'RE SO AMAZING OMG I CAN'T BELIEVE YOU BEAT THE HARDEST GAME OF ALL TIME!!!!!!!!!! \r\n Press 'Esc' to return to the Menu";
            GameObject.FindGameObjectWithTag("EndSong").GetComponent<AudioSource>().Play();
            gameOver = true;
            Vector2 movement = new Vector2(0, 0);
        }
    }

    void GameOver()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
