using System.Text;
using VehicleModels;

namespace CalculationsLibrary;

public static class VehicleFileReader
{
    public static Vehicle[] ReadVehicles(string filePath)
    {
        const int numberOfRecords = 2000001;
        var vehicles = new Vehicle[numberOfRecords];

        using (var fs = File.OpenRead(filePath))
        using (var reader = new BinaryReader(fs))
        {
            var index = default(int);

            while (reader.BaseStream.Position != reader.BaseStream.Length)
            {
                var vehicleId = reader.ReadInt32();
                var vehicleRegistration = ReadAllCharactersUntilNullCharacter(reader);
                var latitude = reader.ReadSingle();
                var longitude = reader.ReadSingle();
                var recordedTimeUtc = reader.ReadUInt64();

                vehicles[index] = new Vehicle(vehicleId, vehicleRegistration, latitude, longitude, recordedTimeUtc);
                index++;
            }
        }

        VehicleFinder.MergeSort(vehicles, 0, vehicles.Length - 1, 0);

        return vehicles.ToArray();
    }

    private static string ReadAllCharactersUntilNullCharacter(BinaryReader reader)
    {
        var sb = new StringBuilder();
        char currentChar;
        while ((currentChar = reader.ReadChar()) != '\0')
        {
            sb.Append(currentChar);
        }
        return sb.ToString();
    }

    private static void SortVehicles(Vehicle[] vehicles, int depth)
    {
        if (vehicles.Length <= 1)
            return;

        Array.Sort(vehicles, (v1, v2) => (depth % 2 == 0)
            ? v1.Longitude.CompareTo(v2.Longitude)
            : v1.Latitude.CompareTo(v2.Latitude));

        var mid = vehicles.Length / 2;
        SortVehicles(vehicles[..mid], depth + 1);
        SortVehicles(vehicles[(mid + 1)..], depth + 1);
    }
}