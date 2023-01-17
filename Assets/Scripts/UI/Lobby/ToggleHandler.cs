using System.Collections.Generic;
using UnityEngine;

public class ToggleHandler : MonoBehaviour
{
    #region Public Variables

    public List<WizardToggle> toggles;

    #endregion

    #region Private Variables

    private WizardToggle _p1Selection, _p2Selection;

    #endregion

    #region Unity LifeCycle

    private void Awake()
    {
        toggles = new List<WizardToggle>();
        WizardToggle.OnToggleSelected.AddListener(OnToggleValueChanged);
    }

    #endregion

    #region Utility Methods

    private void OnToggleValueChanged(KeyValuePair<Player.PlayerID, WizardToggle> valuePair)
    {
        if (valuePair.Key == Player.PlayerID.Player1) _p1Selection = valuePair.Value;
        else _p2Selection = valuePair.Value;
        
        foreach (var toggle in toggles)
        {
            var selected = toggle == _p1Selection || toggle == _p2Selection;

            toggle.OnValueChanged(selected);
        }
    }

    public void AddToggleToHandler(WizardToggle toggle) => toggles.Add(toggle);

    public void Select(Player.PlayerID playerID, int selectionId)
    {
        toggles[selectionId].Select(playerID);
    }

    public bool IsSelected(int n) => toggles[n].IsSelected();

    #endregion
}
