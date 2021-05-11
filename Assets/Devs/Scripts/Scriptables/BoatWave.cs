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

    public bool[] CanSpawn{
        get{
            var result = new bool[8];
            result[0] = CanSpawnNorth;
            result[1] = CanSpawnNorthEast;
            result[2] = CanSpawnEast;
            result[3] = CanSpawnSouthEast;
            result[4] = CanSpawnSouth;
            result[5] = CanSpawnSouthWest;
            result[6] = CanSpawnWest;
            result[7] = CanSpawnNorthWest;

            return result;
        }
    }

    #endregion
}