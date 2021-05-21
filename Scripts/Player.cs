using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private GameManager gameManager;

    private float topBound = 3.5f;
    private float botBound = -3.5f;

    public float speed = 50f;

    private float verticalInput;


    private void Start()
    {
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        verticalInput = Input.GetAxisRaw("Vertical");

        transform.Translate(Vector2.up * verticalInput * speed * Time.deltaTime);

        LevelBounds();
    }

    void LevelBounds()
    {
        if (transform.position.y > topBound)
        {
            transform.position = new Vector2(transform.position.x, topBound);
        }
        if (transform.position.y < botBound)
        {
            transform.position = new Vector2(transform.position.x, botBound);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            gameManager.GameOver();
        }
    }
}
