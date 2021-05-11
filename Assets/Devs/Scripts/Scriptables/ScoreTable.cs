using UnityEngine;

[CreateAssetMenu(menuName = "data/scoreTable", fileName = "new score table")]
public class ScoreTable : ScriptableObject
{
    #region Public Members

    public int baseScore;
    public int[] multiplierByDistanceStep;
    public int multiplierUnarmedWarning;
    public int multiplierUnarmedDanger;
    public int multiplierTimer;

    #endregion
}