<Query Kind="Statements">
  <Connection>
    <ID>09ba9c35-2d06-43f7-b298-382fe360e729</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Server>DESKTOP-70919O9\NAIT2020</Server>
    <DeferDatabasePopulation>true</DeferDatabasePopulation>
    <Database>OMST_2018</Database>
  </Connection>
</Query>

//Q1
var Q1 = from x in TicketCategories
		where x.Tickets.Count() > 0
		select new
		{
			Cat = x.Description,
			TicketsSold = x.Tickets.Count(),
			Revenue = x.Tickets.Sum(y => y.TicketPrice) + x.Tickets.Sum(y => y.TicketPremium)
		
		};
		
Q1.Dump();


//Q2
var Q2= from x in Movies
		orderby x.Title
		select new
		{
			Title = x.Title,
			Year = x.ReleaseYear,
			Locations = from y in ShowTimes
						where y.MovieID == x.MovieID
						orderby y.StartDate
						select new
						{
							Date = y.StartDate,
							Location = y.Theatre.Location.Description
						}
		};
Q2.Dump();

//Q3
var Q3 = from x in Genres
		where x.Movies.Count() > 0
		select new
		{
			Title = x.Description,
			GMovies = from y in Movies
						where y.GenreID == x.GenreID
						orderby y.Title
						select new
						{
							Title = y.Title,
							Rating = y.Rating.Description,
							Screen = y.ScreenType.Description,
							Premium = y.ScreenType.Premium == false ? "No"
							: "Yes"
						}
		};
Q3.Dump();

//Q4
var Q4Date = "2017-12-31";
var Q4 = from x in ShowTimes
		where x.StartDate.Date == DateTime.Parse(Q4Date)
		select new 
		{
			Location = x.Theatre.Location.Description,
			Movie = x.Movie.Title,
			Revenue = x.Tickets.Sum(y => y.TicketPrice) + x.Tickets.Sum(y => y.TicketPremium)
		};
Q4.Dump();


