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
    private bool _isFirstRunCompleted = false; // ���� ���������� ������� �����
    private int _currentLevelIndex = 0; // ������� ������ ������

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
        _currentLevelIndex = level - 1; // ������������� ������� �������
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

        // ���������� ������� �������
        Destroy(_currentLvl);

        // ��������� �������� �����
        _winnerLable.SetActive(false);

        // ���������, �������� �� ������ ���� �����������
        if (!_isFirstRunCompleted)
        {
            // ������� � ���������� ������
            _currentLevelIndex++;
            if (_currentLevelIndex >= _lvlPrefabs.Length)
            {
                _isFirstRunCompleted = true; // ������ ���� ��������
                Debug.Log("First run completed. Switching to random levels.");
            }
        }

        // ���������� ��������� �������
        int nextLevelId = _isFirstRunCompleted
            ? Random.Range(1, _lvlPrefabs.Length + 1) // ��������� �������
            : _currentLevelIndex + 1;                // ���������������� �������

        Debug.Log($"Loading level: {nextLevelId}");

        // ������ ��������� �������
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

        // ���������� ������� �������
        Destroy(_currentLvl);

        // ��������� �������� �����
        _winnerLable.SetActive(false);

        // ������������� ������� �������
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
        // ������� ��� AudioSource � ������� �����
        AudioSource[] audioSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource source in audioSources)
        {
            if (source.isPlaying)
            {
                source.Stop(); // ������������� ���������������
            }
        }
    }

    public void ShowMeAds()
    {
        YandexGame.FullscreenShow();
    }
}

