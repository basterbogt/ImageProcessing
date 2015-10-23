namespace ImageProcessing.Filtering
{
    /// <summary>
    /// Filters used to determine several values which we can use to determine if the given object is an object we are looking for!
    /// </summary>
    interface Filter
    {
        bool Calculate(Object potentialObject);
    }
}
