using Domain.Models;

namespace Domain.Repository
{
    public interface IVideoRepository
    {
        Video? Get(int id);
        List<Video> GetAll();
        Video GetByName(string name);
        int Create(Video video);
        void Update(int id, Video video);
        void Delete(Video video);
        void CreateVideoInfo(VideoInfo info);
    }
}
