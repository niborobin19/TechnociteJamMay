using System;
using UnityEngine;

public class Boat : MonoBehaviour, ITurnDriven
{
    #region Public Members
    public float m_destroyTime;

    #endregion

    #region Serialized Members
    [SerializeField]
    private BoatDatas _datas;

    #endregion



    #region Public Fields

    public Vector2Int RadarGridPosition
    {
        get{return _radarGridPosition;}

        set
        {
            _radarGridPosition = value;
            _transform.position = RadarManager.Instance.GridToWorldPosition(_radarGridPosition);
            Debug.Log(_radarGridPosition);
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
            _moveTurnCounter = _datas.moveTurnCount;
        }
    }

    #endregion



    #region Public Methods

    public void Damage()
    {
        _currentHealth --;

        if(_currentHealth == 0)
        {
            Destroy(gameObject, m_destroyTime);
        }
    }
    
    #endregion



    #region Unity API
    private void Awake() 
    {
        Initialize();    
    }

    private void Update() 
    {
        UpdateShoot();
    }

    private void OnDestroy() 
    {
        RadarManager.Instance.ReleasePosition(_radarGridPosition);
    }

    #endregion
    
    
    
    #region Utils
    private void Initialize()
    {
        _transform = transform;
        Debug.Log(_datas.startingHealth);
        _currentHealth = _datas.startingHealth;
        _moveTurnCounter = _datas.moveTurnCount;
        _sprite = GetComponent<SpriteRenderer>();
        _sprite.color = _datas.baseColor;
    }

    private void TryMoveCloser()
    {
        var destination = new Vector2Int(RadarGridPosition.x - _datas.movement.x, RadarGridPosition.y + _datas.movement.y);
        RadarManager.Instance.MoveToward(RadarGridPosition, destination);
    }

    private void UpdateShoot()
    {
        _fireTimer += Time.deltaTime;

        if(_fireTimer > _datas.fireTransitions.y)
        {
            var timeToRad = _fireTimer % Math.PI / _datas.flickeringRate;
            var value = (float)Math.Cos(timeToRad);
            _sprite.color = _datas.dangerFlickering.Evaluate(value);
        }else if(_fireTimer > _datas.fireTransitions.x)
        {
            var timeToRad = _fireTimer % Math.PI / _datas.flickeringRate;
            var value = (float)Math.Cos(timeToRad);
            Debug.Log(value);
            _sprite.color = _datas.warningFlickering.Evaluate(value);
        }
        
        if(_hasFire && _fireTimer <= _datas.fireTransitions.z)
        {
            _sprite.color = _datas.shootColor;
            
        }else if(_hasFire && _fireTimer > _datas.fireTransitions.z)
        {
            _sprite.color = _datas.baseColor;
            _hasFire = false;
        }

        if(_fireTimer >= _datas.fireRate)
        {
            PlayerController.Instance.Damage();
            _hasFire = true;
            
            _fireTimer = 0.0f;
        }
    }
    
    #endregion



    #region Private Members
    private int _moveTurnCounter;
    private int _currentHealth;
    private float _fireTimer;
    private bool _hasFire;
    private Vector2Int _radarGridPosition;
    private Transform _transform;
    private SpriteRenderer _sprite;

    #endregion
}
