using UnityEngine;

public class RadarManager : MonoBehaviour
{

    #region Public Members
    public static RadarManager Instance;

    #endregion



    #region fields

    private float _nextspawnTime;
    private int _currentDirection;
    private Transform _transform;
    private Boat[,] _tabBoat = new Boat[4,8];
    [SerializeField] private float _turnTime;
    [SerializeField] private float _radiusPerStep;
    [SerializeField] private Boat _boatPrefab;
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
        if(position.y <= 0) return false;

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

    #endregion



    #region Unity methods
    private void Awake() 
    {
        _transform = transform;
        InitializeInstance();
        SpawnBoat();
    }

    private void Update()
    {
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

    private void SpawnBoat()
    {
        int index = Random.Range(0, 8);
        if (_tabBoat[3, index] == null)
        {
            Boat tempBoat = Instantiate<Boat>(_boatPrefab);
            _tabBoat[3, index] = tempBoat;
            tempBoat.RadarGridPosition = new Vector2Int(3, index);
        }
    }

    private void TurnUpdate()
    {
        if (Time.time >= _nextspawnTime)
        {
            MoveBoat();

            _nextspawnTime = Time.time + _turnTime/8;
            _currentDirection = (_currentDirection + 1) % 8;
        }
    }

    private void MoveBoat()
    {
        for (int i = 0; i < _tabBoat.GetLength(0); i++)
        {
            _tabBoat[i, _currentDirection]?.TurnUpdate();
        }
    }

    #endregion

}