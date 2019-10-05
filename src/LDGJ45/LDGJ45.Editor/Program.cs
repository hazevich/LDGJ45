using System;

namespace LDGJ45.Editor
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var game = new EditorGame())
                game.Run();
        }
    }
}
