﻿using System.Collections.Generic;
using System.Linq;

namespace Scouting.DataLayer
{
    public class CommentRepository : Repository<Comment>
    {
        public List<CommentView> GetAllByPlayerId(int playerId)
        {
            return Db.Query<CommentView>("SELECT C.*, U.UserID, U.DisplayName, U.Picture FROM Comments C INNER JOIN Users U ON (C.GoogleID = U.GoogleID) WHERE (C.PlayerID = @0) AND (C.Deleted = @1)", playerId, false).ToList();
        }
    }
}