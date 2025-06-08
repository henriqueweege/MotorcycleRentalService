using MotorcycleRentalService.Application.Commands.DeliveryManCommands;
using MotorcycleRentalService.Application.Contracts.Services;
using System.Text;

namespace MotorcycleRentalService.Application.Services
{
    public class DocumentSaver : IDocumentSaver
    {
        public string SaveDocument(CreateDriverLicense command)
        {
            string[] fileList = GetOldVersions(command);

            string path = CreateLicense(command);

            RemoveOldVersions(fileList);
            return path;
        }

        private static string[] GetOldVersions(CreateDriverLicense command)
        {
            string filesToDelete = $@"{command.DeliveryManId}*";
            string[] fileList = System.IO.Directory.GetFiles("./", filesToDelete);
            return fileList;
        }

        private static string CreateLicense(CreateDriverLicense command)
        {
            var path = $"./{command.DeliveryManId}_{DateTime.UtcNow.Ticks}.txt";
            using (FileStream fs = File.Create(path))
            {
                byte[] info = new UTF8Encoding(true).GetBytes(command.Base64License);
                fs.Write(info, 0, info.Length);
            }

            return path;
        }

        private static void RemoveOldVersions(string[] fileList)
        {
            foreach (string file in fileList)
            {
                System.IO.File.Delete(file);
            }
        }
    }
}
