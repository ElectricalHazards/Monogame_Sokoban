using System;
using System.Numerics;
using System.Collections;

namespace Monogame_Sokobon.TerminalSokobon{
    public class Player{
        private static Random rand = new Random();
        public Vector2 position;
        
        public Player(){
            position = new Vector2(rand.Next(Board.board.GetLength(0)-3)+1,rand.Next(Board.board.GetLength(0)-3)+1);
        }
        public Player(Vector2 pos){
            position.X = pos.X;
            position.Y = pos.Y;
        }
                                         //   0
        public void Move(int Direction){ // 1    34
            
            switch(Direction){            //  2
                case 2:
                    if(Board.board[(int)position.X+1,(int)position.Y]==0||Board.board[(int)position.X+1,(int)position.Y]==3){
                        Board.boardStates.Add(new BoardState(Board.Player.position,(int[,])Board.board.Clone(),(ArrayList)Board.Goals.Clone(),(ArrayList)Board.Boxes.Clone(),(ArrayList)Board.Obsticals.Clone()));
                        position.X++;
                    }
                    else if(Board.board[(int)position.X+1,(int)position.Y]==4||Board.board[(int)position.X+1,(int)position.Y]==5){
                        if(Board.board[(int)position.X+2,(int)position.Y]==0||Board.board[(int)position.X+2,(int)position.Y]==3){
                            Board.boardStates.Add(new BoardState(Board.Player.position,(int[,])Board.board.Clone(),(ArrayList)Board.Goals.Clone(),(ArrayList)Board.Boxes.Clone(),(ArrayList)Board.Obsticals.Clone()));
                           position.X++;
                           //Board.box.X++;
                           Board.Boxes.Remove(new Vector2(position.X,position.Y));
                            Board.Boxes.Add(new Vector2(position.X+1,position.Y));
                        }
                    }
                    break;
                case 3:
                    if(Board.board[(int)position.X,(int)position.Y+1]==0||Board.board[(int)position.X,(int)position.Y+1]==3){
                        Board.boardStates.Add(new BoardState(Board.Player.position,(int[,])Board.board.Clone(),(ArrayList)Board.Goals.Clone(),(ArrayList)Board.Boxes.Clone(),(ArrayList)Board.Obsticals.Clone()));
                        position.Y++;
                    }
                    else if(Board.board[(int)position.X,(int)position.Y+1]==4||Board.board[(int)position.X,(int)position.Y+1]==5){
                        if(Board.board[(int)position.X,(int)position.Y+2]==0||Board.board[(int)position.X,(int)position.Y+2]==3){
                            Board.boardStates.Add(new BoardState(Board.Player.position,(int[,])Board.board.Clone(),(ArrayList)Board.Goals.Clone(),(ArrayList)Board.Boxes.Clone(),(ArrayList)Board.Obsticals.Clone()));
                            position.Y++;
                            Board.Boxes.Remove(new Vector2(position.X,position.Y));
                            Board.Boxes.Add(new Vector2(position.X,position.Y+1));
                        }                
                    }
                        break;
                case 0:
                    if(Board.board[(int)position.X-1,(int)position.Y]==0||Board.board[(int)position.X-1,(int)position.Y]==3){
                        Board.boardStates.Add(new BoardState(Board.Player.position,(int[,])Board.board.Clone(),(ArrayList)Board.Goals.Clone(),(ArrayList)Board.Boxes.Clone(),(ArrayList)Board.Obsticals.Clone()));
                        position.X--;
                    }
                    else if(Board.board[(int)position.X-1,(int)position.Y]==4||Board.board[(int)position.X-1,(int)position.Y]==5){
                        if(Board.board[(int)position.X-2,(int)position.Y]==0||Board.board[(int)position.X-2,(int)position.Y]==3){
                            Board.boardStates.Add(new BoardState(Board.Player.position,(int[,])Board.board.Clone(),(ArrayList)Board.Goals.Clone(),(ArrayList)Board.Boxes.Clone(),(ArrayList)Board.Obsticals.Clone()));
                            position.X--;
                            //Board.box.X--;
                            Board.Boxes.Remove(new Vector2(position.X,position.Y));
                            Board.Boxes.Add(new Vector2(position.X-1,position.Y));
                        }                       
                    }
                    break;
                case 1:
                    if(Board.board[(int)position.X,(int)position.Y-1]==0||Board.board[(int)position.X,(int)position.Y-1]==3){
                        Board.boardStates.Add(new BoardState(Board.Player.position,(int[,])Board.board.Clone(),(ArrayList)Board.Goals.Clone(),(ArrayList)Board.Boxes.Clone(),(ArrayList)Board.Obsticals.Clone()));
                        position.Y--;
                    }
                    else if(Board.board[(int)position.X,(int)position.Y-1]==4||Board.board[(int)position.X,(int)position.Y-1]==5){
                        if(Board.board[(int)position.X,(int)position.Y-2]==0||Board.board[(int)position.X,(int)position.Y-2]==3){
                            Board.boardStates.Add(new BoardState(Board.Player.position,(int[,])Board.board.Clone(),(ArrayList)Board.Goals.Clone(),(ArrayList)Board.Boxes.Clone(),(ArrayList)Board.Obsticals.Clone()));
                            position.Y--;
                            //Board.box.Y--;
                            Board.Boxes.Remove(new Vector2(position.X,position.Y));
                            Board.Boxes.Add(new Vector2(position.X,position.Y-1));
                        }
                    }
                    break;

            }
        }
    }
}