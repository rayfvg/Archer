using System.Linq;
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
       // InitializeLvl();
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

    public void SelectLevel(int lvlNumber)
    {
        _mainMenu.SetActive(false);
        CreateLvl(lvlNumber);
        InitializeLvl();
    }

    public void StartNextLvl()
    {
        if (_currentLvl == null)
        {
            Debug.LogError("Current level is null!");
            return;
        }

        // Извлекаем число из имени уровня
        string currentLvlName = _currentLvl.name;
        if (!int.TryParse(new string(currentLvlName.Where(char.IsDigit).ToArray()), out int id))
        {
            Debug.LogError($"Unable to parse level number from '{currentLvlName}'");
            return;
        }

        // Уничтожаем текущий уровень
        Destroy(_currentLvl);

        // Отключаем победный лейбл
        _winnerLable.SetActive(false);

        // Создаем следующий уровень
        CreateLvl(id + 1);
        InitializeLvl();
    }
}
