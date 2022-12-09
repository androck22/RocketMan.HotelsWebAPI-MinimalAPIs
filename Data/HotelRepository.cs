public class HotelRepository : IHotelRepository
{    
    private readonly HotelDb _context;
    public HotelRepository(HotelDb context)
    {
        _context = context;
    }
    public async Task<List<Hotel>> GetHotelsAsync() => await _context.Hotels.ToListAsync();

    public async Task<List<Hotel>> GetHotelsAsync(string name) =>
        await _context.Hotels.Where(h => h.Name.Contains(name)).ToListAsync(); 

    public async Task<List<Hotel>> GetHotelsAsync(Coordinate coordinate) =>
        await _context.Hotels.Where(hotel =>
            hotel.Latitude > coordinate.Latitude - 1 &&
            hotel.Latitude < coordinate.Latitude + 1 &&
            hotel.Longitude > coordinate.Longitude - 1 &&
            hotel.Longitude < coordinate.Longitude + 1
        ).ToListAsync();

    public async Task<Hotel> GetHotelAsync(int hotelId) => 
        await _context.Hotels.FindAsync(new object[] {hotelId});

    public async Task AddHotelAsync(Hotel hotel) => await _context.Hotels.AddAsync(hotel);

    public async Task UpdateHotelAsync(Hotel hotel)
    {
        var hotelFromDb = await _context.Hotels.FindAsync(new object[] {hotel.Id});
        if (hotelFromDb == null) return;
        hotelFromDb.Longitude = hotel.Longitude;
        hotelFromDb.Latitude = hotel.Latitude;
        hotelFromDb.Name = hotel.Name;
    }

    public async Task DeleteHotelAsync(int hotelId)
    {
        var hotelFromDb = await _context.Hotels.FindAsync(new object[] {hotelId});
        if (hotelFromDb == null) return;
        _context.Hotels.Remove(hotelFromDb);
    }
    
    public async Task SaveAsync() => await _context.SaveChangesAsync();

    private bool _disposed = false;
    protected virtual void Dispose(bool didposing)
    {
        if (!_disposed)
        {
            if (didposing)
            {
                _context.Dispose();
            }
        }
        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}