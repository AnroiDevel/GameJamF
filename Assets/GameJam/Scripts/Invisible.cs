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
        [SerializeField] private float _timeRecovery = 10.0f;
        [SerializeField] private Damageable _damageable;
        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private GameObject _iconInvisiblePrefab;

        private GameObject _iconInvisible;
        private InvisibleIcon _icon;

        private bool _isInvisibleOn;
        private bool _isReady = true;

        #endregion


        #region UnityMethods

        private void Start()
        {
            var canvas = FindObjectOfType<Canvas>().transform;
            _iconInvisible = Instantiate(_iconInvisiblePrefab, canvas);
            _icon = FindObjectOfType(typeof(InvisibleIcon)) as InvisibleIcon;
        }

        private void Update()
        {
            if (!_isInvisibleOn && _isReady)
                if (Input.GetKeyDown(KeyCode.E))
                {
                    _iconInvisible?.SetActive(false);
                    _isInvisibleOn = true;
                    _isReady = false;
                    StartCoroutine(InvisibleReady());
                }
        }

        #endregion


        #region Methods

        private IEnumerator InvisibleReady()
        {
            StartCoroutine(Timer(_timeInvisible));
            _damageable?.EnableInvulnerability();
            _playerInput?.DisableMeleeAttacking();
            _playerInput?.DisableRangedAttacking();
            while (_isInvisibleOn)
            {
                _renderer.color = Color.red;
                yield return new WaitForSeconds(0.1f);
                _renderer.color = Color.blue;
                yield return new WaitForSeconds(0.1f);
            }
            _playerInput?.EnableMeleeAttacking();
            _playerInput?.EnableRangedAttacking();
            _damageable?.DisableInvulnerability();
            _renderer.color = Color.white;
            StartCoroutine(ReadyTimer(_timeRecovery));
        }

        private IEnumerator Timer(float time)
        {

            yield return new WaitForSeconds(time);
            _isInvisibleOn = false;
        }

        private IEnumerator ReadyTimer(float timeRecovery)
        {
            yield return new WaitForSeconds(timeRecovery);
            _isReady = true;
            _iconInvisible?.SetActive(true);
        }

        internal void Upgrade()
        {
            _playerInput?.EnableMeleeAttacking();
            _playerInput?.EnableRangedAttacking();

            _playerInput = null;
            _icon.Upgrade?.Invoke();
        }

        #endregion  
    }
}