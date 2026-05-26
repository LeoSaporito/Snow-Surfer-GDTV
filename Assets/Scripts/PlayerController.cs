using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Vector2 moveVector;
    
    [SerializeField] float torqueAmount = 1f;
    [SerializeField] float baseSpeed = 15f;
    [SerializeField] float boostSpeed = 20f;
    [SerializeField] ParticleSystem powerUpParticles;
    [SerializeField] ScoreManager scoreManager;

    InputAction moveAction;
    Rigidbody2D myRigidbody2D;
    SurfaceEffector2D surfaceEffector2D;

    float previousRotation;
    float totalRotation;    

    bool canControlPlayer = true;

    int activePowerUpCount;
    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        moveAction.Enable();
        myRigidbody2D = GetComponent<Rigidbody2D>();
        surfaceEffector2D = FindFirstObjectByType<SurfaceEffector2D>();
    }
    void Update()
    {
        if (canControlPlayer)
        { 
            RotatePlayer();
            BoostPlayer();
            CalculateFlips();
        }
    }

    void RotatePlayer()
    {
        moveVector = moveAction.ReadValue<Vector2>();

        if (moveVector.x < 0)
        {
            myRigidbody2D.AddTorque(torqueAmount);
        }
        else if (moveVector.x > 0)
        {
            myRigidbody2D.AddTorque(-torqueAmount);
        }
    }

    void BoostPlayer()
    {
        if (moveVector.y > 0)
        {
            surfaceEffector2D.speed = boostSpeed;
        }

        else
        {
            surfaceEffector2D.speed = baseSpeed;
        }
    }

    public void DisableControls()
    {
        canControlPlayer = false;
    }

    void CalculateFlips()
    {
        float currentRotation = transform.rotation.eulerAngles.z;

        totalRotation += Mathf.DeltaAngle(previousRotation, currentRotation);

        if (totalRotation > 330 || totalRotation < -330)
        {
            totalRotation = 0;

            scoreManager.AddScore(100);
        }

        previousRotation = currentRotation;
    }

    public void ActivatePowerUp(PowerUpSO powerUp)
    {
        powerUpParticles.Play();
        activePowerUpCount += 1;
        if (powerUp.GetPowerUpType() == "speed")
        {
            baseSpeed += powerUp.GetValueChange();
            boostSpeed += powerUp.GetValueChange();
        }

        else if (powerUp.GetPowerUpType() == "torque")
        {
            torqueAmount += powerUp.GetValueChange();
        }
    }

    public void DeactivatePowerUp(PowerUpSO powerUp)
    {
        activePowerUpCount -= 1;

        if (activePowerUpCount == 0)
        {
            powerUpParticles.Stop();
        }
        if (powerUp.GetPowerUpType() == "speed")
        {
            baseSpeed -= powerUp.GetValueChange();
            boostSpeed -= powerUp.GetValueChange();
        }

        else if (powerUp.GetPowerUpType() == "torque")
        {
            torqueAmount -= powerUp.GetValueChange();
        }
    }
}
