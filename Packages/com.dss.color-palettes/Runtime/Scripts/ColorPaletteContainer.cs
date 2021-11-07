using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DSS.ColorPalettes
{
    // @brief A collection of color palettes, for easily switching color palettes in a project.
    [CreateAssetMenu(fileName = "New Color Palette Container", menuName = "DSS/Color Palettes/Color Palette Container", order = 2)]
    public class ColorPaletteContainer : ColorPalette
    {
        [System.Serializable]
        public class IndexChangeEvent : UnityEvent<int> { }

        // @brief The palettes in this container.
        [SerializeField] List<ColorPalette> palettes = default;
        
        // @brief The index of the currently active palette.
        [SerializeField] int index = 0;

        public IndexChangeEvent onIndexChanged = new IndexChangeEvent();

        // @brief Returns the color of the specified key in the specified
        // palette, or pink if the palette is not set, or the key is not found.
        public override Color GetColor(string key)
        {
            try
            {
                return palettes[index].GetColor(key);
            }
            catch
            {
                return defaultColor;
            }
        }

        // @brief Sets the index of the palette to use.
        public void SetIndex(int newIndex)
        {
            if (newIndex == index)
            {
                return;
            }
            if (newIndex < 0 || newIndex >= palettes.Count)
            {
                Debug.LogWarning("Index out of range.");
                return;
            }
            
            index = newIndex;
            foreach (ApplyColorPalette applicator in Object.FindObjectsOfType<ApplyColorPalette>(true))
            {
                applicator.ApplyColor();
            }

            foreach (ApplyColorPaletteToButton applicator in Object.FindObjectsOfType<ApplyColorPaletteToButton>(true))
            {
                applicator.ApplyColor();
            }

            foreach (ApplyColorPaletteToToggle applicator in Object.FindObjectsOfType<ApplyColorPaletteToToggle>(true))
            {
                applicator.ApplyColor();
            }

            onIndexChanged.Invoke(index);
        }

        // @brief Returns the index of the currently active palette.
        public int GetIndex()
        {
            return index;
        }

        // @brief Incremenets the index to the next palette in the list.
        public void IncrementIndex()
        {
            SetIndex((GetIndex() + 1) % GetPaletteCount());
        }

        // @brief Returns the total number of palettes in this container.
        public int GetPaletteCount()
        {
            return palettes.Count;
        }
    }
}