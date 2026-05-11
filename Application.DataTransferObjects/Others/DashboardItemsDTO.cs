namespace Application.DataTransferObjects.Others
{
    public class DashboardItemsDTO
    {
        public int DeliveryCount { get; set; }
        public int ReturnCount { get; set; }
        public int ReceivingCount { get; set; }
        public int DeliveryTodayCount { get; set; }
        public int ReturnTodayCount { get; set; }
        public int ReceivingTodayCount { get; set; }
    }
}
