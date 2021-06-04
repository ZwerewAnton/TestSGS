using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;

public class DrawParametersMenu : MonoBehaviour
{
	[SerializeField] private Vector2 _spacing;
	[SerializeField] private float _rotationDuration;
	[SerializeField] private Ease _rotationEase;
	[SerializeField] private float _expandDuration;
	[SerializeField] private float _collapseDuration;
	[SerializeField] private Ease _expandEase;
	[SerializeField] private Ease _collapseEase;
	[SerializeField] private float _expandFadeDuration;
	[SerializeField] private float _collapseFadeDuration;
	[SerializeField] private UnityEvent<int> _onChangeColorEvent;
	[SerializeField] private UnityEvent _onPressBackButtonEvent;

	private Button _mainButton;
	private DrawParametersMenuItem[] _menuItems;
	private bool _isExpanded = false;
	private Vector2 _mainButtonPosition;
	private Image _mainButtonImage;
	private int _itemsCount;

	private void Start ()
	{
		_itemsCount = transform.childCount - 1;
		_menuItems = new DrawParametersMenuItem[_itemsCount];
		for (int i = 0; i < _itemsCount; i++) 
        {
			_menuItems [i] = transform.GetChild(i + 1).GetComponent<DrawParametersMenuItem>();
		}
		_mainButton = transform.GetChild(0).GetComponent<Button>();
		_mainButton.onClick.AddListener(ToggleMenu);
		_mainButton.transform.SetAsLastSibling();

		_mainButtonPosition = _mainButton.transform.position;
        _mainButtonImage = _mainButton.GetComponent<Image>();
		ResetPositions ();
	}
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
        {
			_onPressBackButtonEvent.Invoke();
		}
	}

	private void OnDestroy ()
	{
		_mainButton.onClick.RemoveListener(ToggleMenu);
	}
	private void ResetPositions ()
	{
		for (int i = 0; i < _itemsCount; i++) 
        {
			_menuItems[i].buttonTranform.position = _mainButtonPosition;
		}
	}

	private void ToggleMenu()
	{
		_isExpanded = !_isExpanded;

		if (_isExpanded) 
        {
			for(int i = 0; i < _itemsCount; i++) 
            {
				_menuItems[i].buttonTranform.DOMove(_mainButtonPosition + _spacing * (i + 1), _expandDuration).SetEase(_expandEase);
				_menuItems [i].image.DOFade(1f, _expandFadeDuration).From(0f);
			}
		} 
        else 
        {
			for (int i = 0; i < _itemsCount; i++) 
            {
				_menuItems[i].buttonTranform.DOMove(_mainButtonPosition, _collapseDuration).SetEase(_collapseEase);
				_menuItems[i].image.DOFade(0f, _collapseFadeDuration);
			}
		}
	}

	public void OnItemClick (int index)
	{
        _onChangeColorEvent.Invoke(index);
        _mainButtonImage.sprite = _menuItems[index].image.sprite;
	}

}
