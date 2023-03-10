//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CopyOfShikshanSankraman.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Mahavidyalaya_tbl
    {
        public int id { get; set; }
        [Required(ErrorMessage = "required..")]

        public string Vargadar { get; set; }
        [Required(ErrorMessage = "required..")]
        public string SansthecheNav { get; set; }
        [Required(ErrorMessage = "required..")]
        public string MahavidyalayacheNav { get; set; }
        [Required(ErrorMessage = "required..")]
        public string SansthechaPatta { get; set; }
        [Required(ErrorMessage = "required..")]
        public string CityName { get; set; }
        [Required(ErrorMessage = "required..")]
        [RegularExpression("^[0-9]{7}$", ErrorMessage = "Not a valid Index number")]

        public string IndexNo { get; set; }
        [Required(ErrorMessage = "required..")]
        public string Taluka { get; set; }
        [Required(ErrorMessage = "required..")]
        public string Jhila { get; set; }
        [Required(ErrorMessage = "required..")]
        [RegularExpression("^[0-9]{6}$", ErrorMessage = "Not a valid Pincode number")]
        public string PincodeNo { get; set; }
        [Required(ErrorMessage = "required..")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "required..")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Phone number")]
        public string MobileNo1 { get; set; }
        [Required(ErrorMessage = "required..")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Phone number")]
        public string MobileNo2 { get; set; }
        [Required(ErrorMessage = "required..")]
        public string VarganiDate { get; set; }
        [Required(ErrorMessage = "required..")]
        public string Vargani { get; set; }
        [Required(ErrorMessage = "required..")]
        public string MonthStart { get; set; }
        [Required(ErrorMessage = "required..")]
        public string MonthEnd { get; set; }
        public string Ip_Address { get; set; }
        public Nullable<System.DateTime> Date_Time { get; set; }
    }
}
