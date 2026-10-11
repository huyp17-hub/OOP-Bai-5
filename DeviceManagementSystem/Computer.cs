/*************************************************
Mã sinh viên: 202418915
Họ tên:       Phạm Quốc Huy
*************************************************/

using System;

namespace DeviceManagementSystem
{
    public class Computer : Device, INetworkable
    {
        public int RamCapacityGb { get; set; }
        public string CpuType { get; set; }
        public bool HasDedicatedGpu { get; set; }

        public string? IpAddress { get; private set; }
        public bool IsConnected => !string.IsNullOrEmpty(IpAddress);

        public Computer(string deviceId, string name, int yearOfUsage, decimal purchasePrice,
                        int ramGb, string cpuType, bool hasDedicatedGpu, DeviceStatus status = DeviceStatus.Active)
            : base(deviceId, name, yearOfUsage, purchasePrice, status)
        {
            RamCapacityGb = ramGb;
            CpuType = cpuType;
            HasDedicatedGpu = hasDedicatedGpu;
        }

        public void Connect(string ipAddress)
        {
            if (IsConnected)
                throw new InvalidOperationException($"Máy tính {DeviceId} đang kết nối tại IP {IpAddress}. Cần ngắt kết nối trước.");

            if (string.IsNullOrWhiteSpace(ipAddress))
                throw new ArgumentException("Địa chỉ IP không được để trống.", nameof(ipAddress));

            IpAddress = ipAddress.Trim();
        }

        public void Disconnect()
        {
            IpAddress = null;
        }

        public override decimal CalculateAnnualMaintenanceCost()
        {
            decimal cost = PurchasePrice * 0.05m;

            if (HasDedicatedGpu)
                cost += PurchasePrice * 0.02m;

            if (GetYearsInUse() > 5)
                cost += PurchasePrice * 0.01m;

            return cost;
        }

        public override string ToString()
        {
            string netStatus = IsConnected ? $"IP: {IpAddress}" : "Chưa kết nối mạng";
            return base.ToString() + $" | RAM: {RamCapacityGb}GB | CPU: {CpuType} | GPU rời: {(HasDedicatedGpu ? "Có" : "Không")} | Mạng: {netStatus}";
        }
    }
}
