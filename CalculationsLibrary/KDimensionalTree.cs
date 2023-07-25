using VehicleModels;

namespace CalculationsLibrary
{
    public class KDimensionalTree
    {
        private readonly KDimensionalTreeNode _root;
        private readonly bool _useLongitude;

        public KDimensionalTree(Vehicle[] vehicles, bool useLongitude = true)
        {
            _useLongitude = useLongitude;
            _root = BuildTree(vehicles);
        }

        private KDimensionalTreeNode BuildTree(Vehicle[] vehicles)
        {
            if (!vehicles.Any())
            {
                return null;
            }

            Array.Sort(vehicles, _useLongitude
                ? (Comparison<Vehicle>)((v1, v2) => v1.Longitude.CompareTo(v2.Longitude))
                : (v1, v2) => v1.Latitude.CompareTo(v2.Latitude));

            var mid = vehicles.Length / 2;

            return new KDimensionalTreeNode
            {
                Vehicle = vehicles[mid],
                Left = BuildTree(vehicles[..mid]),
                Right = BuildTree(vehicles[(mid + 1)..])
            };
        }

        public Vehicle Nearest(float lat, float lon)
        {
            return Nearest(_root, lat, lon);
        }

        private Vehicle Nearest(KDimensionalTreeNode node, float lat, float lon)
        {
            if (node == null) return null;

            node.Vehicle.CalculateDistanceBetweenTwoCoordinates(lat, lon);

            KDimensionalTreeNode nearNode, farNode;
            if ((_useLongitude && node.Vehicle.Longitude > lon) || (!_useLongitude && node.Vehicle.Latitude > lat))
            {
                nearNode = node.Left;
                farNode = node.Right;
            }
            else
            {
                nearNode = node.Right;
                farNode = node.Left;
            }

            var best = Nearest(nearNode, lat, lon);
            if (best == null || Math.Pow(best.Distance, 2) > Math.Pow(node.Vehicle.Distance, 2))
                best = node.Vehicle;

            var bestDistanceSquared = Math.Pow(best.Distance, 2);

            if (_useLongitude)
            {
                if (!(Math.Pow(node.Vehicle.Longitude - lon, 2) < bestDistanceSquared)) return best;
                var tempBest = best;  // create a local copy of best
                var farVehicle = Nearest(farNode, lat, lon);

                if (farVehicle != null && Math.Pow(farVehicle.Distance, 2) < Math.Pow(tempBest.Distance, 2))
                    best = farVehicle;
            }
            else
            {
                if (!(Math.Pow(node.Vehicle.Latitude - lat, 2) < bestDistanceSquared)) return best;
                var tempBest = best;  // create a local copy of best
                var farVehicle = Nearest(farNode, lat, lon);

                if (farVehicle != null && Math.Pow(farVehicle.Distance, 2) < Math.Pow(tempBest.Distance, 2))
                    best = farVehicle;
            }

            return best;
        }
    }
}