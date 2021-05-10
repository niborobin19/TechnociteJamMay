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
    [SerializeField] float _dotTime;
    [SerializeField] float _dashTime;
    private bool _calculate;
    private string _morseCode;
    [SerializeField] private float _validationTime;
    private bool _validation;
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
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _startHoldingTime = Time.fixedTime;
            _calculate = false;
            _validation = false;

        }
        if (Input.GetKeyUp(KeyCode.Space)){
            _endHoldingTime = Time.fixedTime;
            _calculate = true;
            _validation = true;
        }
        
        if (_calculate)
        {
            float holdingTime = _endHoldingTime - _startHoldingTime;

            if (holdingTime < _dotTime)
            {
                _morseCode += ".";
            }
            else if (holdingTime > _dotTime && holdingTime < _dashTime)
            {
                _morseCode += "-";
            }
            else if (holdingTime > _dashTime)
            {
                _morseCode = "";
            }
            _calculate = false;
   
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
    #endregion
}
