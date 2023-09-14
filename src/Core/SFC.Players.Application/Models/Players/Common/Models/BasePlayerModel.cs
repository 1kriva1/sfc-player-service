using AutoMapper;

using SFC.Players.Application.Common.Mappings;
using SFC.Players.Domain.Entities;

namespace SFC.Players.Application.Models.Players.Common.Models;
public class BasePlayerModel
{
    public PlayerProfileModel Profile { get; set; } = null!;

    public PlayerStatsDto Stats { get; set; } = null!;
}
