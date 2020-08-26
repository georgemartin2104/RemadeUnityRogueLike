using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerController : MonoBehaviour {

    public float speed;
    public float jumpHeight;
    private Rigidbody2D rb2d;
    public static int p1WallHealth = 0;
    public bool isAirborne = true;
    public float jumpSpeed;
    private Animator animator;
    public GameObject playerHitboxPrefab;
    private int tmpH = 0;
    private int tmpV = 0;
    public Slider p1HealthSlider;
    public Text p1ComboText;
    private int currentCombo = 0;
    public Text winText;

	// Use this for initialization
	void Start ()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        p1ComboText.text = "";
        p1ComboText.fontSize = 12;
        winText.text = "";
	}
	
	// Update is called once per frame
	void Update ()
    {
        bool tryAttack = Input.GetButtonDown("Attack");

        if (isAirborne)
        {
            rb2d.drag = 0.6F;
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = 0F;
            Vector2 movement = new Vector2(moveHorizontal, moveVertical);
            rb2d.AddForce(movement * speed);
        }
        if(!isAirborne)
        {
            rb2d.drag = 0.6F;
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = 0F;
            bool tryJump = Input.GetButtonDown("Jump");
            if (tryJump)
            {
                moveVertical = jumpSpeed;
                isAirborne = true;
            }
            Vector2 movement = new Vector2(moveHorizontal, moveVertical);
            rb2d.AddForce(movement * speed);
        }
        if(tryAttack)
        {
            animator.SetTrigger("playerChop");
            GameObject x = Instantiate(playerHitboxPrefab);
            x.transform.position = gameObject.transform.position;
        }
        p1HealthSlider.value = p1WallHealth;
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Floor"))
        {
            isAirborne = false;
            currentCombo = 0;
            p1ComboText.text = "";
            p1ComboText.fontSize = 12;
        }
        if(other.gameObject.CompareTag("Player2Hitbox"))
        {
            int verticalSpeed = Random.Range(25, 100);
            int horizontalSpeed = Random.Range(-25, -70);
            tmpH = horizontalSpeed;
            tmpH = tmpH * -1;
            tmpV = verticalSpeed;
            tmpV = tmpV * -1;
            Vector2 movement = new Vector2(horizontalSpeed, verticalSpeed);
            rb2d.AddForce(movement * speed);
            currentCombo++;
            p1ComboText.fontSize++; p1ComboText.fontSize++;
            p1ComboText.text = "x" + currentCombo + " Combo!";
        }
        if (other.gameObject.CompareTag("Player1Wall"))
        {
            other.gameObject.SetActive(false);
            p1WallHealth = p1WallHealth - 10;
            Vector2 movement = new Vector2(tmpH, tmpV);
            rb2d.AddForce(movement * speed);
            if (p1WallHealth <= 0)
            {
                winText.text = "Player 2 Wins!";
            }
        }
    }
}
