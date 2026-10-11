/*************************************************
Mã sinh viên: 202418915
Họ tên:       Phạm Quốc Huy
*************************************************/

using System;

namespace DeviceManagementSystem
{
    public abstract class Device
    {
        public string DeviceId { get; }
        public string Name { get; set; }
        public int YearOfUsage { get; }
        public decimal PurchasePrice { get; }
        public DeviceStatus Status { get; set; }

        protected Device(string deviceId, string name, int yearOfUsage, decimal purchasePrice, DeviceStatus status = DeviceStatus.Active)
        {
            if (string.IsNullOrWhiteSpace(deviceId))
                throw new ArgumentException("Mã thiết bị không được để trống.", nameof(deviceId));

            if (purchasePrice <= 0)
                throw new ArgumentException("Giá mua phải lớn hơn 0.", nameof(purchasePrice));

            int currentYear = DateTime.Now.Year;
            if (yearOfUsage > currentYear)
                throw new ArgumentException($"Năm đưa vào sử dụng ({yearOfUsage}) không thể lớn hơn năm hiện tại ({currentYear}).", nameof(yearOfUsage));

            DeviceId = deviceId.Trim();
            Name = string.IsNullOrWhiteSpace(name) ? "Chưa đặt tên" : name.Trim();
            YearOfUsage = yearOfUsage;
            PurchasePrice = purchasePrice;
            Status = status;
        }

        public int GetYearsInUse() => DateTime.Now.Year - YearOfUsage;

        public abstract decimal CalculateAnnualMaintenanceCost();

        public override string ToString()
        {
            return $"[{DeviceId}] {Name} | Năm SX: {YearOfUsage} | Giá: {PurchasePrice:N0} VNĐ | Trạng thái: {Status}";
        }
    }
}
