namespace LeBarbier.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ReservationService
    {
        [Key]
        public int IdReservationService { get; set; }

        public int? Id_Reservation { get; set; }

        public int? Id_OfferedServices { get; set; }

        public virtual OfferedService OfferedServices { get; set; }

        public virtual Reservation Reservations { get; set; }
    }
}
