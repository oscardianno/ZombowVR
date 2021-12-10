using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartTarget : MonoBehaviour, IArrowHittable
{
    private float textAlphaValue = 0f;
    private float textAlphaTimer = 0f;
    private float textFullAlphaSeconds = 1f;
    private bool textAlphaIncreasing = true;
    private TextMeshProUGUI txtmp;

    // Start is called before the first frame update
    void Start()
    {
        txtmp = transform.parent.GetComponentInChildren<Canvas>()
            .GetComponentInChildren<TextMeshProUGUI>();
        txtmp.alpha = textAlphaValue;
    }

    // Update is called once per frame
    void Update()
    {
        // Method to make the text blink
        if (textAlphaTimer < textFullAlphaSeconds)
        {
            if (textAlphaIncreasing) {
                textAlphaValue = textAlphaTimer / textFullAlphaSeconds;
            } else {
                textAlphaValue = 1f - (textAlphaTimer / textFullAlphaSeconds);
            }
            textAlphaTimer += Time.deltaTime;
        } else {
            textAlphaTimer = 0;
            textAlphaIncreasing = !textAlphaIncreasing;
        }
        txtmp.alpha = textAlphaValue;
    }

    public void Hit(Arrow arrow)
    {
        // Play music
        FindObjectOfType<MusicHandler>().BeginGameMusic();

        // Get the parent (Title) to disable the whole gameObject
        this.transform.parent.gameObject.SetActive(false);

        // Start the enemySpawner to begin the game
        EnemySpawner enemySpawner = FindObjectOfType<EnemySpawner>();
        enemySpawner.enabled = true;
    }
}
