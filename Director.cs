using System;
namespace Greed
{
    class Director
    {
        public void StartGame()
        {
            // Uses the Keyboard.IsKeyDown to determine if a key is down.
            // e is an instance of KeyEventArgs.
            if (Keyboard.IsKeyDown(Key.Return))
            {
                btnIsDown.Background = Brushes.Red;
            }
            else
            {
                btnIsDown.Background = Brushes.AliceBlue;
            }
        }
    }
}