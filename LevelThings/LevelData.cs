using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Monogame_Sokobon.LevelThings {
    public class LevelData {
        public LevelData(int width, int height, List<LayersDum> layers) {
            this.width = width;
            this.height = height;
            this.layers = layers;
        }
        public LevelData() { }
        //public Vector2 BoardSize {get; set;}
        [Required]
        public int width { get; set; }
        [Required]
        public int height { get; set; }
        [Required]
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
        [Required]
        public IList<Entity> entities { get; set; }
    }
    public class Entity {
        public Entity(string name, int x, int y) {
            this.name = name;
            this.x = x;
            this.y = y;
        }
        public Entity() { }
        [Required]
        public string name { get; set; }
        [Required]
        public int x { get; set; }
        [Required]
        public int y { get; set; }
    }
}