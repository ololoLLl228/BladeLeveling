using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmoredMonsterStats : Stats
{
    private Animator animator;
    private Stats stats;
    [SerializeField] private Text levelText;

    private Stats playerStats;

    private void Awake()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<Stats>();
        stats = GetComponent<Stats>();
        animator = GetComponentInChildren<Animator>();
        if (PlayerPrefs.GetInt("IsBegining") == 1)
        {
            int range = Random.Range(0, 2);
            if(range == 0)
            {

                stats.Level = Random.Range(playerStats.Level, playerStats.Level + 3);
            }
            else
            {
                stats.Level = Random.Range(playerStats.Level + 20, playerStats.Level + 30);
            }
        }
        else
        {
            stats.Level = Random.Range(playerStats.Level + 30, playerStats.Level + 40);
        }
        if (stats.Level <= 0)
        {
            stats.Level = 1;
        }
        CountStatsByLevel();
        levelText.text = stats.Level.ToString();

    }

    private protected override void CountStatsByLevel()
    {
        stats.Armor = stats.Level;
        stats.HitPoints = stats.Level;
        stats.AttackSpeed = (3f + 1f * stats.Level)/10;
        animator.SetFloat("AttackSpeed", stats.AttackSpeed);

        stats.Strength = 1f * stats.Level;
    }
}
