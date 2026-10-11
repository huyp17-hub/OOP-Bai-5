/*************************************************
Mã sinh viên: 202418915
Họ tên:       Phạm Quốc Huy
*************************************************/

namespace DeviceManagementSystem
{
    public interface INetworkable
    {
        string? IpAddress { get; }
        bool IsConnected { get; }

        void Connect(string ipAddress);
        void Disconnect();
    }
}
