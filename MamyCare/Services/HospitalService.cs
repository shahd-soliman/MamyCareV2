
using Azure;
using MamyCare.Contracts.Hospitals;
using MamyCare.Entities;
using MamyCare.Errors;
using Mapster;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MamyCare.Services
{
    public class HospitalService(ApplicationDbContext context, IOptions<ServerSettings> options) : IHospitalService
    {
        private readonly ApplicationDbContext _context = context;
        private readonly string _baseUrl = options.Value.BaseUrl;



        public Task<List<GetHospitalsResponse>> GetAllFilteredHospitals()
        {
            throw new NotImplementedException();
        }

        public async Task<List<GetGovernoratesResponse>> GetAllGovernorates()
        {
            var Governorates = await _context.GovernorateHospitals.ToListAsync();
            var governorateResponse = Governorates.Adapt<List<GetGovernoratesResponse>>();

            return governorateResponse;
        }
        public async Task<List<GetHospitalsResponse>> GetAllHospitals(int userid)
        {
            var mother = await _context.Mothers
               .Include(x => x.FavouriteHospitals)
               .FirstOrDefaultAsync(x => x.UserId == userid);

            var hospitals = await _context.Hospitals.Include(x => x.Governorate).ToListAsync();
           
            var hospitalResponse = hospitals.Adapt<List<GetHospitalsResponse>>();
            if (mother!.FavouriteHospitals != null)

                foreach (var hospital in hospitalResponse)
                {
                    hospital.ImageUrl = $"{_baseUrl}{hospital.ImageUrl}";


                    foreach (var fav in mother.FavouriteHospitals)
                {
                    if (hospital.Id == fav.hospitalId)
                        hospital.IsFavourite = true;


                }
            }
          
            return hospitalResponse;
        }
        public async Task<List<GetHospitalsResponse>> GetAllFilteredHospitals(int GovernorateId , int userid)
        {
            var mother = await _context.Mothers
               .Include(x => x.FavouriteHospitals)
               .FirstOrDefaultAsync(x => x.UserId == userid);
            var hospitals = await _context.Hospitals.Where(x => x.GovernorateId == GovernorateId).Include(x => x.Governorate).ToListAsync();
            var hospitalResponse = hospitals.Adapt<List<GetHospitalsResponse>>();
                 foreach (var hospital in hospitalResponse)
            {
                hospital.ImageUrl = $"{_baseUrl}{hospital.ImageUrl}";

                if (mother!.FavouriteHospitals != null)
                foreach (var fav in mother.FavouriteHospitals)
                {
                    if (hospital.Id == fav.hospitalId)
                        hospital.IsFavourite = true;

                }
            }
            return hospitalResponse;
        }
        public async Task<GetHospitalsResponse>GetById(int id , int userid)
        {
            var mother = await _context.Mothers
                .Include(x => x.FavouriteHospitals)
                .FirstOrDefaultAsync(x => x.UserId == userid);


            var hospital = await _context.Hospitals.Include(x => x.Governorate).FirstOrDefaultAsync(x => x.Id == id);
            var hospitalResponse = hospital.Adapt<GetHospitalsResponse>();
            if (mother!.FavouriteHospitals != null)
                hospitalResponse.ImageUrl = $"{_baseUrl}{hospital.ImageUrl}";

            foreach (var fav in mother.FavouriteHospitals)
            {
                if (hospital.Id == fav.hospitalId)
                    hospitalResponse.IsFavourite = true;
               
            }
              
         
            return hospitalResponse;
        }

        public async Task<Result> AddToFav(int hospitalId, int userId, CancellationToken cancellationToken)
        {
            var mother = await _context.Mothers.FirstOrDefaultAsync(x => x.UserId == userId);

            if (mother == null)
                return Result.Failure(UserErrors.UserNotFound); 

            var exist = await _context.FavouriteHospitals
                .FirstOrDefaultAsync(x => x.hospitalId == hospitalId && x.motherId == mother.Id);

            if (exist != null)
                return Result.Failure(HospitalErrors.DuplicateFavouriteHopital);

            var hospital = await _context.Hospitals.FirstOrDefaultAsync(x => x.Id == hospitalId);
            if (hospital == null)
                return Result.Failure(HospitalErrors.InvalidHospital);

            var hospitalfav = new FavouriteHospital()
            {
                motherId = mother.Id,
                hospitalId = hospital.Id,
                Isfavourite = true
            };

            await _context.FavouriteHospitals.AddAsync(hospitalfav, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }


        public async Task<Result<List<GetHospitalsResponse>>> GetFAv( int userId )
        {
            var mother = await _context.Mothers.FirstOrDefaultAsync(x => x.UserId == userId);
            var Favs = await _context.FavouriteHospitals.Where(x => x.motherId == mother!.Id).Include(x=>x.Hospital).ToListAsync();
            var response= Favs.Adapt <List<GetHospitalsResponse>>();
            foreach (var fav in response)
            {
                fav.ImageUrl = $"{_baseUrl}{fav.ImageUrl}";
            }
            return Result.Success(response);

        }


        public async Task<Result> DeleteFav(int hospitalId, int userId, CancellationToken cancellationToken)
        {
            var hospital = await _context.Hospitals.FirstOrDefaultAsync(x => x.Id == hospitalId, cancellationToken);
            if (hospital == null)
            {
                return Result.Failure(HospitalErrors.InvalidHospital);
            }

            var mother = await _context.Mothers.FirstOrDefaultAsync(x => x.UserId == userId);
            var hospitalfav = await _context.FavouriteHospitals.FirstOrDefaultAsync(x => x.hospitalId == hospitalId && x.motherId == mother!.Id);

                      _context.FavouriteHospitals.Remove(hospitalfav!);
                await _context.SaveChangesAsync(cancellationToken);

                return Result.Success();
            }
        }

       
    }

