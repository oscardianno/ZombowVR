using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorTarget : MonoBehaviour
{
    private float initialHealth = 100;
    [SerializeField]
    private float health;
    private bool stillStanding = true;

    private float timer = 0f;
    private float blackOutTime = 3f;
    private bool goingBlack = false;
    private float canvasAlphaValue = 4f;
    private Image blackCanvasImage;

    [Header("HealthBar resources")]
    public Image healthBar;

    // Start is called before the first frame update
    void Start()
    {
        health = initialHealth;
        blackCanvasImage = GameObject.FindGameObjectWithTag("BlackCanvas")
        .GetComponentInChildren<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (goingBlack)
        {
            if (timer < blackOutTime)
            {
                canvasAlphaValue = timer / blackOutTime;
                timer += Time.deltaTime;
            }
            else
            {
                goingBlack = false;
            }
            Color currentColor = blackCanvasImage.color;
            currentColor.a = canvasAlphaValue;
            blackCanvasImage.color = currentColor;
        }
    }

    public void Hit(int damage)
    {
        if (stillStanding)
        {
            health -= damage;
            healthBar.fillAmount = health / initialHealth;
            if (health <= 0)
            {
                stillStanding = false;
                //Destroy(gameObject);
                //Destroy(this);
                Component[] components = GetComponentsInChildren<MeshRenderer>();
                foreach (Component component in components)
                {
                    component.gameObject.SetActive(false);
                }
                //gameObject.SetActive(false);
                StartCoroutine(Defeat());
            }
        }
    }

    public IEnumerator Defeat()
    {
        // Play defeat music
        FindObjectOfType<MusicHandler>().PlayDefeatSong();
        // Wait 1 second
        yield return new WaitForSeconds(1f);
        // Turn off the lights gradually
        goingBlack = true;
        // Wait 5 seconds
        yield return new WaitForSeconds(5f);
        // Restart
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
