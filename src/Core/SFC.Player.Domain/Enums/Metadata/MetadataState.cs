using System.ComponentModel;

namespace SFC.Player.Domain.Enums.Metadata;
public enum MetadataState
{
    [Description("Not Required")]
    NotRequired,
    Required,
    Done
}
