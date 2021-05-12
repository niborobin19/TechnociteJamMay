using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UserInterface : MonoBehaviour
{
    #region fields
    private float _startHoldingTime;
    private float _pressingTime;
    private float _startReleasingTime;
    private float _releaseTime;
    
    [SerializeField] private InputsDetector _input;
    #endregion

    #region public

    public static UserInterface Instance;

    public GameObject _mainMenuPanel;
    public GameObject _victoryPanel;
    public GameObject _defeatPanel;

    [Range(0, 1)]
    public float _loadingProgress;
    public FloatVariable _dashTime;
    public FloatVariable _dotTime;
    public FloatVariable _validationTime;
    public IntVariable _score;
    public Image _loadingImage;
    public Image _validatingImage;
    public Transform _transitionImage;
    public Text _loadingText;
    public Text _morseCode;
    public Text _scoreText;
    #endregion



    #region Public Methods
    public void ShowEndGameUI(bool isWon)
    {
        if(isWon)
        {
            _victoryPanel.SetActive(true);
        }else
        {
            _defeatPanel.SetActive(true);
        }
        Time.timeScale = 0;
    }

    public void StartGameButton_OnClick()
    {
        _mainMenuPanel.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void RestartGameButton_OnClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    #endregion



    #region Unity Api

    private void Awake() 
    {
        Instance = this;
        Time.timeScale = 0;
    }

    private void Update()
    {
        FillProgress();
        FillValidation();
        UpdateScore();
    }
    private void Start()
    {
        _score.value = 0;
        _input.OnMorseChange += InputDetectors_OnMorseChange;
        _startReleasingTime = -_validationTime.value*2;

        var ratio = _dotTime.value / _dashTime.value;
        var angle = ratio * 360.0f;

        _transitionImage.localRotation = Quaternion.Euler(0.0f, 0.0f, -angle);


    }
    #endregion

    #region privates methods    


    public void InputDetectors_OnMorseChange(string morse) 
    {
        _morseCode.text = morse;
    }
        
    
    
    private void FillProgress()
    {
        
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    _releaseTime = 0.0f;
                    _startHoldingTime = Time.time;
                    break;
                case TouchPhase.Moved:
                      _pressingTime = Time.time - _startHoldingTime;
                    break;
                case TouchPhase.Stationary:
                    _pressingTime = Time.time - _startHoldingTime;
                    break;
                case TouchPhase.Ended:
                   
                    _pressingTime = 0.0f;
                    _startReleasingTime = Time.time;
                    break;
                case TouchPhase.Canceled:
                    _pressingTime = 0.0f;
                    _startReleasingTime = Time.time;
                    break;
                default:
                    break;
            }
           
        }
        
        if(Input.GetKeyDown(KeyCode.Space))
        {
            _releaseTime = 0.0f;
            _startHoldingTime = Time.time;
        }else if(Input.GetKey(KeyCode.Space))
        {
            _pressingTime = Time.time - _startHoldingTime;
        }else if(Input.GetKeyUp(KeyCode.Space))
        {
            _pressingTime = 0.0f;
            _startReleasingTime = Time.time;
        }
       


        _loadingProgress = _pressingTime / _dashTime.value;
        _loadingImage.fillAmount = _loadingProgress;
        if (_loadingProgress < 1)
        {
            if (_pressingTime < _dotTime.value)
            {
                _loadingText.text = ".";

            }else if (_pressingTime > _dotTime.value && _pressingTime < _dashTime.value)
            {
                _loadingText.text = " - ";
            }
        }
        else
        {
            _loadingText.text = "Erase";
            _loadingImage.fillAmount = 0;
        }

    }

    private void FillValidation()
    {
        if(NotAnyInput())
        {
            _releaseTime = Time.time - _startReleasingTime;
        }

        var fillAmount = _releaseTime / _validationTime.value;
        fillAmount = (fillAmount > 1) ? 0.0f : fillAmount;
        _validatingImage.fillAmount = fillAmount;
    }

    private void UpdateScore()
    {
        _scoreText.text = $"{_score.value}";
    }

    private bool NotAnyInput()
    {
        if(Input.GetKeyDown(KeyCode.Space)) return false;
        if(Input.GetKey(KeyCode.Space)) return false;
        if(Input.GetKeyUp(KeyCode.Space)) return false;
        if(Input.touchCount > 0) return false;

        return true;
    }
    #endregion
}
