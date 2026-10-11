/*************************************************
Mã sinh viên: 202418915
Họ tên:       Phạm Quốc Huy
*************************************************/

using System;
using System.Collections.Generic;

namespace DeviceManagementSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("========== CHƯƠNG TRÌNH QUẢN LÝ THIẾT BỊ PHÒNG LAB ==========\n");

            var pc1 = new Computer("PC-001", "Dell OptiPlex 7090", 2024, 18000000m, 16, "Core i7", hasDedicatedGpu: true);
            var pc2 = new Computer("PC-002", "HP ProDesk 400", 2018, 12000000m, 8, "Core i5", hasDedicatedGpu: false);

            var pr1 = new NetworkPrinter("PR-001", "HP LaserJet Pro MFP", 2022, 9500000m, PrinterType.Laser, printedPagesCount: 125000, isColor: true);
            var pr2 = new Printer("PR-002", "Canon LBP 2900", 2023, 3800000m, PrinterType.Laser, printedPagesCount: 15000, isColor: false);

            var pj1 = new Projector("PJ-001", "Sony VPL-EX455", 2021, 15000000m, lumens: 3600, lampHoursUsed: 3450);

            var roomA = new LabRoom("LAB-A", "Phòng Máy Tính Thực Hành 01", 40);
            var roomB = new LabRoom("LAB-B", "Phòng Hội Thảo Đa Phương Tiện", 30);

            roomA.AddDevice(pc1);
            roomA.AddDevice(pc2);
            roomA.AddDevice(pr1);

            roomB.AddDevice(pr2);
            roomB.AddDevice(pj1);

            Console.WriteLine("[KIỂM THỬ 1] Thử thêm thiết bị trùng mã 'PC-001' vào phòng LAB-A:");
            try
            {
                var duplicatePc = new Computer("PC-001", "Clone PC", 2024, 15000000m, 8, "Core i3", false);
                roomA.AddDevice(duplicatePc);
            }
            catch (Exception ex)
            {
                Console.WriteLine($" -> Bắt ngoại lệ thành công: {ex.Message}\n");
            }

            Console.WriteLine("[KIỂM THỬ 2] Danh sách thiết bị và tổng chi phí bảo trì dự kiến:");
            roomA.PrintRoomDetails();
            Console.WriteLine($" => TỔNG CHI PHÍ BẢO TRÌ {roomA.RoomId}: {roomA.CalculateAnnualMaintenanceCost():N0} VNĐ\n");

            roomB.PrintRoomDetails();
            Console.WriteLine($" => TỔNG CHI PHÍ BẢO TRÌ {roomB.RoomId}: {roomB.CalculateAnnualMaintenanceCost():N0} VNĐ\n");

            Console.WriteLine("[KIỂM THỬ 3] Liệt kê thiết bị cần bảo trì (Đang bảo trì hoặc dùng > 5 năm):");
            pc2.Status = DeviceStatus.UnderMaintenance;

            var roomANeedMaint = roomA.GetDevicesRequiringMaintenance();
            Console.WriteLine($"- Danh sách phòng {roomA.RoomId} ({roomANeedMaint.Count} thiết bị):");
            foreach (var d in roomANeedMaint)
            {
                Console.WriteLine($"  + {d.DeviceId} ({d.Name}) - Tuổi đời: {d.GetYearsInUse()} năm, Trạng thái: {d.Status}");
            }
            Console.WriteLine();

            Console.WriteLine("[KIỂM THỬ 4] Kết nối mạng và duyệt INetworkable thuần túy:");
            var networkDevices = new List<INetworkable> { pc1, pc2, pr1 };

            pc1.Connect("192.168.1.101");
            pr1.Connect("192.168.1.200");

            foreach (var netDevice in networkDevices)
            {
                if (netDevice.IsConnected)
                {
                    Console.WriteLine($"  * Thiết bị đang ONLINE - Địa chỉ IP: {netDevice.IpAddress}");
                }
                else
                {
                    Console.WriteLine("  * Thiết bị đang OFFLINE (Chưa gán IP)");
                }
            }

            Console.WriteLine("\n[KIỂM THỬ 5] Ngắt kết nối thiết bị PR-001:");
            pr1.Disconnect();
            Console.WriteLine($"  PR-001 sau ngắt kết nối: IsConnected = {pr1.IsConnected}, IP = {pr1.IpAddress ?? "(null)"}");

            Console.WriteLine("\n========== HOÀN THÀNH TOÀN BỘ KIỂM THỬ ==========");
        }
    }
}
