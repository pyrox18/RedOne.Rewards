using RedOne.Rewards.Domain.Entities;
using System;

namespace RedOne.Rewards.Application.Dtos
{
    public class UsageDto
    {
        public string Title { get; set; }
        public string Usage { get; set; }
        public int UsagePercent { get; set; }

        public UsageDto(Usage usage)
        {
            Title = usage.Title;
            Usage = $"{usage.CurrentUsage} {usage.Unit} / {usage.UsageLimit} {usage.Unit}";
            UsagePercent = Convert.ToInt32(Math.Ceiling((decimal)usage.CurrentUsage / usage.UsageLimit * 100));
        }
    }
}
