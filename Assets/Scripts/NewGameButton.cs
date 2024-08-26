using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewGameButton : MonoBehaviour
{
    Button button;

    public List<Button> availableLevels;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);

        for (int i = 0; i < availableLevels.Count; i++)
        {
            string levelName = $"Scenes/Level{i + 2}";
            bool isUnlocked = PlayerPrefs.GetInt(levelName) == 1 ? true : false;

            Debug.Log(levelName + "_" + isUnlocked);
            availableLevels[i].interactable = isUnlocked;
        }
    }

    void OnClick()
    {
        Debug.Log("NewGame 버튼 눌림");

        PlayerPrefs.DeleteAll();

        for (int i = 0; i < availableLevels.Count; i++)
        {
            availableLevels[i].interactable = false;
        }
    }
}
