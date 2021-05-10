using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorseCodeManager : MonoBehaviour
{
    #region Static Members

    public static MorseCodeManager Instance;

    #endregion



    #region Public Methods
    public string TranslateFromMorse(string morseCode)
    {
        if(_morseDictionary.TryGetValue(morseCode, out var value))
        {
            return value;
        }

        return "";
    }

    #endregion



    #region Unity API
    private void Awake() 
    {
        Initialize();
    }

    #endregion



    #region Private Methods

    private void Initialize()
    {
        InitializeInstance();
        InitializeDictionary();
    }

    private void InitializeInstance()
    {
        if(Instance != null)
        {
            Debug.LogWarning("Another instance of MorseCodeManager already exists.");
        }else
        {
            Instance = this;
        }
    }

    private void InitializeDictionary()
    {
        _morseDictionary = new Dictionary<string, string>();

        for (int i = 0; i < _entries.Length; i++)
        {
            var entry = _entries[i];
            _morseDictionary.Add(entry.m_morseCode, entry.m_translation);
        }
    }

    #endregion



    #region Private and Protected Members
    [SerializeField]
    private MorseCodeTableStruct[] _entries;
    private Dictionary<string, string> _morseDictionary;

    #endregion
}
