using HotelBooking.API.DbContext;
using HotelBooking.BAL;
using HotelBooking.BAL.Bookings;
using HotelBooking.BAL.Coupons;
using HotelBooking.BAL.Facilities;
using HotelBooking.BAL.HotelServices;
using HotelBooking.BAL.Interface;
using HotelBooking.BAL.Interface.Bookings;
using HotelBooking.BAL.Interface.Coupons;
using HotelBooking.BAL.Interface.Facilities;
using HotelBooking.BAL.Interface.HotelServices;
using HotelBooking.BAL.Interface.Promotions;
using HotelBooking.BAL.Interface.Search;
using HotelBooking.BAL.Interface.Supports;
using HotelBooking.BAL.Promotions;
using HotelBooking.BAL.Search;
using HotelBooking.BAL.Supports;
using HotelBooking.DAL;
using HotelBooking.DAL.Bookings;
using HotelBooking.DAL.Coupons;
using HotelBooking.DAL.Facilities;
using HotelBooking.DAL.HotelServices;
using HotelBooking.DAL.Interface;
using HotelBooking.DAL.Interface.Bookings;
using HotelBooking.DAL.Interface.Coupons;
using HotelBooking.DAL.Interface.Facilities;
using HotelBooking.DAL.Interface.HotelServices;
using HotelBooking.DAL.Interface.Promotions;
using HotelBooking.DAL.Interface.Supports;
using HotelBooking.DAL.Promotions;
using HotelBooking.DAL.Supports;
using HotelBooking.Domain;
using HotelBooking.Domain.User;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<IBookingRoomDetailsService, BookingRoomDetailsService>();
builder.Services.AddTransient<IBookingService, BookingService>();
builder.Services.AddTransient<IBookingServiceDetailsService, BookingServiceDetailsService>();
builder.Services.AddTransient<IRoomTypeService, RoomTypeService>();
builder.Services.AddTransient<IServiceService, ServiceService>();
builder.Services.AddTransient<ICustomerSevice, CustomerSerice>();
builder.Services.AddTransient<IBookingRepository, BookingRepository>();
builder.Services.AddTransient<IBookingRoomDetailsRepository, BookingRoomDetailsRepository>();
builder.Services.AddTransient<IBookingServiceDetailsRepository, BookingServiceDetailsRepository>();
builder.Services.AddTransient<IRoomTypeRepository, RoomTypeRepository>();
builder.Services.AddTransient<IServiceRepository, ServiceRepository>();
builder.Services.AddTransient<ICustomerRepository, CustomerRepository>();
builder.Services.AddTransient<IPromotionRepository, PromotionRepository>();
builder.Services.AddTransient<IPromotionService, PromotionService>();
builder.Services.AddTransient<IPromotionApplyRepository, PromotionApplyRepository>();
builder.Services.AddTransient<IPromotionApplyService, PromotionApplyService>();
builder.Services.AddTransient<IFacilityRepository, FacilityRepository>();
builder.Services.AddTransient<IFacilityService, FacilityService>();
builder.Services.AddTransient<IFacilityApplyRepository, FacilityApplyRepository>();
builder.Services.AddTransient<IFacilityApplyService, FacilityApplyService>();
builder.Services.AddTransient<ICouponRepository, CouponRepository>();
builder.Services.AddTransient<ICouponService, CouponService>();
builder.Services.AddTransient<IRoomTypeImageRepository, RoomTypeImageRepository>();
builder.Services.AddTransient<IRoomTypeImageService, RoomTypeImageService>();
builder.Services.AddTransient<ISupportRepository, SupportRepository>();
builder.Services.AddTransient<ISupportService, SupportService>();
builder.Services.AddTransient<IServiceImageRepository, ServiceImageRepository>();
builder.Services.AddTransient<IServiceImageService, ServiceImageService>();
builder.Services.AddTransient<ISearchService, SearchService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseHsts();
}

app.UseRouting();
app.UseAuthorization();
app.UseStaticFiles();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hotel Booking APIs");
    c.RoutePrefix = string.Empty;
});
app.Run(async (context) =>
{
    await context.Response.WriteAsync("Could not find Anything.");
});

app.Run();
