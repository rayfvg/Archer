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

    public AudioSource _shotArrow;
    public AudioSource _loadingArrow;
    public AudioSource _ricoshet;
    public AudioSource _shotInWall;
    public AudioSource _damage;
    public AudioSource _victory;

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

        // ��������� ����� �� ����� ������
        string currentLvlName = _currentLvl.name;
        if (!int.TryParse(new string(currentLvlName.Where(char.IsDigit).ToArray()), out int id))
        {
            Debug.LogError($"Unable to parse level number from '{currentLvlName}'");
            return;
        }

        // ���������� ������� �������
        Destroy(_currentLvl);

        // ��������� �������� �����
        _winnerLable.SetActive(false);

        // ������� ��������� �������
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

        // ��������� ����� �� ����� ������
        string currentLvlName = _currentLvl.name;
        if (!int.TryParse(new string(currentLvlName.Where(char.IsDigit).ToArray()), out int id))
        {
            Debug.LogError($"Unable to parse level number from '{currentLvlName}'");
            return;
        }

        // ���������� ������� �������
        Destroy(_currentLvl);

        // ��������� �������� �����
        _winnerLable.SetActive(false);

        // ������� ��������� �������
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
}
