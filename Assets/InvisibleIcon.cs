using System;
using UnityEngine;
using UnityEngine.UI;


namespace GameJam
{
    public class InvisibleIcon : MonoBehaviour
    {
        [SerializeField] private Sprite _upgradedIcon;

        public Action Upgrade;
        private Image _image;

        private void Start()
        {
            _image = GetComponent<Image>();
            Upgrade += OnUpgrade;
        }

        private void OnUpgrade()
        {
            _image.sprite = _upgradedIcon;
        }
    }

}