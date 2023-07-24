using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Big Punch")]
public class BigPunchSO : ScriptableObject
{
    [Header("Big Punch Data")]
    public GameObject punchPrefab;
    public float fireForce;
    public float fireRate;
    public int bigPunchDamage;
}
