using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region fields
    [SerializeField] private int _BoatNumber;
    #endregion

    #region Static Members
    public static GameManager Instance;
    #endregion

    #region Unity API
    #endregion
    void Update()
    {
        
    }

    private void Start()
    {
        
    }


    private void Awake()
    {
        InitializeInstance();
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

    public void PhaseOne()
    {

    }

    #endregion
}
