using WizardName = WizardDB.WizardName;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Lobby
{
    public class LobbyUIController : MonoBehaviour
    {
        #region Private Variables

        [SerializeField] private ToggleGroup _toggleGroup;
        [SerializeField] private Transform _wizardLayout;
        [SerializeField] private GameObject _wizardToggle;
        [SerializeField] private PlayerContent _player1Content, _player2Content;

        private WizardDB _wizardDB;
        private WandDB _wandDB;

        private const string CharacterDBPath = "SO_WizardDB";
        private const string WandDBPath = "SO_WandDB";
        
        #endregion

        #region Unity LifeCycle

        private void Start()
        {
            _wizardDB = Resources.Load<WizardDB>(CharacterDBPath);
            _wandDB = Resources.Load<WandDB>(WandDBPath);
            
            LoadWizards();
            SuscribeToEvents();
        }

        #endregion

        #region Utility Methods

        private void LoadWizards()
        {
            foreach (var wizard in _wizardDB.wizards)
            {
                var wizardToggle = Instantiate(_wizardToggle, _wizardLayout).GetComponent<WizardToggle>();

                wizardToggle.Initialize(_toggleGroup, wizard);
            }
        }

        private void SuscribeToEvents()
        {
            foreach (Transform toggle in _toggleGroup.transform)
                toggle.GetComponent<WizardToggle>().SuscribeToEvents();

            WizardToggle.OnWizardSelected.AddListener(OnWizardSelected);
        }

        private void OnWizardSelected(WizardSO wizard)
        {
            _player1Content.UpdatePlayerContent(wizard);
        }

        #endregion
    }
}