using System.ComponentModel.DataAnnotations;

namespace WebApi.Data
{
    public class Approval
    {
        [Key]
        public int? ID { get; set; }
        public int? EntityTypeID { get; set; }
        public string? EntityIdentifier { get; set; }
        public int? ApprovalTypeID { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public string? LastModifiedBy { get; set; }

    }
}