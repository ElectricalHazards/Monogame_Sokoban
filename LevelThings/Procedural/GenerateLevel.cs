using System;
using System.Collections.Generic;
using System.Numerics;
namespace Monogame_Sokobon.LevelThings.Procedural
{
    public class GenerateLevel{
        public LevelData createLevel(){
            Random rand = new Random();
            int width = rand.Next(2,4);
            int height = rand.Next(2,4);
            return new LevelData(width*3, height*3, new List<LayersDum>(){new LayersDum(genBoard(width, height))});
        }
        

         //2 UP, RIGHT, UP&RIGHT
         //3 UP
         //4 LEFT
         //5 DOWN
         //6 RIGHT
         //7 DOWN, LEFT, DOWN&LEFT
         //8 UP, LEFT, UP&LEFT
         //9 DOWN, RIGHT, DOWN&RIGHT
        public int[,,] test = new int[,,]{
        {{0,0,0},
        {0,0,0},
        {0,0,0}},

        {{1,0,0},
        {0,0,0},
        {0,0,0}},

        {{1,1,2},
        {0,0,0},
        {0,0,0}},

        {{1,1,1},
        {0,0,0},
        {0,0,0}},

        {{1,1,1},
        {1,0,0},
        {1,0,0}},

        {{1,3,0},
        {4,0,0},
        {0,0,1}},

        {{1,0,0},
        {4,0,0},
        {1,0,0}},

        {{1,2,0},
        {4,0,0},
        {1,5,1}},

        {{1,3,1},
        {4,0,6},
        {1,5,1}},

        {{1,3,1},
        {1,0,6}, //Zero in the middle is special for some reason
        {1,1,1}},

        {{1,1,1},
        {4,0,6},
        {1,1,1}},

        {{0,0,6},
        {0,1,6},
        {0,0,0}},

        {{1,1,1},
        {1,1,1},
        {1,1,1}},

        {{1,1,1},
        {1,0,0},
        {7,0,0}},

        {{3,0,3},
        {1,0,1},
        {5,0,5}},

        {{1,1,1},
        {1,1,1},   
        {5,5,5}},

        {{1,1,1},
        {4,1,6},
        {5,5,0}}
        };
        


        public List<Entity> genBoard(int width, int height){
            List<Entity> list = new List<Entity>();
            Dictionary<Vector2,List<Vector2>> previouslyPlaced = new Dictionary<Vector2, List<Vector2>>();
            int [,][,] board = new int[height,width][,];
            for(int f = 0; f < board.GetLength(0); f++){
                for(int z = 0; z < board.GetLength(1); z++){
                    board[f,z] = createOne3x3();
                }
            }
         //2 UP, RIGHT, UP&RIGHT
         //3 UP
         //4 LEFT
         //5 DOWN
         //6 RIGHT
         //7 DOWN, LEFT, DOWN&LEFT
         //8 UP, LEFT, UP&LEFT
         //9 DOWN, RIGHT, DOWN&RIGHT
            for(int f = 0; f < board.GetLength(0); f++){
                for(int z = 0; z < board.GetLength(1); z++){
                    if(f==0&&z==0){
                        while(contains(board[f,z],new int[]{2,3,4,7,8})){
                            board[f,z] = createOne3x3();
                        }
                        continue;
                    }
                    for(int y = 0; y < board[f,z].GetLength(0); y++){
                        if(y!=0&&y!=2)
                            continue;
                        for(int x = 0; x < board[f,z].GetLength(1); x++){
                            if(x!=0&&x!=2)
                                continue;
                            while(f!=0&&doesNedSpaceRight(board[f-1,z]).Contains(invert(x))&&board[f,z][y,x]!=0){
                                board[f,z] = createOne3x3();
                            }
                        }
                    }
                }
            }  
            for(int f = 0; f < board.GetLength(0); f++){
                for(int z = 0; z < board.GetLength(1); z++){
                    for(int i = 0; i<board[f,z].GetLength(0); i++){
                        for(int y =0; y<board[f,z].GetLength(1); y++){
                            int x = board[f,z][y,i];
                            if(x == 1){
                                list.Add(new Entity("Wall",y+(f*3),i+(z*3)));
                            }
                            else if(x!=0){
                                list.Add(new Entity("Box",y+(f*3),i+(z*3)));
                            }
                        }
                    }   
                }
            }
            return list;
        }

