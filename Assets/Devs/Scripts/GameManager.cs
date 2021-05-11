using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region fields
    [SerializeField] private int _BoatNumber;
    [SerializeField] private GameObject[] _boatArray;
    [SerializeField] private GameObject _mediumBoatPrefab;
    [SerializeField] private GameObject _largeBoatPrefab;
    [SerializeField] private BoatWave[] boatWaves;
    private int[] _BoatCountArray;
    private int _currentWave;
    private int _remainToSpawn;

    #endregion

    #region Static Members
    public static GameManager Instance;
    #endregion

    #region Unity API
    #endregion
    void Update()
    {
        CheckWave();
    }

    private void Start()
    {
        _currentWave = 1;
        _remainToSpawn = boatWaves[_currentWave].basicEnemyCount + boatWaves[_currentWave].bossEnemyCount + boatWaves[_currentWave].normalEnemyCount;

    }


    private void Awake()
    {
        InitializeInstance();
        _BoatCountArray = new int[_boatArray.Length];
<<<<<<< Updated upstream
        _BoatCountArray[0] = boatWaves[0].basicEnemyCount;
        _BoatCountArray[1] = boatWaves[0].normalEnemyCount;
        _BoatCountArray[2] = boatWaves[0].bossEnemyCount;
=======
       
>>>>>>> Stashed changes
    }

    #region privates methods
    private void InitializeInstance()
    {
        if (Instance != null)
        {
            Debug.LogWarning("Another instance of GameManager already exists.");
        }
        else
        {
            Instance = this;
        }
    }

    private void CheckWave()
    {

        if((boatWaves[_currentWave].remainingEnemiesForNextWave)<= Boat.Amount + _remainToSpawn)
        {
            
            StartWave(_currentWave);

        }
        else
        {
            ContinueWave(_currentWave);
        }
        
    }

    private void ContinueWave(int wave)
    {
        int random = Random.Range(0, _boatArray.Length);
<<<<<<< Updated upstream
        //if()
        
        //RadarManager.Instance.QueueSpawn();
=======
      //  if()
        
       // RadarManager.Instance.QueueSpawn();
>>>>>>> Stashed changes
    }

    private void StartWave(int wave)
    {
        _BoatCountArray[0] = boatWaves[0].basicEnemyCount;
        _BoatCountArray[1] = boatWaves[0].normalEnemyCount;
        _BoatCountArray[2] = boatWaves[0].bossEnemyCount;

        _remainToSpawn = boatWaves[wave].basicEnemyCount + boatWaves[wave].bossEnemyCount + boatWaves[wave].normalEnemyCount;
    }



    #endregion
}
