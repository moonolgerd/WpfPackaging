namespace WpfPackaging
{
    public interface IDoStuff<TIn, TOut>
    where TIn : notnull
    where TOut : notnull
    {
        TOut DoStuff(TIn input);
    }
}
