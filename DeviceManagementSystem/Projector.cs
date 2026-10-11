/*************************************************
Mã sinh viên: 202418915
Họ tên:       Phạm Quốc Huy
*************************************************/

using System;

namespace DeviceManagementSystem
{
    public class Projector : Device
    {
        public int Lumens { get; set; }
        public int LampHoursUsed { get; set; }

        public Projector(string deviceId, string name, int yearOfUsage, decimal purchasePrice,
                         int lumens, int lampHoursUsed, DeviceStatus status = DeviceStatus.Active)
            : base(deviceId, name, yearOfUsage, purchasePrice, status)
        {
            Lumens = lumens;
            LampHoursUsed = Math.Max(0, lampHoursUsed);
        }

        public override decimal CalculateAnnualMaintenanceCost()
        {
            decimal cost = PurchasePrice * 0.03m;

            if (LampHoursUsed > 3000)
                cost += 1500000m;

            return cost;
        }

        public override string ToString()
        {
            return base.ToString() + $" | Độ sáng: {Lumens} Lumens | Giờ đèn: {LampHoursUsed:N0}h";
        }
    }
}
