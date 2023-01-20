using System;
using System.Collections;
using System.Collections.Generic;
using Persistent_Data;
using UnityEngine;
using PlayerID = Player.PlayerID; 

namespace UI.Lobby
{
    public class LobbyUIController : MonoBehaviour
    {
        #region Private Variables

        [SerializeField] private ToggleHandler _togglehandler;
        [SerializeField] private Transform _wizardLayout;
        [SerializeField] private GameObject _wizardToggle;
        [SerializeField] private PlayerContent _player1Content, _player2Content;

        [SerializeField] private AudioClip _hoverClip, _selectionClip;
        
        private AudioSource _audioSource;
        
        private int _player1SelectedWizard = 0;
        private int _player2SelectedWizard = 1;
        private int _player1SelectedWand, _player2SelectedWand = 0;
        
        private bool _canP1Input, _canP2Input;
        private bool _lockedWizardP1, _lockedWizardP2, _lockedWandP1, _lockedWandP2;

        private List<WandSO> _wandDB;
        private List<WizardSO> _wizardDB;
        
        #endregion

        #region Unity LifeCycle

        private void Awake() => TryGetComponent(out _audioSource);

        private void Start()
        {
            _wizardDB = PersistentData.Instance.GetWizardDB();
            _wandDB = PersistentData.Instance.GetWandDB();

            LoadWizards();
            SuscribeToEvents();

            UpdateToggleHandler(PlayerID.Player1, _player1SelectedWizard);
            UpdateToggleHandler(PlayerID.Player2, _player2SelectedWizard);
            
            _player1Content.UpdateWand(_wandDB[_player1SelectedWand]);
            _player2Content.UpdateWand(_wandDB[_player2SelectedWand]);
            
            _canP1Input = _canP2Input = true;
        }

        private void Update()
        {
            HandleSelectionInputs();
            HandleSubmitInput();
            HandleBackInput();
            HandleInfoInput();
            CheckForMatchStart();
        }

        private void HandleInfoInput()
        {
            if (Input.GetKeyDown(GlobalParams.InfoButtonP1))
                _player1Content.OnShowInfoClicked();

            if(Input.GetKeyDown(GlobalParams.InfoButtonP2))
                _player2Content.OnShowInfoClicked();
        }

        #endregion

        #region Utility Methods

        private void HandleSelectionInputs()
        {
            var horizontalP1Input = Input.GetAxis("HorizontalP1");
            var horizontalP2Input = Input.GetAxis("HorizontalP2");

            if (horizontalP1Input != 0 && _canP1Input)
            {
                if (!_lockedWizardP1)   WizardSelectionHorizontalHandle(PlayerID.Player1, horizontalP1Input, _player1SelectedWizard);
                else if(!_lockedWandP1) WandSelectionHandle(PlayerID.Player1, horizontalP1Input, _player1SelectedWand, _player1Content);
                
                _audioSource.PlayOneShot(_hoverClip);
            }

            if (horizontalP2Input != 0 && _canP2Input)
            {
                if (!_lockedWizardP2)    WizardSelectionHorizontalHandle(PlayerID.Player2, horizontalP2Input, _player2SelectedWizard);
                else if (!_lockedWandP2) WandSelectionHandle(PlayerID.Player2, horizontalP2Input, _player2SelectedWand, _player2Content);
                
                _audioSource.PlayOneShot(_hoverClip);
            }
        }
        
        private void HandleSubmitInput()
        {
            if (Input.GetKeyDown(GlobalParams.SubmitInputP1))
            {
                if (!_lockedWizardP1)
                {
                    _player1Content.OnWizardFixed();
                    _lockedWizardP1 = true;
                    
                    _audioSource.PlayOneShot(_selectionClip);
                }
                else if (!_lockedWandP1)
                {
                    _player1Content.OnWandFixed();
                    _lockedWandP1 = true;
                    
                    _audioSource.PlayOneShot(_selectionClip);
                }
            }
            
            if(Input.GetKeyDown(GlobalParams.SubmitInputP2))
            {
                if (!_lockedWizardP2)
                {
                    _player2Content.OnWizardFixed();
                    _lockedWizardP2 = true;
                }
                else if (!_lockedWandP2)
                {
                    _player2Content.OnWandFixed();
                    _lockedWandP2 = true;
                }
            }
        }        
        
