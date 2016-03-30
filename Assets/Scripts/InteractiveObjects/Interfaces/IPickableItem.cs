namespace Assets.Scripts.InteractiveObjects.Interfaces
{
    public interface IPickableItem: IUsableObject
    {
        int LinkedItemX { get; set; }

        int LinkedItemY { get; set; }
    }
}
