using counselorReview.Models;
using counselorReview.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using counselorReview.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace counselorReview.Services
{
    public class CounselorService : ICounselorService
    {
        private readonly IMongoCollection<Counselor> _counselors;

        public CounselorService(IMongoClient mongoClient, IOptions<MongoDBSettings> settings)
        {
            var database = mongoClient.GetDatabase(settings.Value.DatabaseName);
            _counselors = database.GetCollection<Counselor>("Counselors");
        }

        public async Task<Counselor> RegisterCounselorAsync(Counselor counselor)
        {
            var existingCounselor = await _counselors.Find(c => c.Email == counselor.Email).FirstOrDefaultAsync();
            if (existingCounselor != null)
            {
                throw new Exception("Email is already in use.");
            }

            counselor.Password = BCrypt.Net.BCrypt.HashPassword(counselor.Password);
            counselor.CreatedAt = DateTime.UtcNow;

            await _counselors.InsertOneAsync(counselor);

            counselor.Password = string.Empty; 
            return counselor;
        }

        public async Task<List<Counselor>> GetAllCounselorsAsync()
        {
            return await _counselors.Find(_ => true).ToListAsync();
        }

       public async Task<CounselorDTO> GetCounselorByIdAsync(string id)
{
    var counselor = await _counselors.Find(c => c.Id == id).FirstOrDefaultAsync();
    if (counselor == null)
    {
        throw new Exception("Counselor not found.");
    }

    return new CounselorDTO
    {
        Id = counselor.Id,
        FullName = counselor.FullName,
        Email = counselor.Email,
        Specializations = counselor.Specializations,
        CreatedAt = counselor.CreatedAt,
        UpdatedAt = counselor.UpdatedAt
    };
}

       public async Task<CounselorDTO> UpdateCounselorAsync(string id, CounselorDTO updateDto)
{
    var updateDefinition = Builders<Counselor>.Update
        .Set(c => c.FullName, updateDto.FullName)
        .Set(c => c.Email, updateDto.Email)
        .Set(c => c.Specializations, updateDto.Specializations)
        .Set(c => c.UpdatedAt, DateTime.UtcNow);

    var options = new FindOneAndUpdateOptions<Counselor>
    {
        ReturnDocument = ReturnDocument.After
    };

    var updatedCounselor = await _counselors.FindOneAndUpdateAsync(c => c.Id == id, updateDefinition, options);

    if (updatedCounselor == null)
    {
        throw new Exception("Counselor not found."); 
    }

    return new CounselorDTO
    {
        Id = updatedCounselor.Id,
        FullName = updatedCounselor.FullName,
        Email = updatedCounselor.Email,
        Specializations = updatedCounselor.Specializations,
        CreatedAt = updatedCounselor.CreatedAt,
        UpdatedAt = updatedCounselor.UpdatedAt
    };
}
        public async Task<bool> DeleteCounselorAsync(string id)
        {
            var result = await _counselors.DeleteOneAsync(c => c.Id == id);
            return result.DeletedCount > 0;
        }
    }
}
