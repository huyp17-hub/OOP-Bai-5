/*************************************************
Mã sinh viên: 202418915
Họ tên:       Phạm Quốc Huy
*************************************************/

using System;

namespace DeviceManagementSystem
{
    public class Printer : Device
    {
        public PrinterType PrintType { get; set; }
        public int PrintedPagesCount { get; set; }
        public bool IsColor { get; set; }

        public Printer(string deviceId, string name, int yearOfUsage, decimal purchasePrice,
                       PrinterType printType, int printedPagesCount, bool isColor, DeviceStatus status = DeviceStatus.Active)
            : base(deviceId, name, yearOfUsage, purchasePrice, status)
        {
            PrintType = printType;
            PrintedPagesCount = Math.Max(0, printedPagesCount);
            IsColor = isColor;
        }

        public override decimal CalculateAnnualMaintenanceCost()
        {
            decimal cost = PurchasePrice * 0.04m;

            if (PrintedPagesCount > 100000)
                cost += 500000m;

            if (IsColor)
                cost += 300000m;

            return cost;
        }

        public override string ToString()
        {
            return base.ToString() + $" | Loại: {PrintType} | Đã in: {PrintedPagesCount:N0} trang | In màu: {(IsColor ? "Có" : "Không")}";
        }
    }
}
