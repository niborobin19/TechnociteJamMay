using UnityEngine;

//This class is used to create int datas

[CreateAssetMenu(menuName = "data/integer", fileName = "new integer")]
public class IntVariable : ScriptableObject
{
    #region Public Members
    public int value;

    #endregion
}