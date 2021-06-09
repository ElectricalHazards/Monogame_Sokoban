using System;
using System.Numerics;
using System.Collections.Generic;
using System.Text.Json;
using Monogame_Sokobon.LevelThings;

namespace Monogame_Sokobon.TerminalSokobon
{
    class Soko
    {
        public static void playSokobon(int x, int y, int boxes){
            Board.GenerateBoard(x,y,boxes);
            Board.update();
        }
        public static void loadSokoban(LevelData level){
            Vector2 playerPos = new Vector2();
            List<Vector2> Goals = new List<Vector2>();
            List<Vector2> Walls = new List<Vector2>();
            List<Vector2> Boxes = new List<Vector2>();
            foreach (LayersDum layer in level.layers)
            {
                foreach (Entity Entity in layer.entities)
                {
                    if(Entity.name.Equals("Goal")){
                        Goals.Add(new Vector2(Entity.y+1, Entity.x+1));
                    }
                    else if(Entity.name.Equals("Wall")){
                        Walls.Add(new Vector2(Entity.y+1, Entity.x+1));   
                    }
                    else if(Entity.name.Equals("Box")){
                        Boxes.Add(new Vector2(Entity.y+1, Entity.x+1));
                    }
                    else if(Entity.name.Equals("Player")){
                        playerPos = new Vector2(Entity.y+1, Entity.x+1);
                    }
                }
            }
            Board.loadBoard(level.width, level.height, Goals, Boxes, Walls, playerPos);
            Board.update();
        }
    }
}
