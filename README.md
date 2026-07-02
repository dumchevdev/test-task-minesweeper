# 💣 Minesweeper – Test Task

**Architecture:** Service-oriented architecture with dependency injection, MVP UI, and an explicit session state machine.

**Tech stack:** VContainer, Unity Input System, Unity UI (uGUI), TextMeshPro, NUnit (Edit Mode Tests).

<p align="center">
  <img src="Misc/gameplay-demo.gif" alt="Gameplay Demo" width="600"/>
</p>

<p align="center">
  <a href="https://dumchevdev.github.io/test-task-minesweeper/">
    <img src="https://img.shields.io/badge/Play in browser-66c266?style=for-the-badge" alt="Play in browser"/>
  </a>
</p>

---

## 📋 Test Task Requirements

| # | Requirement |
|---|-----------|
| 1 | **Visuals don't matter** - focus on architecture and logic |
| 2 | **Single scene** - no scene transitions between game states |
| 3 | **Field configuration** - grid size and mine count are set via `GridConfig` (ScriptableObject) |
| 4 | **Safe first click** - mines are placed after the first click, excluding the selected cell |
| 5 | **Quick restart** - pressing `R` at any time restarts the game |
| 6 | **Mouse controls** - LMB opens a cell, RMB places/removes a flag |
| 7 | **Chain reveal** - opening an empty cell iteratively reveals all neighboring cells with zero adjacent mines, and their neighbors |
| 8 | **Full game loop** - Main Menu → Game (timer + pause button) → Pause (resume / restart / exit to menu) |
| 9 | **End screen** - win/loss text, "Restart" and "Exit to Menu" buttons; restart also available via keyboard |

---

## 🧩 Architecture

### 🚀 Startup and Initialization

The application consists of a single scene, so all services live for the application's lifetime. Dependency management is handled via **VContainer**:

- **`BootstrapScope`** - the root IoC container. Registers all dependencies and starts `BootstrapFlow`.
- **`BootstrapFlow`** - entry point. In `Start()`, initializes the config and opens the main menu.
- Dependencies are split across **Installer** classes: `GameplayInstaller` and `UIInstaller`.

---

## ⚙️ Services

| Service | Description                                                                                 |
|--------|---------------------------------------------------------------------------------------------|
| `AssetsProvider` | Loads resources via `Resources.Load<T>`                                                     |
| `ConfigsProvider` | Stores and provides `GridConfig` / `CellViewConfig`; initialized once at startup            |
| `RandomProvider` | Random number provider used by gameplay systems; supports seeded generation for deterministic testing |
| `InputsService` | Wrapper over Unity Input System; translates input into `OnRestartPressed` / `OnPausePressed` events |
| `TimerService` | A simple in-game timer                                                                      |
| `SessionsService` | Creates and manages the current game session; broadcasts events about session start and state changes |
| `GameFlowService` | Coordinates the gameplay loop by orchestrating the session, timer, UI, and input            |
| `UIRouter` | Controls application window navigation                                                  |
| `CellViewFactory` | Factory for creating/destroying `CellView` instances                                        |

---

## 📋 Configs

| Config | Purpose |
|--------|---------|
| `GridConfig` | ScriptableObject defining field dimensions and mine count. The editor includes validation (`OnValidate`): if `mineCount` exceeds the allowed maximum, it's automatically clamped with a console warning |
| `CellViewConfig` | ScriptableObject containing all cell sprites and number colors; injected into `CellView` during initialization for visual rendering |

---

## 🎮 Gameplay

### Session and State Machine

Each game session is represented by a `SessionState` class, managed by a `SessionStateMachine`. The state machine contains four states:

```
WaitingFirstClick → Playing → Won
                            ↘ Lost
```

| State | Behavior |
|-----------|-----------|
| `WaitingFirstClickState` | On the first click (LMB or RMB), places mines excluding the selected cell, then transitions to `Playing`. If the first click was RMB, a flag is placed immediately after the transition; if LMB, the cell is opened |
| `PlayingState` | Handles cell opening and chain-reveal of empty areas; manages flag placement/removal; checks the win condition after each cell opening |
| `WonState` | Win state - blocks any further interaction with the field |
| `LostState` | Loss state - on `OnEnter()`, reveals all remaining mines on the field; blocks any further interaction |

