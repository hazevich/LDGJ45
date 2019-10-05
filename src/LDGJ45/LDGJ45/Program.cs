namespace LDGJ45
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            using (var game = new Ldgj45Game())
            {
                game.Run();
            }
        }
    }
}