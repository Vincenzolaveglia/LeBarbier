namespace LeBarbier.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Reservation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Reservation()
        {
            ReservationServices = new HashSet<ReservationService>();
        }

        [Key]
        public int IdReservation { get; set; }

        public int? Id_User { get; set; }

        public DateTime ReservationDate { get; set; }

        public TimeSpan ReservationTime { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ReservationService> ReservationServices { get; set; }

        public virtual User Users { get; set; }
    }
}
