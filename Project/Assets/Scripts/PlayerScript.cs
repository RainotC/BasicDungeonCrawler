using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerScript : MonoBehaviour
{
    private float horizontal;
    private float vertical;
    private float speed = 4.0f;
    Rigidbody2D rb;
    
    private float health = 100;

    public bool turnedRight = false;
    public Image healthFill;
    private float healthWidth;

    public TextMeshProUGUI mainText;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        healthWidth = healthFill.sprite.rect.width;
    }


    // Update is called once per frame
    void Update()
    {
        horizontal= Input.GetAxisRaw("Horizontal");
        vertical= Input.GetAxisRaw("Vertical");

        rb.velocity = new Vector2(horizontal*speed, vertical*speed);
        turnedRight = false;
        if(horizontal > 0)
        {
            GetComponent<Animator>().Play("Right");
            turnedRight = true;
        } else if (horizontal < 0)
        {
            GetComponent<Animator>().Play("Left");
        } else if (vertical > 0)
        {
            GetComponent<Animator>().Play("Up");
        } else if (vertical < 0)
        {
            GetComponent<Animator>().Play("Down");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            transform.GetChild(0).gameObject.SetActive(true);
            health = health - collision.gameObject.GetComponent<EnemyScript>().GetHitStrength();
            if(health < 1.0f)
            {
                healthFill.enabled = false;
                mainText.gameObject.SetActive(true);
                mainText.text = "Game Over";
            }
            Vector2 temp = new Vector2(healthWidth * (health/100), healthFill.sprite.rect.height); //Code from tutorial - makes height wierd
            //Vector2 temp = new Vector2(healthWidth, 40);
            healthFill.rectTransform.sizeDelta = temp;
            Invoke("HidePlayerBlood", 0.25f);
            
        }
    }

    void HidePlayerBlood()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }
    
}
