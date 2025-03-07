public class IconService : IIconService
{
    private readonly string _iconDirectory = "Data/Icons";

    public byte[]? GetIcon(string iconName)
    {
        var filePath = Path.Combine(_iconDirectory, iconName);
        return File.Exists(filePath) ? File.ReadAllBytes(filePath) : null;
    }
}