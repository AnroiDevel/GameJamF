using Gamekit2D;
using System.Collections;
using UnityEngine;


namespace GameJam
{
    public class Invisible : MonoBehaviour
    {
        #region Fields

        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private float _timeInvisible = 5.0f;


        [SerializeField] private Damageable _damageable;
        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private GameObject _iconInvisiblePrefab;
        private Damager _damager;

        private InvisibleIcon _icon;

        private bool _isInvisibleOn;

        #endregion

        public bool IsInvisibleOn { get => _isInvisibleOn; }


        #region UnityMethods

        private void Start()
        {
            var canvas = GetCanvas();
            Instantiate(_iconInvisiblePrefab, canvas.transform);

            _icon = FindObjectOfType(typeof(InvisibleIcon)) as InvisibleIcon;
            _playerInput?.DisableMeleeAttacking();
            _playerInput?.DisableRangedAttacking();
            _damager = gameObject.GetComponent<Damager>();

        }

        #endregion


        #region Methods

        private Canvas GetCanvas()
        {
            var canvas = FindObjectOfType<Canvas>();
            if (canvas == null)
            {
                var gameObject = new GameObject();
                gameObject.AddComponent<Canvas>();
                gameObject.name = "Canvas";
                canvas = Instantiate(gameObject).GetComponent<Canvas>();
            }
            return canvas;
        }

        private IEnumerator InvisibleReady()
        {
            StartCoroutine(Timer(_timeInvisible));
            _damageable?.EnableInvulnerability(true);
            _damager.EnableDamage();
            while (_isInvisibleOn)
            {
                _renderer.color = Color.red;
                yield return new WaitForSeconds(0.1f);
                _renderer.color = Color.blue;
                yield return new WaitForSeconds(0.1f);
            }
            _damager.DisableDamage();
            _damageable?.DisableInvulnerability();

            _renderer.color = Color.white;
        }

        private IEnumerator Timer(float time)
        {
            var tempTime = time;
            while (tempTime >= 0)
            {
                yield return new WaitForSeconds(1);
                _icon.TimeValue?.Invoke((int)tempTime);
                tempTime--;
            }

            _isInvisibleOn = false;
        }

        internal void TakePerk()
        {
            if (!_isInvisibleOn)
                _isInvisibleOn = true;
            StartCoroutine(InvisibleReady());

        }
        internal void Upgrade()
        {
            _timeInvisible = 9.0f;
            _icon.UpgratedVisual?.Invoke();
        }

        #endregion  
    }
}