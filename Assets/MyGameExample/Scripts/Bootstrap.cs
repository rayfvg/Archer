using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private GameObject[] _lvlPrefabs;
    [SerializeField] private Camera _camera;

    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _winnerLable;

    [SerializeField] private GameObject _menuInGame;
    [SerializeField] private GameObject _optionInGame;

    [SerializeField] private ParticleSystem _winnerParticle;

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
        Time.timeScale = 1;

        FinishRules finishRules = FindObjectOfType<FinishRules>();
        BowAiming bowAiming = FindObjectOfType<BowAiming>();
        PlayerView _playerView = FindObjectOfType<PlayerView>();

        bowAiming.InitCamera(_camera);
        finishRules.Initialize(_playerView, _winnerLable, _winnerParticle);
    }

    public void SelectLevel(int level)
    {
        _mainMenu.SetActive(false);
        _optionInGame.SetActive(true);
        CreateLvl(level);
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

    public void RestartLvl()
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
        CreateLvl(id);
        InitializeLvl();

        _menuInGame.SetActive(false);
    }

    public void ReturnInMenu()
    {
        _menuInGame.SetActive(false);
        _winnerLable.SetActive(false);
        _optionInGame.SetActive(false);

        Destroy(_currentLvl);

        _mainMenu.SetActive(true);
    }

    public void ClosedMenuInGame()
    {
        _menuInGame.SetActive(false);
        Time.timeScale = 1;
    }

    public void OpenMenuInGame()
    {
        _menuInGame.SetActive(true);
        Time.timeScale = 0;
    }
}
