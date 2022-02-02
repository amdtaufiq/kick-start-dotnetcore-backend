using System;

namespace BEService.Core.DTOs
{
    public class MenuAppResponse
    {
        public Guid Id { get; set; }
        public string MenuLabel { get; set; }
        public string MenuUrl { get; set; }
        public int Ordering { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class CreateMenuAppRequest
    {
        public string MenuLabel { get; set; }
        public string MenuUrl { get; set; }
        public int Ordering { get; set; }
    }

    public class UpdateMenuAppRequest
    {
        public Guid? Id { get; set; }
        public string MenuLabel { get; set; }
        public string MenuUrl { get; set; }
        public int Ordering { get; set; }
    }
}