        static List<int> doesNedSpaceRight(int[,] matrix){
            List<int> list = new List<int>();
            int it = 0;
            foreach(int i in matrix){
                if(equalsMany(i,new int[]{2,6,9})){
                    list.Add(it/3);
                }
                it++;
            }

            return list;
        }
        static List<int> doesNeedSPaceLeft(int[] matrix){
            List<int> list = new List<int>();


            return list;
        }
        static bool equalsMany(int item, int[] many){
            foreach(int i in many){
                if(i == item)
                    return true;
            }
            return false;
        }
        static bool contains(int[,] matrix, int[] items){
            bool flag = false;
            foreach(int i in matrix){
                foreach (int item in items)
                {
                    if(i == item){
                        flag = true;
                        break;
                    }
                }
                if(flag == true){
                    break;
                }
            }
            return flag;
        }
        public int[,] createOne3x3(){
            Random rand = new Random();
            int max = rand.Next(0,test.GetLength(0));
            int[,] lev = new int[3,3];
            
            for(int i = 0; i<3; i++){
                for(int y =0; y<3; y++){
                    lev[i,y] = test[max,y,i];
                }
            }
            //lev = RotateMatrixClockwise(lev, 3);
            for(int i = 0; i < rand.Next(0,4); i++){
                lev = RotateMatrixClockwise(lev, 3);
            }
            
            return lev;
            
        }


         //2 UP, RIGHT, UP&RIGHT -> 8 UP, LEFT, UP&LEFT
         //3 UP -> 4 LEFT
         //4 LEFT -> 5 DOWN
         //5 DOWN -> 6 RIGHT
         //6 RIGHT -> 3 UP
         //7 DOWN, LEFT, DOWN&LEFT -> 9 DOWN, LEFT, DOWN&LEFT
         //8 UP, LEFT, UP&LEFT -> 7 DOWN, RIGHT, DOWN&RIGHT
         //9 DOWN, RIGHT, DOWN&RIGHT -> 2 DOWN, LEFT, DOWN&LEFT
        static int[,] RotateMatrixClockwise(int[,] matrix, int n){
            int[,] ret = new int[n,n];
            int[,] motrix = rotateBoardersClockwise(matrix, n);
            for(int i = 0; i < n; i++){
                for(int j = 0; j < n; j++){
                    ret[i,j] = motrix[n-1-j,i];
                    ret[n-1-j,i] = motrix[n-1-i,n-1-j];
                    ret[n-1-i,n-1-j] = motrix[j,n-1-i];
                    ret[j,n-1-i] = motrix[i,j];
                }
            }

            return ret;
        }

        static int[,] rotateBoardersClockwise(int[,] matrix, int n){
            int[,] ret = (int[,])matrix.Clone();
            for(int i = 0; i < n; i++){
                for(int j = 0; j < n; j++){
                    if(matrix[i,j]==2){
                        ret[i,j] = 8;
                    }
                    else if(matrix[i,j]==3){
                        ret[i,j] = 4;
                    }
                    else if(matrix[i,j]==4){
                        ret[i,j] = 5;
                    }
                    else if(matrix[i,j]==5){
                        ret[i,j] = 6;
                    }
                    else if(matrix[i,j]==6){
                        ret[i,j] = 3;
                    }
                    else if(matrix[i,j]==7){
                        ret[i,j] = 9;
                    }
                    else if(matrix[i,j]==8){
                        ret[i,j] = 7;
                    }
                    else if(matrix[i,j]==9){
                        ret[i,j] = 2;
                    }
                    
                }
            }
            return ret;
        }

        static int invert(int num){
            switch(num){
                case 0:
                    return 2;
                case 1:
                    return 1;
                case 2:
                    return 0;
                default:
                    return -1;
            }
        }
        
    }
}