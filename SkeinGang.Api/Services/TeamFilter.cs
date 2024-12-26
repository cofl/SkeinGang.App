using System.Linq.Expressions;
using NodaTime;
using SkeinGang.Data.Enums;

namespace SkeinGang.Api.Services;

internal class TeamFilter(Expression<Func<Domain.Team, bool>> filter)
{
    public Expression<Func<Domain.Team, bool>> Filter { get; } = filter;

    internal sealed class Regions(List<Region> items):
        TeamFilter(t => items.Contains(t.Region));

    internal sealed class ContentFocuses(List<ContentFocus> items):
        TeamFilter(t=> items.Contains(t.ContentFocus));

    internal sealed class DifficultyLevels(List<ContentDifficulty> items):
        TeamFilter(t => items.Contains(t.ContentDifficulty));

    internal sealed class ExperienceLevels(List<ExperienceLevel> items):
        TeamFilter(t => items.Contains(t.ExperienceLevel));

    internal sealed class DaysOfWeek(List<IsoDayOfWeek> items):
        TeamFilter(t => items.Contains(t.DayOfWeekRaid));

    internal sealed class TimesOfDay(LocalTime from, LocalTime to):
        TeamFilter(t => t.TimeOfRaid >= from && t.TimeOfRaid <= to)
    {
        public TimesOfDay(LocalTime from, LocalTime? to = null) :
            this(from, to ?? from.PlusMinutes(15))
        {
        }
    }

    internal sealed class HasEmptySlots(int minimum, int maximum):
        TeamFilter(t => t.TeamDetail.EmptySlots >= minimum && t.TeamDetail.EmptySlots <= maximum);

    internal sealed class Durations(Duration minimum, Duration maximum):
        TeamFilter(t => t.RunDuration >= minimum && t.RunDuration <= maximum);
}