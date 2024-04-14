using MovieList.DAL.Interfaces;
using MovieList.Domain.Entity.MovieNews;
using MovieList.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieList.Domain.Entity.Profile;
using MovieList.Services.Exceptions;
using MovieList.Domain.DTO.News.Comment;

namespace MovieList.Services.Services
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CommentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public CommentResponse Get(int id)
        {
            var comment = _unitOfWork.GetRepository<Comment>().GetFirstOrDefault(
                predicate: x => x.Id == id);

            if (comment == null)
            {
                throw new RecordNotFoundException(ErrorIdConstans.RecordNotFound,
                    $"Comment with Id: {id} was not found.");
            }

            var response = _mapper.Map<CommentResponse>(comment);

            return response;
        }

        public async Task<List<CommentResponse>> GetAll(int contentId)
        {
            var comments = await _unitOfWork.GetRepository<Comment>().GetAllAsync(
                predicate: x => x.ContentId == contentId,
                include: i => i
                    .Include(x => x.Author.FileModel),
                orderBy: x => x.OrderByDescending(x => x.DateCreated));

            if (comments == null)
            {
                throw new RecordNotFoundException(ErrorIdConstans.RecordNotFound,
                    $"Comments for contentId: {contentId} were not found.");
            }

            var response = _mapper.Map<List<CommentResponse>>(comments);

            return response;
        }

        public CommentResponse Create(CommentRequest model, int userId)
        {
            var userProfile = _unitOfWork.GetRepository<UserProfile>().GetFirstOrDefault(
                predicate: x => x.UserId == userId,
                include: i => i
                    .Include(x => x.FileModel));

            if (userProfile == null)
            {
                throw new RecordNotFoundException(ErrorIdConstans.RecordNotFound,
                     $"Profile with Id: {userId} was not found. Can't create comment.");
            }

            var comment = _mapper.Map<Comment>(model);
            comment.AuthorId = userId;

            _unitOfWork.GetRepository<Comment>().Insert(comment);
            _unitOfWork.SaveChanges();

            var response = _mapper.Map<CommentResponse>(comment);
            response.Author = userProfile.Name;
            response.AvatarUrl = userProfile.FileModel.Path;

            return response;
        }

        public CommentResponse Edit(CommentRequest model)
        {
            var comment = _unitOfWork.GetRepository<Comment>().GetFirstOrDefault(
                predicate: x => x.Id == model.Id);

            if (comment == null)
            {
                throw new RecordNotFoundException(ErrorIdConstans.RecordNotFound,
                    $"Comment with Id: {model.Id} was not found.");
            }

            _mapper.Map(model, comment);

            _unitOfWork.GetRepository<Comment>().Update(comment);
            _unitOfWork.SaveChanges();

            var response = _mapper.Map<CommentResponse>(comment);

            return response;
        }      

        public void Delete(int id)
        {
            var comment = _unitOfWork.GetRepository<Comment>().GetFirstOrDefault(
                predicate: x => x.Id == id);

            if (comment == null)
            {
                throw new RecordNotFoundException(ErrorIdConstans.RecordNotFound,
                    $"Comment with Id: {id} was not found. Can't delete comment.");
            }

            _unitOfWork.GetRepository<Comment>().Delete(id);
            _unitOfWork.SaveChanges();
        }
    }
}
