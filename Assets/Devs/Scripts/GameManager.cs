using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region fields
    [SerializeField] private GameObject[] _boatArrayPrefabs;
    [SerializeField] private BoatWave[] boatWaves;
    private int[] _BoatCountArray;
    private int _currentWave;
    private int _remainToSpawn;
    private int _maxToSpawn;
    private int _instancesBoats = 0;
    private float _timeInstantiate;
    

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
        _remainToSpawn = _BoatCountArray[0] + _BoatCountArray[1] + _BoatCountArray[2];
        _maxToSpawn = _BoatCountArray[0] + _BoatCountArray[1] + _BoatCountArray[2]; 

    }


    private void Awake()
    {
        InitializeInstance();
        _BoatCountArray = new int[3];

        _BoatCountArray[0] = boatWaves[0].basicEnemyCount;
        _BoatCountArray[1] = boatWaves[0].normalEnemyCount;
        _BoatCountArray[2] = boatWaves[0].bossEnemyCount;

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

            if (_instancesBoats <=  _maxToSpawn)
            {

                ContinueWave(_currentWave);

            }
            else if(_currentWave<boatWaves.Length)
            {
                _currentWave++;
                StartWave(_currentWave);
            }
        

        
    }

    private void ContinueWave(int wave)
    {

        int random = Random.Range(0, _boatArrayPrefabs.Length);
        
        
        CheckWaveOver();
        if (Time.time >= _timeInstantiate)
        {
            if (_BoatCountArray[random] >= 0)
            {
                   _BoatCountArray[random]--;
                    _instancesBoats++;
                    RadarManager.Instance.
                    QueueSpawn(_boatArrayPrefabs[random]);
                    _timeInstantiate = Time.time + Random.Range(boatWaves[wave - 1].spawnTimeRange.x, boatWaves[wave - 1].spawnTimeRange.y);
                    return;
            }  
            else 
            {
                if(_remainToSpawn !=0)
                 {
                    ContinueWave(wave);
                 }

        }

        }

    }

    private void CheckWaveOver()
    {
        _remainToSpawn = _BoatCountArray[0] + _BoatCountArray[1] + _BoatCountArray[2];
    }

    private void StartWave(int wave)
    {
        Debug.Log("Start wave" + _currentWave);
        _BoatCountArray[0] = boatWaves[wave - 1].basicEnemyCount;
        _BoatCountArray[1] = boatWaves[wave - 1].normalEnemyCount;
        _BoatCountArray[2] = boatWaves[wave - 1].bossEnemyCount;

       
       


    }



    #endregion
}
