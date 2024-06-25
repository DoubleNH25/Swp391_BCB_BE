using System.Globalization;

namespace Entities.RequestObject
{
    public class NewPostInfo
    {
        public string? LevelSlot { get; set; }
        public string? CategorySlot { get; set; }
        public string? Title { get; set; }
        public string? Address { get; set; }
        public List<SlotInfo>? Slots { get; set; }
        public string? Description { get; set; }
        public string? HighlightUrl { get; set; }
        public List<string>? ImgUrls { get; set; }

        public string SlotsToString()
        {
            var res = string.Empty;
            if (Slots != null)
            {
                for (var i = 0; i < Slots.Count(); i++)
                {
                    var savedData = Slots[i];
                    res += i + 1 == Slots.Count() ? $"{savedData.SaveData}" : $"{savedData.SaveData};";
                }
            }
            return res;
        }
    }

    public class SlotInfo
    {
        public SlotInfo(string info)
        {
            var infos = info.Split(',');
            for (var i = 0; i < infos.Count(); i++)
            {
                var temp = infos[i];
                switch (i)
                {
                    case 0:
                        {
                            StartTime = DateTime.ParseExact(temp, "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                            break;
                        }
                    case 1:
                        {
                            EndTime = DateTime.ParseExact(temp, "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                            break;
                        }
                    case 2:
                        {
                            Price = decimal.Parse(temp);
                            break;
                        }
                    case 3:
                        {
                            AvailableSlot = int.Parse(temp);
                            break;
                        }
                }
            }
        }
        public SlotInfo()
        {
            AvailableSlot = 0;
        }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public decimal? Price { get; set; }
        public int AvailableSlot { get; set; }
        public string SaveData => $"{StartTime},{EndTime},{Price},{AvailableSlot}";
    }
}
