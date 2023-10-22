 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BallController : MonoBehaviour
{
    // Start is called before the first frame update

    private bool ignoreNextCollision;
    public Rigidbody rb;
    public float speed = 5.0f;

    private Vector3 startPos;

    public int perfectPass; 
    public bool isSuperSpeedActive;

    public AudioSource landSound;

    public GameObject splatPrefab;

    public AudioSource perfectPassAudio;
    
 
     private void Awake() 
    {
        startPos = transform.position;
    }
    
    private void OnCollisionEnter(Collision collision) 
    {
        if (ignoreNextCollision)
            return;
        
        if(isSuperSpeedActive)
            if(!collision.transform.GetComponent<Goal>())
                Destroy(collision.transform.parent.gameObject);
                Debug.Log("Destroying platform");





        //Reset Level functionality via DeathPart
        DeathPart deathPart = collision.transform.GetComponent<DeathPart>();
        if(deathPart)
            deathPart.HitDeathPart();
       // Debug.Log("Ball touched something");

        rb.velocity = Vector3.zero;
        rb.AddForce(Vector3.up * speed, ForceMode.Impulse);
        //Ball SFX
        landSound.Play();

        //Ball breaking Ground Animation
        GameObject newSplat = Instantiate(splatPrefab, new Vector3(transform.position.x, collision.transform.position.y, transform.position.z), transform.rotation);

        ignoreNextCollision = true;
        Invoke("AllowCollision", .2f);

        perfectPass = 0;
        isSuperSpeedActive = false;
        perfectPassAudio.Stop();
    }

    private void Update() {
        if(perfectPass >=3 && !isSuperSpeedActive)
        {
            isSuperSpeedActive = true;
            rb.AddForce(Vector3.down * 10, ForceMode.Impulse);
            perfectPassAudio.Play();
            Debug.Log("Perfect Pass!");
        }
    
    }

    private void AllowCollision() 
    {
        ignoreNextCollision = false;
    }

    // Update is called once per frame
    public void ResetBall()
    {
        transform.position = startPos;
    }
}
