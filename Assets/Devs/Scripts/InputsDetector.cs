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

        }
        if (Input.GetKeyUp(KeyCode.Space)){
            _endHoldingTime = Time.fixedTime;
            _calculate = true;
        }

        if (_calculate)
        {
            float holdingTime = _endHoldingTime - _startHoldingTime;

            if (holdingTime < _dotTime)
            {
                Debug.Log("points  " + holdingTime);
            }
            else if (holdingTime > _dotTime && holdingTime < _dashTime)
            {
                Debug.Log("tiret " + holdingTime);
            }
            else if (holdingTime > _dashTime)
            {
                Debug.Log("Annulation " + holdingTime);
            }
            _calculate = false;
        }
    }

    #endregion private methods
}
