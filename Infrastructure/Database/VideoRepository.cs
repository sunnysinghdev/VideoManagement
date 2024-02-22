using Domain.Models;
using Domain.Repository;

namespace Infrastructure.Database
{
    public class VideoRepository : IVideoRepository
    {
        readonly AppDbContext _appDbContext;
        public VideoRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public int Create(Video video)
        {
            _appDbContext.Videos.Add(video);
            Save();
            return video.Id;
        }

        public void CreateVideoInfo(VideoInfo info)
        {
            _appDbContext.VideoInfos.Add(info);
            Save();
        }

        public void Delete(Video name)
        {
            _appDbContext.Videos.Remove(name);
        }

        public Video? Get(int id)
        {
            return _appDbContext.Videos.Where(x => x.Id == id).FirstOrDefault();
        }

        public List<Video> GetAll()
        {
            return _appDbContext.Videos.ToList();
        }

        public Video GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public void Update(int id, Video name)
        {
            throw new NotImplementedException();
        }

        private void Save()
        {
            _appDbContext.SaveChanges();
        }
    }
}
