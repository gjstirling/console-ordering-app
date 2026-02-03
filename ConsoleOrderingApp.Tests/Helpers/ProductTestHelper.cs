using ConsoleOrderingApp.Data;
public static class TestHelpers
{
    public static IProductRepository CreateRepository(bool useEF = false)
    {
        // Integration testing
        if (useEF)
        {
            // return DBContext here
        }

        // Unit testing
        return new InMemoryProductRepository();
    }
}
