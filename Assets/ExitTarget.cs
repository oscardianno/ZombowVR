using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitTarget : MonoBehaviour, IArrowHittable
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Hit(Arrow arrow)
    {
        // FindObjectOfType<MusicHandler>().PlayByeSound();
        StartCoroutine(Quit());
    }

    public IEnumerator Quit()
    {
        yield return new WaitForSeconds(1f);
        Application.Quit();
    }
    
}
