# Unity_Node_Assignment

![License](https://img.shields.io/badge/License-MIT-blue.svg)

## About

Node_Server for Unity is a project that serves as an external server to store high scores and manage user accounts for a game built in Unity. The server is implemented in Node.js and utilizes MongoDB to store the data. The Unity client, written in C#, interacts with the server to handle user registration and high score management.

## Features

- User registration
- High score storage and retrieval
- Seamless integration with Unity game client

## Technologies Used

- Node.js
- Express.js
- MongoDB
- Unity
- C#

## Getting Started

To run this project locally, follow the instructions below:

### Prerequisites

- Node.js (v18.12.1)
- MongoDB 
- Unity Game Engine (2021.3.20f1)

### Installation

1. Clone the repository:
   Clone the repositry by typing the command below in a terminal
   ```bash
   git clone https://github.com/KABOOTAR-1/Unity_Node_Assignment.git

2. Install Dependencies:
     open the Unity_Node_Assignment folder and open the terminal and execute the below commands
      ```bash
      cd NodeJs_Server
      npm install

4. Configuration:
   if env file is not there create a .env file in the server directory with the following content:
    
     ```bash
       PORT=4000
       MONGO_URI=mongodb+srv://xyz1233214321:godplan@cluster0.j5xz0ab.mongodb.net/

 5. Run the Server:
    Start the NodeJs Server by executing below commands
   
      ```bash
        cd NodeJs_Server
        nodemon app.js
##  Open Unity: 
 1. Open UnityHub
 -If UnityHub is not present download UnityHub
 -Install Unity Version 2021.3.20f1
 2. Open the Game
 - Open the game through UnityHub by clicking Open project and Add project from Disk
 - Find the Client_Unity Folder in the Repo and select it to open the project in Unity


## Game Controls

1. Game Start
- At the start of the game a panel with top 5 highscores will be shown along with the username
- Click Next to go to User_Registartion
- If the User is new type the username in New_username input
- If the User is registreted before enter teh username in exsisting username input
- Click Submit below the respective InputField you typed the username

2. Movement
- Use W,A,S,D or the arrows key to move the player up,down,left or right

3. Objective
- Collect the diamonds to increase score and avoid the bombs
- If a bomb is touced the game is over

4.Game Over
- Game Over Screen will load
- Press Play Again to play the game or Exit to exit the game



 
