using MovieList.Domain.Chat;
using MovieList.Domain.Pagination;
using MovieList.Domain.RequestModels.Chat;
using MovieList.Domain.Response;
using MovieList.Domain.ResponseModels.Chat;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MovieList.Services.Interfaces
{
    public interface IMessageService
    {
        public Task<IBaseResponse<List<MessageResponse>>> Get(int pageIndex, int pageSize);
        public IBaseResponse<MessageResponse> Create(MessageRequest model, int userId);
        public IBaseResponse<bool> Delete(int id);
    }
}
