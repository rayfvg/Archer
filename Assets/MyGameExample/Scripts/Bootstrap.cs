using UnityEngine;
using YG;

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
    private bool _isFirstRunCompleted = false; // Флаг завершения первого цикла
    private int _currentLevelIndex = 0; // Текущий индекс уровня

    public AudioSource _shotArrow;
    public AudioSource _loadingArrow;
    public AudioSource _ricoshet;
    public AudioSource _shotInWall;
    public AudioSource _damage;
    public AudioSource _victory;

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
        Time.timeScale = 1;
        AudioListener.pause = false;
        StopAllMusic();

        FinishRules finishRules = FindObjectOfType<FinishRules>();
        BowAiming bowAiming = FindObjectOfType<BowAiming>();
        PlayerView _playerView = FindObjectOfType<PlayerView>();
        CreatorArrow creatorArrow = FindObjectOfType<CreatorArrow>();

        bowAiming.InitCamera(_camera);
        finishRules.Initialize(_playerView, _winnerLable, _winnerParticle, _victory);
        creatorArrow.InitSound(_shotArrow, _ricoshet, _shotInWall, _damage);
        _playerView.InitSound(_loadingArrow);
    }

    public void SelectLevel(int level)
    {
        _mainMenu.SetActive(false);
        _optionInGame.SetActive(true);
        _currentLevelIndex = level - 1; // Устанавливаем текущий уровень
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

        ShowMeAds();

        // Уничтожаем текущий уровень
        Destroy(_currentLvl);

        // Отключаем победный лейбл
        _winnerLable.SetActive(false);

        // Проверяем, завершён ли первый цикл прохождения
        if (!_isFirstRunCompleted)
        {
            // Переход к следующему уровню
            _currentLevelIndex++;
            if (_currentLevelIndex >= _lvlPrefabs.Length)
            {
                _isFirstRunCompleted = true; // Первый цикл завершён
                Debug.Log("First run completed. Switching to random levels.");
            }
        }

        // Определяем следующий уровень
        int nextLevelId = _isFirstRunCompleted
            ? Random.Range(1, _lvlPrefabs.Length + 1) // Случайный уровень
            : _currentLevelIndex + 1;                // Последовательный уровень

        Debug.Log($"Loading level: {nextLevelId}");

        // Создаём следующий уровень
        CreateLvl(nextLevelId);
        InitializeLvl();
    }

    public void RestartLvl()
    {
        if (_currentLvl == null)
        {
            Debug.LogError("Current level is null!");
            return;
        }

        // Уничтожаем текущий уровень
        Destroy(_currentLvl);

        // Отключаем победный лейбл
        _winnerLable.SetActive(false);

        // Перезапускаем текущий уровень
        CreateLvl(_currentLevelIndex + 1);
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
        StopAllMusic();
    }

    public void ClosedMenuInGame()
    {
        _menuInGame.SetActive(false);
        Time.timeScale = 1;
        AudioListener.pause = false;
    }

    public void OpenMenuInGame()
    {
        _menuInGame.SetActive(true);
        Time.timeScale = 0;
        AudioListener.pause = true;
    }

    public static void StopAllMusic()
    {
        // Находим все AudioSource в текущей сцене
        AudioSource[] audioSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource source in audioSources)
        {
            if (source.isPlaying)
            {
                source.Stop(); // Останавливаем воспроизведение
            }
        }
    }

    public void ShowMeAds()
    {
        YandexGame.FullscreenShow();
    }
}

