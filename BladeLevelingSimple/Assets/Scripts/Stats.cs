using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public float HitPoints {get; set;}
    public int Level { get; set; }
    public float Armor { get; set; }
    public float AttackSpeed { get; set; }
    public float Strength { get; set; }

    public static bool isBegining = true;

    private void Awake()
    {
        PlayerPrefs.SetInt("IsBegining", 1);
    }
    private void Start()
    {

        PlayerPrefs.SetInt("IsBegining", 0);
    }
    private protected virtual void CountStatsByLevel()
    {

    }
}
