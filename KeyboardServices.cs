using Raylib_cs;
namespace Greed
{
    public static class KeyboardService
    {
        public KeyboardService()
        {
            
        }
        public void Move()
        {
            int dx = 250;

            if (Raylib.IsKeyDown(KeyboardKey.KEY_LEFT))
            {
                dx += -1;
            }
            if (Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT))
            {
                dx += 1;
            }

        }
    }
}