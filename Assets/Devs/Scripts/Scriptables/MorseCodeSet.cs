using UnityEngine;

[CreateAssetMenu(menuName = "data/morseCodeSet", fileName = "new morseCodeSet")]
public class MorseCodeSet : ScriptableObject
{
    #region Public Members
    public MorseCodeTableStruct[] entries;
    #endregion
}