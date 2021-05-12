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

    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private GameObject _aliveGraphic;
    [SerializeField] private GameObject _deadGraphic;
    [SerializeField] private AudioClip _damageSound;
    [SerializeField, Range(0.0f, 5.0f)] private float _damageSoundVolume = 1.2f;

    [SerializeField] private AudioClip _explosionSound;
    [SerializeField, Range(0.0f, 5.0f)] private float _explosionSoundVolume = 3f;

    [SerializeField] private AudioClip _playerExplosionSound;
    [SerializeField, Range(0.0f, 5.0f)] private float _playerExplosionSoundVolume = 2.5f;
    
    [SerializeField] private AudioClip _launchedSound;
    [SerializeField, Range(0.0f, 5.0f)] private float _launchedSoundVolume = 1.2f;

    [SerializeField] private AudioClip _disarmedSound;
    [SerializeField, Range(0.0f, 5.0f)] private float _disarmedSoundVolume = 1.2f;

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
        if(_currentHealth == 0)return;
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
        var damaged = false;
        switch(_attackStep)
        {
            case 0:
            {
                damaged = true;
                ResetColor();
                _currentHealth--;
            }break;

            case 1:
            {
                //score cancel 1
                GameManager.Instance.AddScore(_datas.Coefficient);
                ResetColor();
                _fireTimer = UnityEngine.Random.Range(-_datas.fireRateRandomness, 0.0f);
                SoundManager.Instance.PlayAudioClipSpatialized(_disarmedSound, _disarmedSoundVolume, RadarGridPosition.y);

                
            }break;

            case 2:
            {
                 //score cancel 2
                GameManager.Instance.AddScore(_datas.Coefficient);
                ResetColor();
                _fireTimer = UnityEngine.Random.Range(-_datas.fireRateRandomness, 0.0f);
                SoundManager.Instance.PlayAudioClipSpatialized(_disarmedSound, _disarmedSoundVolume, RadarGridPosition.y);
            }break;
        }

        if(_currentHealth == 0)
        {
            SoundManager.Instance.PlayAudioClipSpatialized(_explosionSound, _explosionSoundVolume, RadarGridPosition.y);

            _aliveGraphic.SetActive(false);
            _deadGraphic.SetActive(true);
            Destroy(gameObject, m_destroyTime);
            GameManager.Instance.AddScore(RadarGridPosition.x);
        }
        else if(damaged)
        {
            SoundManager.Instance.PlayAudioClipSpatialized(_damageSound, _damageSoundVolume, RadarGridPosition.y);
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
            SoundManager.Instance.PlayAudioClipSpatialized(_launchedSound, _launchedSoundVolume, RadarGridPosition.y);
            Invoke("PlayExplosionDelayed", 1.0f);

            _hasFire = true;
            
            _fireTimer = UnityEngine.Random.Range(_datas.fireRateRandomness, 0.0f);
        }
    }

    private void PlayExplosionDelayed()
    {
        SoundManager.Instance.PlayAudioClipSpatialized(_playerExplosionSound, _playerExplosionSoundVolume, RadarGridPosition.y);
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

    #endregion
}
