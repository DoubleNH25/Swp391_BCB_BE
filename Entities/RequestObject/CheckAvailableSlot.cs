namespace Entities.RequestObject
{
    public class CheckAvailableSlot
    {
        //public int UserId { get; set; }
        //public int PostId { get; set; }
        //public List<SlotsInfo> SlotsInfo { get; set; }
    }

    public class SlotsInfo
    {
        public DateTime DateRegis { get; set; }
        public int NumSlots { get; set; }
    }
}
