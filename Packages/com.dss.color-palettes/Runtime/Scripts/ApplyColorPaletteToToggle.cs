using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DSS.ColorPalettes
{
    // @brief A special color palette applicator, specifically meant for Toggles.
    // It allows you to specify different color palette entries for the Toggle
    // (and any contained graphics), that change depending on wether the Toggle is
    // hovered, clicked, and activated, or not.
    [RequireComponent(typeof(Toggle))]
    [ExecuteInEditMode]
    public class ApplyColorPaletteToToggle : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [System.Serializable]
        public class ApplicationTarget
        {
            public Graphic graphic;
            public string unclickedEntryName;
            public string hoveredEntryName;
            public string clickedEntryName;
            public string activatedEntryName;
        }

        [SerializeField] ColorPalette preset = default;
        [SerializeField] ApplicationTarget[] targets = default;

        [SerializeField] AnimationCurve lerpCurve = AnimationCurve.Linear(0f,0f,1f,1f);
        [SerializeField] float lerpDuration = 0.25f;

        float hoverTimer = 0f;
        bool hover = false;

        float clickTimer = 0f;
        bool click = false;

        private Toggle _toggle;
        private Toggle toggle
        {
            get
            {
                if (_toggle == null)
                {
                    _toggle = GetComponent<Toggle>();
                }
                return _toggle;
            }
        }

        void Update()
        {
            if (preset == null)
            {
                return;
            }

            hoverTimer += (hover ? 1f : -1f) * Time.deltaTime / lerpDuration;
            hoverTimer = Mathf.Clamp01(hoverTimer);

            clickTimer += (click ? 1f : -1f) * Time.deltaTime / lerpDuration;
            clickTimer = Mathf.Clamp01(clickTimer);

            ApplyColor();
        }

        public void OnPointerDown(PointerEventData data)
        {
            click = toggle.interactable;
        }

        public void OnPointerUp(PointerEventData data)
        {
            click = false;
        }

        public void OnPointerEnter(PointerEventData data)
        {
            hover = toggle.interactable;
        }

        public void OnPointerExit(PointerEventData data)
        {
            hover = false;
        }

        private void OnDisable()
        {
            hover = false;
            click = false;
            hoverTimer = 0f;
            clickTimer = 0f;
            ApplyColor();
        }

        private void OnEnable()
        {
            ApplyColor();
        }

        public void ApplyColor()
        {
            foreach (ApplicationTarget target in targets)
            {
                if (target == null || target.graphic == null)
                {
                    continue;
                }
                target.graphic.color = Color.Lerp(
                    Color.Lerp(
                        toggle.isOn ? preset.GetColor(target.activatedEntryName) : preset.GetColor(target.unclickedEntryName),
                        preset.GetColor(target.hoveredEntryName),
                        lerpCurve.Evaluate(hoverTimer)
                    ),
                    preset.GetColor(target.clickedEntryName),
                    lerpCurve.Evaluate(clickTimer)
                );
            }
        }
    }
}