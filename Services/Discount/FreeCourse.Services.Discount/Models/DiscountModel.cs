using System.ComponentModel.DataAnnotations.Schema;

namespace FreeCourse.Services.Discount.Models
{
    [Dapper.Contrib.Extensions.Table("discount")]
    public class DiscountModel
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("userid")]
        public string UserId { get; set; }
        [Column("rate")]
        public int Rate { get; set; }
        [Column("code")]
        public string Code { get; set; }
        [Column("created_date")]
        public DateTime CreatedDate { get; set; }
    }
}
