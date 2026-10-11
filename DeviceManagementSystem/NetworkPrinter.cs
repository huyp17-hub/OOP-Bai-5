/*************************************************
Mã sinh viên: 202418915
Họ tên:       Phạm Quốc Huy
*************************************************/

using System;

namespace DeviceManagementSystem
{
    public class NetworkPrinter : Printer, INetworkable
    {
        public string? IpAddress { get; private set; }
        public bool IsConnected => !string.IsNullOrEmpty(IpAddress);

        public NetworkPrinter(string deviceId, string name, int yearOfUsage, decimal purchasePrice,
                              PrinterType printType, int printedPagesCount, bool isColor, DeviceStatus status = DeviceStatus.Active)
            : base(deviceId, name, yearOfUsage, purchasePrice, printType, printedPagesCount, isColor, status)
        {
        }

        public void Connect(string ipAddress)
        {
            if (IsConnected)
                throw new InvalidOperationException($"Máy in {DeviceId} đang kết nối tại {IpAddress}. Cần ngắt kết nối trước.");

            if (string.IsNullOrWhiteSpace(ipAddress))
                throw new ArgumentException("Địa chỉ IP không được để trống.", nameof(ipAddress));

            IpAddress = ipAddress.Trim();
        }

        public void Disconnect()
        {
            IpAddress = null;
        }

        public override string ToString()
        {
            string netStatus = IsConnected ? $"IP: {IpAddress}" : "Chưa kết nối mạng";
            return base.ToString() + $" | Hỗ trợ mạng: Có ({netStatus})";
        }
    }
}
