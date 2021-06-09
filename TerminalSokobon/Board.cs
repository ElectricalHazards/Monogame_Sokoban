using System;
using System.Numerics;
using System.Collections;
using System.Collections.Generic;

namespace Monogame_Sokobon.TerminalSokobon{

    public class Board{

        public static int[,] board;
        public static Player Player;
        public static ArrayList boardStates = new ArrayList();
        public static ArrayList Goals = new ArrayList();
        public static ArrayList Boxes = new ArrayList();
        public static ArrayList Obsticals = new ArrayList();
        public static bool IsRunning = true;
        public static void update(){
            redraw();
            checkWin();
            for(int row = 0; row < board.GetLength(0); row ++){
                for(int col = 0; col < board.GetLength(1); col++){
                   // Console.Write(board[row,col]);
                }
                //Console.WriteLine();
            }
        }
        public static void GenerateBoard(int width, int height, int boxes){
            board = new int[width+2,height+2];
            Boxes.Clear();
            boardStates.Clear();
            Goals.Clear();
            Obsticals.Clear();
            Player = new Player();
            makeEdges();
            placeGoal(boxes);
            placeObsticals(boxes*2);
            placeBoxes(boxes);
            
            redraw();
        }

        public static void loadBoard(int width, int height, List<Vector2> goals, List<Vector2> boxes, List<Vector2> walls, Vector2 playerPos){
            board = new int[width+2,height+2];
            Boxes.Clear();
            boardStates.Clear();
            Goals.Clear();
            Obsticals.Clear();
            Player = new Player(playerPos);
            foreach (var goal in goals)
            {
                Goals.Add(goal);
            }
            foreach (var box in boxes)
            {
                Boxes.Add(box);
            }
            foreach (var wall in walls)
            {
                Obsticals.Add(wall);
            }
            makeEdges();
            redraw();
        }
        public static void Undo(){
            if(boardStates.Count == 0)
                return;
            BoardState last = (BoardState)boardStates[boardStates.Count-1];
            boardStates.RemoveAt(boardStates.Count-1);
            board = last.Board;
            Player.position = last.Player;
            Boxes = last.Boxes;
            Goals = last.Goals;
            redraw();

        }        

        private static void makeEdges(){
            for(int row = 0; row < board.GetLength(0); row ++){
                for(int col = 0; col < board.GetLength(1); col++){
                    if((row == 0 || col == 0)||(row == board.GetLength(0)-1 || col == board.GetLength(1)-1)){
                        board[row,col] = 1;
                    }
                }
            }
        }
        
        public static void placeObsticals(int count){
            for(int i = 0; i < count; i++){
                Vector2 obst = new Vector2();
                Random rand = new Random();
                if(i == 0){
                    while(obst.X<1||obst.X>board.GetLength(0)-2||obst.Y<1||obst.Y>board.GetLength(1)-2||obst.X==Player.position.X||obst.Y==Player.position.Y||board[(int)obst.X,(int)obst.Y]==3||board[(int)obst.X,(int)obst.Y]==6){
                     obst.X = rand.Next(board.GetLength(0));
                     obst.Y = rand.Next(board.GetLength(1));
                    }
                }
                else{
                    while(obst.X<1||obst.X>board.GetLength(0)-2||obst.Y<1||obst.Y>board.GetLength(1)-2||obst.X==Player.position.X||obst.Y==Player.position.Y||board[(int)obst.X,(int)obst.Y]==3||(board[(int)obst.X,(int)obst.Y-1]!=6&&board[(int)obst.X+1,(int)obst.Y]!=6&&board[(int)obst.X,(int)obst.Y+1]!=6&&board[(int)obst.X-1,(int)obst.Y]!=6)||board[(int)obst.X,(int)obst.Y]==6){
                     obst.X = rand.Next(board.GetLength(0));
                     obst.Y = rand.Next(board.GetLength(1));
                    }

                }
                Board.board[(int)obst.X,(int)obst.Y] = 6;
                Obsticals.Add(obst);
            }
            foreach (Vector2 item in Obsticals)
            {
                Board.board[(int)item.X,(int)item.Y] = 6;
            }
        }

