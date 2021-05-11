using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputsDetector : MonoBehaviour
{
    #region properties
    #endregion properties



    #region fields
    private float _startHoldingTime;
    private float _endHoldingTime;
    [SerializeField] FloatVariable _dotTime;
    [SerializeField] FloatVariable _dashTime;
    private bool _calculate;
    private string _morseCode;
    [SerializeField] private float _validationTime;
    private bool _validation;

    [SerializeField] private AudioClip _bipSound;
    [SerializeField] private AudioClip _tacSound;
    #endregion fields



    #region public methods
    #endregion public methods



    #region unity messages
    private void Awake()
    {
    }

    private void Start()
    {

    }

    private void Update()
    {
        DetectInputs();
    }
    #endregion unity messages



    #region private methods

    private void DetectInputs()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            float pressingTime = Time.time - _startHoldingTime;
        }
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    SoundManager.Instance.PlayBip(_bipSound, 1.0f);
                    SoundManager.Instance.PlayAudioClip(_tacSound, 1.0f);
            

                    _startHoldingTime = Time.fixedTime;
                    _calculate = false;
                    _validation = false;
                    break;
                case TouchPhase.Moved:
                    SoundManager.Instance.StopBip();

                    float pressingTime = Time.time - _startHoldingTime;
                    Debug.Log(pressingTime);
                    break;
                case TouchPhase.Stationary:
                     pressingTime = Time.time - _startHoldingTime;
                    Debug.Log(pressingTime);
                    break;
                case TouchPhase.Ended:
                    _endHoldingTime = Time.fixedTime;
                    _calculate = true;
                    _validation = true;
                    break;
                case TouchPhase.Canceled:
                    break;
                default:
                    break;
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SoundManager.Instance.PlayBip(_bipSound, 1.0f);
            SoundManager.Instance.PlayAudioClip(_tacSound, 1.0f);

            _startHoldingTime = Time.fixedTime;
            _calculate = false;
            _validation = false;

        }
        if (Input.GetKeyUp(KeyCode.Space)){
            SoundManager.Instance.StopBip();

            _endHoldingTime = Time.fixedTime;
            _calculate = true;
            _validation = true;
        }
        
        if (_calculate)
        {
            float holdingTime = _endHoldingTime - _startHoldingTime;

            if (holdingTime < _dotTime.value)
            {
                _morseCode += ".";
            }
            else if (holdingTime > _dotTime.value && holdingTime < _dashTime.value)
            {
                _morseCode += "-";
            }
            else if (holdingTime > _dashTime.value)
            {
                _morseCode = "";
            }
            _calculate = false;
            OnMorseChange?.Invoke(_morseCode);
        }

        if ((Time.time > _endHoldingTime + _validationTime)&&(_validation))
        {
            string result = MorseCodeManager.Instance.TranslateFromMorse(_morseCode);
            OnValidation?.Invoke(result);
            _morseCode = "";
            _validation = false;

        } 
        
    }
    #endregion private methods
    #region public methods

    public delegate void ValidationHandler(string morse);

    public event ValidationHandler OnValidation;

    public delegate void MorseCodeChangerHandler(string morse);

    public event MorseCodeChangerHandler OnMorseChange;
    #endregion
}
