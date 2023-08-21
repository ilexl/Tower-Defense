using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lives : MonoBehaviour
{
    [SerializeField] int maxLives;
    [SerializeField] int currentLives;
    private void Awake() { currentLives = maxLives; }
    public int GetCurrentLives() { return currentLives; }
    public void ChangeLives(int change) { currentLives += change; }
}
