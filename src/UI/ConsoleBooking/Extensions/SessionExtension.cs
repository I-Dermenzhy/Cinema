using Domain.Models;

namespace ConsoleBooking.Extensions;
internal static class SessionExtension
{
    public static string GetInfo(this Session session) =>
            $"Назва фільму: {session.Movie.Title}\n" +
            $"Дата показу: {session.Duration.Start:dd.MM.yyyy}\n" +
            $"Час початку: {session.Duration.Start:HH:mm}\n" +
            $"Час завершення: {session.Duration.End:HH:mm}";
}
