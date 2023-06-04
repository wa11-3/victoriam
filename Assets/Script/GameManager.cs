using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private GameObject[] Structures;
    private GameObject[] Enemies;

    [SerializeField] private TMP_Text structuresText;
    [SerializeField] private TMP_Text enemiesText;
    [SerializeField] private RectTransform healthBar;
    [SerializeField] private GameObject explotion;

    private int structuresCount;
    private int enemiesCount;
    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        Structures = GameObject.FindGameObjectsWithTag("Structure");
        structuresCount = Structures.Length;
        structuresText.text = $"{structuresCount}";

        Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemiesCount = Enemies.Length;
        enemiesText.text = $"{enemiesCount}";
    }

    private void Update()
    {
        //Debug.Log("You Win");
    }

    public void DestroyedStructure(Vector3 spawn)
    {
        structuresCount -= 1;
        structuresText.text = $"{structuresCount}";
        Instantiate(explotion, spawn, Quaternion.identity);
        if (structuresCount <= 0)
        {
            Debug.Log("You Win");
        }
    }

    public void DestroyedEnemies(Vector3 spawn)
    {
        enemiesCount -= 1;
        enemiesText.text = $"{structuresCount}";
        Instantiate(explotion, spawn, Quaternion.identity);
    }

    public void Damage()
    {
        healthBar.sizeDelta -= new Vector2(110, 0);
    }
}
