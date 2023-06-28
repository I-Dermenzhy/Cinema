using Domain.Models;

namespace ConsoleBooking.Extensions;

internal static class TicketExtension
{
    public static string GetInfo(this Ticket ticket)
    {
        var session = ticket.Session;
        var movie = session.Movie;
        var seat = ticket.Seat;

        return
            $"Назва фільму: {movie.Title}\n" +
            $"Жанр фільму: {movie.Genre}\n" +
            $"Продюсер: {movie.Producer}\n" +
            $"Ряд: {seat.Row}\n" +
            $"Місце: {seat.Place}\n" +
            $"Категорія: {seat.Category}\n" +
            $"Ціна: {Math.Round(ticket.Price)} гривень\n" +
            $"Дата показу: {session.Duration.Start:dd.MM.yyyy}\n" +
            $"Час початку: {session.Duration.Start:HH:mm}\n" +
            $"Час завершення: {session.Duration.End:HH:mm}";
    }
}
