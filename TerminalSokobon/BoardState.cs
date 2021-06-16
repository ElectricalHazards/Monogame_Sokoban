using System.Collections;
using System.Numerics;

namespace Monogame_Sokobon.TerminalSokobon {
    public class BoardState {
        public Vector2 Player;
        public int[,] Board;
        public ArrayList Goals;
        public ArrayList Boxes;
        public ArrayList Obsticals;

        public BoardState(Vector2 player, int[,] board, ArrayList goals, ArrayList boxes, ArrayList obsticals) {
            Player = player;
            Board = board;
            Goals = goals;
            Boxes = boxes;
            Obsticals = obsticals;
        }
    }
}