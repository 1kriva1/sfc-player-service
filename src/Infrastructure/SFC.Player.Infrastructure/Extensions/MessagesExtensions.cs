using SFC.Player.Messages.Commands.Common;

namespace SFC.Player.Infrastructure.Extensions;
public static class MessagesExtensions
{
    public static T SetCommandInitiator<T>(this T command, string initiator) where T : InitiatorCommand
    {
        command.Initiator = initiator;
        return command;
    }
}
