using MamyCare.Contracts.BabyFeature;

namespace MamyCare.Services
{
    public interface IGallaryService
    {
        public Task<Result<List<GallaryResponse>>> GetAll();
        public Task<Result<GallaryResponse>> GetById(int id );
        public Task<Result> Create(GallaryRequest request, CancellationToken cancellationToken);
        public Task<Result> Delete(int id,CancellationToken cancellationToken);
        public Task<Result> Update(string Description, int id,CancellationToken cancellationToken);
    }
}
