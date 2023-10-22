using System.Collections.Generic;
using UnityEngine;

public class DeathPart : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource deathSound;
    private void OnEnable()
    {
            GetComponent<Renderer>().material.color = Color.red;

    }

    public void HitDeathPart () 
    {
        GameManager.singleton.RestartLevel();
        deathSound = GetComponent<AudioSource>();
        GameManager.singleton.deathSound.Play();
        
        
    }
}
  