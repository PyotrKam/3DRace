using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    [SerializeField] protected bool Interactable;

    private bool focuse = false;
    public bool Focuse => focuse;

    public UnityEvent OnClick;

    public event UnityAction<UIButton> PointerEnter;
    public event UnityAction<UIButton> PointerExit;
    public event UnityAction<UIButton> PointerClick;

    public virtual void SetFocuse()
    {
        if (Interactable == false) return;

        focuse = true;
    }

    public virtual void SetUnFocuse()
    {
        if (Interactable == false) return;

        focuse = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (Interactable == false) return;

        PointerEnter?.Invoke(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (Interactable == false) return;

        PointerExit?.Invoke(this);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Interactable == false) return;

        PointerClick?.Invoke(this);

        OnClick?.Invoke();
    }
    
}
