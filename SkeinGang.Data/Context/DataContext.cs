using Microsoft.EntityFrameworkCore;
using NodaTime;
using SkeinGang.Data.Entities;
using SkeinGang.Data.Enums;

namespace SkeinGang.Data.Context;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public virtual DbSet<DiscordServer> DiscordServers { get; set; }
    public virtual DbSet<Player> Players { get; set; }
    public virtual DbSet<Team> Teams { get; set; }
    public virtual DbSet<TeamMembership> TeamMemberships { get; set; }
    public virtual DbSet<TeamDetail> TeamDetails { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder
            .UseLazyLoadingProxies()
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
            .UseNpgsql(options => options.UseNodaTime());
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder
            .Properties<Region>()
            .HaveConversion<EnumMemberToStringConverter<Region>>();
        configurationBuilder
            .Properties<MembershipType>()
            .HaveConversion<EnumMemberToStringConverter<MembershipType>>();
        configurationBuilder
            .Properties<ContentDifficulty>()
            .HaveConversion<EnumMemberToStringConverter<ContentDifficulty>>();
        configurationBuilder
            .Properties<ContentFocus>()
            .HaveConversion<EnumMemberToStringConverter<ContentFocus>>();
        configurationBuilder
            .Properties<ExperienceLevel>()
            .HaveConversion<EnumMemberToStringConverter<ExperienceLevel>>();
        configurationBuilder
            .Properties<IsoDayOfWeek>()
            .HaveConversion<EnumMemberToSnakeCaseStringConverter<IsoDayOfWeek>>();
        configurationBuilder
            .Properties<DateTimeZone>()
            .HaveConversion<DateTimeZoneToStringConverter>();
    }
}