### Mine Placement - `MinesPlacer`

Uses a partial Fisher-Yates shuffle: shuffles only the first N elements of the candidate list (all cells except the one clicked first).

### Chain Reveal - `FloodFiller`

Starts from the selected cell by opening it and removing its flag if present. If the cell has no adjacent mines, all unopened non-mine neighbors are pushed onto the stack, iteratively revealing the connected empty area without recursion.
### Cell Data - `CellData`

Cell state is encoded via two enums:

- `CellState` (`Closed` / `Open`) - primary state
- `CellAttributes` (bit flags: `Mine`, `Flagged`, `Exploded`) - additional attributes

---

## 🖱️ Controls

| Action | Input |
|----------|------|
| Open cell | LMB on a cell |
| Place/remove flag | RMB on a cell |
| Pause / resume | HUD button or `Escape` key |
| Restart | `R` key (at any time) |

---

## 🧮 UI

The UI follows the **MVP (Model-View-Presenter)** pattern. Each screen has a View + Presenter pair:

| View | Presenter | Purpose |
|------|-----------|------------|
| `MainMenuWindow` | `MainMenuPresenter` | Start button |
| `HudWindow` | `HudPresenter` | Gameplay screen: grid, timer, flag counter, pause button |
| `PauseWindow` | `PausePresenter` | Pause menu: resume, restart, exit |
| `GameOverWindow` | `GameOverPresenter` | Game result: outcome, restart, exit |

**Grid Components:**

| Component | Purpose                                                                                             |
|-----------|-----------------------------------------------------------------------------------------------------|
| `GridView` | Manages layout of the grid via `GridLayoutGroup`                                                    |
| `GridScaleCalculator` | Static helper that computes the uniform scale required to fit the grid into the available viewport. Used by `GridView` during setup |
| `GridPresenter` | Creates/destroys `CellView` objects; manages `CellBinding` instances                                |
| `CellView` | Renders individual cell state                                                                       |
| `CellBinding` | Bridges `CellView` and `CellData`; subscribes to state changes and updates the view                 |

`UIRoot` is a single MonoBehaviour container holding references to all windows. `UIRouter` toggles their visibility.

### HUD

- **Timer** (`TimerView`) - starts when the first cell is opened (transition to `Playing` state), stops on pause or game over.
- **Flag Counter** (`FlagCounterView`) - displays `MineCount - FlagCount`. Updated via the `OnFlagCountChanged` event in `GridPresenter`.
- **Pause Button** - pauses the game and opens `PauseWindow` when pressed.

### Grid

`GridPresenter` creates `CellView` instances via `CellViewFactory` and manages a set of `CellBinding` objects linking `GridData` and `CellView`. Each `CellBinding` subscribes to `CellData` changes and calls `CellView.Render()` when the cell state updates. `GridPresenter` also maintains the flag counter and notifies the HUD via the `OnFlagCountChanged` event.

---

## ✅ Testing

Tests are written using **NUnit Edit Mode** and live in a separate `Minesweeper.Tests.EditMode` assembly, isolated from runtime code.

| Test Class         | What's Covered |
|--------------------|---|
| `FloodFillerTests` | Reveal of a connected empty area; reveal stops at numbered cells; flag removal when revealing a flagged cell |
| `SessionTests`     | State machine transitions (`WaitingFirstClick → Playing → Won/Lost`); first-click safety; blocking cell opening on flagged cells; mine reveal transitions session to `Lost`; opening all safe cells transitions to `Won` |
| `MinesPlacerTests` | Correct mine count placed; correct `AdjacentMineCount` calculation for every non-mine cell |

`TestsHelper` builds a 9×9 / 10-mine `GridData` and a seeded (`seed = 42`) `IRandomProvider`, so mine placement is deterministic across test runs.

---

## 🔭 Possible Improvements

- **Pool `GridData` / `CellData`** – Reuse and reset grid data between sessions instead of reallocating it to reduce GC pressure.
- **Pool `CellBinding`** – Reuse bindings when the grid size is unchanged instead of recreating them.
- **Pool `CellView`** – Replace `Instantiate`/`Destroy` with object pooling to eliminate GameObject churn during grid rebuilds.