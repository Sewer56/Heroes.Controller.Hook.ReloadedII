<div align="center">
	<h1>Heroes Essentials: Controller Hook</h1>
	<img src="https://i.imgur.com/BjPn7rU.png" width="150" align="center" />
	<br/> <br/>
	<strong>Same Game, New Controllers</strong>
    <p>When the stock mouse support annoys you so much<br/>
    when hacking that you make an entire input mod to avoid it.</p>
<b>Id: sonicheroes.controller.hook</b>
</div>

# Table of Contents
- [About This Project](#about-this-project)
  - [Inside This Repository](#inside-this-repository)
      - [Mods](#mods)
      - [Controllers (For Programmers)](#controllers-for-programmers)
  - [New Features](#new-features)
  - [How to Use](#how-to-use)

# About This Project

This project is a set of mods for [Reloaded II](https://github.com/Reloaded-Project/Reloaded-II) Mod Loader that provide support for sending and post processing of inputs that get sent to the game.

## Inside This Repository

#### Mods
Hook: *Provides support for sending and post processing inputs to the game.*

PostProcess: *Provides support for common input post processing effects such as deadzones, swapping triggers.* (Requires Hook)

XInput: *Provides support for XInput Controllers to be used by the game.* (Requires Hook)

#### Controllers (For Programmers)
Heroes.Controller.Hook.Interfaces: *Provides support for other mods to set inputs, do input post processing, receive events on input.*

## New Features
- Support rotations with Triggers.
	- Normally the game doesn't read the trigger values from any controllers, outright ignoring them in the PC version.
	- That said, the Engine underneath still supports rotation with triggers, and thus this mod allows you to use the triggers once again.

- Support 3P, 4P inputs.
	- Of course the game isn't 4 player, although it is believed the game may have had 4 player support prior to the E3 beta.
	- Normally the game only reads inputs for the first two controllers, but the last two controllers are still used in some debug menus.
	- One of these debug menus is the "Easy Menu/EASY_SELECT" menu.

## How to Use
**A.** Install Reloaded Mods as usual. (Extract to mod directory)

**B.** Enable all mods and run the game. (This auto-generates the config files)

**C.** Adjust controller configurations. (if necessary)

Controller configurations can be found in each of the respective mod folders.

`PostProcess` will allow you to adjust individual deadzones for each axis as well as a few other features.
`XInput` will allow you to rebind all of the buttons for your Xbox or Xbox-like controller.
