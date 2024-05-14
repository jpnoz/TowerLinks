using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextWaveButtonController : MonoBehaviour
{
    public GameObject nextWaveButton;
    
    private void OnEnable()
    {
        EnemySpawner.OnFinishedSpawning += ShowButton;
    }

    private void OnDisable()
    {
        EnemySpawner.OnFinishedSpawning -= ShowButton;
    }

    public void HideButton()
    {
        nextWaveButton.SetActive(false);
    }

    void ShowButton()
    {
        nextWaveButton.SetActive(true);
    }
}
