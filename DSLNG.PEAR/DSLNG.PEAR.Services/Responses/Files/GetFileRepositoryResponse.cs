﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSLNG.PEAR.Services.Responses.Files
{
    public class GetFileRepositoryResponse : BaseResponse
    {
        public int Id { get; set; }

        public int Year { get; set; }
        public int Month { get; set; }

        public string Name { get; set; }
        public string Summary { get; set; }

        public string Filename { get; set; }
        public byte[] Data { get; set; }

        public DateTime? LastWriteTime { get; set; }
    }
}