        public static void placeBoxes(int boxes){
            for(int i = 0; i < boxes; i++){
                Vector2 box = new Vector2();
                Random rand = new Random();  
                while(box.X<=1||box.X>=board.GetLength(0)-2||box.Y<=1||box.Y>=board.GetLength(1)-2||box.X==Player.position.X||box.Y==Player.position.Y||board[(int)box.X,(int)box.Y]==3||board[(int)box.X,(int)box.Y]==4||board[(int)box.X,(int)box.Y]==6){
                    box.X = rand.Next(board.GetLength(0));
                    box.Y = rand.Next(board.GetLength(1));
                }
                Board.board[(int)box.X,(int)box.Y] = 4;
                Boxes.Add(box);
            }
            foreach (Vector2 item in Boxes)
            {
                Board.board[(int)item.X,(int)item.Y] = 4;
            }
        }

        private static void placeGoal(int boxes){
            for(int i = 0; i < boxes; i++){
                Vector2 goal = new Vector2();
                Random rand = new Random();
                if(i == 0){
                    while(goal.X<1||goal.X>board.GetLength(0)-2||goal.Y<1||goal.Y>board.GetLength(1)-2||goal.X==Player.position.X||goal.Y==Player.position.Y||board[(int)goal.X,(int)goal.Y]==3||board[(int)goal.X,(int)goal.Y]==6){
                     goal.X = rand.Next(board.GetLength(0));
                     goal.Y = rand.Next(board.GetLength(1));
                    }
                }
                else{
                    while(goal.X<1||goal.X>board.GetLength(0)-2||goal.Y<1||goal.Y>board.GetLength(1)-2||goal.X==Player.position.X||goal.Y==Player.position.Y||board[(int)goal.X,(int)goal.Y]==3||(board[(int)goal.X,(int)goal.Y-1]!=3&&board[(int)goal.X+1,(int)goal.Y]!=3&&board[(int)goal.X,(int)goal.Y+1]!=3&&board[(int)goal.X-1,(int)goal.Y]!=3)||board[(int)goal.X,(int)goal.Y]==6){
                     goal.X = rand.Next(board.GetLength(0));
                     goal.Y = rand.Next(board.GetLength(1));
                    }

                }
                Board.board[(int)goal.X,(int)goal.Y] = 3;
                Goals.Add(goal);
            }
            foreach (Vector2 item in Goals)
            {
                Board.board[(int)item.X,(int)item.Y] = 3;
            }

        }

        
        public static void redraw(){
         for(int row = 0; row < board.GetLength(0); row ++){
                for(int col = 0; col < board.GetLength(1); col++){
                    board[row,col] = 0;
                    if((row == 0 || col == 0)||(row == board.GetLength(0)-1 || col == board.GetLength(1)-1)){
                        board[row,col] = 1;
                    }
                    foreach (Vector2 box in Boxes)
                    {
                        if(row == box.X && col == box.Y){
                            board[row,col] = 4;
                        }
                    }
                    foreach (Vector2 goal in Goals)
                    {
                        if(board[row,col]==4 && row == goal.X && col == goal.Y){
                            board[row,col] = 5;
                        }
                        else if(row == goal.X && col == goal.Y){
                            board[row,col] = 3;
                        }
                    }
                    foreach (Vector2 goal in Obsticals)
                    {
                        if(row == goal.X && col == goal.Y){
                            board[row,col] = 6;
                        }
                    }
                    if(row == Player.position.X && col == Player.position.Y){
                        board[row,col] = 2;
                    }
                }
            }   
        }
        public static void checkWin(){
            bool flag = true;
            foreach (Vector2 goal in Goals)
            {
                if(board[(int)goal.X,(int)goal.Y]!=5)
                    flag = false;
            }
            IsRunning = !flag;
        }
    }

}