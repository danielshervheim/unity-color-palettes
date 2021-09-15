# Color Palettes

A system to keep UnityEngine.Graphic colors consistent throughout a project.

## Example

![palette](https://i.imgur.com/Qs3akQy.png)

> In this palette, I've declared three name-color pairs corresponding to the background, text, and an accent color. You can add as many name-color pairs, but the names should be unique from each other.

![container](https://i.imgur.com/Z3mZ88U.png)

> In this palette container, I've added the dark theme from before, as well as other themes. I can easily swap between them by checking the appropriate box (as well as through custom scripts).

![apply](https://i.imgur.com/dU2woJE.png)

> I've added an `ApplyColorPalette` component to `Image` component, and set it to use the "accent" color from the previous palette container. Notice that the container itself acts as a palette. (We could have just as easily dropped in a single color palette instance as well).

## Demo

[![demo](https://i.imgur.com/pCBwgqv.gif)](https://imgur.com/a/qSkGVFI)


## How To Install

The color-palettes package uses the [scoped registry](https://docs.unity3d.com/Manual/upm-scoped.html) feature to import
dependent packages. Please add the following sections to the package manifest
file (`Packages/manifest.json`).

To the `scopedRegistries` section:

```
{
  "name": "DSS",
  "url": "https://registry.npmjs.com",
  "scopes": [ "com.dss" ]
}
```

To the `dependencies` section:

```
"com.dss.color-palettes": "1.2.1"
```

After changes, the manifest file should look like below:

```
{
  "scopedRegistries": [
    {
      "name": "DSS",
      "url": "https://registry.npmjs.com",
      "scopes": [ "com.dss" ]
    }
  ],
  "dependencies": {
    "com.dss.color-palettes": "1.2.1",
    ...
```