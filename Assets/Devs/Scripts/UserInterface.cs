using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInterface : MonoBehaviour
{
    #region fields
    private float _startHoldingTime;
    private float _pressingTime;[SerializeField]
    private InputsDetector _input;
    #endregion

    #region public
    [Range(0, 1)]
    public float _loadingProgress;
    public FloatVariable _dashTime;
    public FloatVariable _dotTime;
    public Image _loadingImage;
    public Text _loadingText;
    public Text _morseCode;
    #endregion
    #region Unity Api

    private void Update()
    {
        FillProgress();
    }
    private void Start()
    {
        _input.OnMorseChange += InputDetectors_OnMorseChange;
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
                    _startHoldingTime = Time.time;
                    break;
                case TouchPhase.Moved:
                      _pressingTime = Time.time - _startHoldingTime;
                    break;
                case TouchPhase.Stationary:
                    _pressingTime = Time.time - _startHoldingTime;
                    break;
                case TouchPhase.Ended:
                   
                    _pressingTime = 0;
                    break;
                case TouchPhase.Canceled:
                    _pressingTime = 0;
                    break;
                default:
                    break;
            }
           
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
            _loadingText.text = "annulation";
            _loadingImage.fillAmount = 0;
        }

    }
    #endregion
}
