namespace CurrencyConverter.DataLayer.Model
{
    public interface IDeepCloneable<out T>
    {
        T DeepClone(int innerDepth);
    }
}