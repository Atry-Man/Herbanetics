using UnityEngine;

[CreateAssetMenu(menuName ="PlayerConfig")]
public class PlayerConfig : ScriptableObject
{
    [Header("PlayerStats")]
    public int maxHealth;
    public float movementSpeed;
    [Range(0f, 0.5f)]
    public float movementSpeedPen;
    public float dashSpeed;
    public float dashDuration;
}
