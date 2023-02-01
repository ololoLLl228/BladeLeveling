using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : Stats
{
    

    private Animator animator;
    [SerializeField] private Text levelText;


    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        
        Level = 1;
        CountStatsByLevel();
        animator.SetFloat("AttackSpeed", AttackSpeed);
        levelText.text = Level.ToString();

    }

    private protected override void CountStatsByLevel()
    {
        HitPoints = Level;
        Strength = Level;
        
    }

    public void GetExperience(int enemyLevel)
    {

        Level += enemyLevel;
        CountStatsByLevel();
        levelText.text = Level.ToString();

        

    }

   
}
