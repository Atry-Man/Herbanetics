using UnityEngine;

[CreateAssetMenu(menuName ="Abilities/Smol Bolts")]
public class SmolBoltsSO : ScriptableObject
{
    [Header("Smol Bolt Data")]
    public GameObject boltPrefab;
    public float fireForce;
    public float fireRate;
    public int boltDamage;
}
