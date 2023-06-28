using ConsoleBooking.Extensions;

using Contracts.PriceEvalution;
using Contracts.Repositories;

using Domain.Models;

using System.Text;

namespace ConsoleBooking;
internal class App
{
    private readonly IClientRepository _clientRepository;
    private readonly ISessionRepository _sessionRepository;
    private readonly ISeatRepository _seatRepository;
    private readonly ITicketRepository _ticketRepository;

    private readonly ITicketEvaluator _ticketEvaluator;

    public App(ITicketRepository ticketRepository, ISessionRepository sessionRepository,
        IClientRepository clientRepository, ISeatRepository seatRepository,
        ITicketEvaluator ticketEvaluator)
    {
        _ticketRepository = ticketRepository;
        _sessionRepository = sessionRepository;
        _clientRepository = clientRepository;
        _seatRepository = seatRepository;

        _ticketEvaluator = ticketEvaluator;
    }

    public async Task RunAsync()
    {
        ConfigureConsole();
        ShowHelloMessage();

        await DisplayMainMenu();
    }

    private static void ConfigureConsole() =>
        Console.OutputEncoding = Encoding.UTF8;

    private static void ShowHelloMessage() =>
        Console.WriteLine("Вітаємо в консольному сервісі бронювання квитків в кінотеатр!");

    private static void WriteSeparatorLine() =>
        Console.WriteLine("----------------------------------------");

    private async Task DisplayMainMenu()
    {
        WriteSeparatorLine();
        Console.WriteLine("Оберіть послугу:");
        Console.WriteLine("1 - створити бронювання");
        Console.WriteLine("2 - скасувати бронювання");
        Console.Write("Ваш вибір: ");

        await HandleServiceChoice();
    }

    private async Task HandleServiceChoice()
    {
        var input = Console.ReadLine();

        if (input == "1")
            await DisplayCreateBookingMenu();
        else
            await DisplayCancelBookingMenu();
    }

    private async Task DisplayCancelBookingMenu()
    {
        Console.Clear();
        Console.Write("Ключ бронювання: ");
        await HandleBookingKeyCancellationInput();
    }

    private async Task HandleBookingKeyCancellationInput()
    {
        try
        {
            var input = Guid.Parse(Console.ReadLine()!);

            var ticket = await _ticketRepository.GetByIdAsync(input);
            DisplayTicketInformation(ticket);

            await _ticketRepository.RemoveAsync(input);

            await DisplaySuccessfulCancellationInfo();
        }
        catch
        {
            WriteSeparatorLine();
            Console.WriteLine("Бронювання за заданим ключем не знайдено.");
            Console.WriteLine("Спробуйте ще раз.");

            Thread.Sleep(9000);
            await DisplayCancelBookingMenu();
        }
    }

    private async Task DisplayCreateBookingMenu()
    {
        var sessions = (await _sessionRepository.GetAllAsync())
            .OrderBy(session => session.Duration.Start)
            .ToList();

        Console.Clear();
        Console.WriteLine("НАЙБЛИЖЧІ СЕАНСИ:\n");

        sessions.Select((session, index) => $"{index + 1} - {session.GetInfo()}\n")
            .ToList()
            .ForEach(Console.WriteLine);

        await HandleUserSessionChoice(sessions);
    }

    private async Task HandleUserSessionChoice(IList<Session> sessions)
    {
        var client = await CreateNewClient();

        var chosenSession = GetChosenSession(sessions);
        var availableSeats = await GetAvailableSeats();
        var chosenSeat = GetChosenSeat(availableSeats);

        var ticket = new Ticket
        (
            client: client,
            session: chosenSession,
            seat: chosenSeat,
            price: 0
        );

        ticket.Price = _ticketEvaluator.EvaluateCost(ticket);

        DisplayTicketInformation(ticket);

        var id = await InsertTicketAsync(_ticketRepository, ticket);

        Console.WriteLine($"Ключ бронювання: {id}");

        Thread.Sleep(12000);
        Console.Clear();
        await DisplayMainMenu();
    }

