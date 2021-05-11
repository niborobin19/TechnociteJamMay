using UnityEngine;

[CreateAssetMenu(menuName = "data/wave", fileName = "new wave")]
public class BoatWave : ScriptableObject
{
    #region Public Members
    
    public int basicEnemyCount;
    public int normalEnemyCount;
    public int bossEnemyCount;
    public Vector2 spawnTimeRange;
    public int remainingEnemiesForNextWave;

    public bool CanSpawnNorth;
    public bool CanSpawnNorthEast;
    public bool CanSpawnEast;
    public bool CanSpawnSouthEast;
    public bool CanSpawnSouth;
    public bool CanSpawnSouthWest;
    public bool CanSpawnWest;
    public bool CanSpawnNorthWest;

    #endregion
}