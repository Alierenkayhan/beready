using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.SceneManagement;

public class SaveFormAnswers : MonoBehaviour
{
    public List<TMP_Dropdown> dropdowns;         // Dropdown listesi
    public List<TMP_Text> dropdownsFeedback;     // Dropdown'lara bağlı TMP_Text listesi
    public TMP_Text warning;                     // Uyarı için TMP_Text bileşeni
    public int clickNumber;

    private void Start()
    {
        clickNumber = 0;

        // Her dropdown'a değer değişiklikleri için bir dinleyici ekle
        foreach (TMP_Dropdown dropdown in dropdowns)
        {
            dropdown.onValueChanged.AddListener(delegate { OnDropdownValueChanged(dropdown); });
        }
    }

    private void OnDropdownValueChanged(TMP_Dropdown changedDropdown)
    {
        // Seçili metni al
        string selectedText = changedDropdown.options[changedDropdown.value].text;

        // Eğer "Lütfen seçiniz..." ise geri dön (işlem yapma)
        if (selectedText == "Lütfen seçiniz...") return;

        // Diğer dropdown'lardan birinde aynı seçenek varsa onu "Lütfen seçiniz..." olarak ayarla
        foreach (TMP_Dropdown dropdown in dropdowns)
        {
            if (dropdown == changedDropdown) continue;

            if (dropdown.options[dropdown.value].text == selectedText)
            {
                dropdown.value = 0; // "Lütfen seçiniz..." varsayılan olarak 0. indexte
                Debug.Log("Aynı seçenek başka bir dropdown tarafından seçildi. Lütfen başka bir seçim yapın.");
            }
        }
    }

    public void SaveSelectionsToFile()
    {
        // Seçilmeyen dropdown olup olmadığını kontrol et
        foreach (TMP_Dropdown dropdown in dropdowns)
        {
            if (dropdown.options[dropdown.value].text == "Lütfen seçiniz...")
            {
                warning.text = "Lütfen aşağıda binayı termek için ideal olan sıralmayı yapınız.\nSeçmediğiniz seçenek var, lütfen onu seçip tekrar deneyin.";
                return; // Eğer herhangi bir dropdown'da seçim yoksa işlemi durdur
            }
        }

        // Eğer tüm dropdown'lar seçildiyse, uyarı metnini sıfırla
        warning.text = "Lütfen aşağıda binayı termek için ideal olan sıralmayı yapınız.";

        clickNumber++;

        string timestamp = System.DateTime.Now.ToString("HH-mm-yyyy-MM-dd");
        string fileName = $"{timestamp} - DropdownSelections.txt";
        string filePath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop), fileName);

        using (StreamWriter writer = new StreamWriter(filePath))
        {
            writer.WriteLine("Selections on " + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            writer.WriteLine();

            for (int i = 0; i < dropdowns.Count; i++)
            {
                string selectedText = dropdowns[i].options[dropdowns[i].value].text;
                writer.WriteLine("Dropdown " + (i + 1) + ": " + selectedText);

                // Her dropdown için ilgili feedback text alanını güncelle
                if (selectedText == "masa")
                {
                    dropdownsFeedback[i].text = "Uzmanlar tarafından öneriliyor.";
                }
                else if (selectedText == "pencere")
                {
                    dropdownsFeedback[i].text = "Uzmanlar tarafından önerilmiyor.";
                }
                else
                {
                    dropdownsFeedback[i].text = "Uzmanlar tarafından daha az öneriliyor.";
                }
            }
        }

        // Seçim kaydedildikten sonra ikinci kez butona basılınca sahne geçişi yap
        if (clickNumber == 2)
        {
            Debug.Log("Selections saved to " + filePath);
            SceneManager.LoadScene("Level 2");
        }
    }
}
