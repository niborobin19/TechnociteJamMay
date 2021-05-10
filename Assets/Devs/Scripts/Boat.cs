using System;
using UnityEngine;

public class Boat : MonoBehaviour, ITurnDriven
{

    #region Serialized Members
    [SerializeField]
    private IntVariable _moveTurnCount;

    [SerializeField]
    private IntVariable _maxHealth;

    [SerializeField]
    private IntVariable _fireRange;

    [SerializeField, Tooltip("The time between 2 torpedo shoots.")]
    private FloatVariable _fireDelay;

    #endregion



    #region Public Fields

    public Vector2Int RadarGridPosition
    {
        get{return _radarGridPosition;}

        set
        {
            _radarGridPosition = value;
            //_transform.position = RadarManager.Instance.GridToWorldPosition(_radarGridPosition);
        }
    }

    #endregion


    
    #region Interfaces Implementations

    public void TurnUpdate()
    {
        _moveTurnCounter --;

        if(_moveTurnCounter == 0)
        {
            TryMoveCloser();
            _moveTurnCounter = _moveTurnCount.value;
        }
    }

    #endregion



    #region Public Methods
    
    #endregion



    #region Unity API
    private void Awake() 
    {
        Initialize();    
    }

    private void Update() 
    {
        
    }

    #endregion
    
    
    
    #region Utils
    private void Initialize()
    {
        _transform = transform;
        _currentHealth = _maxHealth.value;
        _moveTurnCounter = _moveTurnCount.value;
    }

    private void TryMoveCloser()
    {
        throw new NotImplementedException();
    }
    
    #endregion



    #region Private Members
    private int _moveTurnCounter;
    private int _currentHealth;
    private float _fireTimer;
    private Vector2Int _radarGridPosition;
    private Transform _transform;

    #endregion
}
