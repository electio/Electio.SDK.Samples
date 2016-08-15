using System;

namespace MPD.Electio.SDK.QuickStartSample
{
    /// <summary>
    /// This is a sample application to get you up and running with the SDK as quickly as possible.
    /// <remarks>
    /// The code is not designed to run in a production environment, but to give you a flavour of how 
    /// to work with the Electio .NET SDK.
    /// </remarks>
    /// </summary>
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Electio SDK Quick Start Sample");
            MainMenu();
        }

        #region Menu

        private static void MainMenu()
        {
            var menuOption = ChooseMenuOption();
            switch (menuOption)
            {
                case 1:
                    CreateConsignment();
                    break;
                case 2:
                    GetConsignment();
                    break;
                case 3:
                    AllocateConsignment();
                    break;
                case 4:
                    DeallocateConsignment();
                    break;
                case 5:
                    GetLabel();
                    break;
                case 6:
                    ManifestConsignment();
                    break;
                default:
                    break;
            }
        }

        private static int ChooseMenuOption()
        {
            var selectedResult = 0;

            while (selectedResult == 0)
            {
                ShowMenuOptions();
                var selected = Console.ReadLine();
                int result;
                if (!int.TryParse(selected, out result)) continue;
                if (result > 0 && result <= 6)
                {
                    selectedResult = result;
                }
            }

            return selectedResult;
        }

        private static void ShowMenuOptions()
        {
            Console.WriteLine("Choose from the following options: ");
            Console.WriteLine("\t1. Create a new sample consignment");
            Console.WriteLine("\t2. Retrieve a consignment");
            Console.WriteLine("\t3. Allocate a consignment");
            Console.WriteLine("\t4: De-allocate a consignment");
            Console.WriteLine("\t5. Get labels for a consignment");
            Console.WriteLine("\t6. Manifest a consignment");

            Console.WriteLine();
        }

        #endregion Menu


        private static void CreateConsignment()
        {
            var proxy = new ConsignmentProxy();
            proxy.CreateNewConsignment();
            RestartProcess();
        }

        private static void GetConsignment()
        {
            var consignmentReference = GetConsignmentReference();
            var proxy = new ConsignmentProxy();
            proxy.GetConsignment(consignmentReference);
            RestartProcess();
        }

        private static void AllocateConsignment()
        {
            var consignmentReference = GetConsignmentReference();
            var proxy = new AllocationProxy();
            proxy.AllocateConsignment(consignmentReference);
            RestartProcess();
        }

        private static void DeallocateConsignment()
        {
            var consignmentReference = GetConsignmentReference();
            var proxy = new AllocationProxy();
            proxy.DeallocateConsignment(consignmentReference);
            RestartProcess();
        }

        private static void GetLabel()
        {
            var consignmentReference = GetConsignmentReference();
            var proxy = new LabelProxy();
            proxy.GetLabelForConsignment(consignmentReference);
            RestartProcess();
        }

        private static void ManifestConsignment()
        {
            var consignmentReference = GetConsignmentReference();
            var proxy = new ManifestProxy();
            proxy.ManifestConsignment(consignmentReference);
            RestartProcess();
        }

        private static string GetConsignmentReference()
        {
            Console.WriteLine("Please enter a consignment reference:");
            var consignmentReference = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(consignmentReference))
            {
                Console.WriteLine($"Reference '{consignmentReference}' cannot be empty. Try again.");
                return GetConsignmentReference();
            }

            if (consignmentReference.Length != 14 || consignmentReference.Substring(0, 2).ToUpper() != "EC")
            {
                Console.WriteLine($"Reference '{consignmentReference}' is not valid. Try again.");
                return GetConsignmentReference();
            }

            return consignmentReference;
        }

        private static void RestartProcess()
        {
            Console.WriteLine("Process completed. Press any key to continue.");
            Console.ReadLine();
            MainMenu();
        }
    }
}