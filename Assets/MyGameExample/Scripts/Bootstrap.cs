using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private GameObject[] _lvlPrefabs;
    [SerializeField] private Camera _camera;

    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _winnerLable;

    private GameObject _currentLvl;

    private void Awake()
    {
        InitializeLvl();
    }

    public void CreateLvl(int id)
    {
        _currentLvl = Instantiate(_lvlPrefabs[id - 1]);
    }

    public void InitializeLvl()
    {
        FinishRules finishRules = FindObjectOfType<FinishRules>();
        BowAiming bowAiming = FindObjectOfType<BowAiming>();
        PlayerView _playerView = FindObjectOfType<PlayerView>();

        bowAiming.InitCamera(_camera);
        finishRules.Initialize(_playerView, _winnerLable);
    }

    public void StartGame()
    {
        _mainMenu.SetActive(false);
        CreateLvl(1);
        InitializeLvl();
    }

    public void StartNextLvl()
    {
        string currentLvl = _currentLvl.name;
        char format = currentLvl[0];
        int id = int.Parse(format.ToString());

        if (_currentLvl != null)
            Destroy(_currentLvl);

        _winnerLable.SetActive(false);
        CreateLvl(id + 1);
        InitializeLvl();
    }
}
