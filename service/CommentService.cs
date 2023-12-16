using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using infrastructure.DataModels;


using infrastructure.Repositories;

namespace service;

public class CommentService
{
    private readonly CommentRepository _commentRepository;

    public CommentService(CommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }
    public async Task<Comment> GetCommentByIdAsync(int id)
    {
        return await _commentRepository.GetCommentByIdAsync(id);
    }

}