using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Tile : MonoBehaviour
{
	private TMPro.TextMeshProUGUI text;
    private UnityEngine.UI.Image image;
	private RectTransform rectTransform;
	public int Value { get; set; }
	public (int, int) Coordinate { get; set; }
    public bool moved;

	private void Awake()
	{
		rectTransform = GetComponent<RectTransform>();
		text = GetComponentInChildren<TMPro.TextMeshProUGUI>();
        image = GetComponent<UnityEngine.UI.Image>();
	}

	public void UpdateUIAnimation()
	{
		text.text = Value.ToString();
        image.DOColor(ColorManager.instance.GetColor(Value), 0.05f);
	}

	public void SetPosition(Vector2 pos, bool animate, System.Action onCompleteMethod = null)
	{
        if (animate)
        {
            rectTransform.DOAnchorPos(pos, 0.1f).OnComplete(() => {
                if(onCompleteMethod != null)
                    onCompleteMethod.Invoke();
            });

        }
        else
        {
            rectTransform.anchoredPosition = pos;
            if(onCompleteMethod != null)
                    onCompleteMethod.Invoke();
        } 
            
        gameObject.name = $"({Coordinate.Item1},{Coordinate.Item2})";
	}

    public void InAnimation()
    {
        rectTransform.localScale = Vector3.one * 0.2f;
        rectTransform.DOScale(1, 0.1f);
    }

    public void MergeAnimation()
    {
        rectTransform.DOPunchScale(Vector3.one * 0.1f, 0.1f, 1, 1);
    }

}
