using UnityEngine;

public class SnowTrail : MonoBehaviour
{
    [SerializeField] ParticleSystem snowTrailParticles;

    int layerIndex;

    void Start()
    {
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        layerIndex = LayerMask.NameToLayer("Floor");

        if (collision.gameObject.layer == layerIndex)
        { 
            snowTrailParticles.Play();            
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        layerIndex = LayerMask.NameToLayer("Floor");
        if (collision.gameObject.layer == layerIndex)
        { 
            snowTrailParticles.Stop();    
        }
    }
}
