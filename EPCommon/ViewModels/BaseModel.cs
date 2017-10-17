using System;

namespace EPCommon.ViewModels
{
    public class BaseModel
    {
        public BaseModel()
        {
            CreatedBy = 1234;
            CreatedAt = DateTime.Now;
            UpdatedBy = 1234;
            UpdatedAt = DateTime.Now;
        }
        public long CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; }

        public long UpdatedBy { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
