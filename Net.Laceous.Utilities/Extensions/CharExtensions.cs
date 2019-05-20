namespace Net.Laceous.Utilities.Extensions
{
    /// <summary>
    /// Adds extension methods to chars
    /// </summary>
    public static class CharExtensions
    {
        /// <summary>
        /// Checks if the char is a hex char (0-9, A-F, a-f)
        /// </summary>
        /// <param name="c">Char to check</param>
        /// <returns>True if hex, false if not hex</returns>
        public static bool IsHex(this char c)
        {
            return (c >= '0' && c <= '9') || (c >= 'A' && c <= 'F') || (c >= 'a' && c <= 'f');
        }
    }
}
