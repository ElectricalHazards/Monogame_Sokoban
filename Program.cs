using System;

namespace Monogame_Sokobon {
    public static class Program {
        [STAThread]
        static void Main() {
            using (var game = new SokobonGame())
                game.Run();
        }
    }
}
