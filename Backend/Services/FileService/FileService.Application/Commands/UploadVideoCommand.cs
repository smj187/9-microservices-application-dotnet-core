﻿using FileService.Core.Domain;
using FileService.Core.Domain.Aggregates.Video;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Application.Commands
{
    public class UploadVideoCommand : IRequest<VideoAsset>
    {
        public string Folder { get; set; } = default!;

        public Guid ExternalEntityId { get; set; }
        public IFormFile Video { get; set; } = default!;
        public AssetType AssetType { get; set; } = default!;

        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Tags { get; set; }

    }
}
