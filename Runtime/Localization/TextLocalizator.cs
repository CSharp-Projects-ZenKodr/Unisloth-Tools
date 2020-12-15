﻿using UnityEngine;
using UnityEngine.UI;

namespace Unisloth.Localization.CSV
{
    [RequireComponent(typeof(Text))]
    public class TextLocalizator : MonoBehaviour
    {

        public string localizationKey;

        // Use this for initialization
        void Start()
        {
            UpdateLocale();
        }

        public void UpdateLocale()
        {
            GetComponent<Text>().text = LocalizationManager.Instance.GetLocString(localizationKey);
        }
    }
}
