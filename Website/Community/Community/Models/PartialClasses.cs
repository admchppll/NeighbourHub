using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Community.Models
{
    [MetadataType(typeof(AddressMetaData))]
    public partial class Address{ }

    [MetadataType(typeof(AuditMetaData))]
    public partial class Audit { }

    [MetadataType(typeof(ContactMetaData))]
    public partial class Contact { }

    [MetadataType(typeof(EventMetaData))]
    public partial class Event { }

    [MetadataType(typeof(InformationMetaData))]
    public partial class Information { }

    [MetadataType(typeof(MessageMetaData))]
    public partial class Message { }

    [MetadataType(typeof(NotificationMetaData))]
    public partial class Notification { }

    [MetadataType(typeof(OrganisationMetaData))]
    public partial class Organisation { }

    [MetadataType(typeof(PointMetaData))]
    public partial class Point { }

    [MetadataType(typeof(ProfileMetaData))]
    public partial class Profile { }

    [MetadataType(typeof(ReviewMetaData))]
    public partial class Review { }

    [MetadataType(typeof(ReportMetaData))]
    public partial class Report { }

    [MetadataType(typeof(RoleMetaData))]
    public partial class Role { }

    [MetadataType(typeof(SkillMetaData))]
    public partial class Skill { }

    [MetadataType(typeof(TagMetaData))]
    public partial class Tag { }

    [MetadataType(typeof(TransactionMetaData))]
    public partial class Transaction { }

    [MetadataType(typeof(UserMetaData))]
    public partial class User { }

    [MetadataType(typeof(UserOrganisationMetaData))]
    public partial class UserOrganisation { }

    [MetadataType(typeof(UserRoleMetaData))]
    public partial class UserRole { }

    [MetadataType(typeof(VolunteerMetaData))]
    public partial class Volunteer { }
}
