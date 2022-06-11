using System.Collections.Generic;
using Unit04.Game.Casting;
using Unit04.Game.Services;


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
            List<Actor> artifacts = cast.GetActors("artifacts");
            List<int> deleteList = new List<int>();
            int delCount = 0;

            banner.SetText("");
            int maxX = videoService.GetWidth();
            int maxY = videoService.GetHeight();

            robot.MoveNext(maxX, maxY);



            //moves artifacts down
            Point addLoc = new Point(0,5);

            Point location = new Point(0,0);

            foreach (Actor actor in artifacts)
            {
                location = actor.GetPosition();
                location = location.Add(addLoc);
                actor.SetPosition(location);
            }

            //respawns artifacts at the top when they touch the bottom
            foreach (Actor actor in artifacts)
            {
                location = actor.GetPosition();
                if(location.GetY() >= 600)
                {
                    location = new Point(location.GetX(), 0);
                    actor.SetPosition(location);
                }
            }



            foreach (Actor actor in artifacts)
            {
                if (robot.GetPosition().Equals(actor.GetPosition()))
                {
                    Artifact artifact = (Artifact) actor;
                    pointTotal = pointTotal + 1;
                    banner.SetText($"Points: {pointTotal}");

                    //create a list that will be used to remove touched actors
                    deleteList.Add(delCount);
                }
                delCount = delCount + 1;
            }

            //deletes touched items
            foreach(int i in deleteList)
            {
                //cast.RemoveActor("artifacts", );
            }
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