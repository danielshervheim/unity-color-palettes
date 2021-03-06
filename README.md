# unity-color-palettes

A system to keep UnityEngine.Graphic colors consistent throughout a project.

## Installation

**via unity package manager**

1. Add this repositories `upm` branch as a dependancy to your project's `manifest.json`

```json
"com.dss.color-palettes": "ssh://git@github.com/danielshervheim/unity-color-palettes.git#upm"
```

**manually**

1. Clone this repository
2. Copy the `Assets/DSS/ColorPalettes` folder into your own project's `Assets` folder.

## Usage

1. Create a Color Palette ScriptableObject.
    - Right click in your Assets window
    - Choose `Create > DSS > Color Palettes > Color Palette`
2. Set up the name-color pairs in the Scriptable Object.
    - The names you choose will act as keys, so they must be unique from each other
    - I've found that "background", "text", and "accent" go a long ways, but you can set it up however
3. Select your gameObjects that have `Graphic` components (`Image`, `TextMeshPro`, etc)
4. Add an `ApplyColorPalette` component to them
5. Drag your ScriptableObject into the "Preset" slot
    - The color will be pink initially, which is the "default"
6. Set the "name" slot to the key in the palette that you want to use for this Graphic component

There is also a `Create > DSS > Color Palettes > Color Palette Container` ScriptableObject, which acts itself as a color palette (you can assign it to `ApplyColorPalette` components).

It's benefit is that, rather than declaring colors directly, you can instead provide other color palettes. Basically it allows you an easy way to switch entire palettes in a project just by toggling a check box.

## Example

![palette](https://i.imgur.com/Qs3akQy.png)

> In this palette, I've declared three name-color pairs corresponding to the background, text, and an accent color. You can add as many name-color pairs, but the names should be unique from each other.

![container](https://i.imgur.com/Z3mZ88U.png)

> In this palette container, I've added the dark theme from before, as well as other themes. I can easily swap between them by checking the appropriate box (as well as through custom scripts).

![apply](https://i.imgur.com/dU2woJE.png)

> I've added an `ApplyColorPalette` component to `Image` component, and set it to use the "accent" color from the previous palette container. Notice that the container itself acts as a palette. (We could have just as easily dropped in a single color palette instance as well).

## Demo

[![demo](https://i.imgur.com/pCBwgqv.gif)](https://imgur.com/a/qSkGVFI)

