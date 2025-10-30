// 代码生成时间: 2025-10-30 22:39:05
 * It demonstrates good C# practices, error handling, and is structured for maintainability and extensibility.
 */
using System;

namespace VirtualizationManager
{
    // Exception class for virtualization-related errors.
    public class VirtualizationException : Exception
    {
        public VirtualizationException(string message) : base(message)
        {
        }
    }

    public class VirtualMachine
    {
        // Properties of a virtual machine.
        public string Name { get; set; }
        public string Status { get; set; }
        public string Provider { get; set; }
        // Additional properties can be added here.

        // Constructor for creating a new virtual machine.
        public VirtualMachine(string name, string status, string provider)
        {
            Name = name;
            Status = status;
            Provider = provider;
        }
    }

    public class VirtualizationManager
    {
        // List to hold virtual machines.
        private List<VirtualMachine> virtualMachines = new List<VirtualMachine>();

        // Method to add a new virtual machine.
        public void AddVirtualMachine(VirtualMachine vm)
        {
            if (vm == null)
                throw new VirtualizationException("Cannot add a null virtual machine.");

            virtualMachines.Add(vm);
            Console.WriteLine("Virtual machine added: " + vm.Name);
        }

        // Method to remove a virtual machine.
        public void RemoveVirtualMachine(string name)
        {
            var vm = virtualMachines.Find(v => v.Name == name);
            if (vm == null)
                throw new VirtualizationException("Virtual machine not found: " + name);

            virtualMachines.Remove(vm);
            Console.WriteLine("Virtual machine removed: " + name);
        }

        // Method to start a virtual machine.
        public void StartVirtualMachine(string name)
        {
            var vm = virtualMachines.Find(v => v.Name == name);
            if (vm == null)
                throw new VirtualizationException("Virtual machine not found: " + name);

            if (vm.Status != "Stopped")
                throw new VirtualizationException("Virtual machine is already running: " + name);

            vm.Status = "Running";
            Console.WriteLine("Virtual machine started: " + name);
        }

        // Method to stop a virtual machine.
        public void StopVirtualMachine(string name)
        {
            var vm = virtualMachines.Find(v => v.Name == name);
            if (vm == null)
                throw new VirtualizationException("Virtual machine not found: " + name);

            if (vm.Status != "Running")
                throw new VirtualizationException("Virtual machine is already stopped: " + name);

            vm.Status = "Stopped";
            Console.WriteLine("Virtual machine stopped: " + name);
        }

        // Method to list all virtual machines.
        public void ListVirtualMachines()
        {
            Console.WriteLine("Listing all virtual machines: ");
            foreach (var vm in virtualMachines)
            {
                Console.WriteLine($"Name: {vm.Name}, Status: {vm.Status}, Provider: {vm.Provider}");
            }
        }
    }

    // Example usage of the VirtualizationManager class.
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                VirtualizationManager manager = new VirtualizationManager();
                manager.AddVirtualMachine(new VirtualMachine("VM1", "Stopped", "HyperV"));
                manager.AddVirtualMachine(new VirtualMachine("VM2", "Stopped", "VMware"));

                manager.ListVirtualMachines();

                manager.StartVirtualMachine("VM1");
                manager.StopVirtualMachine("VM1");
            }
            catch (VirtualizationException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}