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

    #region Statics members
    public static int Amount;
    #endregion


    #region Public Fields

    public Vector2Int RadarGridPosition
    {
        get{return _radarGridPosition;}

        set
        {
            _radarGridPosition = value;
            _transform.position = RadarManager.Instance.GridToWorldPosition(_radarGridPosition);
            var scale = (_radarGridPosition.y >= 4) ? -1f : 1f; 
            _transform.localScale = new Vector3(scale, 1f, 1f);
            
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
        switch(_attackStep)
        {
            case 0:
            {
                ResetColor();
                _currentHealth--;
            }break;

            case 1:
            {
                //score cancel 1
                Debug.LogWarning($"cancel {_attackStep}");
                ResetColor();
                _fireTimer = UnityEngine.Random.Range(-_datas.fireRateRandomness, 0.0f);
                
            }break;

            case 2:
            {
                //score cancel 2
                Debug.LogWarning($"cancel {_attackStep}");
                ResetColor();
                _fireTimer = UnityEngine.Random.Range(-_datas.fireRateRandomness, 0.0f);
            }break;
        }

        if(_currentHealth == 0)
        {
            Destroy(gameObject, m_destroyTime);
        }
    }
    
    #endregion



    #region Unity API
    private void Awake() 
    {
        Amount++;
        Initialize();    
    }

    private void Update() 
    {
        TryShoot();
    }

    private void OnDestroy() 
    {
        Amount--;
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

    private void TryShoot()
    {
        if(RadarGridPosition.x <= _datas.attackRange)
        {
            UpdateShoot();
        }
    }

    private void UpdateShoot()
    {
        _fireTimer += Time.deltaTime;

        if(_fireTimer > _datas.fireTransitions.y)
        {
            var timeToRad = _fireTimer % Math.PI / _datas.flickeringRate;
            var value = (float)Math.Cos(timeToRad);
            _sprite.color = _datas.dangerFlickering.Evaluate(value);

            _attackStep = 2;
        }else if(_fireTimer > _datas.fireTransitions.x)
        {
            var timeToRad = _fireTimer % Math.PI / _datas.flickeringRate;
            var value = (float)Math.Cos(timeToRad);
            _sprite.color = _datas.warningFlickering.Evaluate(value);

            _attackStep = 1;
        }
        
        if(_hasFire && _fireTimer <= _datas.fireTransitions.z)
        {
            _sprite.color = _datas.shootColor;

            _attackStep = 3;
            
        }else if(_hasFire && _fireTimer > _datas.fireTransitions.z)
        {
            ResetColor();
        }

        if(_fireTimer >= _datas.fireRate)
        {
            PlayerController.Instance.Damage();
            _hasFire = true;
            
            _fireTimer = UnityEngine.Random.Range(_datas.fireRateRandomness, 0.0f);
        }
    }

    private void ResetColor()
    {
        _sprite.color = _datas.baseColor;
        _hasFire = false;

        _attackStep = 0;
    }
    
    #endregion



    #region Private Members
    private int _moveTurnCounter;
    private int _currentHealth;
    private int _attackStep;
    private float _fireTimer;
    private bool _hasFire;
    private Vector2Int _radarGridPosition;
    private Transform _transform;
    private SpriteRenderer _sprite;

    #endregion
}
