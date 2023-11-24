using System.ComponentModel.DataAnnotations;
using api.CustomDataAnnotations;
using api.Filters;
using api.TransferModels;
using infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using service;

namespace library.Controllers;


public class BlogController : ControllerBase
{

    private readonly ILogger<BlogController> _logger;
    private readonly BlogService _blogService;

    public BlogController(ILogger<BlogController> logger,
        BlogService blogService)
    {
        _logger = logger;
        _blogService = blogService;
    }



    [HttpGet]
    [Route("/api/box")]
    public ResponseDto Get()
    {
        HttpContext.Response.StatusCode = 200;
        return new ResponseDto()
        {
            MessageToClient = "Successfully fetched",
            ResponseData = _blogService.GetBoxForFeed()
        };
    }
    [HttpGet]
    [Route("/api/boxes/{boxId}")]
    public async Task<ResponseDto> GetAllBoxByIdAsync([FromRoute] int boxId)
    {
        var box = await _blogService.GetBoxByIdAsync(boxId);
    
        if (box == null)
        {
            HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;
            return new ResponseDto()
            {
                MessageToClient = "Box not found"
            };
        }

        return new ResponseDto()
        {
            MessageToClient = "Successfully fetched box",
            ResponseData = blog
        };
    }


    [HttpPost]
    [ValidateModel]
    [Route("/api/box")]
    public ResponseDto Post([FromBody] CreateBlogRequestDto dto)
    {
        HttpContext.Response.StatusCode = StatusCodes.Status201Created;
        return new ResponseDto()
        {
            MessageToClient = "Successfully created a box",
            ResponseData = _blogService.CreateBox(dto.BoxName, dto.BoxWeight)
        };
    }

    [HttpPut]
    [ValidateModel]
    [Route("/api/box/{boxId}")]
    public ResponseDto Put([FromRoute] int boxId,
        [FromBody] UpdateBlogRequestDto dto)
    {
        HttpContext.Response.StatusCode = 201;
        return new ResponseDto()
        {
            MessageToClient = "Successfully updated",
            ResponseData =
                _blogService.UpdateBox(boxId, dto.BoxName, dto.BoxWeight)
        };

    } 

    [HttpDelete]
    [Route("/api/box/{boxId}")]
    public ResponseDto Delete([FromRoute] int boxId)
    {
        _blogService.DeleteBox(boxId);
        return new ResponseDto()
        {
            MessageToClient = "Succesfully deleted"
        };

    }
}


