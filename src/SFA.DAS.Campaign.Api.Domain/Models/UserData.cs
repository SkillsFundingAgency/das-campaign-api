#nullable disable 

using System.ComponentModel.DataAnnotations;

namespace SFA.DAS.Campaign.Api.Domain.Models;

public class UserData
{
    [Key]
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string UkEmployerSize { get; set; }
    public string PrimaryIndustry { get; set; }
    public string PrimaryLocation { get; set; }
    public DateTime AppsgovSignUpDate { get; set; }
    public string PersonOrigin { get; set; }
    public bool IncludeInUR { get; set; }
}