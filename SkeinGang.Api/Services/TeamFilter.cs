using System.Linq.Expressions;
using NodaTime;
using SkeinGang.Data.Enums;

namespace SkeinGang.Api.Services;

public record TeamFilter(Expression<Func<Domain.Team, bool>> Filter)
{
    internal sealed record Regions(List<Region> Items):
        TeamFilter(t => Items.Contains(t.Region));

    internal sealed record ContentFocuses(List<ContentFocus> Items):
        TeamFilter(t=> Items.Contains(t.ContentFocus));

    internal sealed record DifficultyLevels(List<ContentDifficulty> Items):
        TeamFilter(t => Items.Contains(t.ContentDifficulty));

    internal sealed record ExperienceLevels(List<ExperienceLevel> Items):
        TeamFilter(t => Items.Contains(t.ExperienceLevel));

    internal sealed record DaysOfWeek(List<IsoDayOfWeek> Items):
        TeamFilter(t => Items.Contains(t.DayOfWeekRaid));

    internal sealed record TimesOfDay(LocalTime From, LocalTime To):
        TeamFilter(t => t.TimeOfRaid >= From && t.TimeOfRaid <= To)
    {
        public TimesOfDay(LocalTime From, LocalTime? To = null) :
            this(From, To ?? From.PlusMinutes(15))
        {
        }
    }

    public sealed record HasEmptySlots(int Minimum, int Maximum):
        TeamFilter(t => t.TeamDetail.EmptySlots >= Minimum && t.TeamDetail.EmptySlots <= Maximum);

    public sealed record Durations(Duration Minimum, Duration Maximum):
        TeamFilter(t => t.RunDuration >= Minimum && t.RunDuration <= Maximum);
}