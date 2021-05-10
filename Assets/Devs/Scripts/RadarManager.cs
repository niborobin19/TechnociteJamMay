using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarManager : MonoBehaviour
{

    #region fields

    private Boat[,] _tabBoat = new Boat[4, 8];
    private float _nextspawnTime;
    [SerializeField] private float _turnTime;
    [SerializeField] private Boat _boatPrefab;
    #endregion

    #region Unity methods
    private void Update()
    {
        SpawnBoat();
        MoveBoat();



    }
    #endregion

    #region privates methodes

    private void SpawnBoat()
    {

        if (Time.time > _nextspawnTime  )
        {

            int index = Random.Range(0, 8);
            if (_tabBoat[3, index] == null)
            {
                Boat tempBoat = Instantiate<Boat>(_boatPrefab);
                _tabBoat[3, index] = tempBoat;
            }
        }

        if (Time.time >= _nextspawnTime)
        {
            _nextspawnTime = Time.time + _turnTime/8;
        }

    } 

    private void MoveBoat()
    {
    }
    #endregion

}
