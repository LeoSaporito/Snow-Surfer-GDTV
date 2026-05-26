using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    [SerializeField] PowerUpSO powerUp;
    PlayerController playerControllerScript;
    SpriteRenderer spriteRenderer;

    float timeLeft;
    private void Start()
    {
        playerControllerScript = FindFirstObjectByType<PlayerController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        timeLeft = powerUp.GetTime();
    }

    void Update()
    {
        CountdownTimer();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        int layerIndex = LayerMask.NameToLayer("Player");

        if (collision.gameObject.layer == layerIndex && spriteRenderer.enabled)
        {
            spriteRenderer.enabled = false;
            playerControllerScript.ActivatePowerUp(powerUp);
        }
    }

    void CountdownTimer()
    {
        if (spriteRenderer.enabled == false)
        {
            if (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;

                if (timeLeft <= 0)
                {
                    playerControllerScript.DeactivatePowerUp(powerUp);
                }
            }
        }
    }
}
