<Query Kind="Statements">
  <Connection>
    <ID>a48ade6f-fd46-4723-b1e9-096d16befc95</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Server>DESKTOP-70919O9\NAIT2020</Server>
    <DeferDatabasePopulation>true</DeferDatabasePopulation>
    <Database>GroceryList</Database>
  </Connection>
</Query>



//Question 1
var Q1 = from productx in OrderLists
	group productx by productx.Product into productgroup
	orderby productgroup.Count() descending, productgroup.Key.Description ascending
	select new
	{
		Product = productgroup.Key.Description,
		TimesPurchased = productgroup.Count()
	
	};

Q1.Dump();

//Question 2
//var Q2 = from storex in Stores
//		orderby storex.Location
//		select new
//		{
//			Location = storex.Location,
//			Clients = from ordery in storex.Orders
//					group ordery by ordery.Customer into ordergroup
//					orderby ordergroup.Key.Address
//					select new
//					{
//						Address = ordergroup.Key.Address,
//						City = ordergroup.Key.City,
//						Province = ordergroup.Key.Province
//					}
//		};
//		
var Q2 = from storex in Stores
		orderby storex.Location
		select new
		{
			Location = storex.Location,
			Clients = (from ordery in storex.Orders
					
					select new
					{
						Address = ordery.Customer.Address,
						City = ordery.Customer.City,
						Province = ordery.Customer.Province
					}).Distinct()
		};
		
Q2.Dump();


//Question 3
var Q3Month = 12;

var Q3 = from storex in Stores
		orderby storex.City, storex.Location
		select new
		{
			City = storex.City,
			Location = storex.Location,
			Sales = from ordery in storex.Orders
					where ordery.OrderDate.Month == Q3Month && storex.StoreID == ordery.StoreID
					group ordery by ordery.OrderDate into ordergroup
					select new 
					{
						Date = ordergroup.Key,
						Numberoforders = ordergroup.Count(),
						ProductSales = ordergroup.Sum(y => y.SubTotal),
						GST = ordergroup.Sum(y => y.GST)
					}
		
		};

Q3.Dump();


//Question 4
var Q4OrderID = 33;

var Q4 = from producty in Products
		group producty by producty.Category into ygroup
		orderby ygroup.Key.Description ascending
		select new
		{
			Category = ygroup.Key.Description,
			OrderProducts = from orderlistx in OrderLists
							where orderlistx.OrderID == Q4OrderID && orderlistx.Product.CategoryID == ygroup.Key.CategoryID
							orderby orderlistx.Product.Description ascending
							select new
							{
								Product = orderlistx.Product.Description,
								Price = orderlistx.Price,
								PickedQty = orderlistx.QtyPicked,
								Discount = orderlistx.Discount,
								Subtotal = (decimal)orderlistx.QtyPicked * (orderlistx.Price - orderlistx.Discount),
								Tax = (orderlistx.Product.Taxable != false ? (decimal)((decimal)orderlistx.QtyPicked * orderlistx.Price) * (decimal)0.05
										: 0),
								ExtendedPrice =  (orderlistx.Product.Taxable != false ? 
													((decimal)((decimal)orderlistx.QtyPicked * orderlistx.Price) * (decimal)0.05) + ((decimal)((decimal)orderlistx.QtyPicked * orderlistx.Price)) - ((decimal)orderlistx.QtyPicked * orderlistx.Discount)
												: (decimal)((decimal)orderlistx.QtyPicked * orderlistx.Price) - ((decimal)orderlistx.QtyPicked * orderlistx.Discount))
							}
		};
		
Q4.Dump();



//Question 5
var Q5StartDate = "2017-12-17";
var Q5EndDate = "2017-12-23";

var Q5 = from y in Orders
		join y2 in Pickers 
		on y.PickerID equals y2.PickerID 
		group y by new {y2.PickerID,  y2.FirstName, y2.LastName} into ygroup
		select new
		{
			Picker = ygroup.Key.FirstName + ", " + ygroup.Key.LastName,
			PickDates = from x in Orders
						where (x.PickerID == ygroup.Key.PickerID) && (x.OrderDate >= DateTime.Parse(Q5StartDate)) && (x.OrderDate <= DateTime.Parse(Q5EndDate))
						orderby x.OrderDate
						select new
						{
							ID = x.OrderID,
							Date = x.OrderDate
						}
		};
Q5.Dump();

//Question 6
var Q6CustomerID = 1;
var Q6 = from x in Orders
		where x.CustomerID == Q6CustomerID
		group x by x.Customer into xgroup
		select new
		{
			Customer = xgroup.Key.LastName + ", " + xgroup.Key.FirstName,
			OrdersCount = xgroup.Count(),
			Items = from productx in OrderLists
					where productx.Order.CustomerID == xgroup.Key.CustomerID
					group productx by productx.Product into productgroup
					orderby productgroup.Count() descending, productgroup.Key.Description ascending
					select new
					{
						Product = productgroup.Key.Description,
						TimesBought = productgroup.Count()
					
					}
		};
Q6.Dump();