using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using TeduCoreApp.Data.Interfaces;
using TeduCoreApp.Data.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeduCoreApp.Data.Entities
{
    [Table("AppUsers")]
    public class AppUser : IdentityUser<Guid>, IDateTracking, ISwitchable
    {
        public AppUser() : base()
        {
            AppUserRoles = new HashSet<AppUserRole>();
            AppUserClaims = new HashSet<AppUserClaim>();
            AppUserLogins = new HashSet<AppUserLogin>();
            AppUserActions = new HashSet<AppUserActions>();
        }

        public AppUser(string username) : base(username)
        {
            UserName = username;
        }

        public AppUser(string userName, string fullName,
            string email, string phoneNumber, string avatar, Status status)
        {
            this.UserName = userName;
            this.FullName = fullName;
            this.Email = email;
            this.PhoneNumber = phoneNumber;
            this.Avatar = avatar;
            this.Status = status;
        }

        public AppUser(Guid id, string fullName, string userName,
            string email, string phoneNumber, string avatar, Status status)
        {
            Id = id;
            FullName = fullName;
            UserName = userName;
            Email = email;
            PhoneNumber = phoneNumber;
            Avatar = avatar;
            Status = status;
        }

        public string FullName { get; set; }

        public DateTime? BirthDay { set; get; }

        public decimal Balance { get; set; }

        public string Avatar { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public Status Status { get; set; }

        public virtual ICollection<AppUserClaim> AppUserClaims { get; set; }
        public virtual ICollection<AppUserRole> AppUserRoles { get; set; }
        public virtual ICollection<AppUserLogin> AppUserLogins { get; set; }
        public virtual ICollection<AppUserActions> AppUserActions { get; set; }
    }
}