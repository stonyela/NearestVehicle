using VehicleModels;

namespace CalculationsLibrary;

public class KDimensionalTreeNode
{
    public Vehicle? Vehicle { get; set; }
    public KDimensionalTreeNode? Left { get; set; }
    public KDimensionalTreeNode? Right { get; set; }
}