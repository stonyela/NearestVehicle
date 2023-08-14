using VehicleModels;

namespace CalculationsLibrary
{
    public class VehicleFinder
    {
        private readonly Vehicle[] _vehicles;

        public VehicleFinder(Vehicle[] vehicles)
        {
            _vehicles = vehicles;
        }

        public Vehicle Nearest(float lat, float lon)
        {
            Vehicle nearestVehicle = null;
            var smallestDistance = float.MaxValue;

            foreach (var vehicle in _vehicles)
            {
                vehicle.CalculateDistanceBetweenTwoCoordinates(lat, lon);
                if (!(vehicle.Distance < smallestDistance)) continue;
                nearestVehicle = vehicle;
                smallestDistance = vehicle.Distance;
            }

            return nearestVehicle;
        }

        public static void MergeSort(Vehicle[] vehicles, int low, int high, int depth)
        {
            if (low >= high) return;
            var mid = (low + high) / 2;
            MergeSort(vehicles, low, mid, depth + 1);
            MergeSort(vehicles, mid + 1, high, depth + 1);
            Merge(vehicles, low, mid, high, depth % 2 == 0);
        }

        public static void Merge(Vehicle[] vehicles, int low, int mid, int high, bool sortByLongitude)
        {
            var n1 = mid - low + 1;
            var n2 = high - mid;

            var L = new Vehicle[n1];
            var R = new Vehicle[n2];

            for (var x = 0; x < n1; x++)
                L[x] = vehicles[low + x];

            for (var y = 0; y < n2; y++)
                R[y] = vehicles[mid + 1 + y];

            int k = low, i = 0, j = 0;

            while (i < n1 && j < n2)
            {
                if (sortByLongitude ? L[i].Longitude <= R[j].Longitude : L[i].Latitude <= R[j].Latitude)
                {
                    vehicles[k] = L[i];
                    i++;
                }
                else
                {
                    vehicles[k] = R[j];
                    j++;
                }
                k++;
            }

            while (i < n1)
            {
                vehicles[k] = L[i];
                i++;
                k++;
            }

            while (j < n2)
            {
                vehicles[k] = R[j];
                j++;
                k++;
            }
        }
    }
}