# HeliumCompany
A BepInEx plugin for [Lethal Company](https://store.steampowered.com/app/1966720) that allows you to adjust the pitch of the voice chat.

Build instructions are in [BUILDING.md](docs/BUILDING.md).

## Config

```
## Settings file was created by plugin HeliumCompany v1.0.1
## Plugin GUID: HeliumCompany

[General]

## The (base) pitch the voice chat is set to.
# Setting type: Single
# Default value: 3
Pitch = 3

## Overrides any pitch value assigned by the game.
## Recommended due to the TZP-Inhalant increasing player pitch.
# Setting type: Boolean
# Default value: true
Override = true

## Allows you to edit the config in runtime, applying changes whenever you leave the settings menu.
## If disabled during runtime, this will not function again until restart.
# Setting type: Boolean
# Default value: false
RuntimeConfig = false

[OverTime]

## Increases the voice chat pitch over time, resetting each round.
# Setting type: Boolean
# Default value: false
Enable = false

## How many seconds before the pitch increases.
## The lower the value, the more robotic the voice chat becomes.
# Setting type: Single
# Default value: 1
Seconds = 1

## The amount the pitch increases by.
# Setting type: Single
# Default value: 0.005
Amount = 0.005

## The maximum the pitch can go up to.
# Setting type: Single
# Default value: 9
Maximum = 9
```