using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarHealth : MonoBehaviour
{
    [SerializeField] private int health = 5;
    private int MaxHealth;
    public Slider healthBar;
    public Image fill;
    public Gradient gradient;
    // Start is called before the first frame update
    void Start()
    {
        MaxHealth = health;
        if (healthBar != null)
        {
            healthBar.value = health;
            fill.color = gradient.Evaluate(1.0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.C))
        {
            DamageFromPlayer();
        }
    }
    //Updates the Healthbar UI
    void UpdateBarUI()
    {
        if (healthBar != null)
        {
            healthBar.value = health;
            fill.color = gradient.Evaluate(healthBar.normalizedValue);
        }
    }
    //Gets the health
    public int GetHealth()
    {
        return health;
    }
    public void ReplenishHealth()
    {
        if(health <= 0)
            health = Mathf.Clamp(health+2, 1, MaxHealth);
        UpdateBarUI();
    }
    public void DamageFromPlayer()
    {
        health = Mathf.Clamp(health - 1, 0, MaxHealth);
        UpdateBarUI();
    }
    public void DamageFromPowerUp()
    {
        health = Mathf.Clamp(health - 2, 0, MaxHealth);
        UpdateBarUI();
    }

    public void ResetHealth()
    {
        health = MaxHealth;
        UpdateBarUI();
    }
}
