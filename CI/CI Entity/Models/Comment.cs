﻿using System;
using System.Collections.Generic;

namespace CI_Entity.Models;

public partial class Comment
{
    public long CommentId { get; set; }

    public long? UserId { get; set; }

    public long MissionId { get; set; }

    public string MissionTxt { get; set; } = null!;

    public string ApprovalStatus { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual Mission Mission { get; set; } = null!;

    public virtual User? User { get; set; }
}
