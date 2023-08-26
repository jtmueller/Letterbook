﻿using System.Text;
using Letterbook.Core.Models;

namespace Letterbook.Adapter.TimescaleFeeds.Extensions;

public static class ContentExtensions
{
    public static void AppendEntryRow(this StringBuilder sb, int row)
    {
        const int count = 9;
        var start = count * row;
        var end = start + count;
        sb.Append('(');
        sb.AppendJoin(',', Enumerable.Range(start, end).Select(i => $"{{{i}}}"));
        sb.Append(')');
    }

    public static IEnumerable<object?> ToEntryValues(this IContentRef subject, Audience audience, Profile? boostedBy)
    {
        return new List<object?>
        {
            subject.Type,  // Type
            subject.Id.ToString(),  // EntityId
            audience.Id.ToString(),  // AudienceKey
            null,  // AudienceName
            subject.Creators.Select(c => c.Id.ToString()).ToArray(),  // CreatedBy
            subject.Authority,  // Authority
            boostedBy?.Id.ToString(),  // BoostedBy
            subject.CreatedDate,  // CreatedDate
        };
    }
}