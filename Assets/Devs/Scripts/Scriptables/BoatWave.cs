using UnityEngine;

[CreateAssetMenu(menuName = "data/wave", fileName = "new wave")]
public class BoatWave : ScriptableObject
{
    #region Public Members
    public BoatWaveEntryStruct[] boatPrefab;

    #endregion
}