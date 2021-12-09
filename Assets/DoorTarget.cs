using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorTarget : MonoBehaviour
{
    private float initialHealth = 100;
    [SerializeField]
    private float health;

    [Header("HealthBar resources")]
    public Image healthBar;

    // Start is called before the first frame update
    void Start()
    {
        health = initialHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Hit(int damage)
    {
        health -= damage;
        healthBar.fillAmount = health / initialHealth;
        if (health <= 0)
        {
            gameObject.SetActive(false);
            //Destroy(gameObject);
            //Destroy(this);
        }
    }
}
