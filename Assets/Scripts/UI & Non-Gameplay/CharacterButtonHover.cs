using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; // Thêm namespace này để dùng EventTrigger

public class CharacterButtonHover : MonoBehaviour
{
    public Animator characterAnimator; // Animator chứa animation
    public string hoverTrigger = "Hover"; // Tên Trigger trong Animator

    private Button button; // Button Component

    void Start()
    {
        button = GetComponent<Button>();

        // Lấy Event Trigger từ Button
        EventTrigger trigger = gameObject.AddComponent<EventTrigger>();

        // Thêm sự kiện OnPointerEnter
        AddEventTrigger(trigger, EventTriggerType.PointerEnter, OnHoverStart);

        // Thêm sự kiện OnPointerExit
        AddEventTrigger(trigger, EventTriggerType.PointerExit, OnHoverEnd);
    }

    private void AddEventTrigger(EventTrigger trigger, EventTriggerType type, UnityEngine.Events.UnityAction<BaseEventData> action)
    {
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = type;
        entry.callback.AddListener(action);
        trigger.triggers.Add(entry);
    }

    // Khi hover bắt đầu
    public void OnHoverStart(BaseEventData data)
    {
        if (characterAnimator != null)
        {
            characterAnimator.SetTrigger(hoverTrigger);
        }
    }

    // Khi hover kết thúc
    public void OnHoverEnd(BaseEventData data)
    {
        if (characterAnimator != null)
        {
            characterAnimator.ResetTrigger(hoverTrigger);
        }
    }
}
