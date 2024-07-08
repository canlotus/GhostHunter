# Who Is Ghost?

Who Is Ghost? is a multiplayer detective game. Players take turns learning their roles, performing actions, and trying to catch the ghosts. In the game, ghosts attempt to eliminate other players while detectives and protectors try to stop them.

## Installation

### Requirements

- Unity 2020.3 or later
- Android Build Support

### Steps

1. Clone this repository or download the zip file.
    ```sh
    git clone [https://github.com/canlotus/GhostHunter]
    ```
2. Open the project in Unity.

## How to Play

1. **Enter Player Names**: Players enter their names.
2. **Learn Roles**: Players take turns holding the phone and learning their roles.
3. **Voting**: Players take turns casting their votes. The player with the most votes is eliminated.
4. **Actions**: Players take turns performing their roles:
   - **Ghost**: Selects a player to eliminate.
   - **Protector**: Protects a player (including themselves, but can only protect themselves once per game).
   - **Detector**: Learns the role of another player.
5. **Determine Winner**: The game ends when all ghosts are eliminated or the number of ghosts exceeds the other roles combined.

## Project Structure

- **Assets**: All game assets and scripts.
- **Scenes**: Unity scenes.
  - `MainMenu`
  - `PlayerSetup`
  - `RoleAssignment`
  - `RolePlayScreen`
  - `VotingScreen`
  - `ResultScreen`
  - `GameOver`

## Key Scripts

- **GameData.cs**: Manages player data and game state.
- **MainMenu.cs**: Controls the main menu.
- **PlayerSetup.cs**: Handles player name input and game setup.
- **RolePlayScreen.cs**: Manages player roles and actions.
- **VotingScreen.cs**: Allows players to cast their votes.
- **ResultScreen.cs**: Displays voting results and game state.
- **AudioManager.cs**: Manages background music.
- **ReturnMainMenu.cs**: Provides functionality to return to the main menu.

## Development

### UI Scaling

- The Canvas Scaler component is set to `Scale With Screen Size`.
- The reference resolution is set to 1080x1920.

### Android Support

- The project is configured for the Android platform.
- `Scripting Backend` is set to IL2CPP in `Player Settings`.
- ARM64 is added to `Target Architectures`.

### Input Settings

- The game supports touch screen inputs.
- UI elements are compatible with touch screens.



## License

This project is licensed under the MIT License. See the `LICENSE` file for details.
