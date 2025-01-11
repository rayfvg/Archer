using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CurrentButton : MonoBehaviour
{
    private void Start()
    {
        
    }

    private void OnEnable()
    {
        Load();
    }

    private void Load()
    {
        string currentLvlName = name;
        if (!int.TryParse(new string(currentLvlName.Where(char.IsDigit).ToArray()), out int id))
        {
            Debug.LogError($"Unable to parse level number from '{currentLvlName}'");
            return;
        }

        int idLvl = PlayerPrefs.GetInt("lvl" + currentLvlName);

        Debug.Log("я загрузил" + idLvl);

        if (idLvl == 1)
        {
            GetComponent<Image>().color = Color.green;
        }
    }
}
