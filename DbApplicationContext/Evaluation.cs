namespace CRUD_WebAppEvaluation.DbApplicationContext;

public partial class Evaluation
{
    public DateTime StartDate { get; set; }

    public int Storeid { get; set; }

    public int Id { get; set; }

    public byte Detid { get; set; }

    public byte? Score { get; set; }

    public string? Comments { get; set; }

    public bool? IfAmountAsset { get; set; }

    public decimal? AmountAsset { get; set; }

    public DateTime? DateAsset { get; set; }

    public decimal? AmountRemain { get; set; }
}
