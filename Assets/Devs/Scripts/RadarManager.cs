using UnityEngine;
using UnityEngine.AI;

public class RadarManager : MonoBehaviour
{

    #region Public Members
    public static RadarManager Instance;
    #endregion



    #region fields

    private float _nextTurnTime;
    private int _currentDirection;
    private int _direction;
    private Transform _transform;
    private Boat[,] _tabBoat = new Boat[4,8];
    [SerializeField] private float _turnTime;
    [SerializeField] private float _radiusPerStep;
    [SerializeField] private Boat _boatPrefab;
    [SerializeField] private Transform _scannerTransform;
    #endregion



    #region Public Methods
    public Vector3 GridToWorldPosition(Vector2Int gridPosition)
    {
        var degAngle = 360f * gridPosition.y / 8;
        var radAngle = Mathf.Deg2Rad * degAngle;
        var up = _transform.up;
        var right = _transform.right;

        var direction = up * Mathf.Cos(radAngle) + right * Mathf.Sin(radAngle);
        var result = direction * gridPosition.x * _radiusPerStep;

        return _transform.position + result;
    }

    public bool ShootToward(int direction)
    {
        if(direction < 0) return false;

        for (int i = 0; i < _tabBoat.GetLength(0); i++)
        {
            var target = _tabBoat[i, direction];
            if(target)
            {
                target.Damage();
                return true;
            }
        }

        return false;
    }

    public void ReleasePosition(Vector2Int position)
    {
        _tabBoat[position.x, position.y] = null;
    }

    public bool CanMoveOn(Vector2Int position)
    {
        if(position.x >= _tabBoat.GetLength(0)) return false;
        if(position.y >= _tabBoat.GetLength(1)) return false;
        if(position.x <= 0) return false;

        position.y = position.y % 8;

        return _tabBoat[position.x, position.y] == null;
    }

    public void MoveToward(Vector2Int from, Vector2Int to)
    {
        if(CanMoveOn(to))
        {
            _tabBoat[to.x, to.y] = _tabBoat[from.x, from.y];
            _tabBoat[to.x, to.y].RadarGridPosition = to;
            ReleasePosition(from);
        }
    }

    public void GenerateSpawn(GameObject boat)
    {
        if(boat == null)
        {
            Instantiate(gameObject);
        }
    }
    #endregion



    #region Unity methods
    private void Awake() 
    {
        _transform = transform;
        _currentDirection = -1;
       /* InitializeInstance();
        SpawnBoat(3, 0);
        SpawnBoat(3, 1);
        SpawnBoat(3, 2);
        SpawnBoat(3, 3);
        SpawnBoat(3, 4);
        SpawnBoat(3, 5);
        SpawnBoat(3, 6);
        SpawnBoat(3, 7);*/
    }

    private void Update()
    {
        ScannerRotationUpdate();
        TurnUpdate();
    }
    #endregion



    #region privates methodes

    private void InitializeInstance()
    {
        if(Instance != null)
        {
            Debug.LogWarning("Another instance of RadarManager already exists.");
        }else
        {
            Instance = this;
        }
    }

    private void SpawnBoat(int distance, int direction)
    {
        if (_tabBoat[distance, direction] == null)
        {
            Boat tempBoat = Instantiate<Boat>(_boatPrefab);
            _tabBoat[distance, direction] = tempBoat;
            tempBoat.RadarGridPosition = new Vector2Int(distance, direction);
        }
    }

    private void TurnUpdate()
    {
        if (_direction != _currentDirection)
        {
            _currentDirection = _direction;
            MoveBoat();

            _nextTurnTime = Time.time + _turnTime/8f;
            //_currentDirection = (_currentDirection + 1) % 8;
        }
    }

    private void MoveBoat()
    {
        for (int i = 0; i < _tabBoat.GetLength(0); i++)
        {
            _tabBoat[i, _currentDirection]?.TurnUpdate();
        }
    }

    private void ScannerRotationUpdate()
    {
        var ratio = (Time.time % _turnTime) / _turnTime;
        var rotation = Quaternion.Euler(0f, 0f, -360f * ratio);
        
        _direction = (int)(ratio * 8);
        _scannerTransform.localRotation = rotation;
    }

    #endregion

}