    private async Task<Client> CreateNewClient()
    {
        var client = new Client
        {
            Id = Guid.NewGuid(),
            Email = "client@example.gmail",
            Tickets = new List<Ticket>()
        };

        await _clientRepository.InsertAsync(client);

        return client;
    }

    private Session GetChosenSession(IList<Session> sessions)
    {
        WriteSeparatorLine();
        Console.Write("Мій вибір: ");

        var sessionChoice = int.Parse(Console.ReadLine()!);
        return sessions[sessionChoice - 1];
    }

    private async Task<IEnumerable<Seat>> GetAvailableSeats() => await _seatRepository.GetAllAsync();

    private Seat GetChosenSeat(IEnumerable<Seat> availableSeats)
    {
        DisplaySeatCategoryChoice();

        var categoryChoice = Console.ReadLine();

        var filteredSeats = availableSeats.Where(seat =>
            (categoryChoice == "1" && seat.Category == SeatCategory.Standart) ||
            (categoryChoice == "2" && seat.Category == SeatCategory.Premium) ||
            (categoryChoice == "3" && seat.Category == SeatCategory.VIP));

        var availableRows = filteredSeats
            .Select(seat => seat.Row)
            .Distinct()
            .OrderBy(row => row)
            .ToArray();

        DisplayRowChoice(availableRows);

        var rowChoice = int.Parse(Console.ReadLine()!);
        var chosenRow = availableRows[rowChoice - 1];

        var availdablePlaces = filteredSeats
            .Where(seat => seat.Row == chosenRow)
            .Select(seat => seat.Place)
            .Distinct()
            .OrderBy(place => place)
            .ToArray();

        DisplayPlaceChoice(availdablePlaces);

        var placeChoice = int.Parse(Console.ReadLine()!);
        var chosenPlace = availdablePlaces[placeChoice - 1];

        return filteredSeats.First(seat => seat.Row == chosenRow && seat.Place == chosenPlace);
    }

    private async Task<Guid> InsertTicketAsync(ITicketRepository ticketRepository, Ticket ticket)
    {
        Console.Clear();
        DisplayTicketInformation(ticket);
        WriteSeparatorLine();

        return await ticketRepository.InsertAsync(ticket);
    }

    private void DisplaySeatCategoryChoice()
    {
        WriteSeparatorLine();
        Console.WriteLine("Оберіть категорію:");
        Console.WriteLine("1 - Standart");
        Console.WriteLine("2 - Premium");
        Console.WriteLine("3 - VIP");
        Console.Write("Мій вибір: ");
    }

    private void DisplayRowChoice(int[] rows)
    {
        WriteSeparatorLine();
        Console.WriteLine("Оберіть ряд (серед вільних):");
        rows.Select((row, index) => $"{index + 1} - {row}")
            .ToList()
            .ForEach(Console.WriteLine);
        Console.Write("Мій вибір: ");
    }

    private void DisplayPlaceChoice(int[] places)
    {
        WriteSeparatorLine();
        Console.WriteLine("Оберіть місце (серед вільних):");
        places.Select((place, index) => $"{index + 1} - {place}")
            .ToList()
            .ForEach(Console.WriteLine);
        Console.Write("Мій вибір: ");
    }

    private async Task DisplaySuccessfulCancellationInfo()
    {
        WriteSeparatorLine();
        Console.WriteLine("Бронювання успішно скасовано!");
        Thread.Sleep(4000);

        Console.Clear();
        await DisplayMainMenu();
    }

    private void DisplayTicketInformation(Ticket ticket)
    {
        WriteSeparatorLine();
        Console.WriteLine("ІНФОРМАЦІЯ ПРО БРОНЮВАННЯ");
        Console.WriteLine(ticket.GetInfo());
    }
}

