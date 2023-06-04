using UnityEngine;
using UnityEngine.SceneManagement;
using Localization;
using System.Text.RegularExpressions;

public class LevelCheck : MonoBehaviour
{
    private LocalizableText _localizableText;

    private void Start()
    {
        _localizableText = GetComponent<LocalizableText>();

        string sceneName = SceneManager.GetActiveScene().name;
        Match match = Regex.Match(sceneName, @"\d+");

        if (match.Success)
        {
            int sceneNumber = int.Parse(match.Value);

            _localizableText.EnText = "LVL " + sceneNumber;
            _localizableText.RuText = "Óð. " + sceneNumber;
        }
    }
}
