# Spooder Sling

> A 2D web-slinging platformer built in Unity where you play as a spider surviving a hazardous rain storm — how far can you sling before time runs out?

## Overview

Spooder Sling is a physics-based 2D platformer made in Unity 6. You control a spider character that traverses a side-scrolling level by firing spider webs onto grapple surfaces and swinging through the environment. The game ships with two distinct difficulty modes:

- **Easy Mode** — a fixed-angle aim system lets you select from six preset launch angles and fire a web with a single key press, keeping the focus on timing rather than precision aiming.
- **Hard Mode** — a free-aim system where you click the mouse directly on a grapple-able surface to attach your web and swing.

In both modes a countdown timer is actively working against you. The spider is also threatened by falling rain; picking up an umbrella collectible shields you from one hit of rain damage. The goal is to travel as far as possible (measured in centimetres along the X axis) before the timer expires or the spider is destroyed. Distance is tracked live on screen, and when the run ends the game transitions to an end screen.

## Prerequisites

| Requirement | Version |
|---|---|
| [Unity Editor](https://unity.com/releases/editor/archive) | **6000.2.15f1** (Unity 6) |
| Operating System | Windows, macOS, or Linux (desktop builds) |

> The project was created and tested with the exact editor version above. Opening it in a different Unity version may trigger an upgrade wizard and could require resolving API compatibility issues.

No additional SDKs, runtimes, or package managers are required. All Unity package dependencies are declared in `Packages/manifest.json` and are resolved automatically by the Unity Package Manager when you open the project.

## Installation

1. **Install Unity 6000.2.15f1.**
   Download and install the matching Unity Editor version through [Unity Hub](https://unity.com/unity-hub) or the [Unity Download Archive](https://unity.com/releases/editor/archive).

2. **Clone the repository.**
   ```bash
   git clone <repository-url>
   cd Spooder-Sling
   ```

3. **Open the project in Unity Hub.**
   - Launch Unity Hub.
   - Click **Open** > **Add project from disk**.
   - Navigate to the cloned `Spooder-Sling` folder and select it.
   - Unity will resolve all package dependencies listed in `Packages/manifest.json` automatically on first open.

4. **Wait for the import to complete.**
   Unity will import assets, compile scripts, and generate the `Library/` folder. This may take a few minutes on first open.

## Running the Game

### In the Unity Editor (Play Mode)

1. Open the **Main Menu** scene:
   `Assets/Scenes/Main Menu.unity`
2. Press the **Play** button in the Unity toolbar.
3. Use the on-screen buttons to navigate to Easy Mode, Hard Mode, or the Instructions screen.

### Building a Standalone Executable

1. In the Unity Editor, go to **File > Build Settings**.
2. Confirm all five scenes are listed in **Scenes In Build** (they are already configured in `ProjectSettings/EditorBuildSettings.asset`):

   | Build Index | Scene |
   |---|---|
   | 0 | `Assets/Scenes/Main Menu.unity` |
   | 1 | `Assets/Scenes/Hard Mode.unity` |
   | 2 | `Assets/Scenes/Easy Mode.unity` |
   | 3 | `Assets/Scenes/End Screen.unity` |
   | 4 | `Assets/Scenes/Intructions.unity` |

3. Select your target platform, then click **Build** or **Build And Run**.

## Controls

### Hard Mode (Free-Aim Web Slinging)

| Input | Action |
|---|---|
| Left Mouse Button (hold) | Fire web to cursor position / maintain grapple |
| Left Mouse Button (release) | Release web |
| `A` / Left Arrow | Move left |
| `D` / Right Arrow | Move right |
| `W` / Up Arrow | Jump (when grounded) |
| `R` | Restart current run (Hard Mode) |
| `E` | Switch to Easy Mode scene from End Screen |

### Easy Mode (Fixed-Angle Web Slinging)

| Input | Action |
|---|---|
| `Space` or `Left Shift` | Fire web at current aim angle |
| Mouse Scroll Up or `Left Ctrl` | Rotate aim angle upward through preset list |
| Mouse Scroll Down or `Left Alt` | Rotate aim angle downward through preset list |
| Release `Space` / `Left Shift` | Release web |
| `A` / Left Arrow | Move left |
| `D` / Right Arrow | Move right |
| `W` / Up Arrow | Jump (when grounded) |
| `R` | Restart current run (Easy Mode) |

### Available Aim Angles (Easy Mode)

The six preset angles cycle in order: **30°, 45°, 60°, 120°, 135°, 150°** (measured from the positive X axis). An aim line is rendered on screen showing the currently selected direction and its raycast range.

## Gameplay Systems

### Web Slinging

**Hard Mode** uses a mouse-click overlap check against the `grappleLayer` physics layer. When you click on a tagged surface a `DistanceJoint2D` is activated, anchoring the spider at a fixed rope length that lets it swing freely under gravity.

**Easy Mode** fires a `Physics2D.Raycast` along the selected preset angle up to `maxGrappleDistance` (default: 20 units). If the ray hits a collider on the `grappleLayer` the web attaches at the exact contact point and a `DistanceJoint2D` is activated identically to Hard Mode. Both modes render the web rope with a `LineRenderer`.

### Rain Hazard

A particle-system rain effect follows the spider along the X axis (`FollowObject`). Rain particles that collide with the spider destroy it (`RainDamage`). Picking up an umbrella collectible attaches it to the spider's sprite offset and absorbs one rain hit before being destroyed itself (`Umbrella`).

### Timer

A countdown timer starts at **5 seconds** by default (configurable via the `CountdownTimer.totalTime` field in the Inspector). When it reaches zero the spider GameObject is destroyed, which triggers `DeathScreen` to load the **End Screen** scene.

### Distance Tracking

The `DistanceDisplay` component reads the spider's world X position each frame and displays it as `Distance: X.XX cm` using TextMesh Pro.

### Audio

The `SlingSound` component plays an attached `AudioSource` whenever a web-fire input is detected (left mouse click, Space, or Left Shift). Available audio assets in `Assets/Audio/` include web, water, thunder, insect, portal, and flamethrower sounds assignable in the Inspector.

## Project Structure

```
Spooder-Sling/
├── Assets/
│   ├── Audio/               # Sound effect files (mp3, wav, ogg, flac)
│   ├── Materials/           # 2D materials (Insect, WaterDroplet)
│   ├── Resources/           # Runtime-loadable assets
│   ├── Scenes/              # All five Unity scenes
│   │   ├── Main Menu.unity
│   │   ├── Hard Mode.unity
│   │   ├── Easy Mode.unity
│   │   ├── End Screen.unity
│   │   └── Intructions.unity
│   ├── Scripts/
│   │   ├── Audio/
│   │   │   └── SlingSound.cs          # Plays web-fire audio
│   │   ├── Movement/
│   │   │   ├── 2DPlatformerMovement.cs # Horizontal movement and jumping
│   │   │   ├── WebSlinging.cs          # Hard Mode grapple logic
│   │   │   └── EasySlinging.cs         # Easy Mode grapple logic
│   │   ├── Rain/
│   │   │   ├── RainDamage.cs           # Destroys spider on particle hit
│   │   │   ├── Umbrella.cs             # Collectible rain shield
│   │   │   └── FollowPlayer.cs         # Keeps rain system above spider
│   │   └── UI_&_Scenes/
│   │       ├── CountdownTimer.cs       # Counts down from totalTime
│   │       ├── TimeDeath.cs            # Destroys spider when timer expires
│   │       ├── DeathScreen.cs          # Loads End Screen on spider death
│   │       ├── DistanceDisplay.cs      # Shows live distance travelled
│   │       ├── BackToMenu.cs           # Returns to Main Menu (scene 0)
│   │       ├── Instructions.cs         # Loads Instructions scene (scene 4)
│   │       ├── EasyMode.cs             # Loads Easy Mode scene by name
│   │       ├── HardMode.cs             # Loads Hard Mode scene by name
│   │       ├── Restart.cs              # R = Hard Mode, E = Easy Mode restart
│   │       └── SpooderBreak.cs         # Destroys colliding objects (hazard)
│   ├── Sprites/             # Spider, web, insect, and background sprites
│   └── Settings/            # URP renderer and post-processing settings
├── Packages/
│   └── manifest.json        # Unity Package Manager dependencies
├── ProjectSettings/         # Unity project configuration (build, physics, tags)
└── ignore.conf              # Version control ignore rules
```

## Configurable Settings

All tunable values are exposed as `public` fields on their respective MonoBehaviour components and can be adjusted in the Unity Inspector without modifying code.

| Component | Field | Default | Description |
|---|---|---|---|
| `Movement` | `maxSpeed` | `0.01` | Maximum horizontal and swing speed |
| `Movement` | `jump` | `(0, 3)` | Upward impulse vector applied on jump |
| `EasySlinging` | `maxGrappleDistance` | `20` | Maximum raycast range for Easy Mode grapple |
| `EasySlinging` | `aimLineLength` | `5` | Visual length of the aim preview line |
| `EasySlinging` | `aimAngles` | `[30, 45, 60, 120, 135, 150]` | Preset aim angles in degrees |
| `WebSlinging` | `grappleLayer` | _(Inspector)_ | Physics layer mask that webs can attach to |
| `EasySlinging` | `grappleLayer` | _(Inspector)_ | Physics layer mask for Easy Mode raycast |
| `CountdownTimer` | `totalTime` | `5` | Starting countdown duration in seconds |

### Physics Layers

The following layers are defined in `ProjectSettings/TagManager.asset` and are used by the physics and grapple systems:

| Layer | Purpose |
|---|---|
| `Default` | General collidable geometry |
| `Ground` | Surfaces the spider can stand on and jump from |
| `Water` | Water-related physics interactions |
| `Player` | Spider character; used by the Umbrella pickup trigger |

Assign the correct layer(s) to the `grappleLayer` mask on the `WebSlinging` and `EasySlinging` components so that webs only attach to intended surfaces.

## Development

### Recommended IDEs

The project includes IDE integration packages for both **Rider** (`com.unity.ide.rider 3.0.38`) and **Visual Studio** (`com.unity.ide.visualstudio 2.0.25`). Set your preferred editor under **Edit > Preferences > External Tools** in Unity.

### Opening Generated Solution Files

The repository root contains two solution files:

- `Spooder Sling.sln` — current generated solution
- `Spooder_Sling_Claude_V2.sln` — alternate solution file

Open either `.sln` in your IDE to get full IntelliSense and debugging support. These files are regenerated by Unity and are excluded from version control by `ignore.conf`.

### Key Unity Packages Used

| Package | Version | Purpose |
|---|---|---|
| `com.unity.render-pipelines.universal` | 17.2.0 | URP rendering pipeline |
| `com.unity.inputsystem` | 1.16.0 | New Input System (actions asset) |
| `com.unity.ugui` | 2.0.0 | UI Toolkit / uGUI canvas |
| `com.unity.2d.sprite` | 1.0.0 | 2D sprite rendering |
| `com.unity.modules.physics2d` | 1.0.0 | Rigidbody2D, DistanceJoint2D, Raycast |
| `com.unity.modules.particlesystem` | 1.0.0 | Rain particle system |
| `com.unity.modules.audio` | 1.0.0 | AudioSource / AudioMixer |

### Adding New Grapple Surfaces

1. Select the GameObject you want to make grapple-able in the scene.
2. Assign it to the physics layer referenced by the `grappleLayer` mask on the `WebSlinging` or `EasySlinging` component (Hard Mode uses an overlap circle check; Easy Mode uses a raycast and requires a `Collider2D`).
3. Ensure the GameObject has a `Collider2D` component attached.

### Extending Aim Angles (Easy Mode)

Open `Assets/Scripts/Movement/EasySlinging.cs` and edit the `aimAngles` list to add, remove, or reorder preset angles. Angles are in degrees measured counter-clockwise from the positive X axis (0° = right, 90° = straight up).
