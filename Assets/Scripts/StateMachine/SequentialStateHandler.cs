using UnityEngine;

public class SequentialStateHandler : MonoBehaviour
{
    [SerializeField] [RequireInterface(typeof(ISequentialState))] Object[] _states;
    ISequentialState[] _sequentialStates;

    #region Unity
    private void Awake()
    {
        Initialize();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
    #endregion

    #region Private
    private void Initialize()
    {
        _sequentialStates = new ISequentialState[_states.Length];

        for (int i = 0; i < _states.Length; i++)
        {
            _sequentialStates[i] = _states[i] as ISequentialState;
        }
    }
    #endregion
}