        private void HandleBackInput()
        {
            if (Input.GetKeyDown(GlobalParams.BackButtonP1))
            {
                if (_lockedWandP1)
                {
                    _lockedWandP1 = false;
                    _player1Content.OnWandUnfixed();
                }
                else if(_lockedWizardP1)
                {
                    _lockedWizardP1 = false;
                    _player1Content.OnWizardUnfixed();
                }
            }
            
            if(Input.GetKeyDown(GlobalParams.BackButtonP2))
            {
                if (_lockedWandP2)
                {
                    _lockedWandP2 = false;
                    _player2Content.OnWandUnfixed();
                }
                else if(_lockedWizardP2)
                {
                    _lockedWizardP2 = false;
                    _player2Content.OnWizardUnfixed();
                }
            }
        }
        
        private void WizardSelectionHorizontalHandle(PlayerID playerId, float input, int currentSelection)
        {
            StartCoroutine(InputDelay(playerId));

            if (input > 0)
            {
                currentSelection++;
                if (currentSelection > _wizardDB.Count -1) currentSelection = 0;
                
                if (_togglehandler.IsSelected(currentSelection)) currentSelection++;
                if (currentSelection > _wizardDB.Count -1) currentSelection = 0;
            }
            else
            {
                currentSelection--;
                if (currentSelection < 0) currentSelection = _wizardDB.Count -1;
                
                if (_togglehandler.IsSelected(currentSelection)) currentSelection--;
                if (currentSelection < 0) currentSelection = _wizardDB.Count -1;
            }

            if (playerId == PlayerID.Player1) _player1SelectedWizard = currentSelection;
            else                              _player2SelectedWizard = currentSelection;

            UpdateToggleHandler(playerId, currentSelection);
        }

        private void WandSelectionHandle(PlayerID playerID, float input, int currentSelection, PlayerContent playerContent)
        {
            StartCoroutine(InputDelay(playerID));
            
            if (input > 0)
            {
                currentSelection++;
                if (currentSelection > _wandDB.Count -1) currentSelection = 0;
            }
            else
            {
                currentSelection--;
                if (currentSelection < 0) currentSelection = _wandDB.Count -1;
            }

            if (playerID == PlayerID.Player1) _player1SelectedWand = currentSelection;
            else                              _player2SelectedWand = currentSelection;
            
            playerContent.UpdateWand(_wandDB[currentSelection]);
        }
        
        private IEnumerator InputDelay(PlayerID id)
        {
            if (id == PlayerID.Player1) _canP1Input = false;
            else                        _canP2Input = false;
            
            yield return new WaitForSecondsRealtime(0.2f);
            
            if (id == PlayerID.Player1) _canP1Input = true;
            else                        _canP2Input = true;
        }

        private void UpdateToggleHandler(PlayerID playerID, int selectionID) => _togglehandler.Select(playerID, selectionID);

        private void LoadWizards()
        {
            foreach (var wizard in _wizardDB)
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

        private void OnWizardSelected(KeyValuePair<PlayerID, WizardSO> valuePair)
        {
            switch (valuePair.Key)
            {
                case PlayerID.Player1:
                    _player1Content.UpdatePlayerContent(valuePair.Value);
                    break;
                case PlayerID.Player2:
                    _player2Content.UpdatePlayerContent(valuePair.Value);
                    break;
                case PlayerID.None:
                default:
                    Debug.Log("No Player Selected");
                break;
            }
        }
        
        private void CheckForMatchStart()
        {
            if(_lockedWizardP1 && _lockedWizardP2 && _lockedWandP1 && _lockedWandP2) OnMatchSettedUp();
        }

        private void OnMatchSettedUp()
        {
            PersistentData.Instance.SetMatchData(_player1SelectedWizard, _player2SelectedWizard, _player1SelectedWand, _player2SelectedWand);
        }
        
        #endregion
    }
}