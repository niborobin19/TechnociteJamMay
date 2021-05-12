using UnityEngine;

public class LifeDisplayManager : MonoBehaviour
{
    #region Public Members

    #endregion


    #region Serialized Members

    [SerializeField]
    private Renderer[] _diodesRenderers;

    #endregion


    #region Unity API

    private void Awake() 
    {
        HideAll();    
    }
    private void Start() 
    {
        PlayerController.Instance.OnHealthChange += PlayerController_OnHealthChange;        
    }



    #endregion


    #region Utils
    private void PlayerController_OnHealthChange(int health)
    {
        for (int i = 0; i < _diodesRenderers.Length; i++)
        {
            if(i >= health)
            {
                _diodesRenderers[i].material.EnableKeyword("_EMISSION");
            }else
            {
                _diodesRenderers[i].material.DisableKeyword("_EMISSION");
            }
        }
    }

    private void HideAll()
    {
        for (int i = 0; i < _diodesRenderers.Length; i++)
        {
            _diodesRenderers[i].material.DisableKeyword("_EMISSION");
        }
    }

    #endregion


    #region Private And Protected

    #endregion
}