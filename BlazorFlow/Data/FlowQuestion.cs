﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorFlow.Data
{
    public class FlowQuestion
    {
        public int FlowQuestionId { get; set; }
        public string? FlowQuestionCode { get; set; }
        public string? FlowQuestionTextEn { get; set; }
        public string? FlowQuestionTextFr { get; set; }
    }
}
