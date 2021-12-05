using System;
using UnityEngine;
using UnityEngine.UI;


namespace GameJam
{
    public class InvisibleIcon : MonoBehaviour
    {
        [SerializeField] private Text _timeOver;
        private Image _icon;


        public Action<int> TimeValue;
        internal Action UpgratedVisual;

        private void Start()
        {
            _timeOver = GetComponentInChildren<Text>();
            if (_timeOver != null)
            {
                TimeValue += OnTimeValue;
                _timeOver.text = string.Empty;
            }
            _icon = GetComponent<Image>();
            if (_icon != null)
                UpgratedVisual += OnUpgratedVisual;
        }

        private void OnUpgratedVisual()
        {
            _icon.color = Color.red;
            _timeOver.color = Color.green;
        }

        private void OnTimeValue(int timeValue)
        {
            _timeOver.text = timeValue.ToString();
            if (timeValue <= 0)
                _timeOver.text = string.Empty;

        }
    }

}