using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] int currentScore;
    public void ChangeScore(int change)
    {
        currentScore += change;
    }
    public int GetScore() { return currentScore; }
    public void SaveCurrentScore() { PlayerPrefs.SetInt("LEGNER-STUDIO-GAME*TD*" + "LIVESCORE", currentScore); }
    public void LoadCurrentScore() { currentScore = PlayerPrefs.GetInt("LEGNER-STUDIO-GAME*TD*" + "LIVESCORE", 0); }
    public void ResetScore() { currentScore = 0; } 
}
