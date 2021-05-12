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

    
    [SerializeField] private AudioClip _repairedSound;
    [SerializeField, Range(0.0f, 5.0f)] private float _repairedSoundVolume = 1.2f;


    #endregion



    #region Static Members

    public static PlayerController Instance;

    #endregion



    #region Public Methods
    public void Damage()
    {
        Health = Mathf.Clamp(_currentHealth-1, 0, _maxHealth.value);

        if(Health == 0)
        {
            UserInterface.Instance.ShowEndGameUI(false);
        }
    }

    #endregion



    #region Unity API
    private void Awake() 
    {
        Initialize();
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
        RadarManager.Instance.ShootToward(direction);
    }

    private void Repair()
    {
        Health = Mathf.Clamp(_currentHealth+1, 0, _maxHealth.value);
        if(!_repairedSound) return;
        SoundManager.Instance.PlayAudioClip(_repairedSound, _repairedSoundVolume);
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

            case "Repair":
            {
                Repair();
            }
            break;
        }

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