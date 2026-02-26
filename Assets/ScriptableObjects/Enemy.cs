using UnityEngine;


[CreateAssetMenu(fileName = "Enemy", menuName = "Scriptable Objects/Enemy")]
public enum EnemyType
{
    Flying,
    Ground,
    Hybrid,
    Boss
}
public class Enemy : ScriptableObject
{
    public string enemyName;
    public float maxHealth = 100f;
    public float moveSpeed = 3f;
    public int damage = 10;
    public Sprite sprite;
    public bool isAgressive;
    public bool isAlive;
    public float currentHealth;
    public EnemyType enemyType;
}
