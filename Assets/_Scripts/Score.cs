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
}
