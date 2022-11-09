using TMPro;
using UnityEngine;

namespace UI {

    public class PlayerStatsDisplay : MonoBehaviour {

        #region Dependencies
        [Header("Dependencies")] [SerializeField]
        private PlayerCharacter playerCharacter;

        [SerializeField] private TextMeshProUGUI healthDisplay;
        #endregion

        private void Awake() {
            playerCharacter.MaxHealth.OnStatChanged += UpdateHealthDisplay;
            playerCharacter.OnHealthChanged += UpdateHealthDisplay;
        }

        private void Start() {
            // Initial update
            UpdateHealthDisplay();
        }

        private void UpdateHealthDisplay() {
            healthDisplay.text = $"{playerCharacter.Health} / {playerCharacter.MaxHealth.Value}";
        }

    }

}
