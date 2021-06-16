using System.Collections.Generic;

namespace Monogame_Sokobon.LevelThings {
    public class LevelData {
        public LevelData(int width, int height, List<LayersDum> layers) {
            this.width = width;
            this.height = height;
            this.layers = layers;
        }
        public LevelData() { }
        //public Vector2 BoardSize {get; set;}
        public int width { get; set; }
        public int height { get; set; }
        public IList<LayersDum> layers { get; set; }
        //public ArrayList Goals {get; set;}
        //public ArrayList Boxes {get; set;}
        //public ArrayList Obsticals {get; set;}
        //public Vector2 PlayerPosition {get; set;}
    }
    public class LayersDum {
        public LayersDum(List<Entity> entities) {
            this.entities = entities;
        }
        public LayersDum() { }
        public IList<Entity> entities { get; set; }
    }
    public class Entity {
        public Entity(string name, int x, int y) {
            this.name = name;
            this.x = x;
            this.y = y;
        }
        public Entity() { }
        public string name { get; set; }
        public int x { get; set; }
        public int y { get; set; }
    }
}