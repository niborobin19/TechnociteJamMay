using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    #region Public Fields
    
    public int Health{
        get{return _currentHealth;}

        set
        {
            if(_maxHealth != null)
            {
                _currentHealth = Mathf.Clamp(value, 0, _maxHealth.value);
            }else
            {
                _currentHealth = Mathf.Clamp(value, 0, _defaultHealth);
                Debug.LogWarning($"Max health isn't initialized so the default value of {_defaultHealth} was taken.");
            }
            OnHealthChange?.Invoke(_currentHealth);
        }
    }

    #endregion



    #region Serialized Members
    [SerializeField]
    private InputsDetector _input;

    [SerializeField]
    private IntVariable _maxHealth;


    #endregion



    #region Static Members

    public static PlayerController Instance;

    #endregion



    #region Public Methods
    public void Damage(int amount)
    {
        Health -= amount;

        if(Health == 0)
        {
            Debug.LogError("GAME OVER");
        }
    }

    #endregion



    #region Unity API
    private void Awake() 
    {
        InitializeInstance();
        InitializeHealth();
    }

    private void Start() 
    {
        Subscribe();
    }

    #endregion



    #region Utils

    private void Initialize()
    {
        InitializeInstance();
        InitializeHealth();
    }

    private void InitializeInstance()
    {
        if(Instance != null)
        {
            Debug.LogWarning("Another instance of PlayerController already exists.");
        }else
        {
            Instance = this;
        }
    }

    private void InitializeHealth()
    {
        if(_maxHealth != null)
        {
            Health = _maxHealth.value;
        }else
        {
            Health = _defaultHealth;
        }
    }

    private void Subscribe()
    {
        _input.OnValidation += InputsDetector_OnValidation;
    }

    private void Shoot(int direction)
    {
        throw new NotImplementedException();
        //RadarManager.shootat(int direction)
    }

    private void InputsDetector_OnValidation(string validation)
    {
        var direction = -1;

        switch(validation)
        {
            case "North":
            {
                direction = 0;
            }
            break;

            case "North-East":
            {
                direction = 1;
            }
            break;

            case "East":
            {
                direction = 2;
            }
            break;

            case "South-East":
            {
                direction = 3;
            }
            break;

            case "South":
            {
                direction = 4;
            }
            break;

            case "South-West":
            {
                direction = 5;
            }
            break;

            case "West":
            {
                direction = 6;
            }
            break;

            case "North-West":
            {
                direction = 7;
            }
            break;
        }

        Debug.Log($"{validation} : {direction}");

        Shoot(direction);
    }

    #endregion



    #region Private And Protected
    private int _currentHealth;
    private int _defaultHealth = 5;
    
    #endregion



    #region Events
    public delegate void HealthChangeHandler(int next);
    public event HealthChangeHandler OnHealthChange;

    #endregion
}