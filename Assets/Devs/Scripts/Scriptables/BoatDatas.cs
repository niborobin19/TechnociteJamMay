using UnityEngine;

[CreateAssetMenu(menuName = "data/boat", fileName = "new boat")]
public class BoatDatas : ScriptableObject
{
    #region Public Members

    [Tooltip("The starting health of the boat.")]
    public int startingHealth;
    
    [Tooltip("The amount of turns between 2 movements.")]
    public int moveTurnCount;

    [Tooltip("The distance crossed when the boat move (x > 0 : move closer, y > 0 : rotate clockwise)")]
    public Vector2Int movement;
    
    [Tooltip("The maximum distance at which the boat can shoot.")]
    public int attackRange;

    [Tooltip("The time between 2 torpedo shots.")]
    public float fireRate;

    [Tooltip("A random modifier between 2 torpedo shots")]
    public float fireRateRandomness;

    [Tooltip("The times for the transitions (warning, danger, shoot invulnerability time)")]
    public Vector3 fireTransitions;

    [Tooltip("Coefficient qui multiplie le score")]
    public int Coefficient;

    public Color baseColor;
    public Gradient warningFlickering;
    public Gradient dangerFlickering;
    public Color shootColor;
    public float flickeringRate;

    #endregion
}