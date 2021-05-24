using System;

namespace SqlBulkTools.TestCommon.Model
{
    public class ComplexTypeModelWithoutAttribute
    {
        public int Id { get; set; }

        public EstimatedStatsWithoutAttribute MinEstimate { get; set; }

        public EstimatedStatsWithoutAttribute AverageEstimate { get; set; }

        public double SearchVolume { get; set; }

        public double Competition { get; set; }
    }

    public class EstimatedStatsWithoutAttribute
    {
        public EstimatedStatsWithoutAttribute() => 
            CreationDate = DateTime.UtcNow;

        public double? TotalCost { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
