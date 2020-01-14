# Unity-Q-Learner
Q-Learning agent made with the Unity 3D engine

Overview:

GridSquareAgentSpawn spawns a GridNavigator instance on start

GridNavigator subscribes to periodic time events from Timer

On each time event, GridNavigator gets a GridState encoding from the Grid

GridState is passed to the Navigator's QLearningAgent instance

GridNavigator gets an action from the QLearningAgent based on the current State

Navigator performs action

Reward is received upon reaching an exit square
