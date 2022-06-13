namespace Unit04.Game.Casting
{
    /// <summary>
    /// <para>An item of cultural or historical interest.</para>
    /// <para>
    /// 
    /// </para>
    /// </summary>
    public class FallingObjects : Actor
    {
        private int points; 

        /// <summary>
        /// Constructs a new instance of an Artifact.
        /// </summary>
        public FallingObjects()
        {
        }

        /// <summary>
        /// Gets the artifact's message.
        /// </summary>
        /// <returns>The message.</returns>
        public int GetPoints()
        {
            return points; 
        }

        /// <summary>
        /// Sets the artifact's message to the given value.
        /// </summary>
        /// <param name="message">The given message.</param>
        public void SetPoints(int points)
        {
            this.points = points;
        }
        
       
        
    }
}