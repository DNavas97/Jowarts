using System.Collections;
using UnityEngine;

namespace UI.Lobby
{
    public class LobbyUIController : MonoBehaviour
    {
        #region Private Variables

        [SerializeField] private ToggleHandler _togglehandler;
        [SerializeField] private Transform _wizardLayout;
        [SerializeField] private GameObject _wizardToggle;
        [SerializeField] private PlayerContent _player1Content, _player2Content;

        private WizardDB _wizardDB;
        private WandDB _wandDB;

        private const string CharacterDBPath = "SO_WizardDB";
        private const string WandDBPath = "SO_WandDB";

        private int _player1Selection, _player2Selection = 0;
        
        private bool _canP1Input, _canP2Input;
        
        #endregion

        #region Unity LifeCycle

        private void Start()
        {
            _wizardDB = Resources.Load<WizardDB>(CharacterDBPath);
            _wandDB = Resources.Load<WandDB>(WandDBPath);
            
            LoadWizards();
            SuscribeToEvents();
            
            _player1Content.Initialize(_wandDB);
            _player2Content.Initialize(_wandDB);
            
            _canP1Input = _canP2Input = true;
        }

        private void Update()
        {
            HandleInputs();
        }

        #endregion

        #region Utility Methods

        private void HandleInputs()
        {
            var horizontalP1Input = Input.GetAxis("HorizontalP1");
            var horizontalP2Input = Input.GetAxis("HorizontalP2");

            
            if (horizontalP1Input != 0 && _canP1Input)
            {
                StartCoroutine(InputDelay(Player.PlayerID.Player1));

                if (horizontalP1Input > 0)
                {
                    _player1Selection++;
                    if (_player1Selection > _wizardDB.wizards.Count -1) _player1Selection = 0;
                    
                    if (_togglehandler.IsSelected(_player1Selection)) _player1Selection++;
                    if (_player1Selection > _wizardDB.wizards.Count -1) _player1Selection = 0;
                }
                else
                {
                    _player1Selection--;
                    if (_player1Selection < 0) _player1Selection = _wizardDB.wizards.Count -1;
                    
                    if (_togglehandler.IsSelected(_player1Selection)) _player1Selection--;
                    if (_player1Selection < 0) _player1Selection = _wizardDB.wizards.Count -1;
                }

                UpdateToggleHandler(Player.PlayerID.Player1, _player1Selection);
            }

            if (horizontalP2Input != 0 && _canP2Input)
            {
                StartCoroutine(InputDelay(Player.PlayerID.Player2));
                
                if (horizontalP2Input > 0)
                {
                    _player2Selection++;
                    if (_player2Selection > _wizardDB.wizards.Count -1) _player2Selection = 0;

                    if (_togglehandler.IsSelected(_player2Selection)) _player2Selection++;
                    if (_player2Selection > _wizardDB.wizards.Count -1) _player2Selection = 0;
                }
                else
                {
                    _player2Selection--;
                    if (_player2Selection < 0) _player2Selection = _wizardDB.wizards.Count -1;
                    
                    if (_togglehandler.IsSelected(_player2Selection)) _player2Selection--;
                    if (_player2Selection < 0) _player2Selection = _wizardDB.wizards.Count -1;
                }
                
                UpdateToggleHandler(Player.PlayerID.Player2, _player2Selection);
            }
        }

        private IEnumerator InputDelay(Player.PlayerID id)
        {
            if (id == Player.PlayerID.Player1) _canP1Input = false;
            else                               _canP2Input = false;
            
            yield return new WaitForSecondsRealtime(0.15f);
            
            if (id == Player.PlayerID.Player1) _canP1Input = true;
            else                               _canP2Input = true;
        }

        private void UpdateToggleHandler(Player.PlayerID playerID, int selectionID) => _togglehandler.Select(playerID, selectionID);

        private void LoadWizards()
        {
            foreach (var wizard in _wizardDB.wizards)
            {
                var wizardToggle = Instantiate(_wizardToggle, _wizardLayout).GetComponent<WizardToggle>();

                _togglehandler.AddToggleToHandler(wizardToggle);
                wizardToggle.Initialize(wizard);
            }
        }

        private void SuscribeToEvents()
        {
            foreach (Transform toggle in _togglehandler.transform)
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