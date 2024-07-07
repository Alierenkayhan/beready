using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Rewrite.Level0
{
    public class BagContentsOnTV: MonoBehaviour
    {
        public TMP_Text informationTxt;

        private void Start()
        {
            throw new NotImplementedException();
        }

        public void displayItems(List<string> l)
        {
            var x = string.Join(", ", l);
            informationTxt.text = "Çantanın içindekiler: " + x;
        }
    }
}