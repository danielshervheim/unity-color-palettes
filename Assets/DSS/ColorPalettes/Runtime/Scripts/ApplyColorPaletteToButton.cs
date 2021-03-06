using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DSS.ColorPalettes
{
    // @brief A special color palette applicator, specifically meant for Buttons.
    // It allows you to specify two different color palette entries for the Button
    // (and any contained graphics), that change depending on wether the button is
    // clicked or not.
    [RequireComponent(typeof(Button))]
    [ExecuteInEditMode]
    public class ApplyColorPaletteToButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [System.Serializable]
        public class ApplicationTarget
        {
            public Graphic graphic;
            public string clickedEntryName;
            public string unclickedEntryName;
        }

        [SerializeField] ColorPalette preset = default;
        [SerializeField] ApplicationTarget[] targets = default;

        [SerializeField] AnimationCurve lerpCurve = AnimationCurve.Linear(0f,0f,1f,1f);
        [SerializeField] float lerpDuration = 0.25f;

        float timer = 0f;
        bool clicked = false;


        void Update()
        {
            if (preset == null)
            {
                return;
            }

            timer += (clicked ? 1f : -1f) * Time.deltaTime / lerpDuration;
            timer = Mathf.Clamp01(timer);

            foreach (ApplicationTarget target in targets)
            {
                if (target == null || target.graphic == null)
                {
                    continue;
                }
                target.graphic.color = Color.Lerp(preset.GetColor(target.unclickedEntryName), preset.GetColor(target.clickedEntryName), lerpCurve.Evaluate(timer));
            }
        }

        public void OnPointerDown(PointerEventData data)
        {
            clicked = true;
        }

        public void OnPointerUp(PointerEventData data)
        {
            clicked = false;
        }
    }
}