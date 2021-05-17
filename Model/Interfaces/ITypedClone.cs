namespace PaceMe.BlazorApp.Model
{
    //
    // Summary:
    //     Supports cloning, which creates a new instance of a class with the same value
    //     as an existing instance.
    public interface ITypedClone<T>
    {
        //
        // Summary:
        //     Creates a new object that is a copy of the current instance.
        //
        // Returns:
        //     A new object that is a copy of this instance.
        T Clone();
    }
}