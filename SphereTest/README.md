# Description

Modified Roll-a-ball tutorial, which you clear the targets via 
smash attack (space in air) or shooting (right-click).

Tutorial link: https://unity3d.com/pt/learn/tutorials/s/roll-ball-tutorial

# Features & Elements

- Player plane Movement (X e Z) with acceleration (gradual)

- Player vertical Movement (Y) with velocity (instant)

- Player jump enabled only when "grounded" (implemented with `Physics.Raycast`)

- Left mouse shoots a color changing ray on the targets

- Right mouse shoots a disabling ray on the targets

- Space in the air results in a "smash" that disables the targets

- Teleport to origin implemented in case the player falls off the plane

- Targets are spawned by a manager and are also periodically reactived after a while

- HUD: Remaining targets text + Win text + Restart button
