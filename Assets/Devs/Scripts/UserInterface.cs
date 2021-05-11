using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInterface : MonoBehaviour
{
    #region fields
    private float _startHoldingTime;
    private float _pressingTime;
    #endregion

    #region public
    [Range(0, 1)]
    public float _loadingProgress;
    public FloatVariable _dashTime;
    public Image _loadingImage;
    public Text _loadingText;
    #endregion
    #region Unity Api

    private void Update()
    {
        FillProgress();
    }
    #endregion

    #region privates methods    

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
                    Debug.Log(_pressingTime + "moved");
                    break;
                case TouchPhase.Stationary:
                    _pressingTime = Time.time - _startHoldingTime;
                    Debug.Log(_pressingTime + "station ");
                    break;
                case TouchPhase.Ended:
                    _loadingProgress = 0;
                    break;
                case TouchPhase.Canceled:
                    break;
                default:
                    break;
            }
           
        }
        _loadingProgress = _pressingTime / _dashTime.value;
        Debug.Log(_loadingProgress);
        _loadingImage.fillAmount = _loadingProgress;
        if (_loadingProgress < 1)
        {
            _loadingText.text = Mathf.RoundToInt(_loadingProgress * 100) + "%nChargement ...";
        }
        else
        {
            _loadingText.text = "fait";
        }
    }
    #endregion
}
