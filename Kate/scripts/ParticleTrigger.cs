using System.Xml.Schema;
using UnityEngine;

public class ParticleTrigger : MonoBehaviour
{
    public ParticleSystem collisionParticleSystem;
    public SpriteRenderer sr;
    public bool once = true;

    private void OnTriggerEnter2D(Collider2D other){

        if (other.gameObject.CompareTag("Player") && once){

            var em = collisionParticleSystem.emission;
            var dur = collisionParticleSystem.main.duration; 

            em.enabled = true;
            collisionParticleSystem.Play();

            once = false;
            Destroy(sr);
            Invoke(nameof(DestroyObj), dur);

        }
    }

    void DestroyObj(){
        Destroy(gameObject);

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
