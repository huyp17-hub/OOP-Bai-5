/*************************************************
Mã sinh viên: 202418915
Họ tên:       Phạm Quốc Huy
*************************************************/

using System;
using System.Collections.Generic;

namespace DeviceManagementSystem
{
    public class LabRoom
    {
        public string RoomId { get; }
        public string RoomName { get; set; }
        public int Capacity { get; set; }

        private readonly List<Device> _devices;

        public IReadOnlyList<Device> Devices => _devices.AsReadOnly();

        public LabRoom(string roomId, string roomName, int capacity)
        {
            if (string.IsNullOrWhiteSpace(roomId))
                throw new ArgumentException("Mã phòng không được để trống.", nameof(roomId));

            RoomId = roomId.Trim();
            RoomName = roomName;
            Capacity = capacity;
            _devices = new List<Device>();
        }

        public void AddDevice(Device device)
        {
            if (device == null)
                throw new ArgumentNullException(nameof(device), "Không được thêm thiết bị null.");

            if (_devices.Exists(d => d.DeviceId.Equals(device.DeviceId, StringComparison.OrdinalIgnoreCase)))
                throw new InvalidOperationException($"Lỗi: Thiết bị có mã '{device.DeviceId}' đã tồn tại trong phòng {RoomId}.");

            _devices.Add(device);
        }

        public bool RemoveDevice(string deviceId)
        {
            var dev = FindDevice(deviceId);
            return dev != null && _devices.Remove(dev);
        }

        public Device? FindDevice(string deviceId)
        {
            return _devices.Find(d => d.DeviceId.Equals(deviceId, StringComparison.OrdinalIgnoreCase));
        }

        public decimal CalculateAnnualMaintenanceCost()
        {
            decimal total = 0;
            foreach (var device in _devices)
            {
                total += device.CalculateAnnualMaintenanceCost();
            }
            return total;
        }

        public List<Device> GetDevicesRequiringMaintenance()
        {
            return _devices.FindAll(d => d.Status == DeviceStatus.UnderMaintenance || d.GetYearsInUse() > 5);
        }

        public void PrintRoomDetails()
        {
            Console.WriteLine($"=== PHÒNG: {RoomName} ({RoomId}) - Sức chứa: {Capacity} ===");
            if (_devices.Count == 0)
            {
                Console.WriteLine("  (Chưa có thiết bị nào)");
                return;
            }

            foreach (var d in _devices)
            {
                Console.WriteLine($"  - {d} | Chi phí bảo trì/năm: {d.CalculateAnnualMaintenanceCost():N0} VNĐ");
            }
        }
    }
}
