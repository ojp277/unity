using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {
    public float jumpSpeed = 300.0f;
    public float moveSpeed = 10.0f;
    public int playerLives = 10;
    public int coinPickups = 0;
    public Text livesCounter;
    public Text coinCounter;
    bool facingRight = true;
    public float smoothTimeY = 0.1f;
    public float smoothTimeX = 0.1f;
    GameObject Camera;
    Vector2 spawnPoint;
    Vector2 cameraVelocity;
    Animator anim;
    Rigidbody2D rigid;

    // Use this for initialization
    void Start () {
        spawnPoint = transform.position;
        Camera = GameObject.FindGameObjectWithTag("MainCamera");
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        UpdateCounter();

    }
	
	// Update is called once per frame
	void Update () {
        float posX = Mathf.SmoothDamp(Camera.transform.position.x, transform.position.x, ref
        cameraVelocity.x, smoothTimeX);
        float posY = Mathf.SmoothDamp(Camera.transform.position.y, transform.position.y, ref
        cameraVelocity.y, smoothTimeY);
        Camera.transform.position = new Vector3(posX, posY, Camera.transform.position.z);



    }

    void FixedUpdate(){
        float move = Input.GetAxis("Horizontal");
        rigid.velocity = new Vector2(move * moveSpeed, rigid.velocity.y);

        if (move > 0 && !facingRight)
        {
            FlipFacing();
        }
        else if (move < 0 && facingRight)
        {
            FlipFacing();
        }

        if (Input.GetButtonDown("Jump") && rigid.velocity.y == 0)
        {
            rigid.AddForce(Vector2.up * jumpSpeed);
        }

    }

    void FlipFacing()
    {
        facingRight = !facingRight;
        Vector3 charScale = transform.localScale;
        charScale.x = charScale.x * -1;
        transform.localScale = charScale;
    }

    void UpdateCounter()
    {
        livesCounter.text = playerLives.ToString();
        coinCounter.text = coinPickups.ToString();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Coin")
        {
            DestroyObject(col.gameObject);
            coinPickups = coinPickups + 1;
            UpdateCounter();
        }
        if (col.gameObject.tag == "Deathzone")
        {
            if (playerLives > 0)
            {
                playerLives = playerLives - 1;
                transform.position = spawnPoint;
                //Application.LoadLevel("otto");
            }
            else
            {
                livesCounter.text = "10";
                coinCounter.text = "0";
                transform.position = spawnPoint;
                //Application.LoadLevel("otto");
                Destroy(gameObject);
            }
        }
        UpdateCounter();
    }
}




