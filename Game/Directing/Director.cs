using System.Collections.Generic;
using Unit04.Game.Casting;
using Unit04.Game.Services;

using System;
using System.IO;
using System.Linq;
using Unit04.Game.Directing;

namespace Unit04.Game.Directing
{
    /// <summary>
    /// <para>A person who directs the game.</para>
    /// <para>
    /// The responsibility of a Director is to control the sequence of play.
    /// </para>
    /// </summary>
    public class Director
    {
        private KeyboardService keyboardService = null;
        private VideoService videoService = null;
        public int pointTotal = 0;

        /// <summary>
        /// Constructs a new instance of Director using the given KeyboardService and VideoService.
        /// </summary>
        /// <param name="keyboardService">The given KeyboardService.</param>
        /// <param name="videoService">The given VideoService.</param>
        public Director(KeyboardService keyboardService, VideoService videoService)
        {
            this.keyboardService = keyboardService;
            this.videoService = videoService;
        }

        /// <summary>
        /// Starts the game by running the main game loop for the given cast.
        /// </summary>
        /// <param name="cast">The given cast.</param>
        public void StartGame(Cast cast)
        {
            videoService.OpenWindow();
            while (videoService.IsWindowOpen())
            {
                GetInputs(cast);
                DoUpdates(cast);
                DoOutputs(cast);
            }
            videoService.CloseWindow();
        }

        /// <summary>
        /// Gets directional input from the keyboard and applies it to the robot.
        /// </summary>
        /// <param name="cast">The given cast.</param>
        private void GetInputs(Cast cast)
        {
            Actor robot = cast.GetFirstActor("robot");
            Point velocity = keyboardService.GetDirection();
            robot.SetVelocity(velocity);     
        }

        /// <summary>
        /// Updates the robot's position and resolves any collisions with artifacts.
        /// </summary>
        /// <param name="cast">The given cast.</param>
        private void DoUpdates(Cast cast)
        {
            Actor banner = cast.GetFirstActor("banner");
            Actor robot = cast.GetFirstActor("robot");
            List<Actor> FallingObjects = cast.GetActors("FallingObjects");
            List<int> deleteList = new List<int>();

            banner.SetText("");
            int maxX = videoService.GetWidth();
            int maxY = videoService.GetHeight();

            robot.MoveNext(maxX, maxY);



            //moves artifacts down
            Random rnd = new Random();

            int randomInt = rnd.Next(0,60);
            randomInt = randomInt * 15;

            Point addLoc = new Point(0,5);

            Point location = new Point(0,0);

            foreach (Actor actor in FallingObjects)
            {
                location = actor.GetPosition();
                location = location.Add(addLoc);
                actor.SetPosition(location);
            }

            //respawns artifacts at the top when they touch the bottom
            foreach (Actor actor in FallingObjects)
            {
                location = actor.GetPosition();
                if(location.GetY() >= 600)
                {
                    location = new Point(randomInt, 0);
                    actor.SetPosition(location);
                }
            }



            foreach (Actor actor in FallingObjects)
            {
                if (robot.GetPosition().Equals(actor.GetPosition()))
                {
                    FallingObjects artifact = (FallingObjects) actor;
                    pointTotal = pointTotal + 1;

                    location = new Point(randomInt, 0);
                    actor.SetPosition(location);
                }
                
            }
            banner.SetText($"Points: {pointTotal}");
            

        }

        /// <summary>
        /// Draws the actors on the screen.
        /// </summary>
        /// <param name="cast">The given cast.</param>
        public void DoOutputs(Cast cast)
        {
            List<Actor> actors = cast.GetAllActors();
            videoService.ClearBuffer();
            videoService.DrawActors(actors);
            videoService.FlushBuffer();
        }

    }
}