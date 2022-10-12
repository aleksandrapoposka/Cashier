using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Articles;

namespace Entities.Orders
{
    public class OrderDetails : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public long OrderId { get; set; }
        
        [ForeignKey("OrderId")]
        public virtual Order Order { get; set;}

        public long ArticleId { get; set; }
        
        [ForeignKey("ArticleId")]
        public virtual Article Article { get; set; }

        public decimal Count { get; set; }
        public decimal Price { get; set; }
    }
}
