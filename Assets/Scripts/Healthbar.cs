using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Image healthsprite;

    public void UpdateHealthBar(float maxHealth,float currentHealth)
    {
        healthsprite.fillAmount = currentHealth / maxHealth;
    }
}
