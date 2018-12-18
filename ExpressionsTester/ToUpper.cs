namespace ExpressionsTester
{
    /// <summary>
    /// A class wrapper for <see cref="string.ToUpper()"/>
    /// </summary>
    public class ToUpper
    {
        /// <summary>
        /// Wraps <see cref="string.ToUpper()"/>.
        /// </summary>
        /// <param name="input">The string to uppercase</param>
        /// <returns>The uppercase string</returns>
        public static string Execute(string input)
        {
            return input.ToUpper();
        }
    }
}