﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Community.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class CommunityEntities : DbContext
    {
        public CommunityEntities()
            : base("name=CommunityEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Audit> Audits { get; set; }
        public virtual DbSet<Bookmarked> Bookmarkeds { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<EventTag> EventTags { get; set; }
        public virtual DbSet<Information> Information { get; set; }
        public virtual DbSet<Interest> Interests { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<Organisation> Organisations { get; set; }
        public virtual DbSet<Profile> Profiles { get; set; }
        public virtual DbSet<Report> Reports { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Skill> Skills { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserClaim> UserClaims { get; set; }
        public virtual DbSet<UserInterest> UserInterests { get; set; }
        public virtual DbSet<UserLogin> UserLogins { get; set; }
        public virtual DbSet<UserOrganisation> UserOrganisations { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<UserSkill> UserSkills { get; set; }
        public virtual DbSet<Volunteer> Volunteers { get; set; }
        public virtual DbSet<Point> Points { get; set; }
        public virtual DbSet<EventSearch> EventSearches { get; set; }
    
        public virtual int confirmVolunteer(Nullable<int> volunteerID)
        {
            var volunteerIDParameter = volunteerID.HasValue ?
                new ObjectParameter("VolunteerID", volunteerID) :
                new ObjectParameter("VolunteerID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("confirmVolunteer", volunteerIDParameter);
        }
    
        public virtual int createGeoLocationAddress(Nullable<int> addressID)
        {
            var addressIDParameter = addressID.HasValue ?
                new ObjectParameter("AddressID", addressID) :
                new ObjectParameter("AddressID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("createGeoLocationAddress", addressIDParameter);
        }
    
        public virtual int reduceSenderBalance(Nullable<int> transactionID)
        {
            var transactionIDParameter = transactionID.HasValue ?
                new ObjectParameter("TransactionID", transactionID) :
                new ObjectParameter("TransactionID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("reduceSenderBalance", transactionIDParameter);
        }
    
        public virtual int reverseTransaction(Nullable<int> transactionID)
        {
            var transactionIDParameter = transactionID.HasValue ?
                new ObjectParameter("TransactionID", transactionID) :
                new ObjectParameter("TransactionID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("reverseTransaction", transactionIDParameter);
        }
    
        public virtual int withdrawVolunteer(Nullable<int> volunteerID)
        {
            var volunteerIDParameter = volunteerID.HasValue ?
                new ObjectParameter("VolunteerID", volunteerID) :
                new ObjectParameter("VolunteerID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("withdrawVolunteer", volunteerIDParameter);
        }
    
        public virtual int clearNotifications()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("clearNotifications");
        }
    }
}
