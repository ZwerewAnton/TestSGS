using UnityEngine;
using UnityEngine.UI;

public class DrawParametersMenuItem : MonoBehaviour
{
    [HideInInspector] public Image image;
	[HideInInspector] public Transform buttonTranform;
	private DrawParametersMenu _settingsMenu;
	private Button _button;
	private int _index;

	private void Awake ()
	{
		image = GetComponent<Image>();
		buttonTranform = transform;

		_settingsMenu = buttonTranform.parent.GetComponent<DrawParametersMenu>();

		_index = buttonTranform.GetSiblingIndex() - 1;

		_button = GetComponent<Button>();
		_button.onClick.AddListener(OnItemClick);
	}

	private void OnItemClick()
	{
		_settingsMenu.OnItemClick(_index);
	}

	private void OnDestroy()
	{
		_button.onClick.RemoveListener(OnItemClick);
	}
}
