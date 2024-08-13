using System.Collections.Generic;
using UnityEngine;
using TMPro;  
using System.IO;
using UnityEngine.SceneManagement;

public class saveformanswers : MonoBehaviour
{
    public List<TMP_Dropdown> dropdowns;

    public void SaveSelectionsToFile()
    {
        string timestamp = System.DateTime.Now.ToString("HH-mm-yyyy-MM-dd");
        string fileName = $"{timestamp} - DropdownSelections.txt";
        string filePath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop), fileName);

        using (StreamWriter writer = new StreamWriter(filePath))
        {
            writer.WriteLine("Selections on " + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            writer.WriteLine();

            for (int i = 0; i < dropdowns.Count; i++)
            {
                writer.WriteLine("Dropdown " + (i + 1) + ": " + dropdowns[i].options[dropdowns[i].value].text);
            }
        }

        Debug.Log("Selections saved to " + filePath);
        SceneManager.LoadScene("Level 2");

    }
}
