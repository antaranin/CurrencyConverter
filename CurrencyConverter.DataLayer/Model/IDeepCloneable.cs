namespace CurrencyConverter.DataLayer.Model
{
    public interface IDeepCloneable<out T>
    {
        /// <summary>
        /// Creates a deep clone of the object and all inner IDeepCloneable up
        /// to the provided depth.
        /// This should apply to all inner objects that implement IDeepCloneable
        /// It is undefined what will happen to the complex objects that
        /// do not implement IDeepCloneable interface
        /// </summary>
        /// <param name="innerDepth">
        /// The depth of complex objects that will be cloned.
        /// All objects the provided depth should be set as null.
        /// Provide 0 to not copy any inner objects.
        /// Provide negative number to copy all of the inner objects.
        /// </param>
        /// <returns>A deep clone of the provided object.</returns>
        T DeepClone(int innerDepth);
    }
}