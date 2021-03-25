# ColorPalettes

## 1.0.0

- Initial release.

## 1.0.1

- Refreshed GUIDs

## 1.1.0

- Added `ApplyColorPaletteToButton` class for handling the special case of buttons (which often change color during interaction).

## 1.1.1

- Fixed bug related to default value of `Targets` property in `ApplyColorPaletteToButton` script.

## 1.1.2

- Added a `hoveredEntryName` option to the `ApplyColorPaletteToButton` class.

## 1.1.3

- Fixed a bug where the `ApplyColorPaletteToButton` state wouldn't properly reset if it's gameObject was disabled.

## 1.1.4

- Added `ApplyColorPaletteToToggle` specialized class for Toggles.

## 1.2.0

- Changed the way `ApplycolorPaletteToButton` and `ApplyColorPaletteToToggle` work. Now they will only show their unclicked colors if their respective buttons or toggles are marked "not interactable". (i.e. they will not change colors based on hover or click events).