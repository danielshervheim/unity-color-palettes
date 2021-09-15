using UnityEngine;
using UnityEngine.UI;

namespace DSS.ColorPalettes
{
    [RequireComponent(typeof(Graphic))]
    [ExecuteInEditMode]
    public class ApplyColorPalette : MonoBehaviour
    {
        [SerializeField] ColorPalette preset = default;
        [SerializeField] string entryName = default;

        Graphic _target = null;
        Graphic target
        {
            get
            {
                if (_target == null)
                {
                    _target = GetComponent<Graphic>();
                }
                return _target;
            }
        }

        void Update()
        {
            if (preset == null)
            {
                return;
            }

            target.color = preset.GetColor(entryName);
        }
    }
}